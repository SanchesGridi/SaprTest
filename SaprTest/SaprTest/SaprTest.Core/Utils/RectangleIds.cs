namespace SaprTest.Core.Utils;

// tmp class
public static class RectangleIds
{
    private static int _pathRectangleId = 0;
    private static int _polygonRectangleId = 0;

    public static int NewPathId() => ++_pathRectangleId;
    public static int NewPolygonId() => ++_polygonRectangleId;
}
