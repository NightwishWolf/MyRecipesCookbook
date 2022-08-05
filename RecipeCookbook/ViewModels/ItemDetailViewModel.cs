using RecipeCookbook.Models;
using RecipeCookbook.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace RecipeCookbook.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel, IQueryAttributable
    {
        readonly RecipeService _recipeService = new RecipeService();

           public ItemDetailViewModel()
        {
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
    }
}
