using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1014 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public List<string> levelTexts;
    public List<float> levelTimes;
    public List<Vector2> levelDesign;

    public List<Sprite> sprites;
    public List<string> strings;
    public List<string> words;
    public List<Image> questionImg;
    public List<Text> questionTexts;
    public Text questionWord;
    public List<Sprite> questionSprites;
    public List<string> questionStrings;
    public List<Sprite> answerSprites;
    public List<string> answerStrings;
    public List<Toggle> imageToggle, textToggle, wordToggle;
    public AudioSource source;
    public AudioClip correctSound, wrongSound;
    public List<Toggle> selectedToggle;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        answerSprites = new List<Sprite>();
        questionStrings = new List<string>();
        selectedToggle = new List<Toggle>();
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
        ResetLevel();
        switch (level)
        {
            case 0:
                PrepareRandom();
                break;
            case 1:
                PrepareTexts(0,10);
                break;
            case 2:
                PrepareImages(0,sprites.Count);
                break;
            case 3:
                PrepareTexts(0,10);
                break;
            case 4:
                PrepareTexts(11,strings.Count);
                break;
            case 5:
                PrepareWords();
                break;
            case 6:
                PrepareImages(0,sprites.Count);
                break;
            case 7:
                PrepareTexts(0,10);
                break;
            case 8:
                PrepareTexts(11,strings.Count);
                break;
            case 9:
                PrepareWords();
                break;
            default:
                PrepareRandom();
                break;
        }
    }
    void PrepareRandom()
    {
        int rnd = Random.RandomRange(0, 4);
        switch (rnd)
        {
            case 0:
                PrepareWords();
                break;
            case 1:
                PrepareTexts(0, 10);
                break;
            case 2:
                PrepareImages(0, sprites.Count);
                break;
            case 3:
                PrepareTexts(11, strings.Count);
                break;
                default:
                PrepareImages(0, sprites.Count);
                break;
        }
    }
    void PrepareImages(int min,int max)
    {
        int imgType = Random.RandomRange(0, 3);
        switch (imgType)
        {
            case 0:
                min = 0;
                max = 51;
                break;
            case 1:
                min = 51;
                max = 100;
                break;
            case 2:
                min = 100;
                max = max;
                break;
            default:
                min = min;
                max = max;
                break;
        }
        int answerCount = (int)levelDesign[level].x * (int)levelDesign[level].y;
        imageToggle[0].GetComponentInParent<GridLayoutGroup>().constraintCount = (int)levelDesign[level].x;
        int random = Random.RandomRange(min, max);
        questionSprites.Add(sprites[random]);
        //questionImg[questionSprites.Count - 1].transform.parent.gameObject.SetActive(true);
        questionImg[0].sprite = sprites[random];

        while (answerSprites.Count < questionSprites.Count)
        {
            int rnd = Random.RandomRange(min, max);
            if (!questionSprites.Contains(sprites[rnd]))
            {
                answerSprites.Add(sprites[rnd]);

            }
        }

        for (int i = 0; i < answerCount; i++)
        {
            imageToggle[i].gameObject.SetActive(true);
            imageToggle[i].transform.GetChild(0).GetComponent<Image>().sprite = answerSprites[0];
        }

        int correct = Random.RandomRange(0, answerCount);
        imageToggle[correct].transform.GetChild(0).GetComponent<Image>().sprite = questionImg[0].sprite;
    }
    void PrepareTexts(int min, int max)
    {
        int answerCount = (int)levelDesign[level].x * (int)levelDesign[level].y;
        textToggle[0].GetComponentInParent<GridLayoutGroup>().constraintCount = (int)levelDesign[level].x;

        int random = Random.RandomRange(min,max);
        //int randomColor = Random.RandomRange(0, colors.Count);
        
        questionStrings.Add(strings[random]);
        //questionTexts[questionStrings.Count - 1].transform.parent.gameObject.SetActive(true);
        questionTexts[0].text = strings[random];
        //questionTexts[0].color = colors[randomColor];

        int rnd=0;
        //int rndColor=0;
        while (answerStrings.Count < questionStrings.Count)
        {
            rnd = Random.RandomRange(min,max);
            //rndColor = Random.RandomRange(0, colors.Count);

            if (!questionStrings.Contains(strings[rnd]))
            {
                answerStrings.Add(strings[rnd]);

            }
        }

        for (int i = 0; i < answerCount; i++)
        {
            textToggle[i].gameObject.SetActive(true);
            textToggle[i].transform.GetChild(1).GetComponent<Text>().text = strings[rnd];
            //textToggle[i].transform.GetChild(1).GetComponent<Text>().color = colors[rndColor];
        }
        int correct = Random.RandomRange(0, answerCount);
        textToggle[correct].transform.GetChild(1).GetComponent<Text>().text = questionTexts[0].text;
        //textToggle[correct].transform.GetChild(1).GetComponent<Text>().color = questionTexts[0].color;
    }
    void PrepareWords()
    {
        int answerCount = (int)levelDesign[level].x * (int)levelDesign[level].y;
        wordToggle[0].GetComponentInParent<GridLayoutGroup>().constraintCount = (int)levelDesign[level].x;

        int random = Random.RandomRange(0, words.Count);
        questionWord.transform.parent.gameObject.SetActive(true);
        questionStrings.Add(words[random]);
        questionWord.text = words[random];

        //var splitted = words[random].Split("");
        while (answerStrings.Count < questionStrings.Count)
        {
            string str = GenerateWord(words[random]);

            if (!answerStrings.Contains(str))
            {
                answerStrings.Add(str);

            }
        }
        for (int i = 0; i < answerCount; i++)
        {
            wordToggle[i].gameObject.SetActive(true);
            wordToggle[i].transform.GetChild(1).GetComponent<Text>().text = answerStrings[0];
        }

        int correct = Random.RandomRange(0, answerCount);
        wordToggle[correct].transform.GetChild(1).GetComponent<Text>().text = questionWord.text;

    }
    public void CheckAnswer(Toggle toggle)
    {
        if (!toggle.isOn)
        {
            selectedToggle.Remove(toggle);
        }
        else
            selectedToggle.Add(toggle);

        if (selectedToggle.Count != questionSprites.Count || selectedToggle.Count == 0)
            return;

        int correct = 0;
        for (int i = 0; i < selectedToggle.Count; i++)
        {
            if (questionSprites.Contains(selectedToggle[i].transform.GetChild(0).GetComponent<Image>().sprite))
                correct++;
        }
        if (correct == questionSprites.Count)
            Invoke("Correct", 0.5f);
        else
            Invoke("Fail", 0.5f);
    }
    public void CheckTextAnswer(Toggle toggle)
    {
        if (!toggle.isOn)
        {
            selectedToggle.Remove(toggle);
        }
        else
            selectedToggle.Add(toggle);

        if (selectedToggle.Count != questionStrings.Count || selectedToggle.Count == 0)
            return;

        int correct = 0;
        for (int i = 0; i < selectedToggle.Count; i++)
        {
            if (questionStrings.Contains(selectedToggle[i].transform.GetChild(1).GetComponent<Text>().text))
                correct++;
        }
        if (correct == questionStrings.Count)
            Invoke("Correct", 0.5f);
        else
            Invoke("Fail", 0.5f);

    }

    void Correct()
    {
        EarnPoint();
        source.PlayOneShot(correctSound);
        question.questionTime++;
        SetLevel();
    }
    void Fail()
    {
        source.PlayOneShot(wrongSound);
        question.questionTime++;
        SetLevel();
    }
    private void ResetLevel()
    {
        answerSprites.Clear();
        questionSprites.Clear();
        questionStrings.Clear();
        answerStrings.Clear();
        selectedToggle.Clear();
        questionWord.transform.parent.gameObject.SetActive(false);
        foreach (var qImage in questionImg)
            qImage.transform.parent.gameObject.SetActive(false);

        foreach (var togg in imageToggle)
        {
            togg.isOn = false;
            togg.gameObject.SetActive(false);
        }
        foreach (var togg in textToggle)
        {
            togg.isOn = false;
            togg.gameObject.SetActive(false);
        }
        foreach (var togg in wordToggle)
        {
            togg.isOn = false;
            togg.gameObject.SetActive(false);
        }
    }

    string GenerateWord(string kelime)
    {
        char[] harfDizisi = kelime.ToCharArray();


        int rastgeleIndex = Random.Range(0, harfDizisi.Length);
        char yeniHarf = (char)Random.Range('A', 'Z');

        while (yeniHarf == harfDizisi[rastgeleIndex])
        {
            yeniHarf = (char)Random.Range('A', 'Z');
        }

        harfDizisi[rastgeleIndex] = yeniHarf;

        return new string(harfDizisi);
    }
}
