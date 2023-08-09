using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    [HideInInspector] public int totalQuestion, currentQuestion;
    public float maxQuestionTime;
    [HideInInspector] public float questionTime;

    public string tutorialText;
    public int point;
    public int level;

    [SerializeField] Slider questionSlider,timeSlider;
    [SerializeField] Text questionCountText,timeText,pointText;
    public Text questionTitleText;

    public bool isFinish=false;
    public bool isLap;
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

        if(isLap)
        {
            if (questionTime > maxQuestionTime)
                FinishQuestion();
        }
        else
        {
            questionTime -= Time.deltaTime;
            if (questionTime <= 0)
                FinishQuestion();
        }

        
        SetHeader();
    }
    public void Init()
    {
        questionTime = maxQuestionTime;
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
        GameManager.instance.questManager.FinishScreen();
    }
    void SetHeader()
    {
        if (isLap)
        {
            questionSlider.maxValue = totalQuestion;
            questionSlider.value = currentQuestion+1;
            timeSlider.maxValue = maxQuestionTime;
            timeSlider.value = questionTime;
            timeText.text = questionTime+"/"+maxQuestionTime;
        }
        else
        {
            questionSlider.maxValue = totalQuestion;
            questionSlider.value = currentQuestion+1;
            timeSlider.maxValue = maxQuestionTime;
            timeSlider.value = questionTime;
            timeText.text = Mathf.FloorToInt(questionTime / 60f).ToString("00") + ":" + Mathf.FloorToInt(questionTime % 60f).ToString("00");
        }
        questionCountText.text = (currentQuestion + 1) + " / " + totalQuestion;
        pointText.text = point.ToString();

    }
    public void FullScreen()
    {
        //SetFullScreen(!Screen.fullScreen);
        if (Screen.fullScreen) // Eðer tam ekran zaten açýksa
        {
            Screen.fullScreen = false; // Tam ekrandan çýk
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;
        }
        else
        {
            Screen.fullScreen = true; // Tam ekrana geç
            transform.localScale = new Vector3(1.3f, 1.3f, 1);
            transform.localPosition = new Vector3(0, 50, 0);
        }
    }
    public void SetFullScreen(bool isFullScreen)
    {
        if (isFullScreen)
        {
            // Tam ekran modunu aç.
            //Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;  
            //Application.ExternalEval("unityInstance.SetFullscreen(1)");
            Screen.fullScreen = true;
            transform.localScale = new Vector3(1.3f, 1.3f, 1);
            transform.localPosition = new Vector3(0,50,0);
            
        }
        else
        {
            // Tam ekran modunu kapat ve oyunu normal boyutta çalýþtýr.
            Screen.fullScreenMode = FullScreenMode.Windowed;
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;
        }
    }
    public void Help()
    {

    }
}
