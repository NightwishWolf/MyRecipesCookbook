using RecipeCookbook.Data;
using RecipeCookbook.Models;
using RecipeCookbook.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RecipeCookbook.ViewModels
{
    public class RecipeViewModel : BaseViewModel
    {
        readonly RecipeService _recipeService = new RecipeService();
        public ObservableCollection<RecipeItem> RecipeItems { get; }
        public Command LoadRecipes { get; }

        public RecipeViewModel()
        {
            RecipeItems = new ObservableCollection<RecipeItem>();
            LoadRecipes = new Command(async () => await ExecuteLoadRecipes());
        }

        public async Task ExecuteLoadRecipes()
        {
            IsBusy = true;

            try

            {
                RecipeItems.Clear();
                var recipeItems = await _recipeService.GetAllRecipes();

                foreach (var recipe in recipeItems)
                {
                    RecipeItems.Add(recipe);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing() //Intialize function called from view
        {
            IsBusy = true; //Trigger a ExecuteLoadItemsCommand command by setting to true
        }

    }
}
