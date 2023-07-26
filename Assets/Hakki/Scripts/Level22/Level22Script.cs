using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level22Script : MonoBehaviour
{
    public float targetTime = 5.27f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI btnText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI result;

    private float currentTime = 0.0f;
    private bool kronometreStarted = false;
    [SerializeField] List<string> btnTextes;


    private void Start()
    {
        currentTime = 0.0f;
        timerText.text = "00:00";
        Create();
    }

    void Create()
    {
        targetTime = 0f;
        targetTime += Random.Range(4, 8);
        targetTime += Random.Range(4, 100) / 100f;
        levelText.text = targetTime + "'a en yakin zamanda durdurmaya calisin.";
    }

    void Update()
    {
        if (kronometreStarted)
        {
            currentTime += Time.deltaTime;
            UpdateKronometreText();
        }
    }


    public void StartTimer()
    {
        if (!kronometreStarted)
        {
            StartKronometre();
            btnText.text = btnTextes[1];
        }
        else
        {
            StopKronometre();
            btnText.text = btnTextes[0];
        }
    }


    public void StartKronometre()
    {
        currentTime = 0.0f;
        kronometreStarted = true;
    }

    public void StopKronometre()
    {
        if (kronometreStarted)
        {
            kronometreStarted = false;

            float seconds = currentTime % 60;
            float targetSecond = targetTime;

            if (Mathf.Abs(targetSecond - seconds) < 1)
            {
                
                result.text = "%" + (100 - Mathf.FloorToInt(Mathf.Abs(targetSecond - seconds) * 100f) / 2).ToString();
                if (100 - Mathf.FloorToInt(Mathf.Abs(targetSecond - seconds) * 100f) / 2 >= 85)
                {
                    transform.GetComponent<Question>().point += 10;
                }
            }
            else
            {
                result.text = "%34";
            }
            
            ClearLevel();
            
        }
    }

    void ClearLevel()
    {
        currentTime = 0.0f;
        timerText.text = "00:00";
        Create();
    }


    private void UpdateKronometreText()
    {
        float seconds = currentTime % 60;
        int milliseconds = Mathf.FloorToInt((seconds - Mathf.Floor(seconds)) * 100);

        string kronometreString = string.Format("{0:00}:{1:00}", Mathf.Floor(seconds), milliseconds);
        timerText.text = kronometreString;
    }
}