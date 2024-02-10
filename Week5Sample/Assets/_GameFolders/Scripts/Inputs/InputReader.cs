using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SampleScripts
{
    public class InputReader : IInputReader
    {
        readonly GameInputActions _input;
        
        public Vector2 Direction { get; private set; }

        public InputReader()
        {
            _input = new GameInputActions();

            _input.Player.Move.started += HandleOnMove;
            _input.Player.Move.performed += HandleOnMove;
            _input.Player.Move.canceled += HandleOnMove;
            
            _input.Enable();
        }

        ~InputReader()
        {
            _input.Player.Move.started -= HandleOnMove;
            _input.Player.Move.performed -= HandleOnMove;
            _input.Player.Move.canceled -= HandleOnMove;
            
            _input.Disable();
        }

        void HandleOnMove(InputAction.CallbackContext context)
        {
            Direction = context.ReadValue<Vector2>();
        }
    }
}