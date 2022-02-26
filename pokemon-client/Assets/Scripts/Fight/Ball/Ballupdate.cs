using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlemsg;
using UnityEngine.UI;
//����ballImage�ϣ�������ť��ʼ��
//��Ҳ�������Ҫ���ò��ɵ��
public class Ballupdate : MonoBehaviour
{
    private int j = 0;
    GameObject[] ball = new GameObject[4];
    string[] ballName = { "����������", "�м�������", "�߼�������", "��ʦ������" };
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
        //����Ҫ���ݱ�������Ϣ������ʾ
        for (int i = 0; i < 4; i++)
        {//�����ʼ��
            ball[i].GetComponent<Button>().interactable = false;
            ball[i].transform.GetChild(0).GetComponent<Text>().text = ballName[i] + "\n" + "ӵ�У�0��";
        }
        foreach (PlayerBall playerBall in ballList)//��������0�Ŵ�
        {//���ó�ʼ��
            if (playerBall != null)
            {
                int index = playerBall.ball.id - 1;
                ball[index].transform.GetChild(0).GetComponent<Text>().text = ballName[index] + "\n" + "ӵ�У�" + playerBall.num + "��";
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
    {//������Ϣ���ÿ�����
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
