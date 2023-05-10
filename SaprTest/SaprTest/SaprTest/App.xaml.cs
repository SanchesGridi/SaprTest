using Prism.Ioc;
using Prism.Modularity;
using SaprTest.ViewModels;
using SaprTest.Views;
using System.Windows;

namespace SaprTest;

public partial class App
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        base.ConfigureModuleCatalog(moduleCatalog);
    }
}
