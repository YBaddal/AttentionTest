using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level26Scripts : HSingleton<Level26Scripts>
{
    //Singleton
    protected override void Awake()
    {
        base.Awake();
    }

    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private Image levelSelectColor;
    [SerializeField] private List<Color> _colors;

    private Color levelColor;

    void Start()
    {
        Create();
    }

    void Create()
    {
        levelColor = _colors[Random.Range(0, _colors.Count)];
        levelSelectColor.color = levelColor;
        for (int i = 0; i < _grid.transform.childCount; i++)
        {
            int clrIndex = Random.Range(0, _colors.Count);
            if (_colors[clrIndex] != levelColor)
            {
                _grid.transform.GetChild(i).GetComponent<Image>().color = _colors[clrIndex];
                _grid.transform.GetChild(i).GetComponent<Level26ButtonHandler>().colorIndex = clrIndex;
            }
            else
            {
                i--;
            }
        }
    }

    private int levelCount;

    public void Control()
    {
        levelCount = 0;
        for (int i = 0; i < _grid.transform.childCount; i++)
        {
            if (levelColor == _grid.transform.GetChild(i).GetComponent<Image>().color)
            {
                levelCount++;
            }
        }

        if (levelCount == _grid.transform.childCount)
        {
            transform.GetComponent<Question>().point += 10;
            Create();
        }
        
       
    }
}