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
        //  private readonly RecipeService recipeService;
        readonly RecipeService _recipeService = new RecipeService();

        private string name;

        public EditRecipeViewModel()
        {
          //  this.recipeService = DependencyService.Resolve<RecipeService>();
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

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            ItemId = Int32.Parse(HttpUtility.UrlDecode(query["ItemId"]));
        }

        public async Task LoadItem(int itemId)
        {
            try
            {
                recipeItem = await _recipeService.GetSingleRecipe(itemId);
                //if(itemId != null)
                //{
                //    Name = recipeItem.RecipeName;
                //}


            }

            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
