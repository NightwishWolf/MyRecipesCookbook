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
    public partial class AddRecipe : ContentPage
    {
        NewItemViewModel _newItemViewModel;
        public AddRecipe()
        {
            InitializeComponent();
            BindingContext = _newItemViewModel = new NewItemViewModel();
        }
    }
}