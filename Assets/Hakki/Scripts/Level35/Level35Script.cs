using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level35Script : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;

    void Start()
    {
       // transform.GetComponent<Question>().questionTime = 60f;
        Create();
    }

    private float gridItemSize;
    private float maxGridItemSize = 0f;

    void Create()
    {
        maxGridItemSize = 0f;
        
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            gridItemSize = Random.Range(4, 20)*4;
            gridItemSize /= 100f;
            maxGridItemSize = maxGridItemSize < gridItemSize ? gridItemSize : maxGridItemSize;

            grid.transform.GetChild(i).GetChild(0).localScale = Vector3.one * gridItemSize;
        }
    }

    public void Control(Transform trans)
    {
        if (trans.localScale.x == maxGridItemSize)
        {
            Debug.Log(true);
            transform.GetComponent<Question>().point += 10;
        }
        else
        {
            Debug.Log(trans.localScale.x + "  " + maxGridItemSize);
        }

        Create();
        
    }
}