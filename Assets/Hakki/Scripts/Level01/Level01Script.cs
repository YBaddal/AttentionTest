using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Level01Script : HSingleton<Level01Script>
{
    [SerializeField] int createItemCount = 91;
    [SerializeField] GameObject gridItem;
    [SerializeField] GridLayoutGroup grid;

    [SerializeField] private List<string> levelString;
    [SerializeField] private List<string> findString;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        Create();
    }

    private int currentLevelIndex;
    private int trueItem;

    void Create()
    {
        currentLevelIndex = Random.Range(0, findString.Count);
        
        trueItem = Random.Range(0, createItemCount);

        for (int i = 0; i < createItemCount; i++)
        {
            if (i == trueItem)
            {
                grid.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    findString[currentLevelIndex];
                continue;
            }


            grid.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                levelString[currentLevelIndex];
        }
    }

    public void Control(TextMeshProUGUI text)
    {
        if (text.text == findString[currentLevelIndex])
        {
            transform.GetComponent<Question>().point += 10;
            isSuccess = true;
        }

        StartCoroutine(NextLevel());
    }

    private bool isSuccess = false;

    private void LevelClear()
    {
        // for (int i = grid.transform.childCount - 1; i >= 0; i--)
        // {
        //     Destroy(grid.transform.GetChild(i).gameObject);
        // }
    }


    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1f);
        LevelClear();
        isSuccess = false;
        Create();
    }
}