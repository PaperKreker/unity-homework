using System;
using UnityEngine;

namespace Game
{
    public class CharacterInput : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private InputComponent _input;

        private void FixedUpdate()
        {
            _character.Move(_input.HorizontalAxis);
        }

        private void Update()
        {
            if (_input.JumpInput)
            {
                _character.Jump();
            }

            if (_input.PushInput)
            {
                _character.Push();
            }

            if (_input.TossInput)
            {
                _character.Toss();
            }
        }
    }
}
