using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Block : BlockBase
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (megaBall)
        {
            Factory.Instance.GetMegaBall(transform.position, 0);
        }
        else if (disruption)
        {
            Factory.Instance.GetDisruption(transform.position, 0);
        }
        else if (slow)
        {
            Factory.Instance.GetSlow(transform.position, 0);
        }
        Destroy(gameObject);                // 블록을 파괴
    }
}
