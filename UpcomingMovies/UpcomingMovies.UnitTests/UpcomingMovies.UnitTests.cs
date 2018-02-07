using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.ViewModels;
using Xamarin.Forms;
using Xunit;

namespace UpcomingMovies.UnitTests
{
    public class TestsAreGood
    {
        [Fact]
        public void ThisShouldPass()
        {
            Assert.True(true);
        }

        [Fact]
        public async Task ThisShouldFail()
        {
            await Task.Run(() => { throw new Exception("boom"); });
        }

        [Fact]
        public void WriteEntry()
        {
            var viewModel = new MovieListViewModel();

            var text = "Test";

            viewModel.MovieNameEntry = text;

            viewModel.MovieNameSearchBar = text;

            Assert.Equal(text, viewModel.MovieNameEntry);

            Assert.Equal(text, viewModel.MovieNameSearchBar);
        }
    }
}
