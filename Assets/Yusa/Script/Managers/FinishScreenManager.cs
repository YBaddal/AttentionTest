using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishScreenManager : MonoBehaviour
{
    [SerializeField] Text pointText;
    [SerializeField] RectTransform pointNeedle;
    [SerializeField] Animator animator;
    public List<LeaderboardModel> leaderboard;
    [SerializeField] Transform leaderboardContent;
    [SerializeField] GameObject leaderboardPrefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetScreen()
    {
        int totalPoint=0;
        foreach (var question in GameManager.instance.questManager.selectedQuestionList)
            totalPoint += question.point;


        float z = totalPoint >= 4400 ? -220 : totalPoint / 20;

        pointText.text = totalPoint.ToString();
        pointNeedle.localRotation = Quaternion.Euler(0, 0, z*-1);
    }
    private void OnEnable()
    {
        animator.SetBool("Reset",true);
        SetScreen();
        StartCoroutine(GetLeaderBoard(new UserRequest { userId=GameManager.instance.user.userId}));
    }
    public void Like()
    {
        animator.SetBool("Reset", false);

        animator.SetTrigger("Like");
        SendFeedback(true);

    }
    public void Dislike()
    {
        animator.SetBool("Reset", false);

        animator.SetTrigger("Dislike");
        SendFeedback(false);
    }
    void SendFeedback(bool isLike)
    {
        StartCoroutine(PostFeedback(new PostFeedbackModel { 
        games = GameManager.instance.questManager.dailyGames.games,
        feedback = isLike,
        feedbackNote ="",
        userId = GameManager.instance.user.userId
        }));
    }
    void PopulateLeaderboard()
    {
        GameManager.instance.ClearContent(leaderboardContent);
        for(int i = 0; i < leaderboard.Count; i++)
        {
            GameObject obj = Instantiate(leaderboardPrefab, leaderboardContent);
            obj.GetComponent<LeaderboardCell>().SetCell(leaderboard[i],i);
        }
    }
    public IEnumerator PostFeedback(PostFeedbackModel data)
    {

        PostCtrl post = new PostCtrl();
        yield return StartCoroutine(post.postData(EndPoint.postFeedback, JsonConvert.SerializeObject(data)));

        if (post.resultObj.responseCode != 200)
        {
            //Error;
        }
        else //on server success
        {
        }
    }
    public IEnumerator GetLeaderBoard(UserRequest data)
    {

        PostCtrl post = new PostCtrl();
        yield return StartCoroutine(post.postData(EndPoint.getLeaderboard, JsonConvert.SerializeObject(data)));

        if (post.resultObj.responseCode != 200)
        {
            //Error;
            PopulateLeaderboard();
        }
        else //on server success
        {
            leaderboard = JsonConvert.DeserializeObject<List<LeaderboardModel>>(post.resultObj.downloadHandler.text);
            PopulateLeaderboard();
        }
    }
}
