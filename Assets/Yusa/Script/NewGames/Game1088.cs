using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1088 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public int correctAnswer,wrongAnswer;
    public List<string> levelTexts;
    public List<float> levelTimes;
    public List<Color> colors;
    public List<string> colorStrings;
    public List<Text> answers;
    public Image circleImage;
    public Text circleText;
    public AudioSource source;
    public AudioClip correctSound,wrongSound;
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
                PrepareLevel(2, true, true);
                break;
            case 1:
                PrepareLevel(2, false, true);
                break;
            case 2:
                PrepareLevel(2, true, true);
                break;
            case 3:
                PrepareLevel(2, false, true);
                break;
            case 4:
                PrepareLevel(2, true, false);
                break;
            case 5:
                PrepareLevel(2, false, false);
                break;
            case 6:
                PrepareLevel(6, false, false);
                break;
            case 7:
                PrepareLevel(2, true, false);
                break;
            case 8:
                PrepareLevel(2, false, false);
                break;
            case 9:
                PrepareLevel(6, false, false);
                break;
            default:
                PrepareLevel(6, false, false);
                break;
        }
    }
    void PrepareLevel(int colorCount,bool isPositive,bool isCircleColor)
    {
        int firstNumber = Random.Range(0, colorCount);

        int secondNumber = Random.Range(0, colorCount);
        while (secondNumber == firstNumber)
        {
            secondNumber = Random.Range(0, colorCount);
        }

        correctAnswer = isPositive ? firstNumber : secondNumber;
        wrongAnswer = !isPositive ? firstNumber : secondNumber;

        if (isCircleColor)
            circleImage.color = colors[firstNumber];
        else
        {
            if (isPositive)
            {
                circleText.text = colorStrings[wrongAnswer];
                circleText.color = colors[correctAnswer];
            }
            else
            {
                circleText.text = colorStrings[correctAnswer];
                circleText.color = colors[wrongAnswer];
            }

        }
   
        int randomIndex = Random.Range(0, answers.Count);
        answers[randomIndex].text = colorStrings[firstNumber];
        answers[randomIndex].transform.parent.GetComponent<Button>().onClick.AddListener(delegate { CheckAnswer(firstNumber); });

        randomIndex = (randomIndex + 1) % answers.Count;
        answers[randomIndex].text = colorStrings[secondNumber];
        answers[randomIndex].transform.parent.GetComponent<Button>().onClick.AddListener(delegate { CheckAnswer(secondNumber); });

    }
    public void CheckAnswer(int answer)
    {
        if (correctAnswer == answer)
        {
            EarnPoint();
            source.PlayOneShot(correctSound);
        }
        else
            source.PlayOneShot(wrongSound);
        SetLevel();
    }

    private void ResetLevel()
    {
        foreach(var answer in answers)
            answer.transform.parent.GetComponent<Button>().onClick.RemoveAllListeners();

        circleImage.color = Color.white;
        circleText.text = "";
        
    }
}
