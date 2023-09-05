using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class QuestManager : MonoBehaviour
{
    public List<Question> questionList;
    public List<Question> selectedQuestionList;
    public List<HowToModel> howToModelList;
    public int currentQuestion;

    public Text questionCountText, questionTutorialText;
    public Image howToImg;
    public Text howToText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetDailyGames(new GetDailyGameRequest { userId=GameManager.instance.user.userId,date= DateTime.Now.ToShortDateString() }));
    }

    public IEnumerator GetDailyGames(GetDailyGameRequest data)
    {
        PostCtrl post = new PostCtrl();
        yield return StartCoroutine(post.postData(EndPoint.getDaily, JsonConvert.SerializeObject(data)));

        if (post.resultObj.responseCode != 200)
        {
            //Error;
        }
        else //on server success
        {
            var resp = JsonConvert.DeserializeObject<RecordResponse>(post.resultObj.downloadHandler.text);
            SetQuestionList(resp);
        }
    }
    void SetQuestionList(RecordResponse resp) 
    {
        selectedQuestionList = new List<Question>();
        List<int> rndList = new List<int>();

        while (rndList.Count < resp.games.Count)
        {
            int randomNumber = UnityEngine.Random.RandomRange(0, resp.games.Count);

            if (!rndList.Contains(randomNumber))
            {
                rndList.Add(randomNumber);
            }
        }

        for (int i = 0; i < resp.games.Count; i++)
        {
            var selected = questionList.FirstOrDefault(x => x.gameObject.name == resp.games[i].game);
            if (selected != null)
            {
                selected.currentQuestion = i;
                selected.totalQuestion = resp.games.Count;
                selected.level = resp.games[i].level;
                selectedQuestionList.Add(selected);
            }
            else
            {
                questionList[rndList[i]].currentQuestion = i;
                questionList[rndList[i]].totalQuestion = resp.games.Count;
                selectedQuestionList.Add(questionList[rndList[i]]);
            }

        }
        currentQuestion = 0;
    }
    public void FinishScreen()
    {
        GameManager.instance.OpenPage(Page.QuizFinish);
        var toggles = GameManager.instance.pages[(int)Page.QuizFinish].GetComponentsInChildren<Toggle>();

        foreach (Toggle toggle in toggles)
            toggle.isOn = false;

        int stars = Mathf.CeilToInt(selectedQuestionList[currentQuestion].point / 500);

        for (int i = 0; i < stars; i++)
        {
            if (i >= toggles.Length)
                return;

            toggles[i].isOn = true;
        }
    }
    public void OpenTest()
    {
        selectedQuestionList[currentQuestion].gameObject.SetActive(true);

        //if (howToModelList.Count <= currentQuestion)
        //    return;
        int index = questionList.IndexOf(selectedQuestionList[currentQuestion]);
        howToImg.sprite = howToModelList[index].sprite;
        howToText.text = howToModelList[index].descriptions;
        howToImg.transform.parent.parent.gameObject.SetActive(true);
    }
    void FinishTest()
    {
        GameManager.instance.finishScreenManager.gameObject.SetActive(true);
    }
    public void NextQuestion()
    {
        GameManager.instance.CloseAllPage();
        CloseAll();
        currentQuestion++;
        if (currentQuestion >= selectedQuestionList.Count)
        {
            FinishTest();
        }
        else
        {
            OpenTest();
        }
    }
    public void CloseAll()
    {
        foreach (var question in selectedQuestionList)
            question.gameObject.SetActive(false);
    }
}
