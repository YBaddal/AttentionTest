using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level23Script : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private Image levelSelectColor;
    [SerializeField] private List<Color> _colors;

    [SerializeField] TextMeshProUGUI answerText;

    private List<Color> levelColor = new List<Color>();

    private int levelColorCount;

    void Start()
    {
        CreateLevel();
    }

    void CreateLevel()
    {
        //Select Level Color
        for (int i = 0; i < Random.Range(2, 4); i++)
        {
            Color clr = _colors[Random.Range(0, _colors.Count)];
            if (!levelColor.Contains(clr))
            {
                levelColor.Add(clr);
            }
        }

        levelSelectColor.color = levelColor[Random.Range(0, levelColor.Count)];

        //placing colors on the grid
        for (int i = 0; i < _grid.transform.childCount; i++)
        {
            Color clr = levelColor[Random.Range(0, levelColor.Count)];
            if (clr == levelSelectColor.color)
            {
                levelColorCount++;
            }

            _grid.transform.GetChild(i).GetComponent<Image>().color = clr;
        }

        Debug.Log(levelColorCount);
    }

    void ClearLevel()
    {
        levelColor.Clear();
        levelColorCount = 0;
        answerText.text = "10";
        CreateLevel();
    }

    public void AnswerText(int amount)
    {
        answerText.text = (int.Parse(answerText.text) + amount).ToString();
    }


    public void Control()
    {
        if (levelColorCount == int.Parse(answerText.text))
        {
            transform.GetComponent<Question>().point += 10;
        }
        else
        {
            Debug.Log(false);
        }

        ClearLevel();
    }
}