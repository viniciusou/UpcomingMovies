using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.Models;

namespace UpcomingMovies.ViewModels
{
    public class MovieViewModel: BaseViewModel
    {
        
        #region Properties

        public Movie Movie { get; private set; }

        #endregion


        #region Constructor

        public MovieViewModel(Movie movie)
        {
            Title = "Movie Details";

            Movie = movie;
        }

        #endregion
    }
}
