using UnityEngine;

namespace SampleGame.Common
{
    public struct SerializableQuaternion
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public SerializableQuaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        
        public static implicit operator SerializableQuaternion(Quaternion v)
        {
            return new SerializableQuaternion(v.x, v.y, v.z, v.w);
        }

        public static implicit operator Quaternion(SerializableQuaternion s)
        {
            return new Quaternion(s.X, s.Y, s.Z, s.W);
        }
    }
}