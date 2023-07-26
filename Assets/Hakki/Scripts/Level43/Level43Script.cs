using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level43Script : MonoBehaviour
{
    [SerializeField] private List<Sprite> items;
    [SerializeField] GridLayoutGroup grid;
    [SerializeField] Image selectImage;

    private List<Sprite> levelsShaker = new List<Sprite>();

    void Start()
    {
        Create();
    }


    void Create()
    {
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            Sprite spr = items[Random.Range(0, items.Count)];

            if (!levelsShaker.Contains(spr))
            {
                grid.transform.GetChild(i).GetComponent<Image>().sprite = spr;
                levelsShaker.Add(spr);
            }
            else
            {
                i--;
            }
        }

        selectImage.sprite = items[Random.Range(0, items.Count)];
    }

    // Update is called once per frame
    void Update()
    {
    }

   public void Control(Image img)
    {
        if (img.sprite == selectImage.sprite)
        {
            transform.GetComponent<Question>().point += 10;
        }
        else
        {
            Debug.Log(false);
        }

        levelsShaker.Clear();
        Create();
    }
}