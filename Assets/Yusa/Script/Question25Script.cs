using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Question25Script : MonoBehaviour
{
    public int totalCount, count;
    public int target;
    public List<Question21PrefabCell> prefabList;
    public List<int> shuffledList;
    public List<int> selectedList;
    public int correctAnswerCount;
    public Text targetText;
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
        target = 0;
        SetShuffledList();
        for (int i = 0; i < prefabList.Count; i++)
        {
            prefabList[i].text.text = (shuffledList[i]).ToString();
            prefabList[i].value = shuffledList[i];
            prefabList[i].gameObject.SetActive(false);
            prefabList[i].transform.GetComponent<Toggle>().isOn=false;

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
        while (selectedList.Count < count)
        {
            int rnd = Random.RandomRange(0, shuffledList.Count);
            if (!selectedList.Contains(shuffledList[rnd]-1))
            {
                selectedList.Add(shuffledList[rnd]-1);
                prefabList[shuffledList[rnd]-1].gameObject.SetActive(true);
            }
        }

        int rnd1 = Random.RandomRange(0, selectedList.Count);
        target = prefabList[selectedList[rnd1]].value;
        selectedList.Remove(selectedList[rnd1]);
        int rnd2 = Random.RandomRange(0, selectedList.Count);
        target += prefabList[selectedList[rnd2]].value;

        targetText.text = target.ToString();
    }

    public void AnswerQuestion()
    {
        int answer = 0;
        foreach(var prefab in prefabList)
        {
            if (prefab.GetComponent<Toggle>().isOn)
                answer += prefab.value;
        }

        if (answer == target)
        {
            GenerateQuestion();
            correctAnswerCount++;
        }
    }
}
