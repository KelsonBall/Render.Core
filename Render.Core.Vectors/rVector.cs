using static System.Math;

namespace Render.Core.Vectors
{
    public struct rVector
    {
        public readonly double X;
        public readonly double Y;

        public rVector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static rVector one => new rVector(1, 1);

        public static rVector î => new rVector(1, 0);

        public static rVector ĵ => new rVector(0, 1);

        public static rVector O => new rVector(0, 0);

        public static rVector FromAngle(double angle) => new rVector(Sin(angle), Cos(angle));

        public static rVector operator +(rVector a, rVector b) => a.Add(b);

        public static rVector operator -(rVector a, rVector b) => a.Add(new rVector(-b.X, -b.Y));

        public static rVector operator *(rVector a, double b) => a.Scale(b);

        public static rVector operator *(rVector a, int b) => a.Scale(b);

        public static rVector operator *(double a, rVector b) => b.Scale(a);

        public static rVector operator *(int a, rVector b) => b.Scale(a);

        public static rVector operator /(rVector a, double b) => a.Scale(1 / b);

        public static bool operator ==(rVector a, rVector b) => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(rVector a, rVector b) => !(a == b);

        public static implicit operator OpenTK.Vector2(rVector p) => new OpenTK.Vector2((float)p.X, (float)p.Y);

        public static implicit operator rVector(OpenTK.Vector2 v) => new rVector(v.X, v.Y);

        public static implicit operator System.Numerics.Vector2(rVector p) => new System.Numerics.Vector2((float)p.X, (float)p.Y);

        public static implicit operator rVector(System.Numerics.Vector2 v) => new rVector(v.X, v.Y);

        public static implicit operator Microsoft.Xna.Framework.Vector2(rVector p) => new Microsoft.Xna.Framework.Vector2((float)p.X, (float)p.Y);

        public static implicit operator rVector(Microsoft.Xna.Framework.Vector2 v) => new rVector(v.X, v.Y);

        public static implicit operator rVector((double x, double y) p) => new rVector(p.x, p.y);

        public static implicit operator rVector((int x, int y) p) => new rVector(p.x, p.y);

        public rVector Midpoint(rVector with) => (this + with) / 2;

        public rVector Inverse() => new rVector(Y, X);

        public rVector Perpendicular() => new rVector(-Y, X).Unit();

        public rVector3 ToVector3() => new rVector3(X, 0, Y);

        public rVector Add(rVector to) => new rVector(X + to.X, Y + to.Y);

        public rVector Scale(double scalar) => new rVector(X * scalar, Y * scalar);

        public double Dot(rVector by) => X * by.X + Y + by.Y;

        public double MagnitudeSquared() => X * X + Y * Y;

        public double Magnitude() => Sqrt(MagnitudeSquared());

        public double Angle() => Atan2(Y, X);

        public rVector Unit()
        {
            var mag = Magnitude();
            if (mag == 0)
                return (0, 1);
            return this / Magnitude();
        }

        public rVector Rotate(double angle) => FromAngle(Angle() + angle) * Magnitude();

        public override string ToString() => $"{{{X}, {Y}}}";

        public rVector Copy() => new rVector(X, Y);

        public (rVector pv, rVector nv, rVector ov) GetOuterFaceVector(rVector previous, rVector next)
        {
            rVector dv = (this - previous).Perpendicular();
            rVector dp = previous.Midpoint(this);
            if (((dp + dv) - next).Magnitude() < (dp - next).Magnitude())
                dv *= -1;


            rVector ev = (next - this).Perpendicular();
            rVector ep = this.Midpoint(next);
            if (((ep + ev) - previous).Magnitude() < (ep - previous).Magnitude())
                ev *= -1;

            return (ev, dv, (dv + ev).Unit());
        }
    }
}
