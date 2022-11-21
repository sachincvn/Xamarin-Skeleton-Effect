using MvvmCross.IoC;
using MvvmCross.ViewModels;
using MyXamarinFormsApp.Core.ViewModels.Home;

namespace MyXamarinFormsApp.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<HomeViewModel>();
        }
    }
}
