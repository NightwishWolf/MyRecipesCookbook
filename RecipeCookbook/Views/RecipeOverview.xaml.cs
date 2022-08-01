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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _recipeViewModel.OnAppearing();
        }

        //async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    int recipeId = (e.CurrentSelection.FirstOrDefault() as RecipeItem).RecipeId;
        //    await Shell.Current.GoToAsync($"recipedetails?RecipeId={recipeId}"); ;
        //}

        private void RecipeOverview_Tapped(object sender, System.EventArgs e)
        {
            BindableObject bo = sender as BindableObject;
            _recipeViewModel.ItemTapped.Execute(bo.BindingContext);

        }
    }
}