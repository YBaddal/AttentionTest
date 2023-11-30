using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1058 : MonoBehaviour
{

    Question question;
    public int level;
    public int point;
    public List<string> levelTexts;
    public List<float> levelTimes;

    public AudioSource source;
    public AudioClip correctSound, wrongSound;

    public List<Color> colorList;
    public Image questionObj;
    public List<Button> answerButtons;
    public List<Button> activeButtons;
    public List<int> activeButtonNumbers;

    public List <Vector2> numberCounts;
    public Button correctAnswer;
    int currentColor;
    int currentCount=1;
    int currentQuestion;
    void Start()
    {

    }
    private void OnEnable()
    {
        question = GetComponent<Question>();
        activeButtons = new List<Button>();
        activeButtonNumbers = new List<int>();
        Init();
        ResetLevel();
        currentCount = (int)numberCounts[level].x;
        currentQuestion = (int)numberCounts[level].x;
        for (int i = 0; i < 4; i++)
            PrepareAnswers();
        SetLevel();
        ChangeColor();
    }
    void EarnPoint()
    {
        question.AddPoint(point);
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
                PrepareLevel(true);
                break;
            case 1:
                PrepareLevel(true);
                break;
            case 2:
                PrepareLevel(false);
                break;
            case 3:
                PrepareLevel(false);
                break;
            case 4:
                PrepareLevel(true);
                break;
            case 5:
                PrepareLevel(true);
                break;
            case 6:
                PrepareLevel(false);
                break;
            case 7:
                PrepareLevel(false);
                break;
            case 8:
                PrepareLevel(true);
                break;
            case 9:
                PrepareLevel(true);
                break;
            case 10:
                PrepareLevel(false);
                break;
            case 11:
                PrepareLevel(false);
                break;
            case 12:
                PrepareLevel(false);
                break;
            default:
                PrepareLevel(true);
                break;
        }
    }
    void PrepareLevel(bool isCount)
    {
        if (isCount)
        {
            Text questionText = questionObj.transform.GetChild(0).GetComponent<Text>();
            questionText.gameObject.SetActive(true);
            questionText.text = currentQuestion.ToString();

            int index = activeButtonNumbers.IndexOf(currentQuestion);
            correctAnswer = activeButtons[index];

            if ((int)numberCounts[level].x < (int)numberCounts[level].y)
                currentQuestion = currentQuestion < (int)numberCounts[level].y ? currentQuestion + 1 : (int)numberCounts[level].x;
            else
                currentQuestion = currentQuestion > (int)numberCounts[level].y ? currentQuestion - 1 : (int)numberCounts[level].x;
        }
        else
        {
            for (int i = 0; i < questionObj.transform.childCount; i++)
                questionObj.transform.GetChild(i).gameObject.SetActive(false);

            int rnd = Random.RandomRange(1, 3);
            questionObj.transform.GetChild(rnd).gameObject.SetActive(true);

            int correct = rnd == 1 ? FindMaxNumber(activeButtonNumbers) : FindMinNumber(activeButtonNumbers);

            int index = activeButtonNumbers.IndexOf(correct);
            correctAnswer = activeButtons[index];
        }
    }
    void PrepareAnswers()
    {
        if (!activeButtonNumbers.Contains(currentCount))
        {
            int randomPos = Random.RandomRange(0, answerButtons.Count);
            while (answerButtons[randomPos].gameObject.active)
                randomPos = Random.RandomRange(0, answerButtons.Count);

            answerButtons[randomPos].gameObject.SetActive(true);
            answerButtons[randomPos].GetComponentInChildren<Text>().text = currentCount.ToString();
            activeButtons.Add(answerButtons[randomPos]);
            activeButtonNumbers.Add(currentCount);
        }
        else
        {
            if ((int)numberCounts[level].x < (int)numberCounts[level].y)
                currentCount = currentCount < (int)numberCounts[level].y ? currentCount + 1 : (int)numberCounts[level].x;
            else
                currentCount = currentCount > (int)numberCounts[level].y ? currentCount - 1 : (int)numberCounts[level].x;

            int randomPos = Random.RandomRange(0, answerButtons.Count);
            while (answerButtons[randomPos].gameObject.active)
                randomPos = Random.RandomRange(0, answerButtons.Count);

            answerButtons[randomPos].gameObject.SetActive(true);
            answerButtons[randomPos].GetComponentInChildren<Text>().text = currentCount.ToString();
            activeButtons.Add(answerButtons[randomPos]);
            activeButtonNumbers.Add(currentCount);
        }

        if((int)numberCounts[level].x < (int)numberCounts[level].y)
            currentCount = currentCount < (int)numberCounts[level].y ? currentCount + 1 : (int)numberCounts[level].x;
        else
            currentCount = currentCount > (int)numberCounts[level].y ? currentCount - 1 : (int)numberCounts[level].x;

    }
    public void CheckAnswer(Button answer)
    {
        if (answer == correctAnswer)
        {
            EarnPoint();
            source.PlayOneShot(correctSound);
        }
        else
            source.PlayOneShot(wrongSound);

        int index = activeButtons.IndexOf(answer);
        activeButtons.RemoveAt(index);
        activeButtonNumbers.RemoveAt(index);
        answer.gameObject.SetActive(false);
        ChangeColor();
        PrepareAnswers();
        SetLevel();
    }
    void ChangeColor()
    {
       int rndColor = Random.Range(0, colorList.Count);
        while (rndColor == currentColor)
            rndColor = Random.Range(0, colorList.Count);

        currentColor = rndColor;
        for(int i = 0; i < answerButtons.Count; i++)
            answerButtons[i].image.color = colorList[currentColor];

        questionObj.color = colorList[currentColor];

    }
    void ResetLevel()
    {
        for (int i = 0; i < questionObj.transform.childCount; i++)
            questionObj.transform.GetChild(i).gameObject.SetActive(false);

        for (int j = 0; j < answerButtons.Count; j++)
            answerButtons[j].gameObject.SetActive(false);
    }

    int FindMinNumber(List<int> numbers)
    {
        int min = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] < min)
            {
                min = numbers[i];
            }
        }

        return min;
    }

    int FindMaxNumber(List<int> numbers)
    {
        int max = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] > max)
            {
                max = numbers[i];
            }
        }

        return max;
    }
}
