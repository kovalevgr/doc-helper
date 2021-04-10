import {HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest, HttpResponse} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Observable, of} from "rxjs";
import {startWith, tap} from "rxjs/operators";

import {environment} from '../../environments/environment';

@Injectable()
export class CachingInterceptor implements HttpInterceptor {
  private cache: Map<string, HttpResponse<any>> = new Map();

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    if (!this.isCacheable(req)) {
      return next.handle(req);
    }

    const cachedResponse = this.cache.get(this.getCacheKey(req));

    // cache-then-refresh
    if (req.headers.get('x-refresh')) {
      const results$ = this.sendRequest(req, next, this.cache);
      return cachedResponse ?
        results$.pipe(startWith(cachedResponse)) :
        results$;
    }

    // cache-or-fetch
    return cachedResponse ?
      of(cachedResponse) : this.sendRequest(req, next, this.cache);
  }

  private isCacheable = (req: HttpRequest<any>): boolean => environment.use_cache && req.method === "GET";

  private getCacheKey = (req: HttpRequest<any>): string => `key=${req.url}&location=${req.headers.get("location")}`;

  private sendRequest(
    req: HttpRequest<any>,
    next: HttpHandler,
    cache: Map<string, HttpResponse<any>>): Observable<HttpEvent<any>> {

    return next.handle(req.clone()).pipe(
      tap(event => {
        if (event instanceof HttpResponse) {
          cache.set(this.getCacheKey(req), event);
        }
      })
    );
  }
}
