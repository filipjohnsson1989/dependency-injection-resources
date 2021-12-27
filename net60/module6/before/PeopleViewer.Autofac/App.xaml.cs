using Autofac;
using Autofac.Features.ResolveAnything;
using PeopleViewer.Common;
using PeopleViewer.Presentation;
using PersonDataReader.CSV;
using PersonDataReader.Decorators;
using PersonDataReader.Service;
using PersonDataReader.SQL;
using System.Windows;

namespace PeopleViewer.Autofac;

public partial class App : Application
{
    IContainer Container;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ConfigureContainer();
        ComposeObjects();
        Application.Current.MainWindow.Title = "With Dependency Injection - Autofac";
        Application.Current.MainWindow.Show();
    }

    private void ConfigureContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<ServiceReader>().As<IPersonReader>()
            .SingleInstance(); // lifetime
        //builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());// Autoregistration,
// It is not recommended  by Auto-fac's Documentation but it does have this similar features to what we saw with Ninject
        builder.RegisterType<PeopleViewerWindow>().InstancePerDependency(); // Manually registration- Lifetime notation
        builder.RegisterType<PeopleViewModel>().InstancePerDependency();
        Container = builder.Build();
    }

    private void ComposeObjects()
    {
        Application.Current.MainWindow = Container.Resolve<PeopleViewerWindow>();

    }
}
