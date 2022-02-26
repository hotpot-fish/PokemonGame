using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlemsg;
using UnityEngine.UI;
//绑在ballImage上，精灵球按钮初始化
//玩家操作后需要设置不可点击
public class Ballupdate : MonoBehaviour
{
    private int j = 0;
    GameObject[] ball = new GameObject[4];
    string[] ballName = { "初级精灵球", "中级精灵球", "高级精灵球", "大师精灵球" };
    public void Ballchange(PlayerBall[] ballList)
    {
        if (j == 0)
        {
            ball[0] = GameObject.Find("Ball1");
            ball[1] = GameObject.Find("Ball2");
            ball[2] = GameObject.Find("Ball3");
            ball[3] = GameObject.Find("Ball4");
            j = 1;
        }
        //不需要根据宝可梦信息设置显示
        for (int i = 0; i < 4; i++)
        {//界面初始化
            ball[i].GetComponent<Button>().interactable = false;
            ball[i].transform.GetChild(0).GetComponent<Text>().text = ballName[i] + "\n" + "拥有：0个";
        }
        foreach (PlayerBall playerBall in ballList)//数量大于0才传
        {//可用初始化
            if (playerBall != null)
            {
                int index = playerBall.ball.id - 1;
                ball[index].transform.GetChild(0).GetComponent<Text>().text = ballName[index] + "\n" + "拥有：" + playerBall.num + "个";
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setflag(bool a)
    {
        for (int i = 0; i < 4; i++)
        {
            ball[i].GetComponent<Button>().interactable = a;
        }
    }
    public void Show(PlayerBall[] ballList)
    {//根据信息设置可用性
        foreach (PlayerBall playerBall in ballList)
        {
            if (playerBall != null)
            {
                int index = playerBall.ball.id - 1;
                if (playerBall.num == 0)
                {
                    ball[index].GetComponent<Button>().interactable = false;
                }
                else
                {
                    ball[index].GetComponent<Button>().interactable = true;
                }
            }
        }
    }
}
