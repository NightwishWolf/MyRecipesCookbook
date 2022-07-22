using RecipeCookbook.Data;
using RecipeCookbook.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeCookbook
{
    public partial class App : Application
    {
        public static RecipeManager ToRecipesManager { get; private set; }
        public App()
        {
            InitializeComponent();

            //   MainPage = new MainPage();
            ToRecipesManager = new RecipeManager(new RestService());
            MainPage = new NavigationPage(new RecipeOverview());
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
