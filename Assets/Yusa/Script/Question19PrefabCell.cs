using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question19PrefabCell : MonoBehaviour
{
    public Text questionText;
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
        questionText.text = questionString;
        answerText.text = answerString;
    }
}
