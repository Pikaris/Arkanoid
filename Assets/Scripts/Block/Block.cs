using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Block : BlockManager
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(disruption)
        {
            Transform child = transform.GetChild(0);
            child.gameObject.SetActive(true);
        }
        //Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}
