using UnityEngine;

namespace Mana
{


    public static class Vector3Extensions
    {
        public static Vector3 MirrorX(this Vector3 vector) => new Vector3(-vector.x, vector.y, vector.z);
        public static Vector3 MirrorY(this Vector3 vector) => new Vector3(vector.x, -vector.y, vector.z);
        public static Vector3 MirrorZ(this Vector3 vector) => new Vector3(vector.x, vector.y, -vector.z);

        public static float AngleTo(this Vector3 a, Vector3 b) => a == b ? 0f : Mathf.Acos(Vector3.Dot(a, b));

        public static float SignedAngleTo(this Vector3 a, Vector3 b, Vector3 normal)
        {
            var angle = a.AngleTo(b);
            var cross = Vector3.Cross(a, b);
            return Vector3.Dot(cross, normal) < 0 ? -angle : angle;
        }

        public static Vector3 Rotate(this Vector3 vector, float angle, Vector3 axis) => Quaternion.AngleAxis(angle, axis) * vector;

        public static Vector3 Pitch(this Vector3 vector, float angle) => Rotate(vector, angle, Vector3.right);
        public static Vector3 Roll(this Vector3 vector, float angle)  => Rotate(vector, angle, Vector3.forward);
        public static Vector3 Yaw(this Vector3 vector, float angle)   => Rotate(vector, angle, Vector3.up);

        public static Vector3 PitchUp90(this Vector3 vector)   => new Vector3(vector.x, vector.z, -vector.y);
        public static Vector3 PitchDown90(this Vector3 vector) => new Vector3(vector.x, -vector.z, vector.y);

        public static Vector3 RollLeft90(this Vector3 vector)  => new Vector3(-vector.y, vector.x, vector.z);
        public static Vector3 RollRight90(this Vector3 vector) => new Vector3(vector.y, -vector.x, vector.z);

        public static Vector3 YawLeft90(this Vector3 vector)  => new Vector3(-vector.z, vector.y, vector.x);
        public static Vector3 YawRight90(this Vector3 vector) => new Vector3(vector.z, vector.y, -vector.x);

        public static Vector3 RotateAroundPoint(this Vector3 vector, Vector3 pivot, Quaternion angle) => angle * (vector - pivot) + pivot;

        public static Vector3 RotateAroundPoint(this Vector3 vector, Vector3 pivot, float angle, Vector3 axis) => Quaternion.Euler(axis * angle) * (vector - pivot) + pivot;

        public static Vector3 Multiply(this Vector3 vector, Vector3 scale) => new Vector3(vector.x * scale.x, vector.y * scale.y, vector.z * scale.z);

        public static float Average(this Vector3 vector) => (vector.x + vector.y + vector.z) / 3f;

        public static float Max(this Vector3 vector) => Mathf.Max(vector.x, vector.y, vector.z);

        public static Vector3 ScaleBy(this Vector3 v, Vector3 scaleVector) => new Vector3(v.x * scaleVector.x, v.y * scaleVector.y, v.z * scaleVector.z);


#pragma warning disable IDE1006 // Naming Styles

        public static Vector2 xx(this Vector3 v) => new Vector2(v.x, v.x);
        public static Vector2 xy(this Vector3 v) => new Vector2(v.x, v.y);
        public static Vector2 xz(this Vector3 v) => new Vector2(v.x, v.z);

        public static Vector2 yx(this Vector3 v) => new Vector2(v.y, v.x);
        public static Vector2 yy(this Vector3 v) => new Vector2(v.y, v.y);
        public static Vector2 yz(this Vector3 v) => new Vector2(v.y, v.z);

        public static Vector2 zx(this Vector3 v) => new Vector2(v.z, v.x);
        public static Vector2 zy(this Vector3 v) => new Vector2(v.z, v.y);
        public static Vector2 zz(this Vector3 v) => new Vector2(v.z, v.z);

        public static Vector3 xxx(this Vector3 v) => new Vector3(v.x, v.x, v.x);
        public static Vector3 xxy(this Vector3 v) => new Vector3(v.x, v.x, v.y);
        public static Vector3 xxz(this Vector3 v) => new Vector3(v.x, v.x, v.z);

        public static Vector3 xyx(this Vector3 v) => new Vector3(v.x, v.y, v.x);
        public static Vector3 xyy(this Vector3 v) => new Vector3(v.x, v.y, v.y);
        public static Vector3 xyz(this Vector3 v) => new Vector3(v.x, v.y, v.z);

        public static Vector3 xzx(this Vector3 v) => new Vector3(v.x, v.z, v.x);
        public static Vector3 xzy(this Vector3 v) => new Vector3(v.x, v.z, v.y);
        public static Vector3 xzz(this Vector3 v) => new Vector3(v.x, v.z, v.z);

        public static Vector3 yxx(this Vector3 v) => new Vector3(v.y, v.x, v.x);
        public static Vector3 yxy(this Vector3 v) => new Vector3(v.y, v.x, v.y);
        public static Vector3 yxz(this Vector3 v) => new Vector3(v.y, v.x, v.z);

        public static Vector3 yyx(this Vector3 v) => new Vector3(v.y, v.y, v.x);
        public static Vector3 yyy(this Vector3 v) => new Vector3(v.y, v.y, v.y);
        public static Vector3 yyz(this Vector3 v) => new Vector3(v.y, v.y, v.z);

        public static Vector3 yzx(this Vector3 v) => new Vector3(v.y, v.z, v.x);
        public static Vector3 yzy(this Vector3 v) => new Vector3(v.y, v.z, v.y);
        public static Vector3 yzz(this Vector3 v) => new Vector3(v.y, v.z, v.z);

        public static Vector3 zxx(this Vector3 v) => new Vector3(v.z, v.x, v.x);
        public static Vector3 zxy(this Vector3 v) => new Vector3(v.z, v.x, v.y);
        public static Vector3 zxz(this Vector3 v) => new Vector3(v.z, v.x, v.z);

        public static Vector3 zyx(this Vector3 v) => new Vector3(v.z, v.y, v.x);
        public static Vector3 zyy(this Vector3 v) => new Vector3(v.z, v.y, v.y);
        public static Vector3 zyz(this Vector3 v) => new Vector3(v.z, v.y, v.z);

        public static Vector3 zzx(this Vector3 v) => new Vector3(v.z, v.z, v.x);
        public static Vector3 zzy(this Vector3 v) => new Vector3(v.z, v.z, v.y);
        public static Vector3 zzz(this Vector3 v) => new Vector3(v.z, v.z, v.z);

#pragma warning restore IDE1006 // Naming Styles


    }

}