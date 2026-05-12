using System;
using UnityEngine;

namespace Game
{
    public class JumpComponent : MonoBehaviour
    {
        [SerializeField] 
        private Vector2 _force = new (0.0f, 10.0f);
        [SerializeField]
        private Rigidbody2D _rigidbody;

        public void Jump()
        {
            _rigidbody.AddForce(_force, ForceMode2D.Impulse);
        }
    }
}