using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.TextCore.Text;

public class Ball : MonoBehaviour
{
    public float ballSpeed = 3.0f;

    BoxCollider2D playerCollider;
    Vector3 playerPosition;

    Vector3 direction;

    Vector3 positionReset;

    Rigidbody2D rigidBody2D;

    //List<GameObject> collisionList = new List<GameObject>();

    Player player;
    //LifePanel lifepanel;

    MegaBall disruption;

    Ball ball;

    float elapsedTime = 0.0f;

    bool playerShootFlag = false;

    bool movingFlag = false;

    bool megaBallFlag = false;
    bool disruptionFlag = false;



    private void Awake()
    {
        //lifepanel = new LifePanel();
        rigidBody2D = GetComponent<Rigidbody2D>();

        positionReset = transform.position;

        direction = Vector3.zero;
    }

    private void FixedUpdate()
    {
        OnShoot();
        if (movingFlag)
        {
            rigidBody2D.MovePosition(transform.position + Time.fixedDeltaTime * ballSpeed * direction);

            elapsedTime += Time.fixedDeltaTime;
        }
        else if(disruptionFlag)
        {
            rigidBody2D.MovePosition(new Vector2(playerPosition.x + Time.fixedDeltaTime * ballSpeed * direction.x,
                playerPosition.y + playerCollider.size.y));
            elapsedTime += Time.fixedDeltaTime;
        }
    }

    private void Update()
    {
        if (disruptionFlag)
        {
            DisruptionBall();
            disruptionFlag = false;
        }
    }

    /// <summary>
    /// 플레이어 데이터를 가져오기 위한 함수
    /// </summary>
    /// <param name="collider">플레이어의 콜라이더</param>
    /// <param name="position">플레이어의 위치</param>
    public void GetPlayerData(BoxCollider2D collider, Vector3 position)
    {
        playerCollider = collider;
        playerPosition = position;
    }

    /// <summary>
    /// 시작시에 플레이어가 볼을 발사했는지 확인하기 위한 함수
    /// </summary>
    /// <param name="shoot">볼을 발사시에 true</param>
    public void GetFireData(bool shoot)
    {
        playerShootFlag = shoot;
    }

    void OnShoot()
    {
        if(playerShootFlag && !movingFlag)
        {
            direction = Quaternion.Euler(0.0f, 0.0f, 30.0f) * Vector3.up;
            movingFlag = true;
            playerShootFlag = false;
        }
    }

    private void BallReset()
    {
        playerShootFlag = false;
        movingFlag = false;
        transform.position = positionReset;
    }
    public void GetMegaBallFlag()
    {
        megaBallFlag = true;
    }
    public void GetDisruptionFlag()
    {
        disruptionFlag = true;
    }
    public void DisruptionBall()
    {
        //ball = GetComponent<Ball>();
        Factory.Instance.GetBall(transform.position, direction.x * 0.8f, direction.y * 0.8f);
        Factory.Instance.GetBall(transform.position, direction.x * 1.2f, direction.y * 1.2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float playerSizeX = playerCollider.size.x;
        float playerPosX = playerPosition.x;

        if(collision.gameObject.CompareTag("KillZone"))
        {
            player.Life -= 1;
            BallReset();
            //lifepanel.OnLifeChange(player.life);
        }

        if (collision.gameObject.CompareTag("Border"))
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
        }

        if (collision.gameObject.CompareTag("Block") && (elapsedTime > 0.001f))
        {
            Debug.Log(megaBallFlag);
            if (!megaBallFlag)
            {
                direction = Vector2.Reflect(direction, collision.contacts[0].normal);
                elapsedTime = 0.0f;
                ballSpeed += 0.1f;
            }
        }

        //Debug.Log(collisionList.Count);

        if (collision.gameObject.CompareTag("Player"))
        {
            //collisionList.Remove(collision.gameObject);
            if (transform.position.x < (playerPosX - playerSizeX * 0.4f))
            {
                Debug.Log("LeftLeftLeftLeft");
                direction = Quaternion.Euler(0.0f, 0.0f, 70.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosX - playerSizeX * 0.25f))
            {
                Debug.Log("LeftLeftLeft");
                direction = Quaternion.Euler(0.0f, 0.0f, 50.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosX - playerSizeX * 0.1f))
            {
                Debug.Log("LeftLeft");
                direction = Quaternion.Euler(0.0f, 0.0f, 30.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosX))
            {
                Debug.Log("Left");
                direction = Quaternion.Euler(0.0f, 0.0f, 20.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosX + playerSizeX * 0.1f))
            {
                Debug.Log("Right");
                direction = Quaternion.Euler(0.0f, 0.0f, -20.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosX + playerSizeX * 0.2f))
            {
                Debug.Log("RightRight");
                direction = Quaternion.Euler(0.0f, 0.0f, -30.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosX + playerSizeX * 0.35f))
            {
                Debug.Log("RightRightRight");
                direction = Quaternion.Euler(0.0f, 0.0f, -50.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosX + playerSizeX * 0.5f))
            {
                Debug.Log("RightRightRightRight");
                direction = Quaternion.Euler(0.0f, 0.0f, -70.0f) * Vector3.up;
            }
        }
    }
}
