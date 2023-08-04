using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question1Script : MonoBehaviour
{
    public bool visibleNext;
    public bool nonColor;
    public Transform questionContent;
    public GameObject questionPrefab;
    public int prefabCount,colorListCount,spriteListCount,correctAnswerCount;
    public List<Question1Generator> generatedList;
    public int continueCount;
    // Start is called before the first frame update
    void Start()
    {
        if (nonColor)
            GenerateNonColorQuetion();
        else
            GenerateQuetion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateQuetion()
    {
        generatedList = new List<Question1Generator>();
        Question1Generator g = new Question1Generator { ColorID=-1,SpriteID=-1};
        g.ColorID = Random.RandomRange(0, colorListCount);
        g.SpriteID = Random.RandomRange(0, spriteListCount);
        generatedList.Add(g);
        for (int i = 1; i < prefabCount; i++)
        {
            Question1Generator generated = new Question1Generator { ColorID = -1, SpriteID = -1 };
            int rnd = Random.RandomRange(0, 2);
            if (rnd == 0)
            {
                generated.SpriteID = generatedList[i - 1].SpriteID;

                generated.ColorID = Random.RandomRange(0, colorListCount);
            }
            else
            {
                generated.ColorID = generatedList[i - 1].ColorID;
                generated.SpriteID = Random.RandomRange(0, spriteListCount);

            }
            generatedList.Add(generated);
        }
        SetQuestion();
    }
    void GenerateNonColorQuetion()
    {
        generatedList = new List<Question1Generator>();
        Question1Generator g = new Question1Generator { ColorID = 0, SpriteID = -1 };
        g.SpriteID = Random.RandomRange(0, spriteListCount);
        generatedList.Add(g);
        for (int i = 1; i < prefabCount; i++)
        {
            Question1Generator generated = new Question1Generator { ColorID = 0, SpriteID = -1 };
                generated.SpriteID = Random.RandomRange(0, spriteListCount);
            generatedList.Add(generated);
        }
        SetQuestion();
    }
    void SetQuestion()
    {
        correctAnswerCount = 0;
        foreach (var generated in generatedList)
        {
            var obj = Instantiate(questionPrefab, questionContent);
            obj.SetActive(visibleNext);
            obj.GetComponent<Question1PrefabCell>().colorID=generated.ColorID;
            obj.GetComponent<Question1PrefabCell>().spriteID = generated.SpriteID;
            obj.GetComponent<Question1PrefabCell>().Init();
            obj.SetActive(visibleNext);
        }
        if (!visibleNext)
            questionContent.GetChild(0).gameObject.SetActive(true);
    }
    public void StartGame()
    {
        if (!visibleNext)
        {
            questionContent.GetChild(0).gameObject.SetActive(false);
            questionContent.GetChild(1).gameObject.SetActive(true);

        }

    }
    public void ContinueGame()
    {
        if (!visibleNext)
        {
            questionContent.GetChild(1).gameObject.SetActive(false);
            questionContent.GetChild(2).gameObject.SetActive(true);

        }

    }
    public void AnswerQuestion(int answer)
    {
        Question1PrefabCell cell0 = questionContent.transform.GetChild(0).GetComponent<Question1PrefabCell>();
        Question1PrefabCell cell1 = questionContent.transform.GetChild(1+ continueCount).GetComponent<Question1PrefabCell>();

        switch (answer)
        {
            case 0:
                if (cell0.spriteID == cell1.spriteID)
                {
                    Debug.Log("Þekiller Ayný");
                    correctAnswerCount++;
                }
                break;
            case 1:
                if (cell0.colorID == cell1.colorID)
                {
                    Debug.Log("Renkler Ayný");
                    correctAnswerCount++;
                }
                break;
            case 2:
                if (/*cell0.colorID == cell1.colorID && */cell0.spriteID == cell1.spriteID)
                {
                    Debug.Log("Ayný");
                    correctAnswerCount++;
                }
                break;
            case 3:
                if (/*cell0.colorID != cell1.colorID || */cell0.spriteID != cell1.spriteID)
                {
                    Debug.Log("Farklý");
                    correctAnswerCount++;
                }
                break;
        }      

        //if (answer == 0)
        //{
        //    if (cell0.spriteID == cell1.spriteID)
        //    {
        //        Debug.Log("Þekiller Ayný");
        //        correctAnswerCount++;
        //    }
        //}
        //else
        //{
        //    if (cell0.colorID == cell1.colorID)
        //    {
        //        Debug.Log("Renkler Ayný");
        //        correctAnswerCount++;
        //    }
        //}

        
        if (!visibleNext)
        {
            cell1.gameObject.SetActive(false);
            questionContent.transform.GetChild(2+continueCount).gameObject.SetActive(true);

        }

        Destroy(cell0.gameObject);
    }
}

[System.Serializable]
public class Question1Generator
{
    public int ColorID;
    public int SpriteID;
}