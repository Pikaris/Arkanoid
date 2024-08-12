using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    Block block;

    private void Awake()
    {
        int count = transform.childCount;
        Debug.Log(count);
        for (int i = 0; i < count; i += 2)
        {
            block.BlockFlag = true;

            for (int j = 1; j < count; j += 2)
            {
                block.BlockFlag = false;
            }
        }

        Debug.Log(count);
        //Debug.Log(obj);
    }
}
