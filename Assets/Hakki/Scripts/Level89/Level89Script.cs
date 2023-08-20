using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Level89Script : MonoBehaviour
{
    [SerializeField] private RectTransform left;

    [SerializeField] private RectTransform right;
    private Vector2 originalSize;
    public float scaleFactor = 1.01f;
    [SerializeField] TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        Create();
        originalSize = left.sizeDelta;
    }

    private void Create()
    {
        isClick = true;
        left.sizeDelta = Vector2.one * 100;
        right.sizeDelta = Vector2.one * Random.Range(150, 450);
    }

    bool isResizing;
    bool isClick = true;
    bool isGrowing;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isClick)
        {
            isClick = false;

            isGrowing = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isGrowing = false;
            Control();
        }

        if (isGrowing)
        {
            GrowRectTransform();
        }
    }


    void GrowRectTransform()
    {
        RectTransform rectTransform = left;
        Vector2 currentSize = rectTransform.sizeDelta;


        float scaleSpeed = Random.Range(80, 150);
        currentSize.x += Time.deltaTime * scaleSpeed;
        currentSize.y += Time.deltaTime * scaleSpeed;


        currentSize.x = Mathf.Min(currentSize.x, originalSize.x * 5f);
        currentSize.y = Mathf.Min(currentSize.y, originalSize.y * 5f);

        if (left.sizeDelta.x > 500)
        {
            isClick = false;
            Control();
        }

        rectTransform.sizeDelta = currentSize;
    }

    void Control()
    {
        float result = Mathf.Abs(left.sizeDelta.x - right.sizeDelta.x);

        if (result < 30f)
        {
            transform.GetComponent<Question>().point += 10;
            text.text = "%" + (result / 2).ToString("F2");
        }
        else if (result < 70f)
        {
            text.text = "%" + (result / 2).ToString("F2");
        }
        else
        {
            text.text = "%34";
        }

        DOVirtual.DelayedCall(0.5f, Create);
    }
}