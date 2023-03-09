using UnityEngine;

namespace Zlitz.Utilities
{
    public static class MaterialExtensions
    {
        public static Vector2 GetVector2(this Material material, string name)
        {
            return material.GetVector(name);
        }

        public static Vector2 GetVector2(this Material material, int nameId)
        {
            return material.GetVector(nameId);
        }

        public static Vector3 GetVector3(this Material material, string name)
        {
            return material.GetVector(name);
        }

        public static Vector3 GetVector3(this Material material, int nameId)
        {
            return material.GetVector(nameId);
        }

        public static Vector4 GetVector4(this Material material, string name)
        {
            return material.GetVector(name);
        }

        public static Vector4 GetVector4(this Material material, int nameId)
        {
            return material.GetVector(nameId);
        }
    }
}
