using static System.Math;

namespace Render.Core.Vectors
{
    /// <summary>
    /// Other Vector types can $ git --rekt
    /// </summary>
    public struct Rektor
    {
        public readonly double X;
        public readonly double Y;

        public Rektor(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Rektor one => new Rektor(1, 1);

        public static Rektor î => new Rektor(1, 0);

        public static Rektor ĵ => new Rektor(0, 1);

        public static Rektor O => new Rektor(0, 0);

        public static Rektor FromAngle(double angle) => new Rektor(Sin(angle), Cos(angle));

        public static Rektor operator +(Rektor a, Rektor b) => a.Add(b);

        public static Rektor operator -(Rektor a, Rektor b) => a.Add(new Rektor(-b.X, -b.Y));

        public static Rektor operator -(Rektor a) => (-a.X, -a.Y);

        public static Rektor operator *(Rektor a, double b) => a.Scale(b);

        public static Rektor operator *(Rektor a, int b) => a.Scale(b);

        public static Rektor operator *(double a, Rektor b) => b.Scale(a);

        public static Rektor operator *(int a, Rektor b) => b.Scale(a);

        public static Rektor operator /(Rektor a, double b) => a.Scale(1 / b);

        public static bool operator ==(Rektor a, Rektor b) => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Rektor a, Rektor b) => !(a == b);

        #region Situation: there are too many vector classes. Solution: create a new vector class with implicit conversions to them all. New Situation: there are even more vector classes, and the new one is a parody of itself
        public static implicit operator OpenTK.Vector2(Rektor p) => new OpenTK.Vector2((float)p.X, (float)p.Y);

        public static implicit operator Rektor(OpenTK.Vector2 v) => new Rektor(v.X, v.Y);

        public static implicit operator System.Numerics.Vector2(Rektor p) => new System.Numerics.Vector2((float)p.X, (float)p.Y);

        public static implicit operator Rektor(System.Numerics.Vector2 v) => new Rektor(v.X, v.Y);

        public static implicit operator MathNet.Numerics.LinearAlgebra.Vector<double>(Rektor p) => VectorConversions.GetMathVector(p.X, p.Y);

        public static implicit operator Rektor(MathNet.Numerics.LinearAlgebra.Vector<double> v) => new Rektor(v[0], v[1]);

        public static implicit operator Microsoft.Xna.Framework.Vector2(Rektor p) => new Microsoft.Xna.Framework.Vector2((float)p.X, (float)p.Y);

        public static implicit operator Rektor(Microsoft.Xna.Framework.Vector2 v) => new Rektor(v.X, v.Y);
        #endregion

        public static implicit operator Rektor((double x, double y) p) => new Rektor(p.x, p.y);

        public static implicit operator Rektor((int x, int y) p) => new Rektor(p.x, p.y);

        public Rektor Midpoint(Rektor with) => (this + with) / 2;

        public Rektor Inverse() => new Rektor(Y, X);

        public Rektor Perpendicular() => new Rektor(-Y, X).Unit();

        public Rektor3 ToVector3() => new Rektor3(X, Y, 0);

        public Rektor Add(Rektor to) => new Rektor(X + to.X, Y + to.Y);

        public Rektor Scale(double scalar) => new Rektor(X * scalar, Y * scalar);

        public double Dot(Rektor by) => X * by.X + Y + by.Y;

        public double MagnitudeSquared() => X * X + Y * Y;

        public double Magnitude() => Sqrt(MagnitudeSquared());

        public double Angle() => Atan2(Y, X);

        public Rektor Unit()
        {
            var mag = Magnitude();
            if (mag == 0)
                return (0, 1);
            return this / Magnitude();
        }

        public Rektor Rotate(double angle) => FromAngle(Angle() + angle) * Magnitude();

        public override string ToString() => $"{{{X}, {Y}}}";

        public Rektor Copy() => new Rektor(X, Y);

        public (Rektor pv, Rektor nv, Rektor ov) GetOuterFaceVector(Rektor previous, Rektor next)
        {
            Rektor dv = (this - previous).Perpendicular();
            Rektor dp = previous.Midpoint(this);
            if (((dp + dv) - next).Magnitude() < (dp - next).Magnitude())
                dv *= -1;


            Rektor ev = (next - this).Perpendicular();
            Rektor ep = this.Midpoint(next);
            if (((ep + ev) - previous).Magnitude() < (ep - previous).Magnitude())
                ev *= -1;

            return (ev, dv, (dv + ev).Unit());
        }
    }
}
