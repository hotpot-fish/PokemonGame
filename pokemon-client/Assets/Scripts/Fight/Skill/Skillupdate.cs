using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Battlemsg;
//绑在skillImage上，技能按钮初始化
//玩家操作后需要设置不可点击
public class Skillupdate : MonoBehaviour
{
    public GameObject[] buttons=new GameObject[4];
    private int i = 0;
    public void Skillchange(PokemonInBattle my)
    {
        if (i == 0)
        {
            buttons[0] = GameObject.Find("skillButton1");
            buttons[1] = GameObject.Find("skillButton2");
            buttons[2] = GameObject.Find("skillButton3");
            buttons[3] = GameObject.Find("skillButton4");
            i = 1;
        }
        //需要根据宝可梦信息设置显示
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
        SkillInBattle[] skills = my.skills;
        int index = 0;
        //存在则显示
        foreach (SkillInBattle skill in skills)
        {
            if(skill != null)
            {
                index++;
                string name = skill.skill.name;
                string describe = skill.skill.describe;
                int curPP = skill.curPP;
                int maxPP = skill.skill.maxPP;
                int power = skill.skill.power;
                buttons[index - 1].SetActive(true);
                buttons[index - 1].transform.GetChild(0).GetComponent<Text>().text = name+"\n\t威力："+power+"\n\tPP "+curPP+"/"+maxPP;
                buttons[index - 1].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = describe;
                buttons[index - 1].GetComponent<Button>().interactable = false;
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

    public void Setflag(bool a) {
        for (int i = 0; i < 4; i++) {
            buttons[i].GetComponent<Button>().interactable = a;
        }
    }
    public void Show(PokemonInBattle my)
    {//根据信息设置可用性
        int index = 0;
        SkillInBattle[] skills = my.skills;
        foreach (SkillInBattle skill in skills)
        {
            if (skill != null)
            {
                index++;
                int curPP = skill.curPP;
                if (curPP == 0)
                {
                    buttons[index - 1].GetComponent<Button>().interactable = false;
                }
                else
                {
                    buttons[index - 1].GetComponent<Button>().interactable = true;
                }
            }
        }
    }
}
