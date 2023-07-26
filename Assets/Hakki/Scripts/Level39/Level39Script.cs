using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level39Script : MonoBehaviour
{
    [SerializeField] private List<Transform> watches;
    [SerializeField] private TextMeshProUGUI timerText;
    private float selectedMin;
    private float selectedHour;

    void Start()
    {
        Create();
    }

    void Create()
    {
        int randomSelect = Random.Range(0, 12);
        for (int i = 0; i < watches.Count; i++)
        {
            float min = Random.Range(1, 13) * 30 * -1;
            float hour = Random.Range(1, 13) * 30 * -1;
            if (i == randomSelect)
            {
                selectedMin = min;
                selectedHour = hour;
            }

            watches[i].GetChild(0).transform.eulerAngles =
                new Vector3(0, 0, min);

            watches[i].GetChild(1).transform.eulerAngles =
                new Vector3(0, 0, hour);
        }

        timerText.text = (selectedHour / 30 * -1) + " : " +
                         ((selectedMin / 30 * -1) * 5 == 60 ? 00 : (selectedMin / 30 * -1) * 5);
    }


    public void Control(Transform watch)
    {
        if (watch.GetChild(0).localEulerAngles.z == selectedMin && watch.GetChild(1).localEulerAngles.z == selectedHour)
        {
            Debug.Log(true);
        }
        else
        {
            Debug.Log(watch.GetChild(0).localEulerAngles.z +" =min= "+ selectedMin +"   " +  watch.GetChild(1).localEulerAngles.z +" =hour= "+ selectedHour);
        }
        
    }
}