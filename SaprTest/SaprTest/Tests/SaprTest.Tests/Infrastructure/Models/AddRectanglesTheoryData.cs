using Xunit;

namespace SaprTest.Tests.Infrastructure.Models;

public class AddRectanglesTheoryData :
    TheoryData<(double X, double Y, double Width, double Height, double Offset, int Count)>
{
    public AddRectanglesTheoryData()
    {
        Add((50, 50, 300, 155, 20, 5));
        Add((300, 300, 305, 170, 10, 3));
        Add((150, 190, 100, 70, 100, 10));
    }
}
