using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Game1041 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public List<string> levelTexts;
    public List<float> levelTimes;
    public AudioSource source;
    public AudioClip correctSound, wrongSound;
    public GameObject questionPanel, answerPanel;
    public List<Sprite> sprites;

    public List<Sprite> selectedAnswers;
    public List<Sprite> correctAnswers;
    public List<Sprite> questions;
    public List<Sprite> answers;

    bool isShowing;
    float timeLeft=5;
    int reqAnswerCount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isShowing)
            return;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
            ShowingState(false);
            
    }
    private void OnEnable()
    {
        selectedAnswers = new List<Sprite>();
        correctAnswers = new List<Sprite>();
        questions = new List<Sprite>();
        answers = new List<Sprite>();
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
                PrepareLevel(2, 4, 2);
                break;
            case 1:
                PrepareLevel(3,4,2);
                break;
            case 2:
                PrepareLevel(2, 4, 1);
                break;
            case 3:
                PrepareLevel(2, 4, 2);
                break;
            case 4:
                PrepareLevel(3, 6, 2);
                break;
            case 5:
                PrepareLevel(2, 4, 1);
                break;
            case 6:
                PrepareLevel(3, 4, 2);
                break;
            case 7:
                PrepareLevel(3, 4, 1);
                break;
            case 8:
                PrepareLevel(3, 6, 2);
                break;
            case 9:
                PrepareLevel(4, 8, 2);
                break;
            case 10:
                PrepareLevel(3, 4, 1);
                break;

            default:
                PrepareLevel(3, 4, 2);
                break;
        }
    }
    void PrepareLevel(int questionCount,int answerCount,int correctCount)
    {
        reqAnswerCount = correctCount;
        while(questions.Count < questionCount)
        {
            int rnd = Random.RandomRange(0, sprites.Count);
            if (!questions.Contains(sprites[rnd]))
            { 
                questions.Add(sprites[rnd]);
                questionPanel.transform.GetChild(questions.Count - 1).gameObject.SetActive(true);
                questionPanel.transform.GetChild(questions.Count - 1).GetChild(0).GetComponent<Image>().sprite = sprites[rnd];
            }
        }
        questions = questions.OrderBy(x => Random.value).ToList();

        while (answers.Count < answerCount)
        {
            int rnd = Random.RandomRange(0, sprites.Count);
            if (!questions.Contains(sprites[rnd]))
                answers.Add(sprites[rnd]);
        }

        for (int i = 0; i < correctCount; i++)
        {
            answers[i] = questions[i];
            correctAnswers.Add(questions[i]);
        }

        answers = answers.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < answers.Count; i++)
        {
            answerPanel.transform.GetChild(i).gameObject.SetActive(true);
            answerPanel.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = answers[i];
        }
        ShowingState(true);
    }
    public void OnSelected(Toggle toggle)
    {
        if (toggle.isOn)
            selectedAnswers.Add(toggle.transform.GetChild(0).GetComponent<Image>().sprite);
        else
            selectedAnswers.Remove(toggle.transform.GetChild(0).GetComponent<Image>().sprite);

        CheckAnswer();
    }
    void CheckAnswer()
    {
        if (selectedAnswers.Count != reqAnswerCount)
            return;
        int correctAnswerCount=0;
        
        foreach(var answer in selectedAnswers)
            if(correctAnswers.Contains(answer))
                correctAnswerCount++;

        if (correctAnswerCount == reqAnswerCount)
        {
            EarnPoint();
            source.PlayOneShot(correctSound);
            question.questionTime++;
            SetLevel();
        }
        else
        {
            source.PlayOneShot(wrongSound);
            question.questionTime++;
            SetLevel();
        }
    }

    private void ResetLevel()
    {
        selectedAnswers.Clear();
        correctAnswers.Clear();
        answers.Clear();
        questions.Clear();
        for (int i = 0; i < questionPanel.transform.childCount; i++)
        {
            questionPanel.transform.GetChild(i).gameObject.SetActive(false); 
        }

        for (int i = 0; i < answerPanel.transform.childCount; i++)
        {
            answerPanel.transform.GetChild(i).GetComponent<Toggle>().isOn = false;
            answerPanel.transform.GetChild(i).gameObject.SetActive(false); 
        }
    }
    void ShowingState(bool state)
    {
        isShowing = state;
        timeLeft = 5;

        questionPanel.SetActive(isShowing);
        answerPanel.SetActive(!isShowing);
    }
}
