using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level10Create : MonoBehaviour
{
    [SerializeField] List<Color> colors;
    [SerializeField] GridLayoutGroup _grid;

    void Start()
    {
        //transform.GetComponent<Question>().questionTime = 60f;
        Create();
    }

    private Color diffColor;

    void Create()
    {
        Color color = colors[Random.Range(0, colors.Count)];

        for (int i = 0; i < _grid.transform.childCount; i++)
        {
            _grid.transform.GetChild(i).GetComponent<Image>().color = color;
        }

        diffColor = color * (1 - (Random.Range(0, 10) > 5 ? 0.2f : -0.2f));

        _grid.transform.GetChild(Random.Range(0, _grid.transform.childCount)).GetComponent<Image>().color = diffColor;
    }

    private bool isTrue = false;

    public void Control(Image img)
    {
        if (img.color == diffColor)
        {
            //ScoreUpdate
            transform.GetComponent<Question>().point += 10;
            isTrue = true;
            StartCoroutine(Next());
            return;
        }

        StartCoroutine(Next());
    }

    IEnumerator Next()
    {
        yield return new WaitForSeconds(1f);
        isTrue = false;
        Create();
    }
}