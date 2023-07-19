using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        int level = PlayerPrefs.GetInt("Level");
        transform.GetChild(level).gameObject.SetActive(true);
    }
    
}
