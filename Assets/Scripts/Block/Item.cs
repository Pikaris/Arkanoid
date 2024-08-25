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

    GameObject obj;

    bool popItem;

    public bool PopItem
    {
        set
        {
            popItem = value;

            if (popItem)
            {
                Debug.Log("Pop");
                obj = Instantiate(gameObj, disruptionTransform);
                rigid = obj.GetComponent<Rigidbody2D>();
            }
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

    }

    private void FixedUpdate()
    {
        if (popItem)        
        {
            rigid.MovePosition(transform.position + Time.fixedDeltaTime * Vector3.down * dropSpeed);
        }
    }
}
