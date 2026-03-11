using System;
using UnityEngine;

public class PlayerInput
{
    public event Action OnRight;
    public event Action OnDown;
    public event Action OnLeft;
    public event Action OnUp;

    public void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnRight?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnDown?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnLeft?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnUp?.Invoke();
        }
    }
}
