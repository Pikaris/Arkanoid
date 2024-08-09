using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Transform[] blockTransform;

    private void Awake()
    {
        Transform blockRoot = transform.GetChild(0);
        blockTransform = new Transform[blockRoot.childCount];
        for (int i = 0; i < blockTransform.Length; i++)
        {
            blockTransform[i] = blockRoot.GetChild(i);
        }
    }
}
