using RecipeCookbook.Data;
using RecipeCookbook.Models;
using RecipeCookbook.Services;
using RecipeCookbook.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
        public Command GoToAddRecipeView { get; set; }
        
      //  public RecipeItem _selectedItem;
        public Command<RecipeItem> ItemTapped { get; }
        public Command<RecipeItem> DeleteRecipe { get; }
        public Command<RecipeItem> EditThisRecipe { get; }
        public RecipeViewModel()
        {
            RecipeItems = new ObservableCollection<RecipeItem>();
            LoadRecipes = new Command(async () => await ExecuteLoadRecipes());
            GoToAddRecipeView = new Command(OnGoToAddRecipeView);
            ItemTapped = new Command<RecipeItem>(async(recipe) => await OnItemSelected(recipe));
            DeleteRecipe = new Command<RecipeItem>(async (recipe) => await OnDeleteRecipe(recipe));
            EditThisRecipe = new Command<RecipeItem>(async (recipe) => await OnEditRecipe(recipe));
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

        private static async void OnGoToAddRecipeView(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AddRecipe)); //Open add recipe view
        }
        private async Task OnItemSelected(RecipeItem recipeItem)
        {
            if (recipeItem == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(RecipeDetail)}?ItemId={recipeItem.RecipeId}");
        }

        private async Task OnEditRecipe(RecipeItem recipeItem)
        {
            if (recipeItem == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EditRecipe)}?ItemId={recipeItem.RecipeId}");
        }
        private async Task OnDeleteRecipe(RecipeItem recipe)
        {

            string result = await Application.Current.MainPage.DisplayActionSheet
                ("Are you sure you want to delete this recipe?", "Cancel", "Yes");

            if (result == "Yes")
            {
                await _recipeService.DeleteRecipe(recipe);

                //ExecuteLoadRecipes();
            }
        }

    }
}
