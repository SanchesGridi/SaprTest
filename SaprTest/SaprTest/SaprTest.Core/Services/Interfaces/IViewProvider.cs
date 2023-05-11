using System.Windows;

namespace SaprTest.Core.Services.Interfaces;

public interface IViewProvider
{
    TView GetView<TView>(DependencyObject rootView, string viewName) where TView : DependencyObject;
}
