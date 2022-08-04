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
    public partial class RecipeDetail : ContentPage
    {

        ItemDetailViewModel _itemDetailViewModel;
        public RecipeDetail()
        {
            InitializeComponent();
            BindingContext = _itemDetailViewModel = new ItemDetailViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _itemDetailViewModel.OnAppearing();
        }
    }
}