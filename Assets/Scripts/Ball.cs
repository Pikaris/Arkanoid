using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballSpeed = 1.0f;

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
        if (collision.gameObject.CompareTag("LeftMiddleCollider"))
        {
            direction = Quaternion.Euler(0.0f, 0.0f, 30.0f) * Vector2.up;
            Debug.Log("Hit");
        }
    }
}
