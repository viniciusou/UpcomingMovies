using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpcomingMovies.Models
{
    public class Genre: ObservableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
