using RecipeCookbook.Models;
using RecipeCookbook.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace RecipeCookbook.ViewModels
{
    public partial class EditRecipeViewModel : BaseViewModel, IQueryAttributable
    {
        readonly RecipeService _recipeService = new RecipeService();
        public Command SaveRecipe { get; }

        private string name;
        private string url;
        private string ingredients;
        private string body;
        private int rating;
        private string type;
        private string tags;

        public EditRecipeViewModel()
        {
            SaveRecipe = new Command(async () => await OnSaveRecipe());
        }

        private RecipeItem _recipeItem;
        public RecipeItem recipeItem
        {
            get { return _recipeItem; }
            set
            {
                _recipeItem = value;
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
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string ImageUrl
        {
            get => url;
            set
            {
                url = value;
                OnPropertyChanged(nameof(ImageUrl));
            }
        }
        public string Ingredients
        { get => ingredients; set { ingredients = value; OnPropertyChanged(nameof(Ingredients)); } }
        public string RecipeBody
        { get => body; set { body = value; OnPropertyChanged(nameof(RecipeBody)); } }
        public int Rating
        { get => rating; set { rating = value; OnPropertyChanged(nameof(Rating)); } }
        public string RecipeType
        { get => type; set { type = value; OnPropertyChanged(nameof(RecipeType)); } }
        public string RecipeTags
        { get => tags; set { tags = value; OnPropertyChanged(nameof(RecipeTags)); } }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            ItemId = Int32.Parse(HttpUtility.UrlDecode(query["ItemId"]));
        }

        // Task to load the recipe which is to be editted, so all the information is there
        public async Task LoadItem(int itemId)
        {
            try
            {
                recipeItem = await _recipeService.GetSingleRecipe(itemId);
                if (itemId != null)
                {
                    Name = recipeItem.RecipeName;
                    ImageUrl = recipeItem.ImageUrl;
                    Ingredients = recipeItem.Ingredients;
                    RecipeBody = recipeItem.RecipeBody;
                    Rating = recipeItem.Rating;
                    RecipeType = recipeItem.RecipeType;
                    RecipeTags = recipeItem.RecipeTags;
                }
            }

            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        // Task to actually save the new recipe
        private async Task OnSaveRecipe()
        {
            try
            {
                var recipe = new RecipeItem
                {
                    RecipeId = ItemId,
                    RecipeName = Name,
                    ImageUrl = ImageUrl,
                    Ingredients = Ingredients,
                    RecipeBody = RecipeBody,
                    Rating = Rating,
                    RecipeType = RecipeType,
                    RecipeTags = RecipeTags
                };

                await _recipeService.EditRecipe(recipe);

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
