using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question8PrefabCell : MonoBehaviour
{
    public bool isCorrect;
    public List<Color> colorList;
    public List<string> stringList;
    public Text textField;
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
        int rndColor  = Random.RandomRange(0,colorList.Count);
        int rndString = Random.RandomRange(0, stringList.Count);
        if (isCorrect)
            rndColor = rndString;

        textField.text = stringList[rndString];
        textField.color = colorList[rndColor];
    }
}
