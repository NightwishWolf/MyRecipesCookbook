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
    public partial class EditRecipe : ContentPage
    {
       // EditRecipeViewModel _editRecipeViewModel;
        public EditRecipe()
        {
            InitializeComponent();
          //  BindingContext = _editRecipeViewModel = DependencyService.Resolve<EditRecipeViewModel>();
            this.BindingContext = new EditRecipeViewModel();
        }

        }
}