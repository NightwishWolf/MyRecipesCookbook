using RecipeCookbook.Services;
using RecipeCookbook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using RecipeCookbook.Views;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RecipeCookbook.ViewModels
{
    public partial class NewItemViewModel : BaseViewModel
    {
        private string _recipeName;
        private string _imageUrl;
        private string _ingredients;
        private string _recipeBody;
        private int _rating;
        private string _recipeType;
        private string _recipeTags;

        public string RecipeName { get => _recipeName; set => SetProperty(ref _recipeName, value); }
        public string ImageUrl { get => _imageUrl; set => SetProperty(ref _imageUrl, value); }
        public string Ingredients { get => _ingredients; set => SetProperty(ref _ingredients, value); }
        public string RecipeBody { get => _recipeBody; set => SetProperty(ref _recipeBody, value); }
        public int Rating { get => _rating; set => SetProperty(ref _rating, value); }
        public string RecipeType { get => _recipeType; set => SetProperty(ref _recipeType, value); }
        public string RecipeTags { get => _recipeTags; set => SetProperty(ref _recipeTags, value); }

        private readonly RecipeService recipeService;
        public Command SaveRecipe { get; }

        public NewItemViewModel()
        {
            // this gets resolved without having to initialize it with new
            this.recipeService = DependencyService.Resolve<RecipeService>();

            SaveRecipe = new Command(OnSaveRecipe);
         
        }

        // This command saves the new recipe
        [ICommand]
        private async void OnSaveRecipe(object obj)
        {
            var newRecipe = new RecipeItem();

            // If any of the input fields are empty
            if (string.IsNullOrWhiteSpace(RecipeName) ||
                string.IsNullOrWhiteSpace(ImageUrl) ||
                    string.IsNullOrWhiteSpace(Ingredients) ||
                    string.IsNullOrWhiteSpace(RecipeBody) ||
                    Rating <= 0 || Rating > 10 ||
                    string.IsNullOrWhiteSpace(RecipeType) ||
                    string.IsNullOrWhiteSpace(RecipeTags) 
                        )
            {
                // There's a warning
                await App.Current.MainPage.DisplayAlert("Adding new recipe failed", "You didn't fill in everything properly", "Okay");
                return;
            }
            IsBusy = true;
            newRecipe.RecipeName = RecipeName;
            newRecipe.ImageUrl = ImageUrl;
            newRecipe.Ingredients = Ingredients;
            newRecipe.RecipeBody = RecipeBody;
            newRecipe.Rating = Rating;
            newRecipe.RecipeType = RecipeType;
            newRecipe.RecipeTags = RecipeTags;
            var responseContent = await recipeService.PostRecipe(newRecipe);

            {
                await App.Current.MainPage.DisplayAlert("Succes!", "The recipe was added", "Okay");
            }

            await Shell.Current.GoToAsync("//RecipeOverview");
            IsBusy = false;
        }
    }
}