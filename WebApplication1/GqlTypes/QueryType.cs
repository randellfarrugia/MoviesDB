using MoviesAPI.Data;
using MoviesAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.AspNetCore.Authorization;

namespace MoviesAPI.GqlTypes
{
    public class QueryType
    {

        private readonly ILogger<QueryType> log;

        public QueryType(ILogger<QueryType> logger)
        {
            log = logger;
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin","User" })]
        public async Task<List<Movie>> GetAllMoviesAsync([Service] MovieDBContext context, IResolverContext resolverContext)
        {
            var query = resolverContext.Document;
            log.LogInformation($"Get All Movies Request, with selected fields and filters - {query}");
            return await context.Movie.ToListAsync();
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin", "User" })]
        public async Task<Movie?> GetMovieByIDAsync([Service] MovieDBContext context, IResolverContext resolverContext, int id)
        {
            var query = resolverContext.Document;
            log.LogInformation($"Get Movie By ID Request, with selected fields and filters - {query}");
            return await Task.FromResult(context.Movie.FirstOrDefault(m => m.Id == id));
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin", "User" })]
        public async Task<Movie?> GetMovieByNameAsync([Service] MovieDBContext context, IResolverContext resolverContext, string name)
        {
            var query = resolverContext.Document;
            log.LogInformation($"Get Movie By Name Request, with selected fields and filters - {query}");
            return await Task.FromResult(context.Movie.FirstOrDefault(m => m.Name == name));
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin", "User" })]
        public async Task<List<Movie>?> GetTop5RatedMoviesAsync([Service] MovieDBContext context, IResolverContext resolverContext)
        {
            var query = resolverContext.Document;
            log.LogInformation($"Get Top 5 Movies Request, with selected fields and filters - {query}");
            var MovieList = await GetAllMoviesAsync(context, resolverContext);
            return MovieList.OrderByDescending(m => m.Rating)?.Take(5)?.ToList();
        }
    }
}
