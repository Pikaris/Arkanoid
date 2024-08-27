using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Block : BlockManager
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(megaBall)
        {
            Factory.Instance.GetMegaBall(transform.position, 0);
        }
        if(disruption)
        {
            Factory.Instance.GetDisruption(transform.position, 0);
        }
        Destroy(gameObject);                // 이 블럭을 파괴
    }
}
