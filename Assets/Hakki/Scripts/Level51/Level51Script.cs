using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level51Script : MonoBehaviour
{
    [SerializeField] private List<Sprite> hands;
    private Sprite levelSprite;
    [SerializeField] private Image mainImage;

    void Start()
    {
        //transform.GetComponent<Question>().questionTime = 60f;
        Create();
    }

    private void Create()
    {
        levelSprite = hands[Random.Range(0, hands.Count)];
        mainImage.sprite = levelSprite;
    }

    public void Control(string leftOrRight)
    {
        if (levelSprite.name.Contains(leftOrRight))
        {
            transform.GetComponent<Question>().point += 2;
        }

        DOVirtual.DelayedCall(0.2f, Create);
    }
}