using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1086 : MonoBehaviour
{
    Question question;
    public int level,orjLevel;
    public int point;
    public List<string> levelTexts;
    public List<float> levelTimes;
    public List<Color> colors;
    public List<Sprite> shapes;
    public Image questionImage;
    public List<Image> answerImages;
    public int correctColor, correctShape, correctAnswer, shapeOrColor ;
    public AudioSource source;
    public AudioClip correctSound, wrongSound;

    public List<int> answerShapeList,answerColorList,diffShapeList,diffColorList;
    // Start is called before the first frame update
    void Start()
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
        orjLevel = level;
        question.tutorialText = levelTexts[level];
        question.maxQuestionTime = levelTimes[level];
        question.Init();
    }
    void SetLevel()
    {
        switch (level)
        {
            case 0:
                PrepareAnswer(7);
                PrepareQuestion(true);
                break;
            case 1:
                PrepareAnswer(7);
                PrepareQuestion(false,false);
                break;
            case 2:
                PrepareAnswer(7);
                PrepareQuestion(false, true);
                break;
            case 3:
                level = UnityEngine.Random.RandomRange(1, 3);
                SetLevel();
                break;
            case 4:
                level = UnityEngine.Random.RandomRange(0, 3);
                SetLevel();
                break;
            case 5:
                PrepareAnswer(shapes.Count);
                PrepareQuestion(true);
                break;
            case 6:
                PrepareAnswer(shapes.Count);
                PrepareQuestion(false, false);
                break;
            case 7:
                PrepareAnswer(shapes.Count);
                PrepareQuestion(false, true);
                break;
            case 8:
                level = UnityEngine.Random.RandomRange(6, 8);
                SetLevel();
                break;
            case 9:
                level = UnityEngine.Random.RandomRange(5, 8);
                SetLevel();
                break;
            default:
                level = UnityEngine.Random.RandomRange(5, 8);
                SetLevel();
                break;
        }
    }
    void PrepareQuestion(bool isFullSame,bool isFullDiffirent=false)
    {
        if (isFullSame)
        {
            question.questionTitleText.text = levelTexts[0];

            correctAnswer = UnityEngine.Random.RandomRange(0, answerImages.Count);
            questionImage.color = colors[answerColorList[correctAnswer]];
            questionImage.transform.GetChild(0).GetComponent<Image>().sprite = shapes[answerShapeList[correctAnswer]];
            return;
        }

        if (!isFullDiffirent)
        {
            correctAnswer = UnityEngine.Random.RandomRange(0, answerImages.Count);

            shapeOrColor = Random.Range(0, 2); 
            if(shapeOrColor != 0)
            {
                questionImage.color = colors[answerColorList[correctAnswer]];
                int secondIndex = diffShapeList[ Random.Range(0, diffShapeList.Count) ];

                questionImage.transform.GetChild(0).GetComponent<Image>().sprite = shapes[secondIndex];

            }
            else
            {
                questionImage.transform.GetChild(0).GetComponent<Image>().sprite = shapes[answerShapeList[correctAnswer]];

                int secondIndex = diffColorList[Random.Range(0, diffColorList.Count)];

                questionImage.color = colors[secondIndex];
            }


            question.questionTitleText.text = levelTexts[1];
        }
        else
        {
            correctAnswer = UnityEngine.Random.RandomRange(0, answerImages.Count);

            int firstIndex = correctAnswer;

            while (firstIndex == correctAnswer)
            {
                firstIndex = Random.Range(0, answerImages.Count); // Ýkinci sayýnýn indisini rastgele seç (farklý olana kadar)
            }

            int secondIndex = correctAnswer;

            while (secondIndex == correctAnswer || secondIndex == firstIndex)
            {
                secondIndex = Random.Range(0, answerImages.Count); // Ýkinci sayýnýn indisini rastgele seç (farklý olana kadar)
            }
            questionImage.color = colors[answerColorList[firstIndex]];
            questionImage.transform.GetChild(0).GetComponent<Image>().sprite = shapes[answerShapeList[secondIndex]];

            question.questionTitleText.text = levelTexts[2];
        }

    }
    void PrepareAnswer(int max)
    {
        //if (answerShapeList.Count > 0)
        //    return;

        answerShapeList.Clear();
        answerColorList.Clear();
        diffColorList.Clear();
        diffShapeList.Clear();
        for (int i = 0; i < max; i++)
            diffShapeList.Add(i);

        for (int i = 0; i < colors.Count; i++)
            diffColorList.Add(i);

        while (answerShapeList.Count < 3)
        {
            int randomNumber = UnityEngine.Random.RandomRange(0, max); 

            if (!answerShapeList.Contains(randomNumber)) // Sayýyý kontrol ediyoruz.
            {
                answerShapeList.Add(randomNumber); // Eðer listede yoksa ekliyoruz.
                diffShapeList.Remove(randomNumber);
            }
        }
        while (answerColorList.Count < 3)
        {
            int randomNumber = UnityEngine.Random.RandomRange(0, colors.Count); 

            if (!answerColorList.Contains(randomNumber)) // Sayýyý kontrol ediyoruz.
            {
                answerColorList.Add(randomNumber); // Eðer listede yoksa ekliyoruz.
                diffColorList.Remove(randomNumber);
            }
        }

        for(int i=0;i<answerColorList.Count;i++)
        {
            answerImages[i].color = colors[ answerColorList[i] ];
            answerImages[i].transform.GetChild(0).GetComponent<Image>().sprite = shapes[answerShapeList[i]];
        }
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

        ResetLevel();
        SetLevel();
    }

    private void ResetLevel()
    {
        level = orjLevel;
    }
}
