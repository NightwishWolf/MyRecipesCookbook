using RecipeCookbook.Data;
using RecipeCookbook.ViewModels;
using RecipeCookbook.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeCookbook
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new RecipeOverview();
         
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
