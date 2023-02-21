using UnityEngine;

namespace Common.Util
{
    public static class SpritesUtil
    {
        public static Color GetPixelFromSprite(Sprite sprite, int x, int y)
        {
            return sprite.texture.GetPixel(x% 64 + (int)sprite.rect.x,
                y % 64 + (int)sprite.rect.y);
        }
        
        public static Vector3 ColorToVector3(Color color)
        {
            return new Vector3(color.r, color.g, color.b);
        }
    }
}
