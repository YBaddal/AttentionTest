using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1045 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public List<string> levelTexts;
    public List<float> levelTimes;
    public List<Color> colors;
    public List<Toggle> colorPalette;
    public GridLayoutGroup leftGrid,rightGrid;
    public int selectedColor;
    public AudioSource source;
    public AudioClip correctSound;
    private void OnEnable()
    {
        question = GetComponent<Question>();
        Init();
        SetLevel();
    }
    void EarnPoint()
    {
        question.point += point;
    }
    public void Init()
    {
        level = question.level;
        question.tutorialText = levelTexts[level];
        question.Init();
        question.maxQuestionTime = levelTimes[level];
        question.questionTime = 1;
    }
    void SetLevel()
    {
        ResetLevel();
        switch (level)
        {
            case 0:
                PrepareLevel(4, 4, 1, 5);
                break;
            case 1:
                PrepareLevel(4, 5, 1, 5);
                break;
            case 2:
                PrepareLevel(5, 5, 3, 5);
                break;
            case 3:
                PrepareLevel(5, 6, 3, 5);
                break;
            case 4:
                PrepareLevel(5, 7, 1, 10);
                break;
            case 5:
                PrepareLevel(6, 6, 3, 10);
                break;
            case 6:
                PrepareLevel(6, 6, 1, 10);
                break;
            case 7:
                PrepareLevel(6, 7, 6, 12);
                break;
            case 8:
                PrepareLevel(6, 8, 6, 15);
                break;
            case 9:
                PrepareLevel(6, 8, 6, 15);
                break;
            case 10:
                PrepareLevel(6, 8, 6, 15);
                break;
            default:
                PrepareLevel(6, 6, 1, 5);
                break;
        }
    }
    void PrepareLevel(int column, int row,int colorCount,int selectedCount)
    {
        int totalCount = column * row;

        leftGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        leftGrid.constraintCount = column;
        rightGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        rightGrid.constraintCount = column;

        List<int> selectedGrid = new List<int>();

        while (selectedGrid.Count < selectedCount)
        {
            int randomNumber = UnityEngine.Random.RandomRange(0, totalCount);

            if (!selectedGrid.Contains(randomNumber))
            {
                selectedGrid.Add(randomNumber);
            }
        }

        for (int i = 0; i < totalCount; i++)
        {
            leftGrid.transform.GetChild(i).gameObject.SetActive(true);
            rightGrid.transform.GetChild(i).gameObject.SetActive(true);
        }

        foreach (var s in selectedGrid)
        {
            leftGrid.transform.GetChild(s).GetComponent<Toggle>().isOn = true;

            int randomNumber = 0;

            if (colorCount > 1)
                randomNumber = UnityEngine.Random.RandomRange(0, colorCount);

            leftGrid.transform.GetChild(s).GetComponent<Toggle>().graphic.color = colors[randomNumber];
        }


        if (colorCount > 1)
        for (int i = 0; i < colorCount; i++)
            colorPalette[i].gameObject.SetActive(true);
    }

    public void CheckAnswer()
    {
        bool success = true;
        for(int i = 0; i < leftGrid.transform.childCount; i++)
        {
            if (leftGrid.transform.GetChild(i).GetComponent<Toggle>().isOn != rightGrid.transform.GetChild(i).GetComponent<Toggle>().isOn)
                success = false;
        }
        if (success)
        {
            EarnPoint();
            source.PlayOneShot(correctSound);
            SetLevel();
        }
    }
    public void SelectColor(int color)
    {
        if (colorPalette[color].isOn)
            selectedColor = color;
    }
    public void OnPressToggle(Toggle toggle)
    {
        if (toggle.isOn)
            toggle.graphic.color = colors[selectedColor];
        CheckAnswer();
    }
    private void ResetLevel()
    {
        for(int i=0;i<leftGrid.transform.childCount; i++)
        {
            leftGrid.transform.GetChild(i).GetComponent<Toggle>().isOn = false;
            rightGrid.transform.GetChild(i).GetComponent<Toggle>().isOn = false;
        }

    }
}
