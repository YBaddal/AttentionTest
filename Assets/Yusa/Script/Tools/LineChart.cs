using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineChart : MonoBehaviour
{
    public List<RectTransform> allPoint = new List<RectTransform>();

    public GameObject linePrefab;

    private void Start()
    {

    }

    private void Update()
    {
    }

    public void UpdateLineChart()
    {
        
        for (int a = 0; a < allPoint.Count; a++)
        {
            if (a < allPoint.Count - 1)
            {
                //if(allPoint[a].childCount > 0)
                //    Destroy(allPoint[a].GetChild(0).gameObject);
                GameObject line = GameObject.Instantiate<GameObject>(linePrefab);
                line.transform.SetParent(allPoint[a]);
                line.transform.localPosition = Vector3.zero;
                line.transform.localScale = new Vector3(1, 0.1f, 1);
            }
        }

        for (int a = 0; a < allPoint.Count-1; a++)
        {

            if(allPoint[a+1]!=null)
            {
                Vector3 v = (allPoint[a + 1].anchoredPosition - allPoint[a].anchoredPosition);
             
                allPoint[a].GetChild(0).GetComponent<RectTransform>().sizeDelta =new Vector2( v.magnitude,50);
                allPoint[a].GetChild(0).right = v;

            }
        }
    }
}
