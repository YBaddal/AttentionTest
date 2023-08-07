using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level48Script : MonoBehaviour
{
    [SerializeField] private List<GameObject> items;
    [SerializeField] private RectTransform parent;
    [SerializeField] private TextMeshProUGUI valueText;


    void Start()
    {
        //transform.GetComponent<Question>().questionTime = 60f;
        Create();
    }

    private int levelAnswer;

    void Create()
    {
        levelAnswer = Random.Range(10, 70);
        //30 //25
        for (int i = 0; i < 70; i++)
        {
            if (levelAnswer > i)
            {
                parent.GetChild(i).gameObject.SetActive(true);
                parent.GetChild(i).GetComponent<Image>().color =
                    items[Random.Range(0, items.Count)].GetComponent<Image>().color;
            }
            else
            {
                parent.GetChild(i).gameObject.SetActive(false);
            }
        }
    }


    public void Add(int value)
    {
        if (int.Parse(valueText.text) <= 0)
        {
            return;
        }

        valueText.text = (int.Parse(valueText.text) + value).ToString();
    }

    public void Control()
    {
        if (int.Parse(valueText.text) == levelAnswer)
        {
            transform.GetComponent<Question>().point += 10;
        }

        parent.anchoredPosition = Vector2.zero;

        DOVirtual.DelayedCall(0.5f, Create);
    }
}