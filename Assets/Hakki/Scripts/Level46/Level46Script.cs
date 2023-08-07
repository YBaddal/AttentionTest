using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

public class Level46Script : MonoBehaviour
{
    private bool isGrowing = false;
    private Vector2 originalSize;
    [SerializeField] RectTransform baloon;
    [SerializeField] TextMeshProUGUI resultText;


    void Start()
    {
        // transform.GetComponent<Question>().questionTime = 60;
        originalSize = baloon.sizeDelta;
    }


    void Create()
    {
        baloon.sizeDelta = originalSize;
    }

    private bool isClick = true;

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

    void Control()
    {
        resultText.text = "%" + Mathf.RoundToInt((100 - (400 - baloon.sizeDelta.x) / 2));


        if (Mathf.RoundToInt((100 - (400 - baloon.sizeDelta.x) / 2)) > 85)
        {
            transform.GetComponent<Question>().point += 10;
        }

        StartCoroutine(Wait());
    }

    void GrowRectTransform()
    {
        RectTransform rectTransform = baloon;
        Vector2 currentSize = rectTransform.sizeDelta;


        float scaleSpeed = Random.Range(80, 150);
        currentSize.x += Time.deltaTime * scaleSpeed;
        currentSize.y += Time.deltaTime * scaleSpeed;


        currentSize.x = Mathf.Min(currentSize.x, originalSize.x * 5f);
        currentSize.y = Mathf.Min(currentSize.y, originalSize.y * 5f);

        if (currentSize.x > 420f)
        {
            isGrowing = false;
        }

        rectTransform.sizeDelta = currentSize;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        isClick = true;
        Create();
    }
}