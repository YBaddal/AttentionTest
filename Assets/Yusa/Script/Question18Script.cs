using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question18Script : MonoBehaviour
{
    public Transform questionContent;
    public GameObject questionPrefab;
    public int prefabCount;
    public int correctAnswerCount;
    public VerticalLayoutManager VLM;
    public List<Button> buttonList;
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
        for (int i = 0; i < prefabCount; i++)
        {
            GameObject obj = Instantiate(questionPrefab, questionContent);
        }
        SetLayout();
    }
    public void AnswerQuestion(int answer)
    {
        Question7PrefabCell cell0 = questionContent.transform.GetChild(0).GetComponent<Question7PrefabCell>();
        if (answer == cell0.correctAnswer)
        {
            correctAnswerCount++;
            Debug.Log("Doðru");
        }
        else
        {
            Debug.Log("Yanlýþ");
        }


        cell0.SetAnim(answer);
        ButtonState(false);
        Invoke("SetLayout", 0.4f);
    }

    void SetLayout()
    {
        VLM.SetLayout();
        ButtonState(true);
    }

    void ButtonState(bool State)
    {
        foreach (var but in buttonList)
            but.interactable = State;
    }
}
