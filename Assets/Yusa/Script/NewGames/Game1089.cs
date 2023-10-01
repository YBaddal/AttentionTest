using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Game1089 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public List<string> levelTexts;
    public List<float> levelTimes;
    public int correctAnswer, correctBGColor, correctFGColor, correctShape;
    public AudioSource source;
    public AudioClip correctSound, wrongSound;
    public List<Button> seperatedButtons, combinedButtons;
    public List<Color> bgColors;
    public List<Sprite> sprites;
    public List<AudioClip> colorAudio, spriteAudio,playClip;
    int currentClip;
    bool isPlayingClip;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        if (!isPlayingClip)
            return;

        if (!source.isPlaying)
        {
            if(currentClip < playClip.Count-1)
            {
                currentClip++;
                source.clip = playClip[currentClip];
                source.Play();
            }
            else
            {
                isPlayingClip = false;
            }

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
        question.tutorialText = levelTexts[level];
        question.Init();
        question.maxQuestionTime = levelTimes[level];
        question.questionTime = 1;
    }

    void SetLevel()
    {
        int rnd = Random.RandomRange(5,8);
        switch (level)
        {
            case 0:
                PrepareLevel(true, false,3,4);
                break;
            case 1:
                PrepareLevel(true, true, 3, 4);
                break;
            case 2:
                PrepareLevel(true, true, 4, 4);
                break;
            case 3:
                PrepareLevel(false, true, 3, 4);
                break;
            case 4:
                PrepareLevel(true, false, 4, 4);
                break;
            case 5:
                PrepareLevel(false, true, 3, 4);
                break;
            case 6:
                PrepareLevel(true, true, 4, 4);
                break;
            case 7:
                PrepareLevel(false, true, 4, 4);
                break;
            case 8:
                PrepareLevel(true, true, 4, 4);
                break;
            case 9:
                PrepareLevel(true, true, 4, 6);
                break;
            case 10:
                PrepareLevel(true, true, 5, 6);
                break;
            case 11:
                PrepareLevel(true, true, 6, 6);
                break;
            case 12:
                PrepareLevel(false, true, rnd, 4);
                break;
            default:
                PrepareLevel(false, true, rnd, 4);
                break;
        }
    }

    void PrepareLevel(bool isSeperated,bool randomBg,int answerCount, int maxShape)
    {
        ResetLevel();
        correctAnswer = Random.RandomRange(0, answerCount);
        correctBGColor = randomBg ? Random.RandomRange(2, bgColors.Count) : 0;
        correctFGColor = Random.RandomRange(1, 5);
        correctShape = Random.RandomRange(0, maxShape);

        for(int i = 0; i < answerCount; i++)
        {
            if(isSeperated)
            {
                if (i == correctAnswer)
                {
                    seperatedButtons[i].transform.parent.gameObject.SetActive(true);
                    seperatedButtons[i].image.sprite = sprites[correctShape];
                    seperatedButtons[i].image.color = bgColors[correctBGColor];
                    seperatedButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = sprites[correctShape];
                    seperatedButtons[i].transform.GetChild(0).GetComponent<Image>().color = bgColors[correctFGColor];
                }
                else
                {
                    int wrongShape=-1;
                    int wrongBGColor=-1;
                    int wrongFGColor=-1;
                    bool allSame = true;
                    while (allSame)
                    {
                        wrongShape = Random.Range(0, maxShape);
                        wrongBGColor = randomBg ? Random.RandomRange(2, bgColors.Count) : 0;
                        wrongFGColor = Random.RandomRange(1, 5);
                        if (wrongShape == correctShape && wrongBGColor == correctBGColor && wrongFGColor == correctFGColor)
                            allSame = true;
                        else
                            allSame = false;
                    }

                    seperatedButtons[i].transform.parent.gameObject.SetActive(true);
                    seperatedButtons[i].image.sprite = sprites[wrongShape];
                    seperatedButtons[i].image.color = bgColors[wrongBGColor];
                    seperatedButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = sprites[wrongShape];
                    seperatedButtons[i].transform.GetChild(0).GetComponent<Image>().color = bgColors[wrongFGColor];                 
                }

            }
            else
            {
                if (i == correctAnswer)
                {
                    combinedButtons[i].gameObject.SetActive(true);
                    combinedButtons[i].image.sprite = sprites[correctShape];
                    combinedButtons[i].image.color = bgColors[correctBGColor];
                    combinedButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = sprites[correctShape];
                    combinedButtons[i].transform.GetChild(0).GetComponent<Image>().color = bgColors[correctFGColor];
                }
                else
                {
                    int wrongShape = -1;
                    int wrongBGColor = -1;
                    int wrongFGColor = -1;
                    bool allSame = true;
                    while (allSame)
                    {
                        wrongShape = Random.Range(0, maxShape);
                        wrongBGColor = randomBg ? Random.RandomRange(2, bgColors.Count) : 0;
                        wrongFGColor = Random.RandomRange(1, 5);
                        if (wrongShape == correctShape && wrongBGColor == correctBGColor && wrongFGColor == correctFGColor)
                            allSame = true;
                        else
                            allSame = false;
                    }

                    combinedButtons[i].gameObject.SetActive(true);
                    combinedButtons[i].image.sprite = sprites[wrongShape];
                    combinedButtons[i].image.color = bgColors[wrongBGColor];
                    combinedButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = sprites[wrongShape];
                    combinedButtons[i].transform.GetChild(0).GetComponent<Image>().color = bgColors[wrongFGColor];
                }

            }
        }

        playClip[0] = colorAudio[correctBGColor];
        playClip[2] = colorAudio[correctFGColor];
        playClip[3] = spriteAudio[correctShape];


        currentClip = 0;
        source.clip = playClip[currentClip];
        source.Play();
        isPlayingClip = true;
    }

    public void CheckAnswer(int answer)
    {

        if (answer == correctAnswer)
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
        foreach (var sbutton in seperatedButtons)
        {
            //sbutton.onClick.RemoveAllListeners();
            sbutton.transform.parent.gameObject.SetActive(false);
        }
        foreach (var cbutton in combinedButtons)
        {
            //cbutton.onClick.RemoveAllListeners();
            cbutton.gameObject.SetActive(false); 
        }
    }
}
