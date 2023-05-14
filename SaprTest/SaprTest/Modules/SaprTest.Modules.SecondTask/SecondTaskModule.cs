using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SaprTest.Core.Utils;
using SaprTest.Modules.SecondTask.ViewModels;
using SaprTest.Modules.SecondTask.Views;

namespace SaprTest.Modules.SecondTask;

public class SecondTaskModule : IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
    {
        var regionManager = containerProvider.Resolve<IRegionManager>();

        regionManager.RegisterViewWithRegion(RegionNames.SecondTaskRegion, typeof(SecondTaskControl));
        regionManager.RequestNavigate(RegionNames.SecondTaskRegion, nameof(SecondTaskControl));
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<SecondTaskControl, SecondTaskControlViewModel>();
    }
}