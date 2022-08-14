using RecipeCookbook.Models;
using RecipeCookbook.Services;
using RecipeCookbook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeCookbook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeOverview : ContentPage
    {

        RecipeViewModel _recipeViewModel;

        public RecipeOverview ()
        {
            InitializeComponent();
            BindingContext = _recipeViewModel = new RecipeViewModel();

        }

        // Call function in ViewModel
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _recipeViewModel.OnAppearing();
        }
    }
}