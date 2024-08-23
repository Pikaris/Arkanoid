using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Block : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Item item = gameObject.AddComponent<Item>();
        item.PopItem = true;
        item.ItemTransform = transform;
        Destroy(gameObject);
    }
}
