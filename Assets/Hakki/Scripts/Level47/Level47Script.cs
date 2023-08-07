using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level47Script : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttons;

    [SerializeField] private int levelStep = 3;

    private List<int> steps = new List<int>();

    [SerializeField] private Color clr;

    // Start is called before the first frame update
    void Start()
    {
        //transform.GetComponent<Question>().questionTime = 60;
        Create();
    }

    private void Create()
    {
        ButtonChange(false);
        StartCoroutine(Step());
    }

    private int stepCount;


    void ButtonChange(bool isTrue)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponent<Button>().enabled = isTrue;
        }
    }

    IEnumerator Step()
    {
        int buttonIndex = Random.Range(0, buttons.Count);
        steps.Add(buttonIndex);
        buttons[buttonIndex].transform.GetComponent<Image>().color = clr;
        yield return new WaitForSeconds(0.9f);
        buttons[buttonIndex].transform.GetComponent<Image>().color = Color.white;
        stepCount++;
        if (stepCount != levelStep)
        {
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(Step());
        }
        else
        {
            ButtonChange(true);
            //PlayerStepFollow
        }
    }

    private int controlStep = 0;

    public void Control(int index)
    {
        if (index == steps[controlStep])
        {
            transform.GetComponent<Question>().point += 1;
            StartCoroutine(PlayerStep(index));
            controlStep++;
        }
        else
        {
            buttons[index].transform.GetComponent<Image>().color = Color.red;
            LevelClear();
        }

        if (controlStep == levelStep)
        {
            levelStep++;
            transform.GetComponent<Question>().point += 10;
            DOVirtual.DelayedCall(1, LevelClear);
        }
    }

    IEnumerator PlayerStep(int index)
    {
        buttons[index].transform.GetComponent<Image>().color = clr;
        yield return new WaitForSeconds(0.2f);
        buttons[index].transform.GetComponent<Image>().color = Color.white;
    }

    void LevelClear()
    {
        controlStep = 0;
        stepCount = 0;
        steps.Clear();
        Create();
    }
}