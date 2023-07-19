using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question15PrefabCell : MonoBehaviour
{
    public bool isCorrect;
    public List<Color> colorList;
    public List<string> colorStringList;
    public Text meanText, wordText;
    // Start is called before the first frame update
    void Start()
    {
        GeneratePrefab();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GeneratePrefab()
    {
        int rndColorString = Random.RandomRange(0, colorStringList.Count);
        int rndColor = Random.RandomRange(0, colorList.Count);

        int rnd1 = Random.RandomRange(0, colorStringList.Count);
        int rnd2 = Random.RandomRange(0, colorStringList.Count);


        if (isCorrect)
        {
            meanText.text = colorStringList[rndColorString];
            meanText.color = colorList[rnd1];

            wordText.color = colorList[rndColorString];
            wordText.text = colorStringList[rnd2];
        }
        else
        {
            if(rndColorString==rndColor)
            {
                colorList.Remove(colorList[rndColorString]);
                rndColor = Random.RandomRange(0, colorList.Count);

            }
            meanText.text = colorStringList[rndColorString];
            meanText.color = colorList[rnd1];

            wordText.color = colorList[rndColor];
            wordText.text = colorStringList[rnd2];

        }
    }

}
