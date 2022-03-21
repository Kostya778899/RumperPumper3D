using UnityEngine;

public interface ISetColor
{
    public void SetColor(in Vector3 color) => SetColor(new Color(color.x, color.y, color.z));
    public void SetColor(in Vector4 color) => SetColor(new Color(color.x, color.y, color.z, color.w));
    public void SetColor(Color color);
}
