using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SaprTest.Core.Mvvm.ViewModels;

public abstract class ViewModelBase : BindableBase
{
    protected readonly Application _application;

    public ViewModelBase()
    {
        _application = Application.Current;
    }

    protected virtual async Task DispatchAsync(Action action)
    {
        await _application?.Dispatcher?.InvokeAsync(action);
    }
}
