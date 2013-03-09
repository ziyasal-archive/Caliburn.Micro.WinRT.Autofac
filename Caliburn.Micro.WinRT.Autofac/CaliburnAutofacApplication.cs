using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Windows.ApplicationModel.Activation;

namespace Caliburn.Micro.WinRT.Autofac
{
    public abstract class CaliburnAutofacApplication : CaliburnApplication
    {
        protected IContainer Container;
        private readonly ContainerBuilder _builder;

        protected CaliburnAutofacApplication()
        {
            _builder = new ContainerBuilder();
        }

        protected override void Configure()
        {
            _builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            HandleConfigure(_builder);
            Container = _builder.Build();
        }

        protected override object GetInstance(Type service, string key)
        {
            object instance;
            if (string.IsNullOrEmpty(key))
            {
                if (Container.TryResolve(service, out instance))
                    return instance;
            }
            else
            {
                if (Container.TryResolveNamed(key, service, out instance))
                    return instance;
            }

            throw new Exception(string.Format("Could not locate any instances of service {0}.", service.Name));
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var result = Container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
            return result;
        }

        protected override void BuildUp(object instance)
        {
            Container.InjectProperties(instance);
        }

        protected override void PrepareViewFirst(Windows.UI.Xaml.Controls.Frame rootFrame)
        {
            _builder.RegisterInstance(new FrameAdapter(rootFrame)).As<INavigationService>().SingleInstance();
        }

        public virtual void HandleConfigure(ContainerBuilder builder)
        {
            /*Register all types by default*/
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                .AsSelf()
                .InstancePerDependency();
        }
    }
}
