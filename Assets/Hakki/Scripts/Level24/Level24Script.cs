using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Level24Script : HSingleton<Level24Script>
{
    //Singleton
    protected override void Awake()
    {
        base.Awake();
    }


    [SerializeField] GridLayoutGroup _grid;

    private float speed;
    private float maxSpeed = 3;

    private void Start()
    {
        Create();
    }

    void Create()
    {
        for (int i = 0; i < _grid.transform.childCount; i++)
        {
            _grid.transform.GetChild(i).eulerAngles = new Vector3(0, 0, 0);
            speed = (float)Random.Range(10, 40) / 10;

            _grid.transform.GetChild(i).GetComponent<Level24ButtonHandler>().StartMove(speed);
            if (maxSpeed > speed)
            {
                maxSpeed = speed;
            }
        }
    }

    public void Control(float wheelSpeed)
    {
        for (int i = 0; i < _grid.transform.childCount; i++)
        {
            _grid.transform.GetChild(i).eulerAngles = new Vector3(0, 0, 0);
            _grid.transform.GetChild(i).GetComponent<Level24ButtonHandler>()._tween.Pause();
        }


        if (wheelSpeed == maxSpeed)
        {
            transform.GetComponent<Question>().point += 10;
            Debug.Log(true);
        }
        else
        {
            Debug.Log(false);
        }

        speed = 3f;
        DOVirtual.DelayedCall(2f, Create);
    }
}