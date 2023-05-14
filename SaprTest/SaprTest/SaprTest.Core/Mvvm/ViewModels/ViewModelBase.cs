using Prism.Events;
using Prism.Mvvm;
using SaprTest.Core.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SaprTest.Core.Mvvm.ViewModels;

public abstract class ViewModelBase : BindableBase
{
    protected readonly Application _application;
    protected readonly IViewHelperService _viewHelperService;
    protected readonly IEventAggregator _eventAggregator;

    public ViewModelBase(IViewHelperService viewHelperService, IEventAggregator eventAggregator)
    {
        _application = Application.Current;
        _viewHelperService = viewHelperService;
        _eventAggregator = eventAggregator;
    }

    protected virtual async Task DispatchAsync(Action action)
    {
        await _application?.Dispatcher?.InvokeAsync(action);
    }
}
