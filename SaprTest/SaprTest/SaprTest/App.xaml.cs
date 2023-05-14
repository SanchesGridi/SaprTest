using Prism.Ioc;
using Prism.Modularity;
using SaprTest.Core.Services.Implementations;
using SaprTest.Core.Services.Interfaces;
using SaprTest.Core.Utils;
using SaprTest.Modules.Exceptions;
using SaprTest.Modules.SecondTask;
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
        containerRegistry.RegisterDialog<ColorsDialogControl, ColorsDialogControlViewModel>(Dialogs.ColorsDialog);
        containerRegistry.RegisterSingleton<ISolidColorBrushDialog, SolidColorBrushDialog>();
        containerRegistry.RegisterSingleton<IViewHelperService, ViewHelperService>();
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        base.ConfigureModuleCatalog(moduleCatalog);

        moduleCatalog.AddModule<ExceptionsModule>();
        moduleCatalog.AddModule<SecondTaskModule>();
    }
}
