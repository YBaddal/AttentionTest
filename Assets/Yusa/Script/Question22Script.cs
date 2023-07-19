using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Question22Script : MonoBehaviour
{
    public int count,counter;
    public List<Question21PrefabCell> prefabList;
    List<int> shuffledList;
    public int last;
    public int diff;
    public int correctAnswerCount;
    // Start is called before the first frame update
    void Start()
    {
        GenerateQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateQuestion()
    {
        SetShuffledList();
        for (int i = 0; i < shuffledList.Count; i++)
        {
            prefabList[i].text.text = (shuffledList[i]).ToString();
            prefabList[i].value = shuffledList[i];
            prefabList[i].gameObject.SetActive(true);
        }
    }
    void SetShuffledList()
    {
        shuffledList = new List<int>();
        for (int i = 1; i <= count; i++)
            shuffledList.Add(i);

        shuffledList = shuffledList.OrderBy(x => Random.value).ToList();
    }
    public void AnswerQuestion(Question21PrefabCell cell)
    {
        if(cell.value-last==diff)
        {
            correctAnswerCount++;
            last = cell.value;
            cell.gameObject.SetActive(false);
            counter++;
        }
        else
        {
            Debug.Log("Hatalý " + cell.value);
        }

        if (counter == count)
        {
            transform.GetComponent<Question>().FinishQuestion();
        }
    }
}
