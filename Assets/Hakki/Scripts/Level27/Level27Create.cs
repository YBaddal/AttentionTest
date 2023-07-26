using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Level27Create : HSingleton<Level27Create>
{
    protected override void Awake()
    {
        base.Awake();
    }

    [SerializeField] private List<GameObject> levels;

    private GameObject level;

    private int redImageCount = 10;

    private List<int> redImageIndex = new List<int>();

    private int levelIndex;

    private void Start()
    {
        Create();
    }

    void Create()
    {
        level = levels[Random.Range(0, levels.Count)];
        level.SetActive(true);
        levelIndex = level.transform.childCount;
        for (int i = 0; i < redImageCount; i++)
        {
            int index = Random.Range(0, level.transform.childCount);

            if (!redImageIndex.Contains(index))
            {
                level.transform.GetChild(index).GetComponent<Image>().color = Color.red;
                level.transform.GetChild(index).GetComponent<Level27ImageController>().isRed = true;
                redImageIndex.Add(index);
            }
            else
            {
                i--;
            }
        }
    }


    public void Control(bool isRed)
    {
        if (!isRed)
        {
            levelIndex--;
        }
        else
        {
            Debug.Log(false);
            LevelClear();
        }


        if (levelIndex - redImageCount <= 0)
        {
            transform.GetComponent<Question>().point += 10;
            LevelClear();
            redImageCount += 3;
        }


        void LevelClear()
        {
            redImageIndex.Clear();
            Debug.Log(true);
            for (int i = 0; i < level.transform.childCount; i++)
            {
                level.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                level.transform.GetChild(i).GetComponent<Level27ImageController>().enabled = true;
                level.transform.GetChild(i).GetComponent<Level27ImageController>().isRed = false;
            }

            level.SetActive(false);
            Create();
        }
    }
}