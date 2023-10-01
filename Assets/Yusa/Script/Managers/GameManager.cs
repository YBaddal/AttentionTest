using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public User user;
    public QuestManager questManager;
    public ProfileManager profileManager;
    public FinishScreenManager finishScreenManager;
    public List<GameObject> pages;
    public List<Sprite> profileImages;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
    }

    public void Pause()
    {
        questManager.selectedQuestionList[questManager.currentQuestion].enabled = false;

        OpenPage(Page.Pause);
    }
    public void WaitForResume()
    {
        OpenPage(Page.Loading);
        Invoke("Resume", 3);
    }
    public void Resume()
    {
        CloseAllPage();
        questManager.selectedQuestionList[questManager.currentQuestion].enabled = true;
    }
    public void OpenReport()
    {
        pages[(int)Page.Report].gameObject.SetActive(true);
        var inputs = pages[(int)Page.Report].GetComponentsInChildren<InputField>();
        foreach (var input in inputs)
            input.text = "";
    }
    public void SendReport()
    {
        var inputs = pages[(int)Page.Report].GetComponentsInChildren<InputField>();
        string title = inputs[0] != null ? inputs[0].text : "";
        string desc = inputs[1] != null ? inputs[1].text : "";
        StartCoroutine(PostReport(new PostReportModel
        {
            reportTitle = title,
            reportDesc = desc,
            userId = user.userId,
            level = questManager.selectedQuestionList[questManager.currentQuestion].GetComponent<Question>().level,
            game = questManager.selectedQuestionList[questManager.currentQuestion].name,
        }));

    }
    public void FullScreen()
    {
        
    }
    public void Restart()
    {
        questManager.CloseAll();
        OpenPage(Page.Main);
    }
    public void OpenPage(Page page)
    {
        CloseAllPage();
        pages[(int)page].SetActive(true);
    }
    public void CloseAllPage()
    {
        foreach(var page in pages)
        {
            page.SetActive(false);
        }
    }
    public IEnumerator PostReport(PostReportModel data)
    {

        PostCtrl post = new PostCtrl();
        yield return StartCoroutine(post.postData(EndPoint.postReport, JsonConvert.SerializeObject(data)));

        if (post.resultObj.responseCode != 200)
        {
            //Error;
        }
        else //on server success
        {
        }
    }
    public void ClearContent(Transform content)
    {
        for(int i = content.childCount - 1; i >= 0; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
}
public enum Page
{
    Login = 0,
    Main = 1,
    QuizFinish = 2,
    Pause = 3,
    Report = 4,
    HowTo = 5,
    Loading = 6,
    Finish = 7
}