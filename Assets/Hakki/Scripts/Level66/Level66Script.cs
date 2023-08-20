using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Level66Script : MonoBehaviour
{
    [SerializeField] private List<Sprite> happySprites;
    [SerializeField] private List<Sprite> sadSprites;
    [SerializeField] GridLayoutGroup _grid;

    private int levelCount = 5;
    private List<int> levelIndexes = new List<int>();

    private void Start()
    {
        Create();
    }

    void Create()
    {
        levelIndexes.Clear();
        for (int i = 0; i < levelCount; i++)
        {
            int randomNumber = Random.Range(0, _grid.transform.childCount);
            if (!levelIndexes.Contains(randomNumber))
            {
                levelIndexes.Add(randomNumber);
            }
            else
            {
                i--;
            }
        }

        for (int i = 0; i < _grid.transform.childCount; i++)
        {
            GameObject obj = _grid.transform.GetChild(i).gameObject;
            obj.transform.GetComponent<Image>().enabled = true;
            if (levelIndexes.Contains(i))
            {
                obj.GetComponent<Image>().sprite = happySprites[Random.Range(0, happySprites.Count)];
            }
            else
            {
                obj.GetComponent<Image>().sprite = sadSprites[Random.Range(0, sadSprites.Count)];
            }
        }
    }

    private int answerCount = 0;

    public void Control(Image img)
    {
        if (img.sprite.name.Contains("Happy"))
        {
            transform.GetComponent<Question>().point += 1;
            img.transform.GetComponent<Image>().enabled = false;
            answerCount++;
        }
        else
        {
            DOVirtual.DelayedCall(0.3f, Create);
        }

        if (answerCount == levelCount)
        {
            levelCount++;
            transform.GetComponent<Question>().point += 10;
            answerCount = 0;
            DOVirtual.DelayedCall(0.3f, Create);
        }
    }
}