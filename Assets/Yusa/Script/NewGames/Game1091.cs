using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1091 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public List<string> levelTexts;
    public List<float> levelTimes;
    public AudioSource source;
    public AudioClip correctSound, wrongSound;
    public int correctAnswer;
    public List<Image> palette,questionPalette;
    public List<Button> answerButtons, answerPalette;
    public List<GameObject> cursors;
    public GameObject paletteCursor,createObj,selectObj;
    public int currenctCount;
    public int selectedColor;
    public List<Color> questionColors;
    private void OnEnable()
    {
        question = GetComponent<Question>();
        questionColors = new List<Color>();
        Init();
        SetLevel();
        SelectColor(0);
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
                PrepareLevel(true,4,6);
                break;
            case 1:
                PrepareLevel(false,5,8);
                break;
            case 2:
                PrepareLevel(true,5,8);
                break;
            case 3:
                PrepareLevel(false,6,9);
                break;
            case 4:
                PrepareLevel(true,7,10);
                break;
            case 5:
                PrepareLevel(false,7,11);
                break;
            default:
                PrepareLevel(false, 7, 11);
                break;
        }
    }
    void PrepareLevel(bool isSelect, int min,int max)
    {
        selectObj.SetActive(isSelect);
        createObj.SetActive(!isSelect);
        if (isSelect)
        {
            cursors[currenctCount].SetActive(false);
            currenctCount = currenctCount < min ? min : currenctCount + 1;
            currenctCount = currenctCount > max ? max : currenctCount;
            cursors[currenctCount].SetActive(true);
            int startPos = Random.Range(0, palette.Count - currenctCount);
            cursors[currenctCount].transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(50 * startPos, 0);

            correctAnswer = Random.RandomRange(0, answerButtons.Count);

            //Fill Correct
            for (int i = 0; i < answerButtons.Count; i++)
            {
                for (int j = 0; j < currenctCount; j++)
                {
                    answerButtons[i].transform.GetChild(j).gameObject.SetActive(true);
                    if (correctAnswer == i)
                    {
                        answerButtons[i].transform.GetChild(j).GetComponent<Image>().color = palette[startPos + j].color;
                    }
                    else
                    {
                        int wrongStartPos = startPos;

                        while (wrongStartPos == startPos)
                        {
                            wrongStartPos = Random.Range(0, palette.Count - currenctCount);
                        }
                        answerButtons[i].transform.GetChild(j).GetComponent<Image>().color = palette[wrongStartPos + j].color;
                    }
                }
            }
        }
        else
        {
            currenctCount = currenctCount < min ? min : currenctCount + 1;
            currenctCount = currenctCount > max ? max : currenctCount;

            questionColors.Clear();
            while (questionColors.Count < currenctCount)
            {
                int rnd = Random.RandomRange(0, palette.Count);

                if (!questionColors.Contains(palette[rnd].color))
                {
                    questionColors.Add(palette[rnd].color);
                }
            }
            for(int i = 0; i < questionColors.Count; i++)
            {
                questionPalette[i].gameObject.SetActive(true);
                answerPalette[i].gameObject.SetActive(true);
                questionPalette[i].color = questionColors[i];
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

        question.questionTime++;
        Invoke("ResetLevel", 0.25f);
    }
    public void CheckColors()
    {
        int correctCount = 0;
        for(int i = 0; i < currenctCount; i++)
        {
           if(questionPalette[i].color == answerPalette[i].image.color)
              correctCount++;
        }

        if(correctCount==currenctCount)
        {
            EarnPoint();
            source.PlayOneShot(correctSound);
            question.questionTime++;
            Invoke("ResetLevel", 0.25f);
        }

    }

    public void Colorize(Image image)
    {
        image.color = palette[selectedColor].color;
        CheckColors();
    }
    public void SelectColor(int color)
    {
        selectedColor = color;
        paletteCursor.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(50 * selectedColor, 0);
        ColorBlock cb = answerPalette[0].colors;
        cb.highlightedColor = palette[selectedColor].color;
        foreach (var answer in answerPalette)
            answer.colors = cb;
    }
    void ResetLevel()
    {
        for (int i = 0; i < answerButtons.Count; i++)
        {
            for (int j = 0; j < answerButtons[i].transform.childCount; j++)
            {
                answerButtons[i].transform.GetChild(j).gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < questionPalette.Count; i++)
        {
            answerPalette[i].image.color = Color.white;
            questionPalette[i].color = Color.white;
            answerPalette[i].gameObject.SetActive(false);
            questionPalette[i].gameObject.SetActive(false);
        }
        SetLevel();
    }
}
