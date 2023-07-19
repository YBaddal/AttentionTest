using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question20PrefabCell : MonoBehaviour
{
    public Text questionText;
    public Color questionColor;
    public Image questionImage;
    public InputField answerText;
    public string questionString, answerString;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCell()
    {
        if (questionText)
        {
            questionText.text = questionString;
            questionText.color = questionColor;
        }

        if (answerText)
            answerText.text = answerString;

        if (questionImage)
            questionImage.color = questionColor;
    }
}
