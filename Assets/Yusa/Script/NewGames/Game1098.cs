using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1098 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public List<string> levelTexts;
    public List<float> levelTimes;
    public List<GameObject> questionImages;
    public List<Button> answerButtons;
    public AudioSource source;
    public AudioClip correctSound, wrongSound;
    public int correctAnswer;
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
        ResetLevel();
        switch (level)
        {
            case 0:
                PrepareLevel(1, 9);
                break;
            case 1:
                PrepareLevel(5, 20);
                break;
            case 2:
                PrepareLevel(7, 30);
                break;  

            default:
                PrepareLevel(5, 25);
                break;
        }
    }
    void PrepareLevel(int min, int max)
    {
        correctAnswer = Random.Range(min, max);
        List<int> selectedQuestion = new List<int>();
        while (selectedQuestion.Count < correctAnswer)
        {
            int rnd = Random.Range(0, questionImages.Count);
            if (!selectedQuestion.Contains(rnd))
            {
                selectedQuestion.Add(rnd);
                questionImages[rnd].SetActive(true);
            }
        }
        List<int> selectedAnswer = new List<int>();
        while (selectedAnswer.Count < answerButtons.Count)
        {
            int rndMin=correctAnswer-5<1 ? 1 : correctAnswer-5;
            int rnd = Random.Range((rndMin), (correctAnswer+5));
            if (!selectedAnswer.Contains(rnd)&&rnd!=correctAnswer)
                selectedAnswer.Add(rnd);
        }
        int randomAnswer = Random.Range(0, answerButtons.Count);
        selectedAnswer[randomAnswer] = correctAnswer;

        for(int i = 0; i < answerButtons.Count; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = selectedAnswer[i].ToString();
            answerButtons[i].onClick.RemoveAllListeners();
            int answer = selectedAnswer[i];
            answerButtons[i].onClick.AddListener(delegate { this.CheckAnswer(answer); });
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
            SetLevel();
        }
    }
    private void ResetLevel()
    {
        for (int i = 0; i < questionImages.Count; i++)
            questionImages[i].SetActive(false);
    }
 
}
