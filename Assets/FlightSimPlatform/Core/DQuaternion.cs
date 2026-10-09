using System;

namespace FlightSim.Platform.Core
{
    [Serializable]
    public readonly struct DQuaternion : IEquatable<DQuaternion>
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;
        public readonly double W;

        public DQuaternion(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static DQuaternion Identity => new DQuaternion(0.0, 0.0, 0.0, 1.0);

        public double LengthSquared => X * X + Y * Y + Z * Z + W * W;
        public DQuaternion Conjugate => new DQuaternion(-X, -Y, -Z, W);
        public DQuaternion Inverse => LengthSquared > 0.0
            ? new DQuaternion(-X / LengthSquared, -Y / LengthSquared, -Z / LengthSquared, W / LengthSquared)
            : Identity;
        public DQuaternion Normalized
        {
            get
            {
                double length = Math.Sqrt(LengthSquared);
                return length > 0.0 ? new DQuaternion(X / length, Y / length, Z / length, W / length) : Identity;
            }
        }

        public static DQuaternion FromAxisAngle(DVector3 axis, double angleRad)
        {
            DVector3 normalizedAxis = axis.Normalized;
            if (normalizedAxis == DVector3.Zero)
            {
                return Identity;
            }

            double halfAngle = angleRad * 0.5;
            double scale = Math.Sin(halfAngle);
            return new DQuaternion(
                normalizedAxis.X * scale,
                normalizedAxis.Y * scale,
                normalizedAxis.Z * scale,
                Math.Cos(halfAngle));
        }

        public DVector3 Rotate(DVector3 vector)
        {
            DQuaternion rotation = Normalized;
            DQuaternion vectorQuaternion = new DQuaternion(vector.X, vector.Y, vector.Z, 0.0);
            DQuaternion conjugate = new DQuaternion(-rotation.X, -rotation.Y, -rotation.Z, rotation.W);
            DQuaternion result = rotation * vectorQuaternion * conjugate;
            return new DVector3(result.X, result.Y, result.Z);
        }

        public DVector3 RotateInverse(DVector3 vector)
        {
            return Inverse.Rotate(vector);
        }

        public DQuaternion IntegrateBodyAngularVelocity(DVector3 bodyAngularVelocityRadps, double deltaTimeS)
        {
            double angularSpeedRadps = bodyAngularVelocityRadps.Length;
            if (angularSpeedRadps == 0.0 || deltaTimeS == 0.0)
            {
                return Normalized;
            }

            DQuaternion delta = FromAxisAngle(bodyAngularVelocityRadps / angularSpeedRadps, angularSpeedRadps * deltaTimeS);
            return (Normalized * delta).Normalized;
        }

        public bool Equals(DQuaternion other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);
        }

        public override bool Equals(object obj)
        {
            return obj is DQuaternion other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = X.GetHashCode();
                hash = (hash * 397) ^ Y.GetHashCode();
                hash = (hash * 397) ^ Z.GetHashCode();
                return (hash * 397) ^ W.GetHashCode();
            }
        }

        public static DQuaternion operator *(DQuaternion left, DQuaternion right)
        {
            return new DQuaternion(
                left.W * right.X + left.X * right.W + left.Y * right.Z - left.Z * right.Y,
                left.W * right.Y - left.X * right.Z + left.Y * right.W + left.Z * right.X,
                left.W * right.Z + left.X * right.Y - left.Y * right.X + left.Z * right.W,
                left.W * right.W - left.X * right.X - left.Y * right.Y - left.Z * right.Z);
        }

        public static bool operator ==(DQuaternion left, DQuaternion right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DQuaternion left, DQuaternion right)
        {
            return !left.Equals(right);
        }
    }
}
