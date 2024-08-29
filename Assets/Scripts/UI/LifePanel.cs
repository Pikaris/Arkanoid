using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePanel : MonoBehaviour
{
    public Color disableColor;

    Image[] lifeImage;


    private void Awake()
    {
        lifeImage = new Image[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            lifeImage[i] = child.GetComponent<Image>();
        }

        Player player = FindAnyObjectByType<Player>();
        player.onLifeChange += OnLifeChange;
        //for (int i = 0; i < lifeImage.Length; i++)
        //{
        //    lifeImage[i].gameObject.SetActive(false);
        //}
    }

    private void OnLifeChange(int life)
    {
        Debug.Log("OnLifeChange");
        //for(int i = 0;i < life; i++)
        //{
        //    lifeImage[i].color = Color.white;
        //}
        for(int i = life; i < lifeImage.Length; i++)
        {
            //lifeImage[i].color = Color.yellow;
            lifeImage[i].gameObject.SetActive(false);
        }
    }
}
