using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 0.5f;

    float playerMove;

    BoxCollider2D playerCollider;

    Vector2 mousePosition;
    Vector3 inputDirection;

    PlayerInputAction InputAction;

    Ball ball;

    

    private void Awake()
    {
        InputAction = new PlayerInputAction();
        playerCollider = GetComponent<BoxCollider2D>();

        ball = FindFirstObjectByType<Ball>();
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
        transform.Translate(playerMove, 0, 0);
        ball.GetPlayerData(playerCollider, transform.position);
    }

    void Scroll(InputAction.CallbackContext context)
    {
        float scroll = context.ReadValue<float>();

        playerMove = (scroll / 120.0f) * moveSpeed;
    }

#if UNITY_EDITOR

    protected virtual void DrawGizmos()
    {
        Vector3 p0 = transform.position + (Vector3.left * -playerCollider.size.x) + (Vector3.up * playerCollider.size.y);
        Vector3 p1 = transform.position + (Vector3.left * playerCollider.size.x) + (Vector3.up * -playerCollider.size.y);

        Gizmos.DrawLine(p0, p1);
    }
    protected virtual void DrawGizmosPlayer()
    {
        Gizmos.color = Color.green;
        Vector3 p0 = transform.position + (Vector3.left * -playerCollider.size.x * 0.5f) + (Vector3.up * playerCollider.size.y * 0.5f);
        Vector3 p1 = transform.position + (Vector3.left * playerCollider.size.x * 0.5f) + (Vector3.up * -playerCollider.size.y * 0.5f);
        Vector3 p2 = transform.position + (Vector3.left * -playerCollider.size.x * 0.5f) + (Vector3.up * playerCollider.size.y * 0.5f);
        Vector3 p3 = transform.position + (Vector3.left * playerCollider.size.x * 0.5f) + (Vector3.up * -playerCollider.size.y * 0.5f);

        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p1, p3);
        Gizmos.DrawLine(p0, p2);
    }
#endif
}
