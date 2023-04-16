using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class showText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject text;
    public Image img;
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.SetActive(true);
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0.7f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        text.SetActive(false);
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
    }
}
