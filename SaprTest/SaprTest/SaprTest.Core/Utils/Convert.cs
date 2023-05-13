using SaprTest.Core.Exceptions;
using System;

namespace SaprTest.Core.Utils;

public static class Convert
{
    public static double ToDouble(string input, string inputName)
    {
        try
        {
            return double.Parse(input);
        }
        catch (Exception ex)
        {
            throw new ExceptionWithHint($"Value: [{input}]; Name: [{inputName}]", ex);
        }
    }

    public static int ToInt(string input, string inputName)
    {
        try
        {
            return int.Parse(input);
        }
        catch (Exception ex)
        {
            throw new ExceptionWithHint($"Value: [{input}]; Name: [{inputName}]", ex);
        }
    }
}
