using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField] InputField input;
    // Start is called before the first frame update
    void Start()
    {
        string id = PlayerPrefs.GetString("username");
        if (!string.IsNullOrEmpty(id))
        {
            input.text = id;
        }
    }
    public void Login()
    {
        StartCoroutine(PostLogin(new LoginModel { userName = input.text }));
    }
    public IEnumerator PostLogin(LoginModel data)
    {

        PostCtrl post = new PostCtrl();
        yield return StartCoroutine(post.postData(EndPoint.login, JsonConvert.SerializeObject(data)));

        if (post.resultObj.responseCode != 200)
        {
            //Error;
        }
        else //on server success
        {
            PlayerPrefs.SetString("username",input.text);
            GameManager.instance.user = JsonConvert.DeserializeObject<User>(post.resultObj.downloadHandler.text);
            GameManager.instance.profileManager.SetProfile();
            GameManager.instance.OpenPage(Page.Main);
        }
    }
}
