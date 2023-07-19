using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    [HideInInspector] public int totalQuestion, currentQuestion;
    [HideInInspector] public float maxQuestionTime;
    [HideInInspector] public float questionTime;

    public string tutorialText;
    public int point;

    [SerializeField] Slider questionSlider,timeSlider;
    [SerializeField] Text questionCountText,timeText,pointText;
    [SerializeField] Text questionTitleText;

    bool isFinish=false;
    // Start is called before the first frame update
    private void Awake()
    {
        Init();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFinish)
            return;

        questionTime += Time.deltaTime;
        if (questionTime >= maxQuestionTime)
            FinishQuestion();

        SetHeader();
    }
    private void Init()
    {
        questionTime = 0;
        isFinish = false;
        questionTitleText.text = tutorialText;
    }
    public void AddPoint(int p)
    {
        point += p;
    }
    public void FinishQuestion()
    {
        isFinish = true;
        GameManager.instance.questManager.NextQuestion();
    }
    void SetHeader()
    {
        questionCountText.text = (currentQuestion+1) + " / " + totalQuestion;
        questionSlider.maxValue= totalQuestion;
        questionSlider.value = currentQuestion+ (questionTime / maxQuestionTime); 
        timeSlider.maxValue = maxQuestionTime;
        timeSlider.value = questionTime;
        timeText.text = Mathf.FloorToInt(questionTime / 60f).ToString("00") + ":" + Mathf.FloorToInt(questionTime % 60f).ToString("00");
        pointText.text = point.ToString();
    }
    public void FullScreen()
    {

    }
    public void Help()
    {

    }
}
