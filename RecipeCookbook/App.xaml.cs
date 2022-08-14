
using RecipeCookbook.Services;
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
            //  register the things as dependency for dependency injection;
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
