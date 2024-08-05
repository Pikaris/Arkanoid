using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballSpeed = 1.0f;

    public float LeftFarAngle = 70.0f;

    Vector3 direction;

    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
        direction = Vector3.down;
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * ballSpeed * direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player.playerCollider.size.x * 0.3f > transform.position.x)
        {
            direction = Quaternion.Euler(0.0f, 0.0f, 70.0f) * Vector3.up;
        }
        if (collision.gameObject.CompareTag("Border"))
        {
            direction = Vector3.Reflect(direction, collision.contacts[0].normal);
        }
    }


}
