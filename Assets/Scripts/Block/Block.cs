using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Block : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Item item = new Item();
        item.ItemTransform = transform;
        item.PopItem = true;
        Destroy(gameObject);
    }
}
