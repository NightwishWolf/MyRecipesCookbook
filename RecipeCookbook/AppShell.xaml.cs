using RecipeCookbook.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeCookbook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RecipeOverview), typeof(RecipeOverview));
            Routing.RegisterRoute(nameof(AddRecipe), typeof(AddRecipe));
            Routing.RegisterRoute(nameof(RecipeDetail), typeof(RecipeDetail));
            Routing.RegisterRoute(nameof(EditRecipe), typeof(EditRecipe));
        }
    }
}