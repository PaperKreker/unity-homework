using UnityEngine;

namespace SampleGame.Common
{
    public struct SerializableVector3
    {
        public float X;
        public float Y;
        public float Z;

        public SerializableVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        public static implicit operator SerializableVector3(Vector3 v)
        {
            return new SerializableVector3(v.x, v.y, v.z);
        }

        public static implicit operator Vector3(SerializableVector3 s)
        {
            return new Vector3(s.X, s.Y, s.Z);
        }
    }
}