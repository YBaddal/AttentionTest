using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level06Button : MonoBehaviour
{
    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => Control());
    }

    void Control()
    {
        Level06Create.Instance.Control(transform.GetSiblingIndex());
    }
}