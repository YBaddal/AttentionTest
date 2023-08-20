using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Level02Script : MonoBehaviour
{
    [SerializeField] private Transform _grid;
    private List<int> levelnumbers = new List<int>();

    private bool isTurn = true;

    void Start()
    {
        Create();
    }

    private int startNumber = 1;
    private void Create()
    {
        levelnumbers.Clear();
        for (int i = 0; i < _grid.childCount; i++)
        {
            int number = Random.Range(startNumber, startNumber+24);
            if (!levelnumbers.Contains(number))
            {
                levelnumbers.Add(number);
                _grid.GetChild(i).GetComponent<Image>().enabled = true;
                _grid.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = number.ToString();
                _grid.GetChild(i).GetComponent<Image>().enabled = true;
            }
            else
            {
                i--;
            }
        }

        levelnumbers.Sort();
    }


    public void Control(TextMeshProUGUI text)
    {
        if (isTurn)
        {
            if (levelnumbers[0] == int.Parse(text.text))
            {
                levelnumbers.RemoveAt(0);
                transform.GetComponent<Question>().point += 1;
                text.text = "";
                text.transform.parent.GetComponent<Image>().enabled = false;
            }
            else
            {
                DOVirtual.DelayedCall(0.2f, Create);
            }

            if (levelnumbers.Count == 0)
            {
                DOVirtual.DelayedCall(0.2f, Create);
                isTurn = false;
            }
        }
        else
        {
            if (levelnumbers[levelnumbers.Count - 1] == int.Parse(text.text))
            {
                levelnumbers.RemoveAt(levelnumbers.Count - 1);
                transform.GetComponent<Question>().point += 1;
                text.text = "";
                text.transform.parent.GetComponent<Image>().enabled = false;
            }
            else
            {
                DOVirtual.DelayedCall(0.2f, Create);
            }

            if (levelnumbers.Count == 0)
            {
                DOVirtual.DelayedCall(0.2f, Create);
                isTurn = true;
                startNumber = Random.Range(1, 20);
            }
        }
    }
}