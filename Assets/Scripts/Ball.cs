using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Ball : MonoBehaviour
{
    public float ballSpeed = 3.0f;

    BoxCollider2D playerCollider;

    Vector3 direction;

    Vector3 playerPosition;

    Rigidbody2D rigidBody2D;

    List<GameObject> collisionList = new List<GameObject>();

    float elapsedTime = 0.0f;

    bool playerShoot = false;

    bool moving = false;

    private void Awake()
    {
        //player = GetComponent<player>();
        rigidBody2D = GetComponent<Rigidbody2D>();

        direction = Vector3.zero;
    }

    private void FixedUpdate()
    {
        OnShoot();
        if (moving)
        {
            rigidBody2D.MovePosition(transform.position + Time.fixedDeltaTime * ballSpeed * direction);

            elapsedTime += Time.fixedDeltaTime;
        }
        else
        {
            rigidBody2D.MovePosition(new Vector2(playerPosition.x + Time.fixedDeltaTime * ballSpeed * direction.x, 
                playerPosition.y + playerCollider.size.y));
            elapsedTime += Time.fixedDeltaTime;
        }
    }

    public void GetPlayerData(BoxCollider2D collider, Vector3 position)
    {
        playerCollider = collider;
        playerPosition = position;
    }

    public void GetFireData(bool shoot)
    {
        playerShoot = shoot;
    }

    void OnShoot()
    {
        if(playerShoot)
        {
            direction = Quaternion.Euler(0.0f, 0.0f, 30.0f) * Vector3.up;
            moving = true;
            playerShoot = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float playerSizeX = playerCollider.size.x;
        float playerPosX = playerPosition.x;

        if (collision.gameObject.CompareTag("Border"))
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
        }

        if (collision.gameObject.CompareTag("Block") && (elapsedTime > 0.001f))
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
            elapsedTime = 0.0f;
            ballSpeed += 0.1f;
        }

        //Debug.Log(collisionList.Count);

        if (collision.gameObject.CompareTag("Player"))
        {
            collisionList.Remove(collision.gameObject);
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
