using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1097 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public List<string> levelTexts;
    public List<float> levelTimes;
    public List<string> numberStrings;
    public GridLayoutGroup answerGrid;
    public List<Button> answerButtons;
    public AudioSource source;
    public AudioClip correctSound, wrongSound;
    public Text questionText;
    private int correctAnswer,lap;
    private void OnEnable()
    {
        question = GetComponent<Question>();
        correctAnswer = -1;
        lap = 0;
        RandomizePositions();
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
                PrepareLevel(3, 3,  false, true);
                break;
            case 1:
                PrepareLevel(3, 3,  false, true);
                break;
            case 2:
                PrepareLevel(3, 3,  true, false);
                break;
            case 3:
                PrepareLevel(3, 3,  false, false);
                break;
            case 4:
                PrepareLevel(3, 3,  false, false);
                break;

            default:
                PrepareLevel(5, 5, true, true);
                break;
        }
    }
    void PrepareLevel(int column, int row, bool isText,bool isRandom)
    {
        int totalCount = (column + lap) * (row + lap); 

        if (isRandom)
        { 
            correctAnswer = Random.RandomRange(0, totalCount);
            RandomizePositions();
        }
        else
        {
            lap = correctAnswer != totalCount - 1 ? lap : lap + 1;
            totalCount = (column + lap) * (row + lap);

            if( totalCount > answerButtons.Count)
            {
                lap = 0;
                totalCount = (column + lap) * (row + lap);
                ResetLevel();
            }
            correctAnswer = correctAnswer >= totalCount - 1 ? 0 : correctAnswer+1;

        }

        if (isText)
            questionText.text = numberStrings[correctAnswer];
        else
            questionText.text = (correctAnswer + 1).ToString();



        answerGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        answerGrid.constraintCount = column + lap;
        for (int i = 0; i < totalCount; i++)
        {
            answerButtons[i].gameObject.SetActive(true);
        }
    }

    public void CheckAnswer(int answer)
    {
      
        if (correctAnswer == answer)
        {
            EarnPoint();
            source.PlayOneShot(correctSound);
            SetLevel();
        }
        else
        {
            source.PlayOneShot(wrongSound);
        }
    }
    private void ResetLevel()
    {
       for(int i = 0; i < answerGrid.transform.childCount; i++)
            answerGrid.transform.GetChild(i).gameObject.SetActive(false);
    }
    void RandomizePositions()
    {
        int buttonCount = answerGrid.transform.childCount;
        List<Transform> buttons = new List<Transform>();

        // Grid içindeki butonlarý bir listeye ekleyerek referanslarýný sakla
        for (int i = 0; i < buttonCount; i++)
        {
            buttons.Add(answerGrid.transform.GetChild(i));
        }

        // Butonlarýn sýrasýný karýþtýrmak için Fisher-Yates shuffle algoritmasýný kullan
        for (int i = 0; i < buttonCount; i++)
        {
            int randomIndex = Random.Range(i, buttonCount);
            Transform temp = buttons[randomIndex];
            buttons[randomIndex] = buttons[i];
            buttons[i] = temp;
        }

        // Yeni sýraya göre butonlarý grid içinde yerleþtir
        for (int i = 0; i < buttonCount; i++)
        {
            buttons[i].SetSiblingIndex(i);
        }
    }
}
