using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Block : MonoBehaviour
{
    public GameObject Block_Orange;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(Block_Orange);
        }
    }

        private void Awake()
    {
        //Block_Orange = GetComponent<GameObject>();
        //Transform transform = GetComponent<Transform>();
    }


}
