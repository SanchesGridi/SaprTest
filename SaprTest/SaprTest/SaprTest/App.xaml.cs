using Prism.Ioc;
using Prism.Modularity;
using SaprTest.Core.Services.Implementations;
using SaprTest.Core.Services.Interfaces;
using SaprTest.Core.Services.SelfImplemented;
using SaprTest.Modules.Exceptions;
using SaprTest.ViewModels;
using SaprTest.Views;
using System.Windows;

namespace SaprTest;

public partial class App
{
    protected override Window CreateShell()
    {
        // return Container.Resolve<MainWindow>();
        return Container.Resolve<TestCanvasWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
        containerRegistry.RegisterForNavigation<TestCanvasWindow, TestCanvasWindowViewModel>();

        containerRegistry.RegisterSingleton<ISolidColorBrushDialog, SolidColorBrushDialog>();
        containerRegistry.RegisterSingleton<IViewProvider, ViewProvider>();
        containerRegistry.RegisterSingleton<ViewHelper>();
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        base.ConfigureModuleCatalog(moduleCatalog);

        moduleCatalog.AddModule<ExceptionsModule>();
    }
}
