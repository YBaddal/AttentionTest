using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text;

public enum EndPoint
{
   login,
   addScore,
   getCalendar,
   getWeekly,
   getDaily,
   postReport,
   postFeedback,
   getLeaderboard
}

public class PostCtrl
{
    public UnityWebRequest resultObj;
    string server = "https://www.semvural.com/benego";

    string LoginEndpoint = "/login.php";
    string AddScoreEndpoint = "/get-data.php";
    string GetCalendarEndpoint = "/getcalendar.php";
    string GetWeeklyEndpoint = "/getweek.php";
    string GetDailyEndpoint = "/getdailygames.php";
    string PostReportEndpoint = "/report.php";
    string PostFeedbackEndpoint = "/feedback.php";
    string GetLeaderboardEndpoint = "/leaderboard.php";


    public string GetEndPointURL (EndPoint endPointType){
		switch (endPointType) {
			case EndPoint.login: return server + LoginEndpoint;
            case EndPoint.addScore: return server + AddScoreEndpoint;
            case EndPoint.getCalendar: return server + GetCalendarEndpoint;
            case EndPoint.getWeekly: return server + GetWeeklyEndpoint;
            case EndPoint.getDaily: return server + GetDailyEndpoint;
            case EndPoint.postReport: return server + PostReportEndpoint;
            case EndPoint.postFeedback: return server + PostFeedbackEndpoint;
            case EndPoint.getLeaderboard: return server + GetLeaderboardEndpoint;

            default:
				return "";
		}
	}
       

	public IEnumerator postData(EndPoint endPointType, string jsonData)
    {
        string url = GetEndPointURL(endPointType);
    
        UnityWebRequest request = new UnityWebRequest(url, "POST");
		byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
		request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        //request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("Token"));
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");
        request.chunkedTransfer = false;

        yield return request.SendWebRequest();
		resultObj = request;

        if (request.isNetworkError)
		{
            Debug.LogError("\nUrl:"+url+"\nRequest:"+jsonData + "\nResponse:"+request.error);
        }
		else
		{
            Debug.Log("\nUrl:" + url + "\nRequest:" + jsonData + "\nResponse:" + request.downloadHandler.text);
        }
    }
    public IEnumerator gettData(EndPoint endPointType, string jsonData)
    {
        string url = GetEndPointURL(endPointType)+jsonData;

        UnityWebRequest request = new UnityWebRequest(url, "GET");
        //request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("Token"));
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.Send();
        resultObj = request;

        if (request.isNetworkError)
        {
            Debug.LogError("\nUrl:" + url + "\nRequest:" + jsonData + "\nResponse:" + request.error);
        }
        else
        {
            Debug.Log("\nUrl:" + url + "\nRequest:" + jsonData + "\nResponse:" + request.downloadHandler.text);
        }
    }

}