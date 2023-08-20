using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CalendarDayCell : MonoBehaviour
{
    Image image;
    public Color emptyColor, correctColor, failColor;
    public Text dayText;
    public bool isFilled,isEmpty,isWeekend;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        index = transform.GetSiblingIndex();


        if(isFilled)
            image.color = correctColor;
        else
            image.color = failColor;


        if (isEmpty)
        {
            image.color = emptyColor;
            dayText.color = Color.gray;
        }

        if (isWeekend)
            dayText.color = failColor;

        if(index!=0 && index<=31)
            dayText.text = index.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
