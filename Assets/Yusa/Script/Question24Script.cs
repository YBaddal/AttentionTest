using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Question24Script : MonoBehaviour
{
    public int totalCount,count, counter;
    public List<Question21PrefabCell> prefabList;
    List<int> shuffledList;
    List<int> selectedList;
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
            prefabList[i].gameObject.SetActive(false);
        }
        SetSelectedList();
    }
    void SetShuffledList()
    {
        shuffledList = new List<int>();
        for (int i = 1; i <= totalCount; i++)
            shuffledList.Add(i);

        shuffledList = shuffledList.OrderBy(x => Random.value).ToList();
    }
    void SetSelectedList()
    {
        selectedList = new List<int>();
        for (int i = 0; i < count; i++)
        {
            int rnd = Random.RandomRange(0, shuffledList.Count);
            selectedList.Add((shuffledList[rnd]-1));
            prefabList[(shuffledList[rnd] - 1)].gameObject.SetActive(true);

            shuffledList.Remove((shuffledList[rnd]));
        }
    }

    public void AnswerQuestion(Question21PrefabCell cell)
    {
        bool isLower=false;
        Debug.Log("Number :" + cell.value);
        for(int i=0;i<selectedList.Count;i++)
        {
            if (cell.value > prefabList[selectedList[i]].value)
                isLower = true;
        }

        Debug.Log("index: " + cell.gameObject.transform.GetSiblingIndex());

        if(!isLower)
        {
            counter++;
            correctAnswerCount++;
            cell.gameObject.SetActive(false);
            selectedList.Remove(cell.gameObject.transform.GetSiblingIndex());
            Debug.Log("En Küçük");
        }
        else
        {
            Debug.Log("Hatalý");
        }

        if (counter == count)
        {
            counter = 0;
            GenerateQuestion();
            //transform.GetComponent<Question>().FinishQuestion();
        }
    }
}
