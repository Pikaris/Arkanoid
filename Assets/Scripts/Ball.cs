using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballSpeed = 1.0f;

    public float LeftFarAngle = 70.0f;

    BoxCollider2D playerCollider;

    Vector3 direction;

    Vector3 playerPosition;

    Rigidbody2D rigidBody2D;


    private void Awake()
    {
        //player = GetComponent<player>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        direction = Vector3.down;
    }

    private void FixedUpdate()
    {
        //transform.Translate(Time.deltaTime * ballSpeed * direction);
        rigidBody2D.MovePosition(transform.position + Time.fixedDeltaTime * ballSpeed * direction);
        //Debug.Log(transform.position.x);
    }

    public void GetPlayerData(BoxCollider2D collider, Vector3 position)
    {
        playerCollider = collider;
        playerPosition = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float playerSizeX = playerCollider.size.x;
        float playerPosX = playerPosition.x;

        Ray2D ray = new Ray2D(transform.position, direction);

        Debug.DrawRay(playerPosition, direction, new Color(1,0,0));

        if (collision.gameObject.CompareTag("Border") || collision.gameObject.CompareTag("Block"))
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.position.x < (playerPosX - playerSizeX * 0.4f))
            {
                Debug.Log("HitLeftFar");
                direction = Quaternion.Euler(0.0f, 0.0f, 70.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosX - playerSizeX * 0.25f))
            {
                Debug.Log("HitLeft");
                direction = Quaternion.Euler(0.0f, 0.0f, 40.0f) * Vector3.up;
            }
            else if(transform.position.x < (playerPosX - playerSizeX * 0.1f))
            {
                Debug.Log("HitLeftMiddle");
                direction = Quaternion.Euler(0.0f, 0.0f, 20.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosX + playerSizeX * 0.1f))
            {
                Debug.Log("Middle");
                direction = Quaternion.Euler(0.0f, 0.0f, 0.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosX + playerSizeX * 0.2f))
            {
                Debug.Log("HitRightMiddle");
                direction = Quaternion.Euler(0.0f, 0.0f, -20.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosX + playerSizeX * 0.35f))
            {
                Debug.Log("HitRight");
                direction = Quaternion.Euler(0.0f, 0.0f, -40.0f) * Vector3.up;
            }
            else if (transform.position.x < (playerPosX + playerSizeX * 0.5f))
            {
                Debug.Log("HitRightFar");
                direction = Quaternion.Euler(0.0f, 0.0f, -70.0f) * Vector3.up;
            }
        }

        
    }


}
