using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorFromLevel : MonoBehaviour
{
    public List<Color> colors;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        int level = PlayerPrefs.GetInt("Level");
        transform.GetComponent<Text>().color = colors[level];
    }

}
