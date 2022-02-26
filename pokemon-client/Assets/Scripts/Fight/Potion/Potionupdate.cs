using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlemsg;
using UnityEngine.UI;

//挂在potionImage上，药水道具初始化
//玩家操作后需要设置不可点击
public class Potionupdate : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] potions = new GameObject[6];
    private int j = 0;
    string[] potionName = {"小型HP药剂", "中型HP药剂", "大型HP药剂", "小型PP药剂", "中型PP药剂", "大型PP药剂"};
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
        //不需要根据宝可梦信息设置显示
        for (int i = 0; i < 6; i++)
        {//界面初始化
            potions[i].GetComponent<Button>().interactable = false;
            potions[i].transform.GetChild(0).GetComponent<Text>().text = potionName[i] + "\n" + "拥有：0瓶";
        }
        foreach (PlayerPotion playerPotion in potionList)//数量大于0才传
        {//可用初始化
            if (playerPotion != null)
            {
                int index = playerPotion.potion.id - 1;
                potions[index].transform.GetChild(0).GetComponent<Text>().text = potionName[index] + "\n" + "拥有：" + playerPotion.num + "瓶";
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
    {//根据信息设置可用性
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
