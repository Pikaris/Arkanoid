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
            //item.ItemTransform = transform;     // ���� Ʈ�������� ���������� �ѱ��
            //item.PopItem = true;                // ���� ���� �ε������� �˸�

            //item.PopDisruption(transform);


            Destroy(gameObject);                // �� ���� �ı�
        }
    }
}
