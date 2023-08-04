using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question7Script : MonoBehaviour
{
    public Transform questionContent;
    public GameObject questionPrefab;
    public int prefabCount;
    public int correctAnswerCount;
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
        for(int i = 0; i < prefabCount; i++)
        {
           GameObject obj = Instantiate(questionPrefab, questionContent);
            if(i!=0)
            obj.SetActive(false);
        }
    }
    public void AnswerQuestion(int answer)
    {
        Question7PrefabCell cell0 = questionContent.transform.GetChild(0).GetComponent<Question7PrefabCell>();
        GameObject cell1 = questionContent.transform.GetChild(1).gameObject;

        if (answer == cell0.correctAnswer)
        {
            correctAnswerCount++;
            Debug.Log("Doðru");
        }
        else
        {
            Debug.Log("Yanlýþ");
        }


        Destroy(cell0.gameObject);
            cell1.SetActive(true);
    }
}
