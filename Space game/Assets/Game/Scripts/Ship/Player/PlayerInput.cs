using System;
using UnityEngine;

namespace Game
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Ship _ship;

        private void Update()
        {
            InputMove();
            InputAttack();
        }

        private void InputMove()
        {
            float dx = Input.GetAxisRaw("Horizontal");
            float dy = Input.GetAxisRaw("Vertical");
            _ship.Move(new Vector2(dx, dy));
        }

        private void InputAttack()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _ship.FireForward();
            }
        }
    }
}