using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question21Script : MonoBehaviour
{
    public List<Question21PrefabCell> prefabList;
    public List<string> stringList1, stringList2;
    public int selectedStr,selectedQuestion, correctAnswerCount;
    // Start is called before the first frame update
    void Start()
    {
        GenerateQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateQuestion()
    {
        selectedStr= Random.RandomRange(0, stringList1.Count);
        int rnd = Random.RandomRange(0, 2);
        selectedQuestion = Random.RandomRange(0, prefabList.Count);

        for (int i =0;i<prefabList.Count;i++)
        {
            prefabList[i].value = i;
            if (rnd == 0)
                prefabList[i].text.text = stringList1[selectedStr];
            else
                prefabList[i].text.text = stringList2[selectedStr];
        }

        if (rnd == 0)
            prefabList[selectedQuestion].text.text = stringList2[selectedStr];
        else
            prefabList[selectedQuestion].text.text = stringList1[selectedStr];

    }

    public void AnswerQuestion(int answer)
    {

        if (answer == selectedQuestion)
        {
            Debug.Log("Doðru");
            correctAnswerCount++;
            GenerateQuestion();
        }
        else
        {
            Debug.Log("Yanlýþ");
            GenerateQuestion();
        }
    }
}
