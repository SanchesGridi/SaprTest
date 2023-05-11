using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SaprTest.Core.Utils;
using SaprTest.Modules.Exceptions.ViewModels;
using SaprTest.Modules.Exceptions.Views;

namespace SaprTest.Modules.Exceptions;

public class ExceptionsModule : IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
    {
        var regionManager = containerProvider.Resolve<IRegionManager>();

        regionManager.RegisterViewWithRegion(RegionNames.ExceptionsRegion, typeof(ConsoleControl));
        regionManager.RequestNavigate(RegionNames.ExceptionsRegion, nameof(ConsoleControl));
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<ConsoleControl, ConsoleControlViewModel>();
    }
}