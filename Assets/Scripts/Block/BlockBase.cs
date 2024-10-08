using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockBase : MonoBehaviour
{
    enum Item
    {
        MegaBall,
        Disruption,
        Slow
    }

    int RandomMin = 0;
    int RandomMax = 15;

    Transform[] block;

    protected bool megaBall = false;
    protected bool disruption = false;
    protected bool slow = false;

    private void Awake()
    {
        block = new Transform[transform.childCount];
        for (int i = 0; i < block.Length; i++)
        {
            Transform child = transform.GetChild(i);
            block[i] = child.GetComponent<Transform>();
        }

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
        else if (itemNum == (int)Item.Slow)
        {
            slow = true;
        }
    }
}
