using System.Windows.Media;
using Xunit;

namespace SaprTest.Tests.Infrastructure.Models;

public class BrushTheoryData : TheoryData<SolidColorBrush, SolidColorBrush>
{
    public BrushTheoryData()
    {
        Add(Brushes.Red, Brushes.Red);
        Add(Brushes.Orange, Brushes.Orange);
        Add(Brushes.Yellow, Brushes.Yellow);
        Add(Brushes.LimeGreen, Brushes.LimeGreen);
    }
}
