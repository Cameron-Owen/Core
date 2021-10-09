using UnityEngine;

namespace Mana
{
    public static class Vector2Extensions
    {
        public static Vector3 ToVector3(this Vector2 vector)
        {
            return new Vector3(vector.x, vector.y, 0f);
        }

        public static Vector3 ToVector3(this Vector2 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }

        public static Vector2 ScaleBy(this Vector2 v, Vector2 scaleVector)
        {
            return new Vector2(v.x * scaleVector.x, v.y * scaleVector.y);
        }

        public static Vector2 Abs(this Vector2 v)
        {
            return new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
        }

#pragma warning disable IDE1006 // Naming Styles


        public static Vector2 XX(this Vector2 v)
        {
            return new Vector2(v.x, v.x);
        }

        public static Vector2 YY(this Vector2 v)
        {
            return new Vector2(v.y, v.y);
        }

        public static Vector2 YX(this Vector2 v)
        {
            return new Vector2(v.y, v.x);
        }

        public static Vector3 XXX(this Vector2 v)
        {
            return new Vector3(v.x, v.x, v.x);
        }

        public static Vector3 XYZ(this Vector2 v)
        {
            return new Vector3(v.x, v.y, v.x);
        }

        public static Vector3 XYY(this Vector2 v)
        {
            return new Vector3(v.x, v.y, v.y);
        }

        public static Vector3 YYY(this Vector2 v)
        {
            return new Vector3(v.y, v.y, v.y);
        }
        public static Vector3 YXY(this Vector2 v)
        {
            return new Vector3(v.y, v.x, v.y);
        }

        public static Vector3 YXX(this Vector2 v)
        {
            return new Vector3(v.y, v.x, v.x);
        }

#pragma warning restore IDE1006 // Naming Styles

    }

}