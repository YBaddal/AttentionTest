using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Level27ImageController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color hoverColor;
    private Color originalColor;
    private Image image;
    public bool isRed = false;

    void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isRed)
        {
            image.color = hoverColor;
            transform.GetComponent<Level27ImageController>().enabled = false;
            Control();
        }
        else
        {
            Control();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void Control()
    {
        Level27Create.Instance.Control(isRed);
    }
}