using System.Collections.Generic;
using UnityEngine;

namespace Zlitz.Utilities
{
    public static class SpriteExtensions
    {
        private static Dictionary<Texture2D, Texture2D> s_copiedTextures;
        private static Dictionary<Sprite, Texture2D>    s_spriteTextures;

        private static Texture2D s_transparent;

        public static Texture2D transparent
        {
            get
            {
                if (s_transparent == null)
                {
                    s_transparent = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                    s_transparent.SetPixel(0, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));
                    s_transparent.Apply();
                }
                return s_transparent;
            }
        }

        public static Texture2D GetTexture(this Sprite sprite)
        {
            if (sprite == null)
            {
                return transparent;
            }
            return s_GetOrCreatSpriteTexture(sprite);
        }

        private static void s_Init()
        {
            if (s_copiedTextures == null) 
            { 
                s_copiedTextures = new Dictionary<Texture2D, Texture2D>();
            }
            if (s_spriteTextures == null) 
            {
                s_spriteTextures = new Dictionary<Sprite, Texture2D>();
            }
        }

        private static Texture2D s_GetOrCreateCopyTexture(Texture2D texture)
        {
            s_Init();
            if (s_copiedTextures.TryGetValue(texture, out Texture2D copiedTexture))
            {
                if (copiedTexture != null)
                {
                    return copiedTexture;
                }
                s_copiedTextures.Remove(texture);
            }
            RenderTexture rt = new RenderTexture(texture.width, texture.height, 0, RenderTextureFormat.ARGB32);
            rt.filterMode = FilterMode.Point;
            rt.Create();

            Graphics.Blit(texture, rt);

            Texture2D copyTexture = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);
            RenderTexture.active = rt;
            copyTexture.ReadPixels(new Rect(0.0f, 0.0f, texture.width, texture.height), 0, 0);
            copyTexture.Apply();
            s_copiedTextures.Add(texture, copyTexture);

            return copyTexture;
        }

        private static Texture2D s_GetOrCreatSpriteTexture(Sprite sprite)
        {
            s_Init();
            if (s_spriteTextures.TryGetValue(sprite, out Texture2D spriteTexture))
            {
                if (spriteTexture != null)
                {
                    return spriteTexture;
                }
                s_spriteTextures.Remove(sprite);
            }
            Texture2D copiedTexture = s_GetOrCreateCopyTexture(sprite.texture);
            Texture2D newSpriteTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.ARGB32, false);
            newSpriteTexture.filterMode = FilterMode.Point;
            newSpriteTexture.SetPixels(copiedTexture.GetPixels((int)sprite.rect.x, (int)sprite.rect.y, (int)sprite.rect.width, (int)sprite.rect.height));
            newSpriteTexture.Apply();

            s_spriteTextures.Add(sprite, newSpriteTexture);
            return newSpriteTexture;
        }
    }
}