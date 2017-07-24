using MvvmHelpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.Models;
using UpcomingMovies.Views;
using Xamarin.Forms;

namespace UpcomingMovies.ViewModels
{
    public class MovieListViewModel: BaseViewModel
    {
        #region Properties

        public ObservableRangeCollection<Movie> Movies { get; private set; }

        public int TotalResults { get; private set; }

        Movie movie;

        public Movie SelectedMovie
        {
            get { return movie; }

            set
            {
                if (value != null)
                {
                    SetProperty(ref movie, value);
                    SelectMovieCommand.Execute(null);
                }
            }
        }

        #endregion


        #region Variables

        public static List<Genre> GenresList = new List<Genre>();

        public static Dictionary<int, string> GenresDictionary = new Dictionary<int, string>();

        #endregion


        #region Constructor

        public MovieListViewModel()
        {
            Title = "Upcoming Movies";

            Movies = new ObservableRangeCollection<Movie>();

            LoadMoviesCommand = new Command(async () => await LoadMoviesAsync(), () => !IsBusy);

            SelectMovieCommand = new Command(async () => await SelectMovieViewAsync());

            LoadMoviesCommand.Execute(null);
        }

        #endregion


        #region Commands

        public Command LoadMoviesCommand { get; }

        public Command SelectMovieCommand { get; }

        #endregion


        #region Tasks

        private async Task LoadMoviesAsync()
        {
            try
            {
                IsBusy = true;

                LoadMoviesCommand.ChangeCanExecute();

                //Retrieve Genres List on first loading
                if (Movies.Count == 0)
                {
                    var clientGenres = new HttpClient();

                    var urlGenres = $"{Settings.Settings.BaseUrl}{Settings.Settings.GenreListUrl}?api_key={Settings.Settings.ApiKey}";

                    var responseGenres = await clientGenres.GetAsync(urlGenres);

                    if (responseGenres.IsSuccessStatusCode)
                    {
                        var json = await responseGenres.Content.ReadAsStringAsync();

                        GenresList = JObject.Parse(json)["genres"].ToObject<List<Genre>>();

                        foreach (var genre in GenresList)
                        {
                            GenresDictionary.Add(genre.Id, genre.Name);
                        }
                    }
                }

                var client = new HttpClient();

                var page = (Movies.Count / Settings.Settings.ItemsPerPage) + 1;

                var url = $"{Settings.Settings.BaseUrl}{Settings.Settings.UpcomingMoviesUrl}?api_key={Settings.Settings.ApiKey}&region=US&page={page}";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var genrest = new List<Genre>();
                    var json = await response.Content.ReadAsStringAsync();

                    var jObject = JObject.Parse(json);

                    var movies = jObject.SelectToken("results").ToObject<List<Movie>>(); ;
                    TotalResults = (int)jObject.SelectToken("total_results");

                    foreach (var movie in movies)
                    {
                        movie.PosterPath = string.IsNullOrEmpty(movie.PosterPath) ? "noimage.png" : $"{Settings.Settings.PosterUrl}{movie.PosterPath}";
                        movie.Genre = string.Join(" | ", movie.GenreIds.Select(g => GenresDictionary[g]));
                        Movies.Add(movie);
                    }

                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsBusy = false;

                LoadMoviesCommand.ChangeCanExecute();
            }
        }

        private async Task SelectMovieViewAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MovieView(SelectedMovie));
        }

        #endregion
    }
}
