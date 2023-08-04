using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Question19Script : MonoBehaviour
{
    public List<string> stringList;
    public List<string> selectedStringList;
    public List<int> shuffledList;

    public List<Question19PrefabCell> questionList,answerList;

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
            questionList[i].questionString = selectedStringList[i];
            questionList[i].answerString = i.ToString();
            questionList[i].SetCell();
            questionList[i].gameObject.SetActive(true);
            answerList[i].questionString = selectedStringList[shuffledList[i]];
            answerList[i].SetCell();
            answerList[i].gameObject.SetActive(true);

        }
    }

    public void CheckQuestion()
    {
        for(int i = 0; i < questionCount; i++)
        {
            if (int.Parse(answerList[i].answerText.text) == shuffledList[i])
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
