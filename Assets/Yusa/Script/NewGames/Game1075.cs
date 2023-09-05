using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1075 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public List<float> levelTimes;
    public AudioSource source;
    public AudioClip correctSound, wrongSound;

    public Image questionImage;
    public List<Button> buttonList;
    public List<Sprite> spriteList;
    public List<Color> colorList;

    List<(int spriteIndex,int colorIndex)> selectedSprites;

    public int correctAnswer;
 
    void Start()
    {
        
    }
    private void OnEnable()
    {
        selectedSprites = new List<(int spriteIndex, int colorIndex)>();
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
        question.Init();
    }
    void SetLevel()
    {
        switch (level)
        {
            case 0:
                PrepareLevel(50);
                break;
            case 1:
                PrepareLevel(50);
                break;
            case 2:
                PrepareLevel(100);
                break;
            case 3:
                PrepareLevel(100);
                break;
            case 4:
                PrepareLevel(100);
                break;
            default:
                PrepareLevel(100);
                break;
        }
    }
    void PrepareLevel(int maxSpritecount)
    {
        for ( int i = 0; i < buttonList.Count; i++)
        {
            int rndSprite = UnityEngine.Random.RandomRange(0, maxSpritecount);
            int rndColor = UnityEngine.Random.RandomRange(0, colorList.Count);
           
            selectedSprites.Add((rndSprite, rndColor));
            buttonList[i].image.sprite=spriteList[rndSprite];
            buttonList[i].image.color=colorList[rndColor];
        }

        SetAnswer();
    }

    public void CheckAnswer(int answer)
    {
        if (selectedSprites[correctAnswer].spriteIndex == selectedSprites[answer].spriteIndex && selectedSprites[correctAnswer].colorIndex == selectedSprites[answer].colorIndex)
        {
            EarnPoint();
            source.PlayOneShot(correctSound);
        }
        else
            source.PlayOneShot(wrongSound);

        SetAnswer();
    }
    void SetAnswer()
    {
        correctAnswer = UnityEngine.Random.RandomRange(0, selectedSprites.Count);

        questionImage.sprite = spriteList[selectedSprites[correctAnswer].spriteIndex];
        questionImage.color = colorList[selectedSprites[correctAnswer].colorIndex];
    }

}
