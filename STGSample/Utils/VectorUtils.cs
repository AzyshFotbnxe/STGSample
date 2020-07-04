using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STGSample.Utils.ConstantsTable;

namespace STGSample.Utils
{
    public static class VectorUtils
    {
        public static Vector2 RandomLimitedLengthVector(float length, float minlength = 0)
        {
            Vector2 vec = new Vector2((float)((ConstantsTable.RANDOM.NextDouble() - 0.5)*length), (float)((ConstantsTable.RANDOM.NextDouble() - 0.5) * length));
            float len = Math.Min(length, vec.Length());
            if (minlength != 0) len = Math.Max(len, minlength);
            vec.Normalize();
            vec *= len;
            return vec;
        }
        public static Vector2 RandomFixedLengthVector(float length)
        {
            Vector2 vec = new Vector2((float)(ConstantsTable.RANDOM.NextDouble() - 0.5), (float)(ConstantsTable.RANDOM.NextDouble() - 0.5));
            vec.Normalize();
            vec *= length;
            return vec;
        }

        public static Vector2 InverseVector(Vector2 vector, float length = -1f)
        {
            if (length < 0) length = vector.Length();
            var vect = -vector;
            vect.Normalize();
            if (float.IsNaN(vect.X)) return Vector2.Zero;
            return vect * length;
        }

        public static Vector2 LeftVerticalVector(Vector2 vector, float length = -1f)
        {
            if (length < 0) length = vector.Length();
            var vect = new Vector2(-vector.Y, vector.X);
            vect.Normalize();
            if (float.IsNaN(vect.X)) return Vector2.Zero;
            return vect * length;
        }

        public static Vector2 RightVerticalVector(Vector2 vector, float length = -1f)
        {
            if (length < 0) length = vector.Length();
            var vect = new Vector2(vector.Y, -vector.X);
            vect.Normalize();
            if (float.IsNaN(vect.X)) return Vector2.Zero;
            return vect * length;
        }
        public static Vector2 RotateVector(Vector2 vector, float rotateDegree, float length = -1f)
        {
            if (length < 0) length = vector.Length();
            var rad = rotateDegree * DEGTORAD;
            var vect = Vector2.Transform(vector, Matrix.CreateRotationZ(rad));
            vect.Normalize();
            if (float.IsNaN(vect.X)) return Vector2.Zero;
            return vect * length;
        }
    }
}
