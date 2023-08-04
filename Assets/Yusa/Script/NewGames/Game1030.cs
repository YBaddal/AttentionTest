using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Game1030 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public int correctAnswer;
    public List<string> levelTexts;
    public List<float> levelTimes;
    public List<Sprite> sprites;
    public List<GameObject> arrows, answers;
    public List<int> orders;
    int arrowColor;
    // Start is called before the first frame update
    void Start()
    {
        question = GetComponent<Question>();
        Init();
        arrowColor = UnityEngine.Random.RandomRange(0, arrows.Count-1);
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
        question.maxQuestionTime = levelTimes[level];
        question.Init();
    }
    void SetLevel()
    {
        ResetLevel();
        switch (level)
        {
            case 0:
                PrepareLevel(true,false,false);
                break;
            case 1:
                PrepareLevel(true,true,false);
                break;
            case 2:
                PrepareLevel(true, true, true);
                break;
            case 3:
                PrepareLevel(true, false, false);
                break;
            case 4:
                PrepareLevel(true, true, true);
                break;
            case 5:
                PrepareLevel(false, false, false);
                break;
            case 6:
                PrepareLevel(false, false, true);
                break;
            case 7:
                PrepareLevel(true, true, true);
                break;
            case 8:
                PrepareLevel(false, false, false);
                break;
            case 9:
                PrepareLevel(false, false, true);
                break;
            default:
                PrepareLevel(false, false, true);
                break;
        }
    }

    void PrepareLevel(bool isAnswerArrow, bool isRandomColor, bool isRandomOrder)
    {
        correctAnswer = UnityEngine.Random.RandomRange(0, arrows.Count);
        arrows[correctAnswer].SetActive(true);

        if (isRandomOrder)
            orders = orders.OrderBy(x => Guid.NewGuid()).ToList();


        for (int i = 0; i < answers.Count; i++)
        {
            answers[i].transform.SetSiblingIndex(orders[i]);

            if (isAnswerArrow)
            {
                if (isRandomColor)
                    arrowColor = UnityEngine.Random.RandomRange(0, arrows.Count-1);

                answers[i].GetComponentInChildren<Image>().sprite = sprites[arrowColor];
                answers[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                answers[i].GetComponentInChildren<Image>().sprite = sprites[3];
                answers[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
        }
       
    }
    public void CheckAnswer(int answer)
    {
        if (correctAnswer == answer)
            EarnPoint();

        SetLevel();
    }

    private void ResetLevel()
    {
        for (int i = 0; i < arrows.Count; i++)
        {
            arrows[i].SetActive(false);
            answers[i].GetComponentInChildren<Image>().sprite = sprites[3];
            answers[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
    }

}
