using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = transform.GetComponentInParent<Question>().gameObject.name+"-"+(transform.GetComponentInParent<Question>().level+1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
