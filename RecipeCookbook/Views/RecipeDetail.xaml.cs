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
        public RecipeDetail()
        {
            InitializeComponent();
            this.BindingContext = new ItemDetailViewModel();
        }
    }
}