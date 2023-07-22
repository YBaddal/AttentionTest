using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level14Create : MonoBehaviour
{
    public Color startColor = new Color(0.588f, 0.294f, 0.0f);
    public Color endColor = Color.white;
    public List<Color> colors = new List<Color>();

    [SerializeField] private GridLayoutGroup _grid;

    public List<int> numbers = new List<int>();

    void Start()
    {
        Create();
    }

    int numberOfColors = 12;

    private void Create()
    {
        for (int i = 0; i < numberOfColors; i++)
        {
            float t = i / (float)(numberOfColors - 1);
            Color color = Color.Lerp(startColor, endColor, t);
            colors.Add(color);
        }

        for (int i = 0; i < colors.Count; i++)
        {
            int number = Random.Range(0, _grid.transform.childCount);

            if (!numbers.Contains(number))
            {
                _grid.transform.GetChild(number).GetComponent<Image>().color = colors[i];
                numbers.Add(number);
            }
            else
            {
                i--;
            }
        }
    }

    private bool isSmallToBig = true;
    private int smallToBigIndex = 12;
    private int bigToSmallIndex = -1;
    private int levelIndex = 0;

    public void Control(Image img)
    {
        if (isSmallToBig)
        {
            smallToBigIndex--;
        }
        else
        {
            bigToSmallIndex++;
        }

        if (img.color == colors[isSmallToBig ? smallToBigIndex : bigToSmallIndex])
        {
            img.GetComponent<Button>().enabled = false;
            levelIndex++;
            transform.GetComponent<Question>().point += 1;
        }
        else
        {
            LevelClear();
        }


        if (levelIndex == numberOfColors)
        {
            transform.GetComponent<Question>().point += 10;
            LevelClear();

            if (isSmallToBig)
            {
                isSmallToBig = false;
                transform.GetComponent<Question>().tutorialText = "Renkleri Koyudan Açığa Doğru Sıralayınız.";
            }
            else
            {
                isSmallToBig = true;
                transform.GetComponent<Question>().tutorialText = "Renkleri Açıktan Koyuya Doğru Sıralayınız.";
            }
        }
    }

    private void LevelClear()
    {
        smallToBigIndex = 12;
        bigToSmallIndex = -1;
        levelIndex = 0;
        colors.Clear();
        numbers.Clear();


        for (int i = 0; i < _grid.transform.childCount; i++)
        {
            _grid.transform.GetChild(i).GetComponent<Button>().enabled = true;
        }

        Create();
    }
}