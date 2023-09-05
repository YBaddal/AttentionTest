using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1043 : MonoBehaviour
{
    Question question;
    public int level, orjLevel;
    public int point;
    public List<string> levelTexts;
    public List<float> levelTimes; 
    public int correctAnswer;
    public List<GameObject> fromPlaneList,toPlaneList;
    public AudioSource source;
    public AudioClip correctSound, wrongSound,fromClip,toClip;
    void Start()
    {
       

    }
    private void OnEnable()
    {
        question = GetComponent<Question>();
        Init();
        SetLevel();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
            CheckAnswer(0);

        if (Input.GetKeyUp(KeyCode.DownArrow))
            CheckAnswer(1);

        if (Input.GetKeyUp(KeyCode.RightArrow))
            CheckAnswer(2);

        if (Input.GetKeyUp(KeyCode.LeftArrow))
            CheckAnswer(3);
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
                PrepareLevel(false);
                break;
            case 1:
                PrepareLevel(true);
                break;
            case 2:
                level = UnityEngine.Random.RandomRange(0, 2);
                SetLevel();
                break;
            case 3:
                PrepareLevel(false);
                break;
            case 4:
                PrepareLevel(true);
                break;
            case 5:
                level = UnityEngine.Random.RandomRange(3, 5);
                SetLevel();
                break;
            case 6:
                PrepareLevel(false);
                break;
            case 7:
                PrepareLevel(true);
                break;
            case 8:
                level = UnityEngine.Random.RandomRange(6, 8);
                SetLevel();
                break;
            default:
                level = UnityEngine.Random.RandomRange(0, 8);
                SetLevel();
                break;
        }
    }
    void PrepareLevel(bool isfrom)
    {
        correctAnswer = UnityEngine.Random.RandomRange(0, fromPlaneList.Count);

        if (isfrom)
        { 
            fromPlaneList[correctAnswer].SetActive(true);
            if(level<=2)
                PlaySound(fromClip);
        }
        else
        { 
            toPlaneList[correctAnswer].SetActive(true);
            if (level <= 2)
                PlaySound(toClip);
        }
    }
    public void CheckAnswer(int answer)
    {
        if (correctAnswer == answer)
        {
            EarnPoint();
            PlaySound(correctSound);
        }
        else
            PlaySound(wrongSound);

        ResetLevel();
        SetLevel();
    }
    void PlaySound(AudioClip clip)
    {
        source.Stop();
        source.PlayOneShot(clip);
    }
    private void ResetLevel()
    {
        level = orjLevel;
        foreach (GameObject obj in fromPlaneList)
            obj.SetActive(false);

        foreach (GameObject obj in toPlaneList)
            obj.SetActive(false);
    }
}
