using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Level06Create : HSingleton<Level06Create>
{
    protected override void Awake()
    {
        base.Awake();
    }

    [SerializeField] GridLayoutGroup grid;
    [SerializeField] GameObject item;
    [SerializeField] int itemCount = 30;
    [SerializeField] int levelCount = 4;

    private List<int> levelSelectCount = new List<int>();

    void Start()
    {
        //transform.GetComponent<Question>().questionTime = 60f;
        for (int i = 0; i < itemCount; i++)
        {
            Instantiate(item, grid.transform);
        }

        Create();
    }

    private void Create()
    {
        StartCoroutine(SelectItem());
    }

    private int index = 0;

    IEnumerator SelectItem()
    {
        yield return new WaitForSeconds(1f);
        int count = Random.Range(0, itemCount);
        if (!levelSelectCount.Contains(count))
        {
            levelSelectCount.Add(count);
            grid.transform.GetChild(count).GetComponent<Image>().color = Color.red;
        }
        else
        {
            StartCoroutine(SelectItem());
            yield break;
        }

        yield return new WaitForSeconds(.5f);
        index++;

        if (index != levelCount)
        {
            StartCoroutine(SelectItem());
        }
        else
        {
            GOWhite();
        }
    }

    private void GOWhite()
    {
        for (int i = 0; i < itemCount; i++)
        {
            grid.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
    }

    private int counter = 0;


    private bool isTrue = false;

    public void Control(int index)
    {
        if (levelSelectCount[counter] == index)
        {
            grid.transform.GetChild(index).GetComponent<Image>().color = Color.red;
            counter++;
            Debug.Log(counter +"=="+ levelCount);

            if (counter == levelCount)
            {
                transform.GetComponent<Question>().point += 10;
                isTrue = true;
                levelCount++;
                StartCoroutine(NextLevel());
                //adding points
            }

            return;
        }

        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1f);
        LevelClear();
    }

    void LevelClear()
    {
        counter = 0;
        index = 0;
        levelSelectCount.Clear();
        GOWhite();
        Create();
    }
}