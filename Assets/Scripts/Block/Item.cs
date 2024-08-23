using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float dropSpeed = 2.0f;

    public GameObject gameObj;

    Rigidbody2D rigid;

    Transform disruptionTransform;

    bool popItem = false;

    public bool PopItem
    {
        get { return popItem; }
        set
        {
            popItem = value;
        }
    }

    public Transform ItemTransform
    {
        set
        {
            disruptionTransform = value;
        }
    }

    private void Awake()
    {
        //disruptionTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (popItem)
        {
            Debug.Log("Pop");
            GameObject obj = Instantiate(gameObj, disruptionTransform);
            rigid = obj.GetComponent<Rigidbody2D>();
        }
    }

    private void FixedUpdate()
    {
        if (popItem)
        {
            rigid.MovePosition(transform.position + Time.fixedDeltaTime * Vector3.down * dropSpeed);
        }
    }
}
