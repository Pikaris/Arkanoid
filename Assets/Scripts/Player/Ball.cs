using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.TextCore.Text;

public class Ball : BallBase
{
    public float ballSpeed = 3.0f;

    BoxCollider2D playerCollider;
    Vector3 playerPosition;

    static Vector3 tempDirection;

    Vector3 direction;

    Vector3 positionReset;

    Rigidbody2D rigidBody2D;

    //List<GameObject> collisionList = new List<GameObject>();

    Player player;
    //LifePanel lifepanel;

    MegaBall disruption;

    Ball ball;

    float elapsedTime = 0.0f;

    float playerSizeX;
    float playerPosX;

    /// <summary>
    /// ���۽� �÷��̾ ���� ������ Ȯ���ϴ� �÷���(true�� ����, false�� ���� �� ����)
    /// </summary>
    bool playerShootFlag = false;

    bool movingFlag = false;

    /// <summary>
    /// ���� �ް��� �������� Ȯ���ϴ� �÷���(true�� �ް��� ����, false�� �Ϲݺ� ����)
    /// </summary>
    bool megaBallFlag = false;

    static bool disruptionFlag = false;



    private void Awake()
    {
        //lifepanel = new LifePanel();
        rigidBody2D = GetComponent<Rigidbody2D>();
        positionReset = transform.position;

        if (disruptionFlag)
        {
            direction = new Vector3(tempDirection.x * 0.8f, tempDirection.y, tempDirection.z);
            movingFlag = true;
            //playerSizeX = player.Collider.size.x;
            //playerPosX = player.Position.x;
        }
    }

    private void Start()
    {
        player = GetComponent<Player>();
        playerCollider = player.SetPlayerCollider();
        playerPosition = player.SetPlayerPosition();
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
        else// if(disruptionFlag)
        {
            direction = Vector3.zero;
            rigidBody2D.MovePosition(new Vector2(playerPosition.x + Time.fixedDeltaTime * ballSpeed * direction.x, playerPosition.y + playerPosition.y));
            elapsedTime += Time.fixedDeltaTime;
        }
    }

    private void Update()
    {
    }

    /// <summary>
    /// �÷��̾� �����͸� �������� ���� �Լ�
    /// </summary>
    /// <param name="collider">�÷��̾��� �ݶ��̴�</param>
    /// <param name="position">�÷��̾��� ��ġ</param>
    //public void GetPlayerData(BoxCollider2D collider, Vector3 position)
    //{
    //    playerCollider = collider;
    //    playerPosition = position;
    //}

    /// <summary>
    /// ���۽ÿ� �÷��̾ ���� �߻��ߴ��� Ȯ���ϱ� ���� �Լ�
    /// </summary>
    /// <param name="shoot">���� �߻�ÿ� true</param>
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

    /// <summary>
    /// ���� ���¸� �����ϴ� �Լ�
    /// </summary>
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
    public void Disruption()
    {
        disruptionFlag = true;
        Factory.Instance.GetBall(transform.position, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

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
            playerSizeX = playerCollider.size.x;
            playerPosX = playerPosition.x;
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
