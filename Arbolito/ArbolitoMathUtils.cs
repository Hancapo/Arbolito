using SharpDX;

namespace Arbolito
{
    public class ArbolitoMathUtils
    {
        public static Quaternion EulerVectorToQuaternion(Vector3 getVector)
        {
            var deg = new Vector3((float)Convert.ToDecimal(getVector.X), (float)Convert.ToDecimal(getVector.Y), (float)Convert.ToDecimal(getVector.Z));
            var rads = deg * (float)(Math.PI / 180.0);
            return Quaternion.RotationYawPitchRoll(rads.Y, rads.X, rads.Z);
        }

        public static Vector4 QuaternionToVector4(Quaternion quat)
        {
            return new Vector4(quat.X, quat.Y, quat.Z, quat.W);
        }

        public static Quaternion Vector4ToQuaternion(Vector4 vec4)
        {
            return new Quaternion(vec4.X, vec4.Y, vec4.Z, vec4.W);
        }
    }
}
