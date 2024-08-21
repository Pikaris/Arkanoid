using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Rigidbody2D rigid;

    Transform disruptionTransform;

    Transform blockTransform;

    bool popItem = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        disruptionTransform = transform.GetChild(0);
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(Vector2.down * Time.fixedDeltaTime);
    }
}
