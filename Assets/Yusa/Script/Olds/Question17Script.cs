using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question17Script : MonoBehaviour
{
    public List<Toggle> toggleList;
    public List<Sprite> spriteList;
    private List<int> selectedSpriteList;
    public int hideCount;
    private List<int> hideList;
    private int current;
    private int correctAnswerCount;
    public float waitTime;
    public GameObject anwerObjs;

    // Start is called before the first frame update
    void Start()
    {
        GenerateToggles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateToggles()
    {
        current = 0;
        selectedSpriteList = new List<int>();

        while (selectedSpriteList.Count < 3)
        {
            int rnd = Random.RandomRange(0, spriteList.Count);
            if (!selectedSpriteList.Contains(rnd))
            {
                selectedSpriteList.Add(rnd);
            }
        }

        for(int i = 0; i < toggleList.Count; i++)
        {
            toggleList[i].image.sprite = spriteList[selectedSpriteList[i]];
        }

        anwerObjs.SetActive(false);

        Invoke("SetToggles",waitTime);
        
    }
    void SetToggles()
    {
        HideAnswers();
        anwerObjs.SetActive(true);
    }
    public void HideAnswers()
    {
        hideList = new List<int>();

        while (hideList.Count < hideCount)
        {
            int rnd = Random.RandomRange(0, toggleList.Count);
            if (!hideList.Contains(rnd))
            {
                hideList.Add(rnd);
            }
        }

        for (int i = 0; i < hideList.Count;i++)
        {
            toggleList[hideList[i]].isOn = true;
        }
        AnswerVisibility();
    }
    void AnswerVisibility()
    {
        for (int i = 0; i < hideList.Count; i++)
        {
           if (toggleList[hideList[i]].isOn)
            {
                toggleList[hideList[i]].gameObject.SetActive(false);
            }
        }
        toggleList[hideList[current]].gameObject.SetActive(true);

    }

    public void AnswerQuestion(int answer)
    {
        if (selectedSpriteList[hideList[current]] == answer)
        {
            Debug.Log("Doðru");
            correctAnswerCount++;

        }
        else
        {
            Debug.Log("Doðru");
        }
        toggleList[hideList[current]].isOn = false;
        current++;
        CheckToggles();
    }
    void CheckToggles()
    {
        bool isFinish = true;
        foreach(var tog in toggleList)
        {
            if (tog.isOn)
                isFinish = false;
        }
        if (isFinish)
            Invoke("GenerateToggles", 0.5f);
        else
            AnswerVisibility();
    }
}
