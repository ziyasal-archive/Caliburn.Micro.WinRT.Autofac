using System.Linq;
using Autofac;
using Caliburn.Micro.WinRT.Autofac.Sample.Views;
using Windows.ApplicationModel.Activation;

namespace Caliburn.Micro.WinRT.Autofac.Sample
{
    sealed partial class App
    {
        public App()
        {
            InitializeComponent();
        }
        
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootView<MainPage>();
        }

        public override void HandleConfigure(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
            .AsSelf()
            .InstancePerDependency();
        }
    }
}
