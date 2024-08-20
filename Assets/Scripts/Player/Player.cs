using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    BoxCollider2D playerCollider;
    Rigidbody2D rigid;

    Vector2 mousePosition;
    Vector3 inputDirection;
    Vector3 direction;
    Vector3 worldPosition;

    PlayerInputAction InputAction;

    Ball ball;

    public float moveSpeed = 0.5f;

    float playerMove;

    /// <summary>
    /// 현재 라이프
    /// </summary>
    public int life = 3;

    /// <summary>
    /// 시작 라이프
    /// </summary>
    const int startLife = 3;

    public Action<int> onLifeChange;

    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            if (life != value)
            {
                life = value;

                life = Mathf.Clamp(life, 0, startLife);
                onLifeChange?.Invoke(life);
            }
        }
    }


    bool shoot = false;

    private void Awake()
    {
        InputAction = new PlayerInputAction();
        playerCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();

        direction = Vector3.zero;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        ball = FindFirstObjectByType<Ball>();
        ball.GetPlayerData(playerCollider, transform.position);
    }


    private void OnEnable()
    {
        InputAction.Player.Enable();
        InputAction.Player.Fire.performed += OnFire;
        InputAction.Player.Fire.canceled += OnFire;
        InputAction.Player.Move.performed += OnMousePosition;
    }

    private void OnDisable()
    {
        InputAction.Player.Move.performed -= OnMousePosition;
        InputAction.Player.Fire.canceled -= OnFire;
        InputAction.Player.Fire.performed -= OnFire;
        InputAction.Player.Disable();
    }
    private void FixedUpdate()
    {
        rigid.MovePosition(worldPosition + Time.fixedDeltaTime * moveSpeed * direction);
    }

    private void Update()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        ball.GetPlayerData(playerCollider, transform.position);
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    void OnFire(InputAction.CallbackContext context)
    {
        shoot = true;
        if(shoot)
        {
            ball.GetFireData(shoot);
            shoot = false;
        }
    }

    //public Vector3 GetPlayerPosition()
    //{
    //    return playerTransform.position;
    //}
    //public BoxCollider2D GetPlayerCollider()
    //{
    //    return playerCollider;
    //}


#if UNITY_EDITOR

    //protected virtual void DrawGizmos()
    //{
    //    Vector3 p0 = transform.position + (Vector3.left * -playerCollider.size.x) + (Vector3.up * playerCollider.size.y);
    //    Vector3 p1 = transform.position + (Vector3.left * playerCollider.size.x) + (Vector3.up * -playerCollider.size.y);

    //    Gizmos.DrawLine(p0, p1);
    //}
    //protected virtual void DrawGizmosPlayer()
    //{
    //    Gizmos.color = Color.green;
    //    Vector3 p0 = transform.position + (Vector3.left * -playerCollider.size.x * 0.5f) + (Vector3.up * playerCollider.size.y * 0.5f);
    //    Vector3 p1 = transform.position + (Vector3.left * playerCollider.size.x * 0.5f) + (Vector3.up * -playerCollider.size.y * 0.5f);
    //    Vector3 p2 = transform.position + (Vector3.left * -playerCollider.size.x * 0.5f) + (Vector3.up * playerCollider.size.y * 0.5f);
    //    Vector3 p3 = transform.position + (Vector3.left * playerCollider.size.x * 0.5f) + (Vector3.up * -playerCollider.size.y * 0.5f);

    //    Gizmos.DrawLine(p0, p1);
    //    Gizmos.DrawLine(p1, p2);
    //    Gizmos.DrawLine(p1, p3);
    //    Gizmos.DrawLine(p0, p2);
    //}
#endif
}
