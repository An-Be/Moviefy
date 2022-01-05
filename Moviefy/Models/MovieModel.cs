using System;
using System.ComponentModel.DataAnnotations;



namespace Moviefy.Models
{
    public class MovieModel
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        [Display(Name ="Release Year")]
       public int DateRelease { get; set; }
    }
}
