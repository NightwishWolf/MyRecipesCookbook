using RecipeCookbook.Data;
using RecipeCookbook.ViewModels;
using RecipeCookbook.Views;
using RecipeCookbook.Services;
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
            //  MainPage = new RecipeOverview();
            DependencyService.Register<NewItemViewModel>();
            DependencyService.Register<RecipeService>();

            MainPage = new AppShell();

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
