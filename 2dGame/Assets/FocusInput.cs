using UnityEngine;
using UnityEngine.InputSystem;

public class FocusInput : MonoBehaviour
{
    private bool _inFocus = true;
    private PlayerMovement _movement;

    private void Start()
    {
        _movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (_inFocus != Application.isFocused)
        {
            InputSystem.Update();
            _movement.CheckMoveInput();
            Debug.Log(GetComponent<PlayerInput>().actions.FindActionMap("Player")["Move"].IsPressed());
        }

        _inFocus = Application.isFocused;
    }
}
