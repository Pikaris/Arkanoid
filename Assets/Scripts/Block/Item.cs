using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Block
{
    public float dropSpeed = 2.0f;

    Rigidbody2D rigid;

    Transform disruptionTransform;

    Transform blockTransform;

    bool popItem = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            rigid.MovePosition(transform.position + Time.fixedDeltaTime * Vector3.down * dropSpeed);
        }
    }
}
