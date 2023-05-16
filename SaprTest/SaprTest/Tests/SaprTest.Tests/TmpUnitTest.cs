using System;
using Xunit;

namespace SaprTest.Tests;

public class TmpUnitTest
{
    [Fact]
    public void Test_MathCos_AlmostZero()
    {
        var d = 90d;
        var r = DegreesToRadians(d); // 1.5707963267948966

        var result = Math.Cos(r); // 6.123233995736766E-17

        Assert.Equal(6.123233995736766E-17, result);
        Assert.Equal(0.00000_00000_00000_06123_23399_57367_66, result);
    }

    private static double DegreesToRadians(double d)
    {
        return d * Math.PI / 180d;
    }
}
