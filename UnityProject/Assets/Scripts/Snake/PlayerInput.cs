using Modules;
using System;
using UnityEngine;

public class PlayerInput
{
    public event Action<SnakeDirection> OnRotate;

    public void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnRotate?.Invoke(SnakeDirection.RIGHT);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnRotate?.Invoke(SnakeDirection.DOWN);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnRotate?.Invoke(SnakeDirection.LEFT);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnRotate?.Invoke(SnakeDirection.UP);
        }
    }
}
