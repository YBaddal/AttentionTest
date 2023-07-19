using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question7PrefabCell : MonoBehaviour
{
    public int correctAnswer;
    // Start is called before the first frame update
    void Start()
    {
        correctAnswer = Random.RandomRange(0, transform.childCount);
        transform.GetChild(correctAnswer).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetAnim(int anim)
    {
        transform.GetComponent<Animator>().SetInteger("animID", anim);
        Invoke("DestroySelf", 0.3f);
    }
    void DestroySelf()
    {
        Destroy(transform.gameObject);
    }
}
