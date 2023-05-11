using SaprTest.Core.Services.Interfaces;
using System.Windows.Media;

namespace SaprTest.Core.Services.Implementations;

public class SolidColorBrushDialog : ISolidColorBrushDialog
{
    private Color? _color = null;

    public bool ShowDialog()
    {
        using var dialog = new System.Windows.Forms.ColorDialog();
        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            _color = Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B);
            return true;
        }
        else
        {
            _color = null;
            return false;
        }
    }

    public SolidColorBrush GetBrush()
    {
        if (_color != null)
        {
            return new SolidColorBrush(_color.Value);
        }
        else
        {
            return null;
        }
    }
}
