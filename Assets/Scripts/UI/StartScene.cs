using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    PlayerInputAction inputAction;

    private void Awake()
    {
        inputAction = new PlayerInputAction();
    }

    private void OnEnable()
    {
        inputAction.Click.Enable();
        inputAction.Click.Click.performed += OnClick;
        inputAction.Click.Click.canceled += OnClick;
    }

    private void OnDisable()
    {
        inputAction.Click.Click.canceled -= OnClick;
        inputAction.Click.Click.performed -= OnClick;
        inputAction.Click.Disable();
    }
    private void OnClick(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("Stage1");
    }
}
