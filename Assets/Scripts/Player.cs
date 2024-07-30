using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    float playerMove;

    Vector2 mousePosition;
    Vector3 inputDirection;

    PlayerInputAction InputAction;

    private void Start()
    {
    }
    private void Awake()
    {
        InputAction = new PlayerInputAction();
    }

    private void OnEnable()
    {
        InputAction.Player.Enable();
        InputAction.Player.Scroll.performed += Scroll;
        InputAction.Player.Scroll.canceled += Scroll;
    }

    private void OnDisable()
    {
        InputAction.Player.Scroll.canceled -= Scroll;
        InputAction.Player.Scroll.performed -= Scroll;
        InputAction.Player.Disable();
    }

    private void Update()
    {
        if (playerMove > 0)
            Debug.Log($"Up : {playerMove}");
        else if(playerMove < 0)
            Debug.Log($"Down : {playerMove}");
        transform.Translate(playerMove, 0, 0);
    }

    void Scroll(InputAction.CallbackContext context)
    {
        float scroll = context.ReadValue<float>();

        playerMove = (scroll / 120.0f) * moveSpeed;
    }
}
