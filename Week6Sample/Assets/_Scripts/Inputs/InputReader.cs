using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : IInputReader
{
    readonly GameInputActions _input;
    
    public Vector2 Direction { get; private set; }

    public InputReader()
    {
        _input = new GameInputActions();
        
        _input.Player.Move.performed += HandleOnMovement;
        _input.Player.Move.canceled += HandleOnMovement;
        
        _input.Enable();
    }

    ~InputReader()
    {
        _input.Player.Move.performed -= HandleOnMovement;
        _input.Player.Move.canceled -= HandleOnMovement;

        _input.Disable();
    }
    
    void HandleOnMovement(InputAction.CallbackContext context)
    {
        Direction = context.ReadValue<Vector2>();
    }
}

public interface IInputReader
{
    public Vector2 Direction { get; }
}
