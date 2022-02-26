//类似pokemonwarehouse 搭载在PokemonInfor 下的 Skillchangebar下的viewport下的skill content中 是精灵未学习的技能
using System.Collections;
using System.Collections.Generic;
using Skill_Manager;
using UnityEngine;
using UnityEngine.UI;
using Battlemsg;
using System;

public class UnlearnSkill : MonoBehaviour
{
    private Skill_Scene_Manager ssm;
    private Image skillimage;
    public int pokemonID;//当前精灵ID
    public int mouse_type;
    public Sprite skillsprite;
    public Sprite UIMask;
    public PokemonSkill pokemonskill;
    public PlayerPokemon playerpokemon;
    GameObject web;
    websocket ws;

    //On_Unlearn_Button表示点击按钮
    //技能操作总包括两次点击，例如先点击未学习技能再点击已学习技能来替换，  mousetype表示两次点击中的第几次点击，并执行相应操作
    async public void On_Unlearn_Button()
    {
        int MouseType = ssm.handletype;
        if (MouseType == 0)
        {
            //改变按钮image
            skillimage.sprite = Resources.Load<Sprite>("Bag/skillyes");
            ssm.handletype = mouse_type;
            //将技能存入全局变量中
            ssm.previousbuttonskill = pokemonskill;
            ssm.previousindex = 0;
           //只需将技能存入previous技能
           //previousindex = 0 表示第一次点击为未学习技能
        }
        else if (MouseType != 0) 
        {

            //替换技能，需要知道上次操作是已学习技能还是未学习技能
            //已学习的便替换，未学习的更新选中技能
            if (ssm.previousindex == 0)
            {
                ssm.previousbuttonskill = pokemonskill;
            }

            if (ssm.previousindex != 0) {
                await ws.sendMsgAsync("learn_skill\n" + pokemonID + " " + ssm.previousindex + " " + pokemonskill.id);
                String answer = await ws.receiveMsgAsync();
                //操作结束
                ssm.handletype = 0;
                //刷新技能
                await GameObject.Find("SkillLoad").GetComponent<SkillLoad>().PokemonSkillLoad(pokemonID);
                await GameObject.Find("SkillLoad").GetComponent<SkillLoad>().PokemonUnlearnSkillLoad(pokemonID);
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        web = GameObject.Find("websocket");
        ws = web.GetComponent<websocket>();
        ssm = Skill_Scene_Manager.GetInstance();
        skillimage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
