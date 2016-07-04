using System;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using Microsoft.Practices.Unity;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using XboxMusic1.DataModel;

namespace XboxMusic1
{
    sealed partial class App : MvvmAppBase
    {
        private readonly IUnityContainer mContainer = new UnityContainer();

        public App()
        {
            this.InitializeComponent();
        }

        protected override Task OnLaunchApplication(LaunchActivatedEventArgs args)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            NavigationService.Navigate("Main", null);
            Window.Current.Activate();
            return Task.FromResult<object>(null);
        }

        protected override void OnRegisterKnownTypesForSerialization()
        {
            base.OnRegisterKnownTypesForSerialization();
        }

        protected override object Resolve(Type type)
        {
            return mContainer.Resolve(type);
        }

        protected override void OnInitialize(IActivatedEventArgs args)
        {
            mContainer.RegisterType<IMusicRepository, MusicRepository>(new ContainerControlledLifetimeManager());
            mContainer.RegisterInstance<INavigationService>(NavigationService);
            mContainer.RegisterInstance<ISessionStateService>(SessionStateService);

            ViewModelLocator.SetDefaultViewModelFactory((viewModelType) => mContainer.Resolve(viewModelType));
        }
    }
}
