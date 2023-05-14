using Prism.Events;
using SaprTest.Core.Mvvm.ViewModels;
using SaprTest.Core.Services.Interfaces;

namespace SaprTest.Modules.SecondTask.ViewModels;

public class SecondTaskControlViewModel : ViewModelBase
{
    public SecondTaskControlViewModel(
        IViewHelperService viewHelperService,
        IEventAggregator eventAggregator) : base(viewHelperService, eventAggregator)
    {
    }
}
