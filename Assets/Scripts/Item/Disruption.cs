using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disruption : Item
{
    //public GameObject disruptionObj;

    public float dropSpeed = 2.0f;

    private void FixedUpdate()
    {
        rigid.MovePosition(transform.position + Time.fixedDeltaTime * Vector3.down * dropSpeed);
    }
}
