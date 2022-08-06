﻿using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using RecipeCookbook.Models;
using RecipeCookbook.Views;
using RecipeCookbook.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RecipeCookbook.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel, IQueryAttributable
    {
        readonly RecipeService _recipeService = new RecipeService();

        public Command GoEditRecipe { get; set; }

        public Command<RecipeItem> EditThisRecipe { get; }

        public ItemDetailViewModel()
        {
            GoEditRecipe = new Command(GoToEditRecipeView);
            EditThisRecipe = new Command<RecipeItem>(async (recipe) => await OnEditThisRecipe(recipe));
        }

        private RecipeItem _recipeItem;

        public RecipeItem recipeItem
        {
            get { return _recipeItem; }
            set 
            { _recipeItem = value;
                    OnPropertyChanged();
            }
        }
        
        int _itemId; 

        public int ItemId
        {
            get
            {
                return _itemId;
            }
            set
            {

                _itemId = value;
                LoadItem(value);
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            ItemId = Int32.Parse(HttpUtility.UrlDecode(query["ItemId"]));
        }

        public async Task LoadItem(int itemId)
        {
            try
            {
               recipeItem = await _recipeService.GetSingleRecipe(itemId);
           
                
            }

            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private static async void GoToEditRecipeView(object obj)
        {
            await Shell.Current.GoToAsync(nameof(EditRecipe)); //Open add recipe view
        }

        private async Task OnEditThisRecipe(RecipeItem recipeItem)
        {
            if (recipeItem == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EditRecipe)}?ItemId={recipeItem.RecipeId}");
        }

    }
}
