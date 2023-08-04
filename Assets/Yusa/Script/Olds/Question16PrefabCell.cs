using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question16PrefabCell : MonoBehaviour
{
    public List<Color> colorList;
    public string colorString;
    public Text  wordText;
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
        int rndColor = Random.RandomRange(0, colorList.Count);

            wordText.color = colorList[rndColor];
            wordText.text = colorString;
        
    }
}
