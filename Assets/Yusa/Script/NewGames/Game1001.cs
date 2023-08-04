using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1001 : MonoBehaviour
{
    public int level;
    public int point;
    public List<string> levelTexts;
    public List<float> levelTimes;

    public List<Vector2> waitTimes;
    public List<Color> colors;
    public List<Image> circles;
    public List<Button> answers;

    public int correctCount;
    bool isFinished;
    Question question;

    // Start is called before the first frame update
    void Start()
    {
        question = GetComponent<Question>();
        Init();
        WaitForNext();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFinished)
            return;

        if (question.questionTime <= 1)
        { 
            question.isFinish = true;
            SetAnswer();
        }
    }
    void Init()
    {
        level = question.level ;
        question.tutorialText = levelTexts[level];
        question.maxQuestionTime = levelTimes[level];
        question.Init();
    }
    void WaitForNext()
    {
        float rnd = Random.RandomRange( waitTimes[level].x, waitTimes[level].y+1);
        Invoke("SetLevel", rnd);
    }
    void SetLevel()
    {
        switch (level)
        {
            case 0:
                PrepareLevel(1, 1);
                break;
            case 1:
                PrepareLevel(1, 3);
                break;
            case 2:
                PrepareLevel(1, 1);
                break;
            case 3:
                PrepareLevel(1, 3);
                break;
            case 4:
                PrepareLevel(1, 1);
                break;
            case 5:
                PrepareLevel(1, 3);
                break;
            case 6:
                PrepareLevel(1, 1);
                break;
            case 7:
                PrepareLevel(1, 3);
                break;
            case 8:
                PrepareLevel(2, 3);
                break;
            case 9:
                PrepareLevel(3, 3);
                break;
            default:
                PrepareLevel(3, 3);
                break;
        }
    }
    void PrepareLevel(int circleCount, int colorCount)
    {
        CloseAllCircle();
        List<int> rndList = new List<int>();
        for (int i = 0; i < circleCount; i++)
        {
            circles[i].gameObject.SetActive(true);
            rndList.Add(Random.Range(0, colorCount));
            circles[i].color = colors[rndList[i]];

        }
        switch (circleCount)
        {
            case 1:
                if (rndList[0] == 0)
                    correctCount++;
                break;
            case 2:
                if ((rndList[0] == 0 && rndList[1]==1) || (rndList[1] == 0 && rndList[0]==1))
                    correctCount++;
                    break;
            case 3:
                if (rndList[0] != rndList[1] && rndList[0] != rndList[2] && rndList[1] != rndList[2])
                    correctCount++;
                break;

        }

        Invoke("CloseAllCircle", 1);
        WaitForNext();
    }

    void CloseAllCircle()
    {
        foreach (var circle in circles)
            circle.gameObject.SetActive(false);
    }
    
    void SetAnswer()
    {
        isFinished = true;
        CancelInvoke();
        CloseAllCircle();
        int[] uniqueArray = GenerateUniqueArray(4);
        for (int i = 0; i < uniqueArray.Length; i++)
        {
            answers[i].gameObject.SetActive(true);
            answers[i].GetComponentInChildren<Text>().text = uniqueArray[i].ToString();
            answers[i].onClick.RemoveAllListeners();
            int answer = uniqueArray[i];
            answers[i].onClick.AddListener(delegate { CheckAnswer(answer); });
        }

    }

    public void CheckAnswer(int answer)
    {
        Debug.Log("Seçilen : " + answer);

        if (answer == correctCount)
            question.point = point*level;

        question.FinishQuestion();
    }

    private int[] GenerateUniqueArray(int size)
    {
        System.Random random = new System.Random();
        int[] uniqueArray = new int[size];

        // Rastgele bir konum seçelim
        int randomIndex = random.Next(0, size);

        // 3'ün olduðu konuma 3 deðerini atayalým
        uniqueArray[randomIndex] = correctCount;

        // Diðer elemanlar, 3'ün 0 ile 5 arasýnda bir rastgele sayý kadar eksiði veya fazlasý olmalý
        for (int i = 0; i < size; i++)
        {
            if (i != randomIndex)
            {
                int neighborValue;
                // Rastgele bir sayý üretelim (0-5 arasýnda), ancak benzersiz ve 0'dan büyük olsun
                do
                {
                    int randomDifference = random.Next(0, 6); // 0 ile 5 arasýnda bir rastgele sayý
                    neighborValue = correctCount + (random.Next(0, 2) == 0 ? -randomDifference : randomDifference);
                } while (ArrayContains(uniqueArray, neighborValue) || neighborValue < 0);

                uniqueArray[i] = neighborValue;
            }
        }

        return uniqueArray;
    }

    private bool ArrayContains(int[] array, int number)
    {
        foreach (int element in array)
        {
            if (element == number)
                return true;
        }
        return false;
    }
}
