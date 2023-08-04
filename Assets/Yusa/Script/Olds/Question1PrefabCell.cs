using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Question1PrefabCell : MonoBehaviour
{
    public List<Sprite> spriteList;
    public List<Color> colorList;
    public Image image;
    public int spriteID,colorID;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        image.sprite = spriteList[spriteID];
        image.color = colorList[colorID];

    }
}
