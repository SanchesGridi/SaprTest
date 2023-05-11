namespace SaprTest.Core.Utils;

public static class RectangleIds
{
    private static int _privateId = 0;

    public static int GetNew() => ++_privateId;
}
