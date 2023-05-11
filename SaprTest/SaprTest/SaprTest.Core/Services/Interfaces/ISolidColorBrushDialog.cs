using System.Windows.Media;

namespace SaprTest.Core.Services.Interfaces;

public interface ISolidColorBrushDialog
{
    bool ShowDialog();
    SolidColorBrush GetBrush();
}
