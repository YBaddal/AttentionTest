using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level19Script : MonoBehaviour
{
    private string[] days = { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar" };

    [SerializeField] private List<Transform> buttons;
    [SerializeField] TextMeshProUGUI levelText;

    void Start()
    {
        Create();
    }

    private string trueAnswer;

    private List<string> buttondDays = new List<string>();

    void Create()
    {
        buttondDays.Clear();
        CreateQuestion(Random.Range(0, 3));
        levelText.text = questionString;
        trueAnswer = Answer();
        Debug.Log(trueAnswer);
        buttondDays.Add(trueAnswer);
        for (int i = 0; i < 3; i++)
        {
            string randomDay = days[Random.Range(0, days.Length)];
            if (!buttondDays.Contains(randomDay))
            {
                buttondDays.Add(randomDay);
            }
            else
            {
                i--;
            }
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            int index = Random.Range(0, buttondDays.Count);
            buttons[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = buttondDays[index];
            buttondDays.RemoveAt(index);
        }
    }

    private string levelDay;
    private string questionString;
    private int levelCount = 0;

    //Create new Questions
    void CreateQuestion(int index)
    {
        levelCount = 0;
        int day = Random.Range(0, days.Length);
        levelDay = days[day];

        switch (index)
        {
            case 0:
                int yesterday = Random.Range(1, 3);
                questionString = yesterday == 1
                    ? "Dün " + levelDay + " ise "
                    : yesterday + " Gün Önce " + levelDay + " ise ";

                break;
            case 1:
                questionString = "Bugün " + levelDay + " ise ";
                break;
            case 2:
                int tommorrow = Random.Range(1, 3);
                questionString = tommorrow == 1
                    ? "Yarın " + levelDay + " ise "
                    : tommorrow + " Gün Sonra " + levelDay + " ise ";

                break;
            default:
                break;
        }

        int quesDay = Random.Range(5, 20);
        questionString += quesDay + " sonra hangi gün olur.";
        levelCount += quesDay;
    }


    string Answer()
    {
        int indexOf = Array.IndexOf(days, levelDay);
        
        for (int i = 0; i < levelCount; i++)
        {
            indexOf = indexOf++ < 6 ? indexOf : 0;
            Debug.Log(days[indexOf]);
        }
        return days[indexOf];
      
    }

    public void Control(TextMeshProUGUI text)
    {
        if (text.text == trueAnswer)
        {
            Debug.Log(true);
        }
        else
        {
            Debug.Log(false);
        }

        Create();
    }
}

// public enum TypeOfQuestions
// {
//     Yesterday = 0,
//     Today = 1,
//     Tomorrow = 2
// }