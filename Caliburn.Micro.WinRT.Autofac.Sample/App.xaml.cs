using System.Linq;
using Autofac;
using Caliburn.Micro.WinRT.Autofac.Sample.Views;

namespace Caliburn.Micro.WinRT.Autofac.Sample
{
    sealed partial class App
    {
        public App()
        {
            InitializeComponent();
        }

        public override void HandleOnLaunched()
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
