using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Block : BlockManager
{
    public GameObject Block_Orange;

    public float blockInterval_X = 0.4f;
    public float blockInterval_Y = 0.3f;


    private void Start()
    {
        
    }
    private void Awake()
    {
        Block_Orange = GetComponent<GameObject>(); 
        for (int i = 0; i < blockTransform.Length; i++)
        {
            GameObject obj = Instantiate(blockTransform[i].gameObject, Vector3.right * (blockInterval_X * i), Quaternion.identity);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
