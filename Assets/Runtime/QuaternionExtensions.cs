using UnityEngine;

namespace Mana
{
    public static class QuaternionExtensions
    {
        public static float Length(this Quaternion q)
        {
            return Mathf.Sqrt(q.w * q.w + q.x * q.x + q.y * q.y + q.z * q.z);
        }
    }
}