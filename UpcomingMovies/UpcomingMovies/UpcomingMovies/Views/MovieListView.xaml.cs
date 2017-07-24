using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.Models;
using UpcomingMovies.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UpcomingMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieListView : ContentPage
    {
        MovieListViewModel vm;

        ObservableCollection<Movie> movies;

        public MovieListView()
        {
            InitializeComponent();

            BindingContext = vm = new MovieListViewModel();

            movies = new ObservableCollection<Movie>();

            movies = (ObservableCollection<Movie>)lvwMovies.ItemsSource;

            //Load more items when hitting the bottom of list
            lvwMovies.ItemAppearing += (sender, e) =>
            {
                if (vm.IsBusy || movies.Count == 0 || movies.Count == vm.TotalResults)
                {
                    return;
                }
                else if ((Movie)e.Item == movies[movies.Count - 1])
                {
                    vm.LoadMoviesCommand.Execute(null);
                }

            };

            //Filter list based on partial or full movie name
            sbrMovies.TextChanged += (sender, e) =>
            {
                lvwMovies.BeginRefresh();

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    lvwMovies.ItemsSource = movies;
                }
                else
                {
                    lvwMovies.ItemsSource = movies.Where(m => m.Title.ToLower().Contains(e.NewTextValue.ToLower()));
                }

                lvwMovies.EndRefresh();
            };
        }
    }
}