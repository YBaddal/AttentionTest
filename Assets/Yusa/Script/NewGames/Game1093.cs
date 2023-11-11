using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1093 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public int correctAnswer;
    public AudioSource source;
    public AudioClip correctSound, wrongSound;
    public List<int> waitTimes;
    public List<string> levelTexts;
    public List<float> levelTimes;
    public List<GameObject> questionArrows,oppositeArrows,decoy1,decoy2;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
            CheckAnswer(2);

        if (Input.GetKeyUp(KeyCode.DownArrow))
            CheckAnswer(3);

        if (Input.GetKeyUp(KeyCode.RightArrow))
            CheckAnswer(1);

        if (Input.GetKeyUp(KeyCode.LeftArrow))
            CheckAnswer(0);
    }
    private void OnEnable()
    {
        question = GetComponent<Question>();
        Init();
        SetLevel();
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
        CancelInvoke("SetLevel");
        Invoke("SetLevel", waitTimes[level]);
    }
    void SetLevel()
    {
        ResetLevel();
        switch (level)
        {
            case 0:
                PrepareLevel(false);
                break;
            case 1:
                PrepareLevel(false);
                break;
            case 2:
                PrepareLevel(true);
                break;
            case 3:
                PrepareLevel(false);
                break;
            case 4:
                PrepareLevel(true);
                break;
            default:
                PrepareLevel(true);
                break;
        }
    }

    void PrepareLevel(bool isRandom)
    {
        correctAnswer = Random.RandomRange(0, questionArrows.Count);
        if (isRandom)
        {
            int random = Random.RandomRange(0, 2);
            if (random == 0)
            { 
                questionArrows[correctAnswer].SetActive(true);
                questionArrows[correctAnswer].GetComponent<Image>().color = Color.white;
            }
            else
            { 
                oppositeArrows[correctAnswer].SetActive(true);
                oppositeArrows[correctAnswer].GetComponent<Image>().color = Color.yellow;
            }
        }
        else
            questionArrows[correctAnswer].SetActive(true);

        decoy1[Random.RandomRange(0, decoy1.Count)].SetActive(true);
        decoy2[Random.RandomRange(0, decoy2.Count)].SetActive(true);
    }

    void CheckAnswer(int answer)
    {
        if (answer == correctAnswer)
            Correct();
        else
            Fail();
    }

    void Correct()
    {
        EarnPoint();
        source.PlayOneShot(correctSound);
        SetLevel();
    }
    void EarnPoint()
    {
        question.point += point;
    }
    void Fail()
    {
        source.PlayOneShot(wrongSound);
        SetLevel();
    }
    private void ResetLevel()
    {
      for(int i = 0; i < questionArrows.Count; i++)
      {
            questionArrows[i].SetActive(false);
            oppositeArrows[i].SetActive(false);
            decoy1[i].SetActive(false);
            decoy2[i].SetActive(false);
      }

        if (!question.isFinish)
            WaitForNext();
    }

}
