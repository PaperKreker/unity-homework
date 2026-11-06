using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game
{
    public class InputComponent : MonoBehaviour
    {
        public float HorizontalAxis => Input.GetAxisRaw("Horizontal");
        public float VerticalAxis => Input.GetAxisRaw("Vertical");
        public Vector2 Axis => new (HorizontalAxis, VerticalAxis);
        public bool JumpInput => Input.GetKey(KeyCode.Space);
        public bool TossInput => Input.GetMouseButtonDown(0);
        public bool PushInput => Input.GetMouseButtonDown(1);
    }
}