using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LeaderboardCell : MonoBehaviour
{
    [SerializeField] Text rankText, nameText, pointText;
    [SerializeField] Image profileImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCell(LeaderboardModel leaderboard,int index)
    {
        rankText.text ="#"+(index+1);
        nameText.text = leaderboard.name+" "+leaderboard.surname;
        pointText.text = leaderboard.point.ToString();
        profileImage.sprite = GameManager.instance.profileImages[leaderboard.avatar];
    }
}
