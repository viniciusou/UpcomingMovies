using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpcomingMovies.Models
{
     public class Movie: ObservableObject
     {
        public string Title { get; set; }

        [JsonProperty(PropertyName = "genre_ids")]
        public List<int> GenreIds { get; set; }

        public string Overview { get; set; }

        [JsonProperty(PropertyName = "release_date")]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty(PropertyName = "Poster_Path")]
        public string PosterPath { get; set; }

        public string Genre { get; set; }
     }
}
