using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Block : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            //Item item = new Item();
            //item.ItemTransform = transform;     // 블럭의 트랜스폼을 아이템으로 넘기기
            //item.PopItem = true;                // 블럭에 볼이 부딪혔음을 알림

            //item.PopDisruption(transform);


            Destroy(gameObject);                // 이 블럭을 파괴
        }
    }
}
