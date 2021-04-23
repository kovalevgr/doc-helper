using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DocHelper.Application.Common.Models;

namespace DocHelper.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
            this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

        public static IQueryable<TDestination> Paginate<TDestination>(this IQueryable<TDestination> queryable,
            int pageNumber, int pageSize)
            => queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}