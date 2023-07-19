using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLayoutManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        SetLayout();
    }
    public void SetLayout()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            float y = 0.75f * i;

            if (i > 6)
                y = 0.75f * 6;

            transform.GetChild(i).localPosition = new Vector3(0,y,i);
        }
    }
}
