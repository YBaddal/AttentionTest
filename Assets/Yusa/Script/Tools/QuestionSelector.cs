using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionSelector : MonoBehaviour
{
    public List<GameObject> gameList;
    public Transform content;
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
            Button obj = Instantiate(contentPrefab, content).GetComponent<Button>();
            string order = (gameList[i].GetComponent<Question>().level + 1).ToString();
            obj.GetComponentInChildren<Text>().text = gameList[i].gameObject.name + "-"+order;
            int selected = i;
            obj.onClick.AddListener(delegate { this.Open(selected); });
        }
    }
    public void Open(int selected)
    {
        CloseAll();
        gameList[selected].SetActive(true);
    }
    public void CloseAll()
    {
        foreach (GameObject game in gameList)
            game.SetActive(false);
    }
}
