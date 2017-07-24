using System;
using System.Collections.Generic;
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
    public partial class MovieView : ContentPage
    {
        MovieViewModel vm;

        public MovieView(Movie movie)
        {
            InitializeComponent();

            BindingContext = vm = new MovieViewModel(movie);
        }

        //Change presentation based on device orientation
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                sltMovie.Orientation = StackOrientation.Horizontal;
            }
            else
            {
                sltMovie.Orientation = StackOrientation.Vertical;
            };
        }
    }
}