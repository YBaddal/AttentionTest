using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishScreenManager : MonoBehaviour
{
    [SerializeField] Text pointText;
    [SerializeField] RectTransform pointNeedle;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetScreen()
    {
        int totalPoint=0;
        foreach (var question in GameManager.instance.questManager.selectedQuestionList)
            totalPoint += question.point;


        float z = totalPoint >= 4400 ? -220 : totalPoint / 20;

        pointText.text = totalPoint.ToString();
        pointNeedle.localRotation = Quaternion.Euler(0, 0, z*-1);
    }
    private void OnEnable()
    {
        animator.SetBool("Reset",true);
        SetScreen();
    }
    public void Like()
    {
        animator.SetBool("Reset", false);

        animator.SetTrigger("Like");
    }
    public void Dislike()
    {
        animator.SetBool("Reset", false);

        animator.SetTrigger("Dislike");
    }
}
