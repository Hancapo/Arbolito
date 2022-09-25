using SharpDX;
using System;

namespace ArbolitoAva
{
    public class ArbolitoMathUtils
    {
        public static Quaternion EulerVectorToQuaternion(Vector3 getVector)
        {
            var deg = new Vector3((float)Convert.ToDecimal(getVector.X), (float)Convert.ToDecimal(getVector.Y), (float)Convert.ToDecimal(getVector.Z));
            var rads = deg * (float)(Math.PI / 180.0);
            return Quaternion.RotationYawPitchRoll(rads.Y, rads.X, rads.Z);

        }

        public static int ReturnNormalizedValue(int value, int min, int max)
        {
            return (int)Math.Round((double)(value - min) / (max - min) * 100);
        }


    }

    
}
