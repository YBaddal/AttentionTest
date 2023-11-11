using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionSelector : MonoBehaviour
{
    public List<GameObject> gameList;
    public List<int> gameLevelCount;
    public Transform content,gameContent;
    public GameObject contentPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Populate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Populate()
    {
        for(int i = 0; i < gameList.Count; i++)
        {
            for(int j = 0; j < gameLevelCount[i]; j++)
            {
                int selected = i;
                int level = j;
                Button obj = Instantiate(contentPrefab, content).GetComponent<Button>();            
                obj.GetComponentInChildren<Text>().text = gameList[i].gameObject.name + "-" + (level + 1);
                obj.onClick.AddListener(delegate { this.Open(selected, level); });
            }
        }
    }
    public void Open(int selected,int level)
    {
        CloseAll();
        Question obj = Instantiate(gameList[selected], gameContent).GetComponent<Question>();
        obj.level = level;
        obj.enabled = true;
        obj.gameObject.SetActive(true);
        GameManager.instance.OpenPage(Page.Login);
    }
    public void CloseAll()
    {
        for(int i = gameContent.childCount-1;i>=0;i--)
            Destroy(gameContent.GetChild(i).gameObject);
    }
}
