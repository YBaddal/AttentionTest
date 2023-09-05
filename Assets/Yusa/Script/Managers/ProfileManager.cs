using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProfileManager : MonoBehaviour
{
    public Text nameText, schoolText, classText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetProfile()
    {
        User user = GameManager.instance.user;
        nameText.text = user.name + " " + user.surname;
        schoolText.text = user.school;
        classText.text = user.grade + "/" + user.branch;
    }
}
