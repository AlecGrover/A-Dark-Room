using UnityEngine;

namespace Assets.Scripts.Math
{
    public static class Vector3MathExtension
    {
        public static Vector3 Multiply(Vector3 vector1, Vector3 vector2)
        {
            Vector3 resultVector = new Vector3(vector1.x * vector2.x, vector1.y * vector2.y, vector1.z * vector2.z);
            return resultVector;
        }

        public static Vector3 Rotate3DPointAroundYAxis(Vector3 point, float radians)
        {
            Vector3 outVector = new Vector3(point.x * Mathf.Cos(radians) - point.z * Mathf.Sin(radians),
                                              point.y,
                                            point.x * Mathf.Sin(radians) + point.z * Mathf.Cos(radians));
            return outVector;
        }

        public static Vector3 Abs(Vector3 inVector)
        {
            Vector3 outVector = new Vector3(Mathf.Abs(inVector.x), Mathf.Abs(inVector.y), Mathf.Abs(inVector.z));
            return outVector;
        }
    }
}
