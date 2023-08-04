using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question21PrefabCell : MonoBehaviour
{
    public int value;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
       if(transform.GetComponentInParent<Question21Script>())
        {
            transform.GetComponentInParent<Question21Script>().AnswerQuestion(value);
        }
        if (transform.GetComponentInParent<Question22Script>())
        {
            transform.GetComponentInParent<Question22Script>().AnswerQuestion(this);
        }
        if (transform.GetComponentInParent<Question24Script>())
        {
            transform.GetComponentInParent<Question24Script>().AnswerQuestion(this);
        }
        if (transform.GetComponentInParent<Question25Script>())
        {
            if(transform.GetComponent<Toggle>().isOn)
                transform.GetComponentInParent<Question25Script>().AnswerQuestion();
        }
    }
}
