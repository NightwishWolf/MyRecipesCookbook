using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeCookbook.Models
{
    public class RecipeItem
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string ImageUrl { get; set; }
        public string Ingredients { get; set; }
        public string RecipeBody { get; set; }
        public int Rating { get; set; }
        public string RecipeType { get; set; }
        public string RecipeTags { get; set; }
    }
}
