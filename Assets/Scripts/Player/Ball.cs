using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Ball : BallBase
{
    public float ballSpeed = 3.0f;

    Rigidbody2D rigidBody2D;
    GameObject obj;
    
    LifePanel lifePanel;
    Animator animator;

    Vector3 direction;

    Vector3 positionReset;

    static Vector3 tempDirection;

    float elapsedTime = 0.0f;

    readonly int On_Hash = Animator.StringToHash("MegaBallOnOff");

    public bool GetFireData
    {
        get
        {
            return playerShootFlag;
        }
        set
        {
            playerShootFlag = value;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        positionReset = transform.position;

        if (disruptionFlag)
        {
            float randomDirection = Random.Range(0.0f, 2.0f);
            direction = new Vector3(tempDirection.x * randomDirection, tempDirection.y, tempDirection.z).normalized;
            movingFlag = true;
        }
        if (megaBallFlag)
        {
            animator.SetBool(On_Hash, true);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        OnShoot();
        if (movingFlag)
        {
            rigidBody2D.MovePosition(transform.position + Time.fixedDeltaTime * ballSpeed * direction);
            tempDirection = direction;
            elapsedTime += Time.fixedDeltaTime;
        }
        else
        {
            direction = Vector3.zero;
            SetPlayerData();
            rigidBody2D.MovePosition(new Vector2(playerPosition.x + Time.fixedDeltaTime * ballSpeed * direction.x, playerCollider.size.y + playerPosition.y));
            elapsedTime += Time.fixedDeltaTime;
        }
    }

    private void Update()
    {
        if (megaBallFlag)
        {
            animator.SetBool(On_Hash, true);
        }
        else
        {
            animator.SetBool(On_Hash, false);
        }
        if(slowBallFlag)
        {
            ballSpeed = 3.0f;
            slowBallFlag = false;
        }
    }




    
    /// <summary>
    /// 볼을 발사할 때 처리하는 함수
    /// </summary>
    private void OnShoot()
    {
        if(playerShootFlag && !movingFlag)
        {
            direction = Quaternion.Euler(0.0f, 0.0f, 30.0f) * Vector3.up;
            movingFlag = true;
            playerShootFlag = false;
        }
    }

    /// <summary>
    /// 볼의 상태를 리셋하는 함수
    /// </summary>
    public void BallReset()
    {
        gameObject.SetActive(true);
        playerShootFlag = false;
        movingFlag = false;
        slowBallFlag = false;
        megaBallFlag = false;
        disruptionFlag = false;

        //playerCollider = player.SetPlayerCollider();
        //playerPosition = player.SetPlayerPosition(); 
        transform.position = positionReset;
    }

    /// <summary>
    /// 메가볼 아이템을 먹었을 때 처리하는 함수
    /// </summary>
    public void GetMegaBallFlag()
    {
        megaBallElapsedTime = 0;
        megaBallFlag = true;
        animator.SetBool(On_Hash, true);
    }

    /// <summary>
    /// 슬로우 아이템을 먹었을 때 처리하는 함수
    /// </summary>
    public void GetSlow()
    {
        slowBallFlag = true;
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetPlayerData();

        if (collision.gameObject.CompareTag("KillZone"))
        {
            gameObject.SetActive(false);
            DecreaseLife();
        }

        if (collision.gameObject.CompareTag("Border"))
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
        }

        if (collision.gameObject.CompareTag("Block") && (elapsedTime > 0.001f))
        {
            Block block = FindAnyObjectByType<Block>();
            Scene scene = SceneManager.GetActiveScene();

            int currentScene = scene.buildIndex;

            int nextScene = currentScene + 1;
            if (block == null)
            {
                SceneManager.LoadScene(nextScene);
            }
            if (!megaBallFlag)
            {
                direction = Vector2.Reflect(direction, collision.contacts[0].normal);
                elapsedTime = 0.0f;
                ballSpeed += 0.1f;
            }
        }


        if (collision.gameObject.CompareTag("Player"))
        {

            if (transform.position.x < (playerPosition.x - playerCollider.size.x * 0.4f))
            {
                Debug.Log("LeftLeftLeftLeft");
                direction = Quaternion.Euler(0.0f, 0.0f, 70.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosition.x - playerCollider.size.x * 0.25f))
            {
                Debug.Log("LeftLeftLeft");
                direction = Quaternion.Euler(0.0f, 0.0f, 50.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosition.x - playerCollider.size.x * 0.1f))
            {
                Debug.Log("LeftLeft");
                direction = Quaternion.Euler(0.0f, 0.0f, 30.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosition.x))
            {
                Debug.Log("Left");
                direction = Quaternion.Euler(0.0f, 0.0f, 20.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosition.x + playerCollider.size.x * 0.1f))
            {
                Debug.Log("Right");
                direction = Quaternion.Euler(0.0f, 0.0f, -20.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosition.x + playerCollider.size.x * 0.2f))
            {
                Debug.Log("RightRight");
                direction = Quaternion.Euler(0.0f, 0.0f, -30.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosition.x + playerCollider.size.x * 0.35f))
            {
                Debug.Log("RightRightRight");
                direction = Quaternion.Euler(0.0f, 0.0f, -50.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosition.x + playerCollider.size.x * 0.5f))
            {
                Debug.Log("RightRightRightRight");
                direction = Quaternion.Euler(0.0f, 0.0f, -70.0f) * Vector3.up;
            }
        }

    }
}
