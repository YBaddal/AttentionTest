using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CalendarDayCell : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Color emptyColor, correctColor, failColor;
    [SerializeField] Text dayText;
    [SerializeField] bool isWeekend;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCell(bool isFilled,bool isEmpty,int index,bool isFuture=false)
    {
        dayText.text = (index+1).ToString();
        

        if (isFilled)
            image.color = correctColor;
        else
            image.color = failColor;


        if (isEmpty)
        {
            image.color = emptyColor;
            dayText.text = "";
        }

        if (isWeekend)
        {
            image.color = emptyColor;
            dayText.color = failColor;
        }
        else if (isFuture)
        {
            image.color = emptyColor;
            dayText.color = Color.gray;
        }


    }
}
