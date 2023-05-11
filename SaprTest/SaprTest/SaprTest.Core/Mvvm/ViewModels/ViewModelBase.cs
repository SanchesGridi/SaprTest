using Prism.Events;
using Prism.Mvvm;
using SaprTest.Core.Services.SelfImplemented;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SaprTest.Core.Mvvm.ViewModels;

public abstract class ViewModelBase : BindableBase
{
    protected readonly Application _application;
    protected readonly ViewHelper _viewHelper;
    protected readonly IEventAggregator _eventAggregator;

    public ViewModelBase(ViewHelper viewHelper, IEventAggregator eventAggregator)
    {
        _application = Application.Current;
        _viewHelper = viewHelper;
        _eventAggregator = eventAggregator;
    }

    protected virtual async Task DispatchAsync(Action action)
    {
        await _application?.Dispatcher?.InvokeAsync(action);
    }
}
