using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public QuestManager questManager;
    public List<GameObject> pages;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Login(InputField input)
    { //Dummy data
        switch (input.text)
        {
            case "11111111111":
                PlayerPrefs.SetInt("Level", 0);
                PlayerPrefs.SetString("NameSurname", "Ahmet Demir");
                PlayerPrefs.SetString("Class", "1 / C");

                break;
            case "22222222222":
                PlayerPrefs.SetInt("Level", 1);
                PlayerPrefs.SetString("NameSurname", "Mehmet Tekir");
                PlayerPrefs.SetString("Class", "6 / E");
                break;
            case "33333333333":
                PlayerPrefs.SetInt("Level", 2);
                PlayerPrefs.SetString("NameSurname", "Can Koþar");
                PlayerPrefs.SetString("Class", "11 / H");
                break;
            default:
                PlayerPrefs.SetInt("Level", 0);
                PlayerPrefs.SetString("NameSurname", "Yuþa Baddal");
                PlayerPrefs.SetString("Class", "3 / B");
                break;
        }

        OpenPage(1);

    }
    public void DikkatTesti()
    {
        OpenPage(2);
    }
    public void Restart()
    {
        Application.LoadLevel(0);
    }
    public void OpenPage(int page)
    {
        CloseAllPage();
        pages[page].SetActive(true);
    }
    public void CloseAllPage()
    {
        foreach(var page in pages)
        {
            page.SetActive(false);
        }
    }
   
}
