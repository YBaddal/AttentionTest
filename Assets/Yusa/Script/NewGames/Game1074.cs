using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1074 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public List<float> levelTimes;
    public List<string> levelTexts;
    public AudioSource source;
    public AudioClip correctSound, wrongSound;

    public bool visibleNext;
    public Transform questionContent;
    public GameObject questionPrefab;
    public int prefabCount, colorListCount, spriteListCount;
    public List<Question1Generator> generatedList;
    public List<Button> buttons;
    public GameObject continueButton;
    public int continueCount;
    public void StartGame()
    {
        if (!visibleNext)
        {
            questionContent.GetChild(0).gameObject.SetActive(false);
            questionContent.GetChild(1).gameObject.SetActive(true);

        }
        if (continueCount > 0)
            continueButton.SetActive(true);
        else
            buttons[0].transform.parent.gameObject.SetActive(true);
    }
    public void ContinueGame()
    {
        if (!visibleNext)
        {
            questionContent.GetChild(1).gameObject.SetActive(false);
            questionContent.GetChild(2).gameObject.SetActive(true);

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
                PrepareLevel(0);
                break;
            case 1:
                PrepareLevel(0);
                break;
            case 2:
                PrepareLevel(0);
                break;
            case 3:
                PrepareLevel(0);
                break;
            case 4:
                PrepareLevel(1);
                break;
            case 5:
                PrepareLevel(0);
                break;
            case 6:
                PrepareLevel(0);
                break;
            case 7:
                PrepareLevel(0);
                break;
            case 8:
                PrepareLevel(1);
                break;
            default:
                PrepareLevel(0);
                break;
        }
    }
    void PrepareLevel(int _continueCount)
    {
        continueCount = _continueCount;
        generatedList = new List<Question1Generator>();
        Question1Generator g = new Question1Generator { ColorID = -1, SpriteID = -1 };
        g.ColorID = Random.RandomRange(0, colorListCount);
        g.SpriteID = Random.RandomRange(0, spriteListCount);
        generatedList.Add(g);
        for (int i = 1; i < prefabCount; i++)
        {
            Question1Generator generated = new Question1Generator { ColorID = -1, SpriteID = -1 };
            int rnd = Random.RandomRange(0, 2);
            if (rnd == 0)
            {
                generated.SpriteID = generatedList[i - 1].SpriteID;

                generated.ColorID = Random.RandomRange(0, colorListCount);
            }
            else
            {
                generated.ColorID = generatedList[i - 1].ColorID;
                generated.SpriteID = Random.RandomRange(0, spriteListCount);

            }
            generatedList.Add(generated);
        }
        SetQuestion();
    }
    void SetQuestion()
    {
        foreach (var generated in generatedList)
        {
            var obj = Instantiate(questionPrefab, questionContent);
            obj.SetActive(visibleNext);
            obj.GetComponent<Question1PrefabCell>().colorID = generated.ColorID;
            obj.GetComponent<Question1PrefabCell>().spriteID = generated.SpriteID;
            obj.GetComponent<Question1PrefabCell>().Init();
            obj.SetActive(visibleNext);
        }
        if (!visibleNext)
            questionContent.GetChild(0).gameObject.SetActive(true);
    }
    public void CheckAnswer(int answer)
    {
        Question1PrefabCell cell0 = questionContent.transform.GetChild(0).GetComponent<Question1PrefabCell>();
        Question1PrefabCell cell1 = questionContent.transform.GetChild(1 + continueCount).GetComponent<Question1PrefabCell>();

        switch (answer)
        {
            case 0:
                if (cell0.spriteID == cell1.spriteID)
                {
                    Debug.Log("Ayný");
                    EarnPoint();
                    source.PlayOneShot(correctSound);
                }
                else
                    source.PlayOneShot(wrongSound);
                break;
            case 1:
                if (cell0.spriteID != cell1.spriteID)
                {
                    Debug.Log("Farklý");
                    EarnPoint();
                    source.PlayOneShot(correctSound);
                }
                else
                    source.PlayOneShot(wrongSound);
                break;
        }
        foreach (var but in buttons)
            but.interactable = false;
        Invoke("Next", 0.5f);
    }
    void Next()
    {
        foreach (var but in buttons)
            but.interactable = true;

        Question1PrefabCell cell0 = questionContent.transform.GetChild(0).GetComponent<Question1PrefabCell>();
        Question1PrefabCell cell1 = questionContent.transform.GetChild(1 + continueCount).GetComponent<Question1PrefabCell>();

        if (!visibleNext)
        {
            cell1.gameObject.SetActive(false);
            questionContent.transform.GetChild(2 + continueCount).gameObject.SetActive(true);

        }

        Destroy(cell0.gameObject);

    }
}
