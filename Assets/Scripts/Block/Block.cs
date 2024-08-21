using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Block : MonoBehaviour
{
    enum Item
    {
        MegaBall,
        Disruption,
        Slow
    }

    Transform blockTransform;

    Transform itemTransform;

    bool megaBall = false;
    bool disruption = false;
    bool slow = false;

    public int RandomMin = 0;
    public int RandomMax = 10;

    private void Awake()
    {
        blockTransform = GetComponent<Transform>();

        itemTransform = transform.GetChild(0);

        Animator animator = GetComponent<Animator>();

        int itemNum = Random.Range(RandomMin, RandomMax);

        if (itemNum == (int)Item.MegaBall)
        {
            megaBall = true;
        }
        else if (itemNum == (int)Item.Disruption)
        {
            disruption = true;
        }
        else if(itemNum == (int)Item.Slow)
        { 
            slow = true; 
        }
    }

    public Transform GetBlockData()
    {
        return blockTransform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(disruption)
        {
            
        }

        Destroy(gameObject);
    }
}
