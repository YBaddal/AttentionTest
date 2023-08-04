using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Question20Script : MonoBehaviour
{
    public List<string> stringList;
    public List<Color> colorList;
    public List<string> selectedStringList;
    public List<int> shuffledList;
    public List<Question20PrefabCell> questionList;

    public List<Question20PrefabCell> answerList;

    public int questionCount, correctAnswerCount;
    // Start is called before the first frame update
    void Start()
    {
        shuffledList = new List<int>();
        for (int i = 0; i < questionCount; i++)
            shuffledList.Add(i);
        GenerateQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateQuestion()
    {
        selectedStringList = new List<string>();

        while (selectedStringList.Count < questionCount)
        {
            int rnd = Random.RandomRange(0, stringList.Count);
            if (!selectedStringList.Contains(stringList[rnd]))
            {
                selectedStringList.Add(stringList[rnd]);
            }
        }
        shuffledList = shuffledList.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < selectedStringList.Count; i++)
        {
            questionList[i].questionString = selectedStringList[shuffledList[i]];
            questionList[i].answerString = i.ToString();
            questionList[i].questionColor = colorList[shuffledList[i]];

            questionList[i].SetCell();
            questionList[i].gameObject.SetActive(true);
            answerList[i].questionString = selectedStringList[shuffledList[i]];
            answerList[i].questionColor = colorList[shuffledList[i]];

            answerList[i].SetCell();
            answerList[i].gameObject.SetActive(true);

        }
    }

    public void CheckQuestion()
    {
        correctAnswerCount = 0;
        for (int i = 0; i < questionCount; i++)
        {
            if (answerList[i].answerText.text == selectedStringList[shuffledList[i]])
            {
                Debug.Log(i + ". cevap doðru");
                correctAnswerCount++;
            }
            else
            {
                Debug.Log(i + ". cevap yanlýþ");
            }
        }

        transform.GetComponent<Question>().FinishQuestion();
    }
}
