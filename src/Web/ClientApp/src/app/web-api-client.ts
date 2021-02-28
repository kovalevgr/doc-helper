import {Inject, Injectable, InjectionToken, Optional} from "@angular/core";
import {HttpClient, HttpHeaders, HttpResponse} from "@angular/common/http";
import {Observable, of, throwError} from "rxjs";
import {catchError, mergeMap} from "rxjs/operators";

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable({
  providedIn: 'root'
})
export class WebApiClient {
  protected readonly http: HttpClient;
  protected readonly baseUrl: string;
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

  constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    this.http = http;
    this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
  }

  public get(url: string): Observable<any> {
    const url_ = this.prepareUrl(url);

    const options_: object = {
      observe: "response",
      responseType: "blob",
      headers: new HttpHeaders({
        "Accept": "application/json"
      })
    };

    return this.http.get(url_, options_)
      .pipe(
        mergeMap((response: any) => this.prepareGetResponse(response))
      )
      .pipe(
        catchError((errorResponse) => <Observable<any>><any>throwError(errorResponse))
      );
  }

  protected prepareUrl(url: string): string {
    const url_ = this.baseUrl + url;

    return url_.replace(/[?&]$/, "");
  }

  protected prepareGetResponse(response: HttpResponse<any>): Observable<any> {
    const status = response.status;
    const body = response.body;

    if (status === 200) {
      return this.blobToText(body)
        .pipe(mergeMap(responseText => of(JSON.parse(responseText, this.jsonParseReviver))));
    }
  }

  private blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
      if (!blob) {
        observer.next("");
        observer.complete();
      } else {
        let reader = new FileReader();
        reader.onload = event => {
          observer.next((<any>event.target).result);
          observer.complete();
        };
        reader.readAsText(blob);
      }
    });
  }
}
