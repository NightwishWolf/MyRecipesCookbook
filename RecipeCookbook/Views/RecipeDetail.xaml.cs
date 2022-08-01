using Newtonsoft.Json;
using RecipeCookbook.Models;
using RecipeCookbook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeCookbook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetail : ContentPage
    {

        RecipeViewModel _recipeViewModel;
        public RecipeDetail()
        {
            InitializeComponent();
            BindingContext = _recipeViewModel = new RecipeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _recipeViewModel.OnAppearing();
        }
    }
}