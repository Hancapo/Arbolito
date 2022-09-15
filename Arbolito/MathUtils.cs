using SharpDX;

namespace Arbolito
{
    public class MathUtils
    {
        public static Quaternion EulerVectorToQuaternion(Vector3 getVector)
        {
            var deg = new Vector3((float)Convert.ToDecimal(getVector.X), (float)Convert.ToDecimal(getVector.Y), (float)Convert.ToDecimal(getVector.Z));
            var rads = deg * (float)(Math.PI / 180.0);
            return Quaternion.RotationYawPitchRoll(rads.Y, rads.X, rads.Z);
        }
    }
}
