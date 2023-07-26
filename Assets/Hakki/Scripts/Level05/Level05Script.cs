using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level05Script : MonoBehaviour
{
    [SerializeField] List<Image> buttons;
    [SerializeField] List<Color> colors;
    [SerializeField] List<string> colorNameList;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI levelEndText;


    List<Color> levelColors = new List<Color>();

    private bool isNormal = true;

    void Start()
    {
        Create();
    }


    private int indexLevel;

    void Create()
    {
        indexLevel = Random.Range(0, colors.Count);
        Color clrLevel = colors[indexLevel];
        string colorName = colorNameList[indexLevel];
        levelColors.Add(clrLevel);

        for (int i = 0; i < 2; i++)
        {
            Color clr = colors[Random.Range(0, colors.Count)];

            if (!levelColors.Contains(clr))
            {
                levelColors.Add(clr);
            }
            else
            {
                i--;
            }
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].color = levelColors[i];
        }

        levelText.text = colorName;
        levelText.color = colors[Random.Range(0, colors.Count)];

        if (Random.Range(0, 100) > 60)
        {
            isNormal = false;
            levelEndText.text = "renkli olmayan butona basınız.";
        }
        else
        {
            isNormal = true;
            levelEndText.text = "renkli olan butona basınız.";
        }
    }

    public void Control(Image img)
    {
        if (img.color == colors[indexLevel] && isNormal)
        {
            transform.GetComponent<Question>().point += 10;
        }
        else if (img.color != colors[indexLevel] && !isNormal)
        {
            transform.GetComponent<Question>().point += 10;
        }
        else
        {
            Debug.Log(false);
        }
        levelColors.Clear();
        Create();
    }
}