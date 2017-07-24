using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpcomingMovies.Settings
{
    public static class Settings
    {
        public static string ApiKey { get; } = "1f54bd990f1cdfb230adb312546d765d";
        public static string BaseUrl { get; } = "https://api.themoviedb.org/3";
        public static string GenreListUrl { get; } = "/genre/movie/list";
        public static string UpcomingMoviesUrl { get; } = "/movie/upcoming";
        public static string PosterUrl { get; } = "https://image.tmdb.org/t/p/w500";
        public static int ItemsPerPage { get; } = 20;
    }
}
