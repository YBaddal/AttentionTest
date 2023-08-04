using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question11Script : MonoBehaviour
{
    public List<Image> ButtonImages;
    public List<Color> ButtonColors;
    public List<string> colorStringList;
    public Color correctColor;
    public List<Color> failColors;

    public int activeButtons;
    public int correctAnswer=0;
    public Text textField;

    public int correctAnswerCount;
    // Start is called before the first frame update
    void Start()
    {
        SetQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SetQuestion()
    {
        failColors = new List<Color>();
        foreach(var c in ButtonColors)
        {
            failColors.Add(c);
        }
        int rndColorString = Random.RandomRange(0, ButtonColors.Count);
        correctAnswer = Random.RandomRange(0, activeButtons);
        int rndColor = Random.RandomRange(0, ButtonColors.Count);

        correctColor = failColors[rndColorString];
        failColors.Remove(failColors[rndColorString]);

        for(int i = 0; i<activeButtons;i++)
        {
            int rnd = Random.RandomRange(0, failColors.Count);
            ButtonImages[i].color = failColors[rnd];
            ButtonImages[i].gameObject.SetActive(true);
        }
        ButtonImages[correctAnswer].color = correctColor;
        textField.text = colorStringList[rndColorString];
        textField.color = ButtonColors[rndColor];

    }

    public void AnswerQuestion(int answer)
    {
        if (answer == correctAnswer)
        {
            Debug.Log("Correct");
            correctAnswerCount++;
            if (correctAnswerCount >= 50)
                transform.GetComponent<Question>().FinishQuestion();
            if (correctAnswerCount > 20)
                activeButtons = 5;
            else if (correctAnswerCount > 10)
                activeButtons = 4;
            else
                activeButtons = 3;
        }
        else
        {
            Debug.Log("Fail");
        }

        SetQuestion();
    }
}
