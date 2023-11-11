using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1092 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public List<float> levelTimes;
    public List<string> levelTexts;
    public AudioSource source;
    public AudioClip correctSound, wrongSound;
    public List<Question15PrefabCell> meanQuestion;
    public List<Text> answerTexts;
    public List<string> colorString;
    public List<Color> color;


    public int correctAnswer;

    void Start()
    {
        foreach(var m in meanQuestion)
        {
            m.colorList = color;
            m.colorStringList = colorString;
        }
    }
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
        question.maxQuestionTime = levelTimes[level];
        question.tutorialText = levelTexts[level];
        question.Init();
    }
    void SetLevel()
    {
        switch (level)
        {
            case 0:
                PrepareLevel(false,true);
                break;
            case 1:
                PrepareLevel(false,true);
                break;
            case 2:
                PrepareLevel(true,false);
                break;
            case 3:
                PrepareLevel(false,true);
                break;
            case 4:
                PrepareLevel(false,false);
                break;
            case 5:
                PrepareLevel(false,true);
                break;
            default:
                PrepareLevel(true,false);
                break;
        }
    }
    void PrepareLevel(bool isQuestion,bool isSame)
    {
        meanQuestion[0].transform.parent.gameObject.SetActive(isQuestion);
        answerTexts[0].transform.parent.parent.gameObject.SetActive(!isQuestion);

        if (isQuestion)
        {
            correctAnswer = Random.RandomRange(0, meanQuestion.Count);
            foreach (var m in meanQuestion)
                m.gameObject.SetActive(false);

            meanQuestion[correctAnswer].gameObject.SetActive(true);
            meanQuestion[correctAnswer].GeneratePrefab();
        }
        else
        {
            correctAnswer = Random.RandomRange(0, answerTexts.Count);
            int correctColor = Random.RandomRange(0, color.Count);
            for (int i = 0; i < answerTexts.Count; i++)
            {
                if(i == correctAnswer)
                {
                    if (isSame)
                    {
                        answerTexts[i].text = colorString[correctColor];
                        answerTexts[i].color = color[correctColor];
                    }
                    else
                    {
                        int rndColorString = Random.RandomRange(0, colorString.Count);
                        while (correctColor == rndColorString)
                        {
                            rndColorString = Random.RandomRange(0, color.Count);
                        }
                        answerTexts[i].text = colorString[rndColorString];
                        answerTexts[i].color = color[correctColor];
                    }
                }
                else
                {
                    int randomColor = Random.RandomRange(0, color.Count);
                    int randomColorString = Random.RandomRange(0, colorString.Count);
                    if (isSame)
                    {
                        while (randomColor==correctColor)
                        {
                            randomColor = Random.RandomRange(0, color.Count);
                        }
                        while (randomColor == randomColorString)
                        {
                            randomColorString = Random.RandomRange(0, color.Count);
                        }
                        answerTexts[i].text = colorString[randomColorString];
                        answerTexts[i].color = color[randomColor];
                    }
                    else
                    {
                        answerTexts[i].text = colorString[randomColor];
                        answerTexts[i].color = color[randomColor];
                    }
                }
            }

        }
    }

    public void CheckAnswer(int answer)
    {
        if (answer==correctAnswer)
        {
            EarnPoint();
            source.PlayOneShot(correctSound);
        }
        else
            source.PlayOneShot(wrongSound);

        SetLevel();
    }
}
