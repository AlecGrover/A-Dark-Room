using UnityEngine;

namespace Assets.Scripts.Math
{
    public static class ElementWiseMultiplication
    {
        public static Vector3 Multiply(Vector3 vector1, Vector3 vector2)
        {
            Vector3 resultVector = new Vector3(vector1.x * vector2.x, vector1.y * vector2.y, vector1.z * vector2.z);
            return resultVector;
        }
    }
}
