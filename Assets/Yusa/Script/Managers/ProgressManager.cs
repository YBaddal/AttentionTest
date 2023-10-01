using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public List<RecordResponse> weeklyRecord,monthlyRecord;
    [SerializeField] LineChart lineChart;
    [SerializeField] List<GameObject> days;
    [SerializeField] List<CalendarDayCell> calendarDays;
    DateTime dt;
    // Start is called before the first frame update
    void Start()
    {
        dt = DateTime.Now;
        StartCoroutine(GetMonthlyRecord(new GetCalendarRequest { userId = GameManager.instance.user.userId, month = dt.Month.ToString("00"), year = dt.Year.ToString() }));
        StartCoroutine(GetWeeklyRecord(new UserRequest { userId = GameManager.instance.user.userId}));
    }
    void SetDailyProgress()
    {
        if (lineChart.allPoint.Count == weeklyRecord.Count)
        {
    
            for(int i = 0; i < weeklyRecord.Count; i++)
            {
                int point = 0;
                foreach (var game in weeklyRecord[i].games)
                    point += game.score;

                point = point > 500 ? 500 : point;

                lineChart.allPoint[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(lineChart.allPoint[i].GetComponent<RectTransform>().anchoredPosition.x, point);
            }
            lineChart.UpdateLineChart();
        }
        else
            Debug.Log("Get Week error");
    }
    void SetDailyGames()
    {
        if (days.Count == weeklyRecord.Count)
        {

            for (int i = 0; i < weeklyRecord.Count; i++)
            {
                int dayofweek = (int)dt.DayOfWeek;

                dayofweek = dayofweek == 0 ? 7 : dayofweek;

                if ( i >= GetIndexDayOfWeek(dt.DayOfWeek))
                {
                    days[i].transform.GetChild(2).gameObject.SetActive(true);
                    continue;
                }

                if (weeklyRecord[i].games.Count > 0)
                    days[i].transform.GetChild(1).gameObject.SetActive(true);
                else
                    days[i].transform.GetChild(0).gameObject.SetActive(true);

            }
        }
        else
            Debug.Log("Get Week error");
    }
    void SetCalendar()
    {
        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        int fdom = GetIndexDayOfWeek(firstDayOfMonth.DayOfWeek);

        for (int i = 0; i < calendarDays.Count; i++)
        {
           

            if (i < fdom || i >= monthlyRecord.Count+fdom)
                calendarDays[i].SetCell(false,true,i-fdom);
            else if (i - fdom >= dt.Day)
                calendarDays[i].SetCell(false, false, i - fdom, true);
            else if(monthlyRecord[i-fdom].games.Count>0)
                calendarDays[i].SetCell(true, false, i - fdom);
            else
                calendarDays[i].SetCell(false, false, i - fdom);

        }
    }
    public IEnumerator GetMonthlyRecord(GetCalendarRequest data)
    {

        PostCtrl post = new PostCtrl();
        yield return StartCoroutine(post.postData(EndPoint.getCalendar, JsonConvert.SerializeObject(data)));

        if (post.resultObj.responseCode != 200)
        {
            //Error;
        }
        else //on server success
        {
            monthlyRecord = JsonConvert.DeserializeObject<List<RecordResponse>>(post.resultObj.downloadHandler.text);
            SetCalendar();
        }
    }
    public IEnumerator GetWeeklyRecord(UserRequest data)
    {

        PostCtrl post = new PostCtrl();
        yield return StartCoroutine(post.postData(EndPoint.getWeekly, JsonConvert.SerializeObject(data)));

        if (post.resultObj.responseCode != 200)
        {
            //Error;
        }
        else //on server success
        {
            weeklyRecord = JsonConvert.DeserializeObject<List<RecordResponse>>(post.resultObj.downloadHandler.text);
            SetDailyProgress();
            SetDailyGames();
        }
    }
    int GetIndexDayOfWeek(DayOfWeek dt)
    {
        int dayofweek = (int)dt;

        dayofweek = dayofweek == 0 ? 7 : dayofweek;
        dayofweek--;
        return dayofweek;
    }
}
