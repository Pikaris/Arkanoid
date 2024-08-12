using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Block : BlockManager
{
    bool blockFlag;
    public bool BlockFlag
    {
        get
        {
            return blockFlag;
        }
        set
        {
            blockFlag = value;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
