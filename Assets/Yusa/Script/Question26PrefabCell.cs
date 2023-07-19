using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question26PrefabCell : MonoBehaviour
{
    public bool generated;
    public List<Color> colorList;
    public Image checkMark;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnChanged()
    {
        if (generated)
        {
            int rnd = Random.RandomRange(0, colorList.Count);
            if (checkMark)
                checkMark.color = colorList[rnd];
        }
        else
        {
            if (checkMark)
            {
                checkMark.color = transform.GetComponentInParent<Question3Script>().selectedColor;
                transform.GetComponentInParent<Question3Script>().CheckQuestion();
            }
        }
    }
}
