using RecipeCookbook.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RecipeCookbook.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        readonly RecipeService _recipeService = new RecipeService();

        int _itemId;
        string _recipeName;
        string _imageUrl;
        string _ingredients;
        string _recipeBody;
        int _rating;
        string _recipeType;
        string _recipeTags;

        public string Id { get; set; }

        bool _recipeNameVisible;

        public ItemDetailViewModel()
        {
            RecipeNameVisible = true;
        }

        public int ItemId
        {
            get
            {
                return _itemId;
            }
            set
            {
                if (value == null)
                    return;

                _itemId = value;
                LoadItemId(value);
            }
        }

        public string RecipeName
        {
            get => _recipeName;
            set => SetProperty(ref _recipeName, value);
        }
        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }

        public string RecipeBody
        {
            get => _recipeBody;
            set => SetProperty(ref _recipeBody, value);
        }

        public string Ingredients
        {
            get => _ingredients;
            set => SetProperty(ref _ingredients, value);
        }
        public string RecipeType
        {
            get => _recipeType;
            set => SetProperty(ref _recipeType, value);
        }

        public string RecipeTags
        {
            get => _recipeTags;
            set => SetProperty(ref _recipeTags, value);
        }

        public int Rating
        {
            get => _rating;
            set => SetProperty(ref _rating, value);
        }

        public bool RecipeNameVisible
        {
            get => _recipeNameVisible;
            set => SetProperty(ref _recipeNameVisible, value);
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await _recipeService.GetSingleRecipe(itemId);
                

                RecipeNameVisible = !String.IsNullOrEmpty(RecipeName);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            LoadItemId(_itemId);
        }

    }
}
