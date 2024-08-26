using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject obj;

    protected Rigidbody2D rigid;

    Transform disruptionTransform;

    protected bool popItem = false;

    public bool PopItem
    {
        get { return popItem; }
        set
        {
            popItem = value;
        }
    }

    public Transform ItemTransform
    {
        get { return disruptionTransform; }
        set
        {
            disruptionTransform = value;
            //obj = Instantiate(obj, disruptionTransform);
            //rigid = obj.GetComponent<Rigidbody2D>();
        }
    }

    /// <summary>
    /// 관통 아이템 생성
    /// </summary>
    /// <param name="targetTrans"></param>
    public void PopDisruption(Transform targetTrans)
    {
        GameObject o = Instantiate(obj, targetTrans);
        rigid = o.GetComponent<Rigidbody2D>();
    }
}
