using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question10PrefabCell : MonoBehaviour
{
    public bool isCorrect;
    public List<Color> colorList;
    public List<Sprite> shapeList;
    public List<string> colorStringList,shapeStringList;
    public Text textField;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        textField = transform.parent.parent.parent.GetChild(2).GetComponent<Text>();
        GeneratePrefab();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GeneratePrefab()
    {
        int rndColor = Random.RandomRange(0, colorList.Count);
        int rndShape = Random.RandomRange(0, shapeList.Count);
        int rndColorString = Random.RandomRange(0, colorStringList.Count);
        int rndShapeString = Random.RandomRange(0, shapeStringList.Count);

        if (isCorrect)
        {
            rndColor = rndColorString;
            rndShape = rndShapeString;
        }
        else
        {
            if (rndColor == rndColorString)
            {
                colorList.Remove(colorList[rndColor]);
                rndColor = Random.RandomRange(0, colorList.Count);
            }
        }

        textField.text = colorStringList[rndColorString];
        textField.color = colorList[rndShapeString];
        image.color = colorList[rndColor];
        image.sprite = shapeList[rndShape];
    }
}
