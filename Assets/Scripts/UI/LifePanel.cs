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
        for(int i = 0; i < lifeImage.Length; i++)
        {
            Transform child = transform.GetChild(i);
            lifeImage[i] = child.GetComponent<Image>();
        }
    }

    private void OnInitialize()
    {
        Player player = GetComponent<Player>();
    }

    private void OnLifeChange(int life)
    {
        for(int i = 0;i < life; i++)
        {
            lifeImage[i].color = Color.white;
        }
        for(int i = life; i < lifeImage.Length; i++)
        {
            lifeImage[i].color = disableColor;
        }
    }
}
