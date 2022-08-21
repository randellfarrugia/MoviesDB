using Microsoft.Extensions.Logging;
using MoviesAPI.Data;
using MoviesAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotChocolate.AspNetCore.Authorization;

namespace MoviesAPI.GqlTypes
{
    public class MutationType
    {
        private readonly ILogger<MutationType> log;

        public MutationType(ILogger<MutationType> logger)
        {
            log = logger;
        }

        [Authorize(Roles = new[] { "Admin" })]
        public async Task<Movie> SaveMovieAsync([Service] MovieDBContext context, Movie newMovie)
        {
            log.LogInformation("Inserting new Movie");
            log.LogInformation($"Request - {Newtonsoft.Json.JsonConvert.SerializeObject(newMovie, Newtonsoft.Json.Formatting.Indented)}");

            context.Movie.Add(newMovie);
            await context.SaveChangesAsync();
            return newMovie;
        }

        [Authorize(Roles = new[] { "Admin" })]
        public async Task<string> UpdateMovieAsync([Service] MovieDBContext context, Movie updateMovie)
        {
            log.LogInformation($"Updating Movie with ID - {updateMovie.Id}");
            log.LogInformation($"Request - {Newtonsoft.Json.JsonConvert.SerializeObject(updateMovie, Newtonsoft.Json.Formatting.Indented)}");

            var movieToUpdate = await context.Movie.FirstOrDefaultAsync(m => m.Id == updateMovie.Id);
            if (movieToUpdate == null)
            {
                return "Invalid Operation";
            }

            context.ChangeTracker.Clear();
            context.Movie.Update(updateMovie);
            await context.SaveChangesAsync();
            //return updateMovie;
            return "Record Updated!";
        }

        [Authorize(Roles = new[] { "Admin" })]
        public async Task<string> DeleteMovieByNameAsync([Service] MovieDBContext context, string name)
        {
            log.LogInformation($"Deleting Movie with name - {name}");

            var movieToDelete = await Task.FromResult(context.Movie.FirstOrDefault(m => m.Name == name));
            if (movieToDelete == null)
            {
                return "Invalid Operation";
            }
            context.ChangeTracker.Clear();
            context.Movie.Remove(movieToDelete);
            await context.SaveChangesAsync();
            return "Record Deleted!";
        }

        [Authorize(Roles = new[] { "Admin" })]
        public async Task<string> DeleteMovieByIDAsync([Service] MovieDBContext context, int id)
        {
            log.LogInformation($"Deleting Movie with id - {id}");

            var movieToDelete = await context.Movie.FindAsync(id);
            if (movieToDelete == null)
            {
                return "Invalid Operation";
            }
            context.Movie.Remove(movieToDelete);
            await context.SaveChangesAsync();
            return "Record Deleted!";
        }
    }


}
