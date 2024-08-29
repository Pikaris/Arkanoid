using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Action<int> onLifeChange;

    public float moveSpeed = 0.5f;

    BoxCollider2D playerCollider;
    Rigidbody2D rigid;

    Vector2 mousePosition;
    Vector3 inputDirection;
    Vector3 direction;
    Vector3 worldPosition;

    PlayerInputAction InputAction;

    GameObject obj;

    BallBase ballBase;

    Ball[] balls_P;

    float playerMove;
    bool shoot = false;

    /// <summary>
    /// 현재 라이프
    /// </summary>
    public int life = 0;

    /// <summary>
    /// 시작 라이프
    /// </summary>
    const int startLife = 3;

    static public int ballIndex = 0;

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

    public int BallIndex
    {
        get => ballIndex;
        set
        {
            if (ballIndex != value)
            {
                ballIndex = value;
            }
        }
    }

    private void Awake()
    {
        ballBase = FindFirstObjectByType<BallBase>();
        balls_P = new Ball[ballBase.transform.childCount];
        InputAction = new PlayerInputAction();
        playerCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();

        direction = Vector3.zero;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        for(int i = 0; i < balls_P.Length; i++)
        {
            Transform child = ballBase.transform.GetChild(i);
            balls_P[i] = child.GetComponent<Ball>();
        }
        balls_P[ballIndex].gameObject.SetActive(true);
        //ball.GetPlayerData(playerCollider, transform.position);
    }

    private void Start()
    {
        Life = startLife;
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
        //ball.GetPlayerData(playerCollider, transform.position);
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        shoot = true;
        if(shoot)
        {
            balls_P[ballIndex].GetFireData = shoot;
            shoot = false;
        }
    }

    public BoxCollider2D SetPlayerCollider()
    {
        return playerCollider;
    }

    public Vector3 SetPlayerPosition()
    {
        return transform.position;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("MegaBall"))
        {
            balls_P[ballIndex].GetMegaBallFlag();
        }
        if(collision.gameObject.CompareTag("Disruption"))
        {
            balls_P[ballIndex].GetDisruption();
        }
        if(collision.gameObject.CompareTag("Slow"))
        {
            balls_P[ballIndex].GetSlow();
        }
    }
}
