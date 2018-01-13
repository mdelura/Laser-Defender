using UnityEngine;

class Boundaries
{
    static Vector3 leftBottom;
    static Vector3 rightTop;

    static Boundaries()
    {
        leftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
        rightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));
    }

    public Boundaries()
    {
    }

    public Boundaries(Vector2 padding)
    {
        Padding = padding;
    }

    public Vector2 Padding { get; set; }

    public float XMin => leftBottom.x + Padding.x;
    public float YMin => leftBottom.y + Padding.y;
    public float XMax => rightTop.x - Padding.x;
    public float YMax => rightTop.y - Padding.y;

    public Vector3 GetRestrictedPosition(Vector3 position)
    {
        float restrictedX = Mathf.Clamp(position.x, XMin, XMax);
        float restrictedY = Mathf.Clamp(position.y, YMin, YMax);

        return new Vector3(restrictedX, restrictedY, position.z);
    }


}
