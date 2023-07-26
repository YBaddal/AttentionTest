using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class Level24ButtonHandler : MonoBehaviour
{
    float speed = 1;
    public Tween _tween;
    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => Control());
    }

    public void StartMove(float spd)
    {
        speed = spd;
        _tween = transform.DORotate(new Vector3(0, 0, -180), speed).SetLoops(-1, LoopType.Incremental) .SetEase(Ease.Linear);
        _tween.Play();
    }

    public void Control()
    {
        Level24Script.Instance.Control(speed);
    }
}