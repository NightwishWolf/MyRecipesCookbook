//using Autofac;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using RecipeCookbook.ViewModels;
//using System.Reflection;
//using System.Linq;
//using Xamarin.Forms;
//using RecipeCookbook.Data;
//using RecipeCookbook.Services;

//namespace RecipeCookbook
//{
//    public abstract class Bootstrapper
//    {
//        protected ContainerBuilder ContainerBuilder
//        {
//            get; private
//        set;
//        }
//        public Bootstrapper()
//        {
//            Initialize();
//            FinishInitialization();
//        }
//        protected virtual void Initialize()
//        {
//            var currentAssembly = Assembly.GetExecutingAssembly();
//            ContainerBuilder = new ContainerBuilder();
//            ContainerBuilder.RegisterType<RecipeService>();
//            ContainerBuilder.RegisterType<AppShell>();

//            foreach (var type in currentAssembly.DefinedTypes
//         .Where(e =>
//         e.IsSubclassOf(typeof(Page)) ||
//         e.IsSubclassOf(typeof(RecipeViewModel))))
//            {
//                ContainerBuilder.RegisterType(type.AsType());
//            }
//            ContainerBuilder.RegisterType<RestService>().SingleInstance(
//            );
//        }
//        private void FinishInitialization()
//        {
//            var container = ContainerBuilder.Build();
//            Resolver.Initialize(container);
//        }
//    }
//}
