using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballSpeed = 1.0f;

    public float LeftFarAngle = 70.0f;
    public float LeftAngle = 50.0f;
    public float LeftMiddleAngle = 30.0f;
    public float RightFarAngle = -70.0f;
    public float RightAngle = -50.0f;
    public float RightMiddleAngle = -30.0f;
    public float MiddleAngle = 0.0f;

    Vector3 direction;

    Transform ballTransform;

    private void Awake()
    {
        ballTransform = GetComponent<Transform>();
        direction = Vector3.down;
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * ballSpeed * direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LeftFarCollider"))
        {
            direction = Quaternion.Euler(0.0f, 0.0f, LeftFarAngle) * Vector2.up;
            Debug.Log("Hit");
        }
        else if (collision.gameObject.CompareTag("LeftCollider"))
        {
            direction = Quaternion.Euler(0.0f, 0.0f, LeftAngle) * Vector2.up;
            Debug.Log("Hit");
        }
        else if (collision.gameObject.CompareTag("LeftMiddleCollider"))
        {
            direction = Quaternion.Euler(0.0f, 0.0f, LeftMiddleAngle) * Vector2.up;
            Debug.Log("Hit");
        }
        else if (collision.gameObject.CompareTag("RightMiddleCollider"))
        {
            direction = Quaternion.Euler(0.0f, 0.0f, RightMiddleAngle) * Vector2.up;
            Debug.Log("Hit");
        }
        else if (collision.gameObject.CompareTag("RightCollider"))
        {
            direction = Quaternion.Euler(0.0f, 0.0f, RightAngle) * Vector2.up;
            Debug.Log("Hit");
        }
        else if (collision.gameObject.CompareTag("RightFarCollider"))
        {
            direction = Quaternion.Euler(0.0f, 0.0f, RightFarAngle) * Vector2.up;
            Debug.Log("Hit");
        }
        else if (collision.gameObject.CompareTag("MiddleCollider"))
        {
            direction = Quaternion.Euler(0.0f, 0.0f, MiddleAngle) * Vector2.up;
            Debug.Log("Hit");
        }

        if (collision.gameObject.CompareTag("Border"))
        {
            direction = Vector3.Reflect(direction, collision.contacts[0].normal);
        }
    }

}
