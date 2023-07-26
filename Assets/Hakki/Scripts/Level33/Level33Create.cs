using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Level33Create : MonoBehaviour
{
    private void Start()
    {
        Create();
    }

    private int howManyClick = 15;
    [SerializeField] private TextMeshProUGUI clickText;


    private void Create()
    {
        howManyClick = Random.Range(13, 25);
        clickText.text = howManyClick.ToString();
    }

    private bool startTimer = true;
    private float timer = 0f;

    private void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;

            if (timer >= 3f)
            {
                startTimer = false;
                Control();
            }
        }
    }


    public void Click(Transform trans)
    {
        trans.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.2f).OnComplete((() =>
        {
            trans.DOScale(new Vector3(1, 1, 1), 0.1f);
        }));
        startTimer = true;
        howManyClick--;
        timer = 0f;
        Debug.Log(howManyClick);
    }

    public void Control()
    {
        if (howManyClick == 0)
        {
            Debug.Log(true);
            transform.GetComponent<Question>().point += 10;
        }
        else
        {
            Debug.Log(false);
        }

        timer = 0f;
        Create();
    }
}