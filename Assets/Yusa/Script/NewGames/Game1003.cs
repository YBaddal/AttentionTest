using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1003 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public Image circle1,circle2;
    public Text circleText;
    public int firstRandom, secondRandom;
    public Button correctButton, failButton;
    public AudioSource audioSource;
    public List<Color> colorList;
    public List<string> colorListString;
    public List<AudioClip> colorListAudio;
    public List<Vector2> waitTimes;
    public List<string> levelTexts;
    public List<float> levelTimes;
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
    public void OnPressCorrect()
    {
        CloseAll();
        if (firstRandom == secondRandom)
            EarnPoint();
    }
    public void OnPressFail()
    {
        CloseAll();
        if (firstRandom != secondRandom)
            EarnPoint();
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
    void WaitForNext()
    {
        float rnd = Random.RandomRange(waitTimes[level].x, waitTimes[level].y + 1);
        Invoke("SetLevel", rnd);
    }
    void SetLevel()
    {
        CloseAll();
        switch (level)
        {
            case 0:
                PrepareLevel(true,6);
                break;
            case 1:
                PrepareLevel(false, 6);
                break;
            case 2:
                PrepareLevel(true, 7);
                break;
            case 3:
                PrepareLevel(true, 7);
                break;
            case 4:
                PrepareLevel(false, 7);
                break;
            case 5:
                PrepareLevel(false, 7);
                break;
            case 6:
                PrepareLevel(true, 7);
                break;
            case 7:
                PrepareLevel(true, 9);
                break;
            case 8:
                PrepareLevel(false, 9);
                break;
            case 9:
                PrepareLevel(false, 9);
                break;
                default:
                PrepareLevel(false, 9);
                break;
        }
    }

    void PrepareLevel(bool isText,int colorCount)
    {
        correctButton.interactable = true;
        failButton.interactable = true;

        int chance = Random.RandomRange(0, 2); //Eþit çýkma þansýný yükseltmek için
        if (chance == 0)
        {
            firstRandom = Random.RandomRange(0, colorCount);
            secondRandom = Random.RandomRange(0, colorCount);
        }
        else
        {
            firstRandom = Random.RandomRange(0, colorCount);
            secondRandom = firstRandom;
        }


        circle1.transform.parent.gameObject.SetActive(true);

        if (isText)
        {
            circleText.gameObject.SetActive(true);
            circle1.color = colorList[firstRandom];
            circleText.text = colorListString[secondRandom];
        }
        else
        {
            circle1.color = colorList[firstRandom];
            circle2.color = colorList[secondRandom];
            circle2.transform.parent.gameObject.SetActive(true);
        }

        if(level == 0)
        {
            audioSource.PlayOneShot(colorListAudio[secondRandom]);
            circleText.gameObject.SetActive(false);
            audioSource.PlayOneShot(colorListAudio[secondRandom]);
        }else if(level == 1)
        {
            audioSource.PlayOneShot(colorListAudio[firstRandom]);
            Invoke("SetSecondAudio", 0.75f);

        }
    }
    void SetSecondAudio()
    {
        audioSource.PlayOneShot(colorListAudio[secondRandom]);
    }
    void CloseAll()
    {
        CancelInvoke();
        circle1.transform.parent.gameObject.SetActive(false);
        circle2.transform.parent.gameObject.SetActive(false);
        circleText.gameObject.SetActive(false);
        correctButton.interactable = false;
        failButton.interactable = false;

        if(!question.isFinish)
            WaitForNext();

    }
}
