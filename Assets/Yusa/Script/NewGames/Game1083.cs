using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1083 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public List<string> levelTexts;
    public List<float> levelTimes;
    public AudioSource source;
    public AudioClip correctSound, wrongSound;

    public List<Sprite> spriteList;
    public Transform leftSide,rightSide;
    public List<int> selectedLeft,selectedRight;
    public Toggle leftToggle,rightToggle;
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
        question.Init();
        question.maxQuestionTime = levelTimes[level];
        question.questionTime = 1;
    }
    void SetLevel()
    {
        switch (level)
        {
            case 0:
                PrepareLevel(21, 0, 50, true);
                break;
            case 1:
                PrepareLevel(21, 0, 50, false);
                break;
            case 2:
                PrepareLevel(20, 0, 50, true);
                break;
            case 3:
                PrepareLevel(20, 0, 50, false);
                break;
            case 4:
                PrepareLevel(23, 0, 50, true);
                break;
            default:
                PrepareLevel(23, 0, 50, false);
                break;
        }
    }
    void PrepareLevel(int count,int min,int max,bool isAllSame)
    {
        while (selectedLeft.Count < count)
        {
            int randomNumber = UnityEngine.Random.RandomRange(min, max); 

            if (!selectedLeft.Contains(randomNumber)) 
            {
                selectedLeft.Add(randomNumber); 
            }
        }
        if (isAllSame)
        {
            selectedRight=selectedLeft;
            bool diff = false;
            while (!diff)
            {
                int rnd = UnityEngine.Random.RandomRange(min, selectedLeft.Count);

                if (!selectedLeft.Contains(rnd))
                {
                    selectedRight[0] = rnd;
                    diff = true;
                }
            }
        }
        else
        {
            int rnd = UnityEngine.Random.RandomRange(min, selectedLeft.Count);

            while (selectedRight.Count < count)
            {
                int randomNumber = UnityEngine.Random.RandomRange(min, max);

                if (!selectedLeft.Contains(randomNumber))
                {
                    selectedRight.Add(randomNumber);
                }
            }
            selectedRight[rnd]=selectedLeft[rnd];
        }
       

        for (int i = 0; i < leftSide.childCount; i++)
        {
            int rndLeft = Random.RandomRange(0, leftSide.childCount);
            int rndRight = Random.RandomRange(0, leftSide.childCount);

            leftSide.GetChild(i).SetSiblingIndex(rndLeft);
            rightSide.GetChild(i).SetSiblingIndex(rndRight);
        }
        for (int i = 0; i < leftSide.childCount; i++)
        {
            if (i < count)
            {
                leftSide.GetChild(i).gameObject.SetActive(true);
                rightSide.GetChild(i).gameObject.SetActive(true);
                leftSide.GetChild(i).GetComponent<Toggle>().image.sprite = spriteList[selectedLeft[i]];
                rightSide.GetChild(i).GetComponent<Toggle>().image.sprite = spriteList[selectedRight[i]];
            }
            else
            { 
                leftSide.GetChild(i).gameObject.SetActive(false);
                rightSide.GetChild(i).gameObject.SetActive(false);
            }
        }
      
    }

    public void CheckAnswer()
    {
        if (true)
        {
            EarnPoint();
            source.PlayOneShot(correctSound);
        }
        else
            source.PlayOneShot(wrongSound);

        Invoke("ResetLevel", 0.25f);
    }

    public void SelectToggle()
    {
        ToggleGroup ltg = leftSide.GetComponent<ToggleGroup>();
        leftToggle = ltg.GetFirstActiveToggle(); 
        ToggleGroup rtg = rightSide.GetComponent<ToggleGroup>();
        rightToggle = rtg.GetFirstActiveToggle();

        if (leftToggle != null && rightToggle != null)
            CheckAnswer();
    }
    void ResetLevel()
    {
        leftSide.GetComponent<ToggleGroup>().SetAllTogglesOff();
        rightSide.GetComponent<ToggleGroup>().SetAllTogglesOff();
        leftToggle = null;
        rightToggle = null;
        SetLevel();
    }
}