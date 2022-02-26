using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlemsg;
using UnityEngine.UI;

//����potionImage�ϣ�ҩˮ���߳�ʼ��
//��Ҳ�������Ҫ���ò��ɵ��
public class Potionupdate : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] potions = new GameObject[6];
    private int j = 0;
    string[] potionName = {"С��HPҩ��", "����HPҩ��", "����HPҩ��", "С��PPҩ��", "����PPҩ��", "����PPҩ��"};
    public void Potionchange(PlayerPotion[] potionList)
    {//
        if (j == 0)
        {
            potions[0] = GameObject.Find("Hp1");
            potions[1] = GameObject.Find("Hp2");
            potions[2] = GameObject.Find("Hp3");
            potions[3] = GameObject.Find("Pp1");
            potions[4] = GameObject.Find("Pp2");
            potions[5] = GameObject.Find("Pp3");
            j = 1;
        }
        //����Ҫ���ݱ�������Ϣ������ʾ
        for (int i = 0; i < 6; i++)
        {//�����ʼ��
            potions[i].GetComponent<Button>().interactable = false;
            potions[i].transform.GetChild(0).GetComponent<Text>().text = potionName[i] + "\n" + "ӵ�У�0ƿ";
        }
        foreach (PlayerPotion playerPotion in potionList)//��������0�Ŵ�
        {//���ó�ʼ��
            if (playerPotion != null)
            {
                int index = playerPotion.potion.id - 1;
                potions[index].transform.GetChild(0).GetComponent<Text>().text = potionName[index] + "\n" + "ӵ�У�" + playerPotion.num + "ƿ";
            }
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setflag(bool a)
    {
        for (int i = 0; i < 6; i++)
        {
            potions[i].GetComponent<Button>().interactable = a;
        }
    }
    public void Show(PlayerPotion[] potionList)
    {//������Ϣ���ÿ�����
        foreach (PlayerPotion playerPotion in potionList)
        {
            if (playerPotion != null)
            {
                int index = playerPotion.potion.id - 1;
                if (playerPotion.num == 0)
                {
                    potions[index].GetComponent<Button>().interactable = false;
                }
                else
                {
                    potions[index].GetComponent<Button>().interactable = true;
                }
            }
        }
    }
}
