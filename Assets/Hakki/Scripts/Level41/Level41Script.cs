using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level41Script : MonoBehaviour
{
    [SerializeField] List<string> sentence;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GridLayoutGroup buttonGrid;

    private List<int> buttonText = new List<int>();
    private int wordCount;

    void Start()
    {
        //transform.GetComponent<Question>().questionTime = 60f;
        Create();
    }

    void Create()
    {
        levelText.text = sentence[Random.Range(0, sentence.Count)];
        string[] levelcount = levelText.text.Split(' ');

        wordCount = levelcount.Length;
        buttonText.Add(wordCount);
        //CreateButtonText
        for (int i = 0; i < buttonGrid.transform.childCount - 1; i++)
        {
            int randomNumber = Random.Range(wordCount - 3, wordCount + 5);
            if (!buttonText.Contains(randomNumber))
            {
                buttonText.Add(randomNumber);
            }
            else
            {
                i--;
            }
        }

        for (int i = 0; i < buttonGrid.transform.childCount; i++)
        {
            int randomText = Random.Range(0, buttonText.Count);

            buttonGrid.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                buttonText[randomText].ToString();
            buttonText.RemoveAt(randomText);
        }
    }

    public void Control(TextMeshProUGUI text)
    {

        if (wordCount == int.Parse(text.text))
        {
            transform.GetComponent<Question>().point += 10;
        }
        else
        {
            Debug.Log(false);
        }
        
        Create();
    }
}