using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLoadFromPrefs : MonoBehaviour
{
    public string pref;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        string text = PlayerPrefs.GetString(pref);
        transform.GetComponent<Text>().text = text;
    }

}
