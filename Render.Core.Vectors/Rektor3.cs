using static System.Math;

namespace Render.Core.Vectors
{
    /// <summary>
    /// Your favorite vector type, now with 50% more axes!
    /// </summary>
    public struct Rektor3
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public Rektor3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Rektor3 one => new Rektor3(1, 1, 1);

        // so it's hard to type 'î', 'ĵ', and 'k̂' on your keyboard, but that's what intellisense is for
        public static Rektor3 î => new Rektor3(1, 0, 0);

        public static Rektor3 ĵ => new Rektor3(0, 1, 0);

        public static Rektor3 k̂ => new Rektor3(0, 0, 1);

        public static Rektor3 O => new Rektor3(0, 0, 0);

        public static Rektor3 FromAngle(double angle)
        {
            return new Rektor3(Sin(angle), Cos(angle), 0);
        }

        public static Rektor3 operator +(Rektor3 a, Rektor3 b)
        {
            return a.Add(b);
        }

        public static Rektor3 operator -(Rektor3 a, Rektor3 b)
        {
            return a.Add(new Rektor3(-b.X, -b.Y, -b.Z));
        }

        public static Rektor3 operator -(Rektor3 a) => (-a.X, -a.Y, -a.Z);

        public static Rektor3 operator *(Rektor3 a, Rektor3 b)
        {
            return a.Cross(b);
        }

        public static Rektor3 operator *(Rektor3 a, double b)
        {
            return a.Scale(b);
        }

        public static Rektor3 operator *(Rektor3 a, int b)
        {
            return a.Scale(b);
        }

        public static Rektor3 operator *(double a, Rektor3 b)
        {
            return b.Scale(a);
        }

        public static Rektor3 operator *(int a, Rektor3 b)
        {
            return b.Scale(a);
        }

        public static Rektor3 operator /(Rektor3 a, double b)
        {
            return a.Scale(1 / b);
        }

        #region Situation: there are too many vector classes. Solution: create a new vector class with implicit conversions to them all. New Situation: there are even more vector classes, and the new one is a parody of itself
        public static implicit operator OpenTK.Vector3 (Rektor3 p) => new OpenTK.Vector3((float)p.X, (float)p.Y, (float)p.Z);

        public static implicit operator Rektor3 (OpenTK.Vector3 v) => new Rektor3(v.X, v.Y, v.Z);

        public static implicit operator System.Numerics.Vector3(Rektor3 p) => new System.Numerics.Vector3((float)p.X, (float)p.Y, (float)p.Z);

        public static implicit operator Rektor3(System.Numerics.Vector3 v) => new Rektor3(v.X, v.Y, v.Z);

        public static implicit operator Microsoft.Xna.Framework.Vector3(Rektor3 p) => new Microsoft.Xna.Framework.Vector3((float)p.X, (float)p.Y, (float)p.Z);

        public static implicit operator Rektor3(Microsoft.Xna.Framework.Vector3 v) => new Rektor3(v.X, v.Y, v.Z);

        public static implicit operator MathNet.Numerics.LinearAlgebra.Vector<double>(Rektor3 p) => VectorConversions.GetMathVector(p.X, p.Y, p.Z);

        public static implicit operator Rektor3(MathNet.Numerics.LinearAlgebra.Vector<double> v) => new Rektor3(v[0], v[1], v[2]);
        #endregion

        public Rektor ToVector () => new Rektor(X, Y);

        public static implicit operator Rektor3 ((double x, double y, double z) p) => new Rektor3(p.x, p.y, p.z);

        public Rektor3 Add(Rektor3 to)
        {
            return new Rektor3(X + to.X, Y + to.Y, Z + to.Z);
        }

        public Rektor3 Scale(double scalar)
        {
            return new Rektor3(X * scalar, Y * scalar, Z * scalar);
        }

        public double Dot(Rektor3 by)
        {
            return X * by.X + Y + by.Y;
        }

        public Rektor3 Cross(Rektor3 b)
        {
            return new Rektor3(Y * b.Z - Z * b.Y, X * b.Z - Z * b.X, X * b.Y - Y * b.X);
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

        public Rektor3 Unit()
        {
            return this / Magnitude();
        }

        public Rektor3 Rotate(double angle)
        {
            return FromAngle(Angle() + angle) * Magnitude();
        }

        public override string ToString()
        {
            return $"{{{X}, {Y}}}";
        }
    }
}
