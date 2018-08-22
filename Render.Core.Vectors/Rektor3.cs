using static System.Math;

namespace Render.Core.Vectors
{
    /// <summary>
    /// Your favorite vector type, now with 50% more axes!
    /// </summary>
    public struct Vector3
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3 one => new Vector3(1, 1, 1);

        // so it's hard to type 'î', 'ĵ', and 'k̂' on your keyboard, but that's what intellisense is for
        public static Vector3 î => new Vector3(1, 0, 0);

        public static Vector3 ĵ => new Vector3(0, 1, 0);

        public static Vector3 k̂ => new Vector3(0, 0, 1);

        public static Vector3 O => new Vector3(0, 0, 0);

        public static Vector3 FromAngle(double angle)
        {
            return new Vector3(Sin(angle), Cos(angle), 0);
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return a.Add(b);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return a.Add(new Vector3(-b.X, -b.Y, -b.Z));
        }

        public static Vector3 operator -(Vector3 a) => (-a.X, -a.Y, -a.Z);

        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return a.Cross(b);
        }

        public static Vector3 operator *(Vector3 a, double b)
        {
            return a.Scale(b);
        }

        public static Vector3 operator *(Vector3 a, int b)
        {
            return a.Scale(b);
        }

        public static Vector3 operator *(double a, Vector3 b)
        {
            return b.Scale(a);
        }

        public static Vector3 operator *(int a, Vector3 b)
        {
            return b.Scale(a);
        }

        public static Vector3 operator /(Vector3 a, double b)
        {
            return a.Scale(1 / b);
        }

        #region Situation: there are too many vector classes. Solution: create a new vector class with implicit conversions to them all. New Situation: there are even more vector classes, and the new one is a parody of itself
        public static implicit operator OpenTK.Vector3 (Vector3 p) => new OpenTK.Vector3((float)p.X, (float)p.Y, (float)p.Z);

        public static implicit operator Vector3 (OpenTK.Vector3 v) => new Vector3(v.X, v.Y, v.Z);

        public static implicit operator System.Numerics.Vector3(Vector3 p) => new System.Numerics.Vector3((float)p.X, (float)p.Y, (float)p.Z);

        public static implicit operator Vector3(System.Numerics.Vector3 v) => new Vector3(v.X, v.Y, v.Z);

        public static implicit operator Microsoft.Xna.Framework.Vector3(Vector3 p) => new Microsoft.Xna.Framework.Vector3((float)p.X, (float)p.Y, (float)p.Z);

        public static implicit operator Vector3(Microsoft.Xna.Framework.Vector3 v) => new Vector3(v.X, v.Y, v.Z);

        public static implicit operator MathNet.Numerics.LinearAlgebra.Vector<double>(Vector3 p) => VectorConversions.GetMathVector(p.X, p.Y, p.Z);

        public static implicit operator Vector3(MathNet.Numerics.LinearAlgebra.Vector<double> v) => new Vector3(v[0], v[1], v[2]);
        #endregion

        public Rektor ToVector () => new Rektor(X, Y);

        public static implicit operator Vector3 ((double x, double y, double z) p) => new Vector3(p.x, p.y, p.z);

        public Vector3 Add(Vector3 to)
        {
            return new Vector3(X + to.X, Y + to.Y, Z + to.Z);
        }

        public Vector3 Scale(double scalar)
        {
            return new Vector3(X * scalar, Y * scalar, Z * scalar);
        }

        public double Dot(Vector3 by)
        {
            return X * by.X + Y + by.Y;
        }

        public Vector3 Cross(Vector3 b)
        {
            return new Vector3(Y * b.Z - Z * b.Y, X * b.Z - Z * b.X, X * b.Y - Y * b.X);
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

        public Vector3 Unit()
        {
            return this / Magnitude();
        }

        public Vector3 Rotate(double angle)
        {
            return FromAngle(Angle() + angle) * Magnitude();
        }

        public override string ToString()
        {
            return $"{{{X}, {Y}}}";
        }
    }
}
