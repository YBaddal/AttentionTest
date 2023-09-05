using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Game1011 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public List<Game1011Cell> questions,answers;
    public List<Game1011Symbol> symbols;
    public List<string> levelTexts;
    public List<float> levelTimes;

    public List<int> uniqueIntList = new List<int>();
    public List<int> shuffledList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
  
    }
    private void OnEnable()
    {
        question = GetComponent<Question>();
        Init();
        SetLevel();
    }
    public void OnPressButton()
    {
         CheckAnswer();
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
        switch (level)
        {
            case 0:
                PrepareLevel(0, 10);
                break;
            case 1:
                PrepareLevel(0, 33);
                break;
            case 2:
                PrepareLevel(0, 37);
                break;
            case 3:
                PrepareLevel(0, 38);
                break;
            case 4:
                PrepareLevel(0, 43);
                break;
            case 5:
                PrepareLevel(0, 50);
                break;
            default:
                PrepareLevel(0, 50);
                break;
        }
    }

    void PrepareLevel(int min , int max)
    {
        ResetLevel();
        while (uniqueIntList.Count < 5)
        {
            int randomNumber = UnityEngine.Random.RandomRange(min,max); // 0-30 arasýnda rastgele bir sayý alýyoruz.

            if (!uniqueIntList.Contains(randomNumber)) // Sayýyý kontrol ediyoruz.
            {
                uniqueIntList.Add(randomNumber); // Eðer listede yoksa ekliyoruz.
            }
        }

        for (int i = 0; i < questions.Count; i++)
        {
            if (symbols[uniqueIntList[i]].sprite != null)
                questions[i].questionImage.sprite = symbols[uniqueIntList[i]].sprite;

            if (!string.IsNullOrEmpty( symbols[uniqueIntList[i]].str))
                questions[i].questionText.text = symbols[uniqueIntList[i]].str;

            questions[i].symbol = symbols[uniqueIntList[i]];
            questions[i].answerText.text = (i + 1).ToString();
        }
      
        // Liste elemanlarýný karýþtýrma
        shuffledList = shuffledList.OrderBy(x => Guid.NewGuid()).ToList();

        for (int i = 0; i < answers.Count; i++)
        {
            if (symbols[uniqueIntList[shuffledList[i]]].sprite != null)
                answers[i].questionImage.sprite = symbols[uniqueIntList[shuffledList[i]]].sprite;

            if (!string.IsNullOrEmpty(symbols[uniqueIntList[shuffledList[i]]].str))
                answers[i].questionText.text = symbols[uniqueIntList[shuffledList[i]]].str;

            answers[i].symbol = symbols[uniqueIntList[shuffledList[i]]];
            answers[i].answerText.text = "";
        }

    }

    public void CheckAnswer()
    {
        for(int i=0; i < answers.Count; i++)
        {
            if (answers[i].answerText.text == (shuffledList[i] + 1).ToString())
                EarnPoint();
        }
        question.questionTime++;
        SetLevel();
    }

    private void ResetLevel()
    {
        uniqueIntList = new List<int>();
        for (int i=0; i < answers.Count; i++)
        {
            questions[i].questionText.text = "";
            questions[i].answerText.text = "";
            questions[i].questionImage.sprite = null;
            answers[i].questionText.text = "";
            answers[i].answerText.text = "";
            answers[i].questionImage.sprite = null;
        }
    }

}
[System.Serializable]
public class Game1011Symbol
{
    public Sprite sprite;
    public string str;
}
