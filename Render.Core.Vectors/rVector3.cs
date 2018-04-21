using static System.Math;

namespace Render.Core.Vectors
{
    public struct rVector3
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public rVector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static rVector3 one => new rVector3(1, 1, 1);

        // so it's hard to type 'î', 'ĵ', and 'k̂' on your keyboard, but that's what intellisense is for
        public static rVector3 î => new rVector3(1, 0, 0);

        public static rVector3 ĵ => new rVector3(0, 1, 0);

        public static rVector3 k̂ => new rVector3(0, 0, 1);

        public static rVector3 O => new rVector3(0, 0, 0);

        public static rVector3 FromAngle(double angle)
        {
            return new rVector3(Sin(angle), Cos(angle), 0);
        }

        public static rVector3 operator +(rVector3 a, rVector3 b)
        {
            return a.Add(b);
        }

        public static rVector3 operator -(rVector3 a, rVector3 b)
        {
            return a.Add(new rVector3(-b.X, -b.Y, -b.Z));
        }

        public static rVector3 operator *(rVector3 a, rVector3 b)
        {
            return a.Cross(b);
        }

        public static rVector3 operator *(rVector3 a, double b)
        {
            return a.Scale(b);
        }

        public static rVector3 operator *(rVector3 a, int b)
        {
            return a.Scale(b);
        }

        public static rVector3 operator *(double a, rVector3 b)
        {
            return b.Scale(a);
        }

        public static rVector3 operator *(int a, rVector3 b)
        {
            return b.Scale(a);
        }

        public static rVector3 operator /(rVector3 a, double b)
        {
            return a.Scale(1 / b);
        }

        #region Situation: there are too many vector classes. Solution: create a new vector class with implicit conversions to them all. New Situation: there are even more vector classes, and the new one is a parody of itself
        public static implicit operator OpenTK.Vector3 (rVector3 p) => new OpenTK.Vector3((float)p.X, (float)p.Y, (float)p.Z);

        public static implicit operator rVector3 (OpenTK.Vector3 v) => new rVector3(v.X, v.Y, v.Z);

        public static implicit operator System.Numerics.Vector3(rVector3 p) => new System.Numerics.Vector3((float)p.X, (float)p.Y, (float)p.Z);

        public static implicit operator rVector3(System.Numerics.Vector3 v) => new rVector3(v.X, v.Y, v.Z);

        public static implicit operator Microsoft.Xna.Framework.Vector3(rVector3 p) => new Microsoft.Xna.Framework.Vector3((float)p.X, (float)p.Y, (float)p.Z);

        public static implicit operator rVector3(Microsoft.Xna.Framework.Vector3 v) => new rVector3(v.X, v.Y, v.Z);

        public static implicit operator MathNet.Numerics.LinearAlgebra.Vector<double>(rVector3 p) => VectorConversions.GetMathVector(p.X, p.Y, p.Z);

        public static implicit operator rVector3(MathNet.Numerics.LinearAlgebra.Vector<double> v) => new rVector3(v[0], v[1], v[2]);
        #endregion

        public rVector ToVector () => new rVector(X, Z);

        public static implicit operator rVector3 ((double x, double y, double z) p) => new rVector3(p.x, p.y, p.z);

        public rVector3 Add(rVector3 to)
        {
            return new rVector3(X + to.X, Y + to.Y, Z + to.Z);
        }

        public rVector3 Scale(double scalar)
        {
            return new rVector3(X * scalar, Y * scalar, Z * scalar);
        }

        public double Dot(rVector3 by)
        {
            return X * by.X + Y + by.Y;
        }

        public rVector3 Cross(rVector3 b)
        {
            return new rVector3(Y * b.Z - Z * b.Y, X * b.Z - Z * b.X, X * b.Y - Y * b.X);
        }

        public double MagnitudeSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        public double Magnitude()
        {
            return Sqrt(MagnitudeSquared());
        }

        public double Angle()
        {
            return Atan2(Y, X);
        }

        public rVector3 Unit()
        {
            return this / Magnitude();
        }

        public rVector3 Rotate(double angle)
        {
            return FromAngle(Angle() + angle) * Magnitude();
        }

        public override string ToString()
        {
            return $"{{{X}, {Y}}}";
        }
    }
}
