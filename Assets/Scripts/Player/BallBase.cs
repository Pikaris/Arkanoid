using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallBase : MonoBehaviour
{
    static public float megaBallElapsedTime = 0.0f;
    static public bool megaBallFlag = false;
    static public bool disruptionFlag = false;
    static public bool slowBallFlag = false;

    static public bool movingFlag = false;

    static public bool playerShootFlag = false;

    public BoxCollider2D playerCollider;
    public Vector3 playerPosition;

    protected Player player;

    protected float megaBallTime = 6.0f;

    static Ball[] balls;

    private void Awake()
    {
        balls = new Ball[transform.childCount];

        for (int i = 0; i < balls.Length; i++)
        {
            Transform child = transform.GetChild(i);
            balls[i] = child.GetComponent<Ball>();
        }
    }

    protected virtual void Start()
    {
        player = FindFirstObjectByType<Player>();
        playerCollider = player.SetPlayerCollider();
        playerPosition = player.SetPlayerPosition();
    }

    private void Update()
    {
        if (megaBallFlag)
        {
            megaBallElapsedTime += Time.deltaTime;

            if (megaBallElapsedTime > megaBallTime)
            {
                megaBallFlag = false;
                megaBallElapsedTime = 0;
            }
        }
    }

    public void DecreaseLife()
    {
        GameObject[] objs;

        objs = GameObject.FindGameObjectsWithTag("Ball");

        Debug.Log(objs.Length);
        if (objs.Length < 1)
        {
            player.Life--;
            Debug.Log($"Life:{player.Life}");
            balls[player.BallIndex].BallReset();
        }
    }

    public void GetDisruption()
    {
        if(player.BallIndex < 8)
        {
            player.BallIndex++;
            balls[player.BallIndex].transform.position = balls[player.BallIndex - 1].transform.position;
        }
        if(player.BallIndex > 7)
        {
            player.BallIndex = 0;
        }
        disruptionFlag = true;
        SetPlayerData();

        balls[player.BallIndex].gameObject.SetActive(true);
    }

    public void SetPlayerData()
    {
        playerCollider = player.SetPlayerCollider();
        playerPosition = player.SetPlayerPosition();
    }
}
