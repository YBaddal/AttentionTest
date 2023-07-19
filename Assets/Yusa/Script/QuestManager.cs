using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public List<Question> questionList;
    public List<Question> selectedQuestionList;
    public int currentQuestion,totalQuestion,maxQuestionTime;

    public Text questionCountText, questionTutorialText;
    // Start is called before the first frame update
    void Start()
    {
        SetQuestionList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetQuestionList() //DummyData 
    {
        selectedQuestionList = new List<Question>();
        for (int i = 0; i < totalQuestion; i++)
        {
            questionList[i].currentQuestion = i;
            questionList[i].totalQuestion = totalQuestion;
            questionList[i].maxQuestionTime = maxQuestionTime;
            selectedQuestionList.Add(questionList[i]);
        }
        currentQuestion = 0;

        SetQuestionScreen();
    }
    void SetQuestionScreen()
    {
        questionCountText.text = "Soru " + (int)(currentQuestion + 1);
        questionTutorialText.text = selectedQuestionList[currentQuestion].tutorialText;
    }
    public void OpenTest()
    {
        SetQuestionScreen();
        selectedQuestionList[currentQuestion].gameObject.SetActive(true);
    }
    public void NextQuestion()
    {
        selectedQuestionList[currentQuestion].gameObject.SetActive(false);
        currentQuestion++;
        if (currentQuestion >= selectedQuestionList.Count)
        {
            //FinishTest
            GameManager.instance.OpenPage(4);
        }
        else
        {
            SetQuestionScreen();
        }
    }
}
