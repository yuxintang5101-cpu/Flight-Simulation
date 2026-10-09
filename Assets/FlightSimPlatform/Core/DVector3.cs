using System;

namespace FlightSim.Platform.Core
{
    [Serializable]
    public readonly struct DVector3 : IEquatable<DVector3>
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public DVector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static DVector3 Zero => new DVector3(0.0, 0.0, 0.0);
        public static DVector3 UnitX => new DVector3(1.0, 0.0, 0.0);
        public static DVector3 UnitY => new DVector3(0.0, 1.0, 0.0);
        public static DVector3 UnitZ => new DVector3(0.0, 0.0, 1.0);

        public double LengthSquared => Dot(this, this);
        public double Length => Math.Sqrt(LengthSquared);
        public DVector3 Normalized => Length > 0.0 ? this / Length : Zero;

        public static double Dot(DVector3 left, DVector3 right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }

        public static DVector3 Cross(DVector3 left, DVector3 right)
        {
            return new DVector3(
                left.Y * right.Z - left.Z * right.Y,
                left.Z * right.X - left.X * right.Z,
                left.X * right.Y - left.Y * right.X);
        }

        public bool Equals(DVector3 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            return obj is DVector3 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = X.GetHashCode();
                hash = (hash * 397) ^ Y.GetHashCode();
                return (hash * 397) ^ Z.GetHashCode();
            }
        }

        public static DVector3 operator +(DVector3 left, DVector3 right)
        {
            return new DVector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static DVector3 operator -(DVector3 left, DVector3 right)
        {
            return new DVector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static DVector3 operator -(DVector3 value)
        {
            return new DVector3(-value.X, -value.Y, -value.Z);
        }

        public static DVector3 operator *(DVector3 value, double scalar)
        {
            return new DVector3(value.X * scalar, value.Y * scalar, value.Z * scalar);
        }

        public static DVector3 operator *(double scalar, DVector3 value)
        {
            return value * scalar;
        }

        public static DVector3 operator /(DVector3 value, double scalar)
        {
            return new DVector3(value.X / scalar, value.Y / scalar, value.Z / scalar);
        }

        public static bool operator ==(DVector3 left, DVector3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DVector3 left, DVector3 right)
        {
            return !left.Equals(right);
        }
    }
}
