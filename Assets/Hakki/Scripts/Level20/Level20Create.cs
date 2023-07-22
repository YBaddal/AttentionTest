using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level20Create : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private Sprite sad;
    [SerializeField] private Sprite smile;

    void Start()
    {
        Create();
    }

    private List<int> numbers = new List<int>();

    private int difImageCount = 4;

    private bool isSmile = true;

    void Create()
    {
        for (int i = 0; i < difImageCount; i++)
        {
            int number = Random.Range(0, 70);
            if (!numbers.Contains(number))
            {
                numbers.Add(number);
            }
            else
            {
                i--;
            }
        }


        for (int i = 0; i < 70; i++)
        {
            _grid.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            _grid.transform.GetChild(i).GetComponent<Button>().enabled = true;
            if (!numbers.Contains(i))
            {
                _grid.transform.GetChild(i).GetChild(1).GetComponent<Image>().sprite = isSmile ? smile : sad;
            }
            else
            {
                _grid.transform.GetChild(i).GetChild(1).GetComponent<Image>().sprite = isSmile ? sad : smile;
            }
        }
    }

    public int leveIndex = 0;

    public void Control(Image img)
    {
        if (isSmile ? sad : smile == img.sprite)
        {
            transform.GetComponent<Question>().point += 1;
            img.transform.GetComponent<Button>().enabled = false;
            img.transform.GetChild(0).gameObject.SetActive(true);
            leveIndex++;
        }
        else
        {
            Debug.Log(false);
            LevelClear();
        }


        if (leveIndex == difImageCount)
        {
            transform.GetComponent<Question>().point += 10;
            if (isSmile)
            {
                isSmile = false;
            }
            else
            {
                isSmile = true;
            }

            LevelClear();
        }
    }

    private void LevelClear()
    {
        leveIndex = 0;
        Create();
        numbers.Clear();
    }
}