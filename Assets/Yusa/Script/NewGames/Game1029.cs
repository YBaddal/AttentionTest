using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1029 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public List<string> levelTexts;
    public List<float> levelTimes;
    public List<Color> colors;

    public GridLayoutGroup leftGrid, rightGrid;
    public int selectedColor;
    public AudioSource source;
    public AudioClip correctSound,wrongSound;
    private int selectedCount;
    bool isInOrder=false;
    public List<Toggle> questionlist,answerList;
    private void OnEnable()
    {
        question = GetComponent<Question>();
        questionlist = new List<Toggle>();
        answerList = new List<Toggle>();
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
        question.maxQuestionTime = levelTimes[level];
        question.tutorialText = levelTexts[level];
        question.Init();
    }
    void SetLevel()
    {
        ResetLevel();
        switch (level)
        {
            case 0:
                PrepareLevel(2, 2, 1, 2,false);
                break;
            case 1:
                PrepareLevel(3, 3, 1, 3,false);
                break;
            case 2:
                PrepareLevel(2, 2, 1, 2,true);
                break;
            case 3:
                PrepareLevel(3, 3, 1, 3, false);
                break;
            case 4:
                PrepareLevel(4, 4, 1, 4, false);
                break;
            case 5:
                PrepareLevel(5, 5, 1, 5, false);
                break;
            case 6:
                PrepareLevel(3, 3, 1, 3,true);
                break;
            case 7:
                PrepareLevel(4, 4, 1, 4, true);
                break;
            case 8:
                PrepareLevel(4, 4, 1, 4, false);
                break;
            case 9:
                PrepareLevel(5, 5, 1, 4, false);
                break;
            case 10:
                PrepareLevel(6, 6, 1, 5, false);
                break;
            case 11:
                PrepareLevel(7, 7, 1, 5, false);
                break;
            case 12:
                PrepareLevel(4, 4, 1, 4, true);
                break;
            case 13:
                PrepareLevel(5, 5, 1, 5, true);
                break;
            default:
                PrepareLevel(6, 6, 1, 5, false);
                break;
        }
    }
    void PrepareLevel(int column, int row, int colorCount, int _selectedCount, bool _isInOrder)
    {
        int totalCount = column * row;
        selectedCount = _selectedCount;
        isInOrder = _isInOrder;
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

        for (int s=0;s<selectedGrid.Count;s++)
        {
            leftGrid.transform.GetChild(selectedGrid[s]).GetComponent<Toggle>().isOn = true;
            questionlist.Add(leftGrid.transform.GetChild(selectedGrid[s]).GetComponent<Toggle>());
            if(isInOrder)
             leftGrid.transform.GetChild(selectedGrid[s]).GetComponentInChildren<Text>().text = (s+1).ToString();

            int randomNumber = selectedColor;

            if (colorCount > 1)
                randomNumber = UnityEngine.Random.RandomRange(0, colorCount);

            leftGrid.transform.GetChild(selectedGrid[s]).GetComponent<Toggle>().graphic.color = colors[randomNumber];
        }


        //if (colorCount > 1)
        //    for (int i = 0; i < colorCount; i++)
        //        colorPalette[i].gameObject.SetActive(true);
    }

    public void CheckAnswer()
    {
        bool success = true;
    

        if (answerList.Count < selectedCount)
            return;

        if (isInOrder)
        {
            for (int i = 0; i < answerList.Count; i++)
            {
                if (questionlist[i].gameObject.name != answerList[i].gameObject.name)
                    success = false;
            }
        }
        else
        {
            for (int i = 0; i < leftGrid.transform.childCount; i++)
            {
                if (leftGrid.transform.GetChild(i).GetComponent<Toggle>().isOn != rightGrid.transform.GetChild(i).GetComponent<Toggle>().isOn)
                    success = false;
            }
        }

        if (success)
        {
            EarnPoint();
            source.PlayOneShot(correctSound);
            SetLevel();
        }
        else
        {
            source.PlayOneShot(wrongSound);
            SetLevel();
        }
    }
  
    public void OnPressToggle(Toggle toggle)
    {
        if (toggle.isOn)
        {
            toggle.graphic.color = colors[selectedColor];
            answerList.Add(toggle);
            toggle.GetComponentInChildren<Text>().text = answerList.Count.ToString();
        }
        else
        {
            answerList.Remove(toggle);
            toggle.GetComponentInChildren<Text>().text = "";
        }

        CheckAnswer();
    }
    void ShowRightGrid()
    {
        leftGrid.gameObject.SetActive(false);
        rightGrid.gameObject.SetActive(true);
    }
    private void ResetLevel()
    {
        leftGrid.gameObject.SetActive(true);
        rightGrid.gameObject.SetActive(false);
        Invoke("ShowRightGrid", 5);
        questionlist.Clear();
        answerList.Clear();
        for (int i = 0; i < leftGrid.transform.childCount; i++)
        {
            leftGrid.transform.GetChild(i).GetComponent<Toggle>().isOn = false;
            leftGrid.transform.GetChild(i).GetComponentInChildren<Text>().text = "";
            rightGrid.transform.GetChild(i).GetComponent<Toggle>().isOn = false;
        }

    }
}
