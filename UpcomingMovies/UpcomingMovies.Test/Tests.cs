using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;

namespace UpcomingMovies.Test
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        /*
        //Initial test
        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }
        */
       
        [Test]
        public void WriteEntry()
        {
            //app.Repl();

            var text = "Test";
            app.Tap("entMovies"); 
            app.EnterText(text); 
            (app as AndroidApp).PressUserAction();
            var entMoviesText = app.Query("entMovies").Single().Text;

            Assert.AreEqual(text, entMoviesText);
        }

        [Test]
        public void WriteSearchBar()
        {
            var text = "Test";
            app.Tap("sbrMovies");
            app.EnterText(text);
            (app as AndroidApp).PressUserAction();

            //get view properties
            //app.Query(c => c.Marked("sbrMovies"));

            var sbrMoviesText = app.Query("search_src_text").First().Text;

            Assert.AreEqual(text, sbrMoviesText);
        }

        [Test]
        public void WriteAndScreenShot()
        {
            var screen = new MoviesList(app);

            var text = "Test";
            
            screen.EnterTextEntry(text);

            screen.PressEnter();

            app.Screenshot("Entry text = Test");

            //Assert Entry
            var entMoviesText = app.Query("entMovies").Single().Text;
            Assert.AreEqual(entMoviesText, text);


            app.ClearText("entMovies");

            screen.PressEnter();
            
            screen.EnterTextSearchbar(text);

            screen.PressEnter();

            app.Screenshot("Searchbar text = Test");
            
            //Assert SearchBar
            var sbrMoviesText = app.Query("search_src_text").Single().Text;
            Assert.AreEqual(sbrMoviesText, text);

        }

        class MoviesList
        {
            private readonly IApp app;

            public MoviesList(IApp app)
            {
                this.app = app;
            }

            public void EnterTextEntry(string text)
            {
                app.EnterText("entMovies", text);
            }

            public void EnterTextSearchbar(string text)
            {
                app.EnterText("sbrMovies", text);
            }

            public void PressEnter()
            {
                (app as AndroidApp).PressUserAction();
            }
        }
    }
}
