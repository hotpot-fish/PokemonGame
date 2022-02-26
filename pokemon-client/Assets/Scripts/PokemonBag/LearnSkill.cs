//类似pokemonwarehouse 搭载在PokemonInfor 下的 Skillimage 下的 四个skillbutton上 是精灵已学习的技能
using System.Collections;
using System.Collections.Generic;
using Skill_Manager;
using UnityEngine;
using UnityEngine.UI;
using Battlemsg;

public class LearnSkill : MonoBehaviour
{
    private Skill_Scene_Manager ssm;
    private Image skillimage;
    public int pokemonID;//当前精灵ID
    public int mouse_type;
    public int index;
    public Sprite skillsprite;
    public Sprite UIMask;
    public PokemonSkill pokemonskill;
    GameObject web;
    websocket ws;
    // Start is called before the first frame update
    void Awake()
    {
        pokemonskill = new PokemonSkill();
        web = GameObject.Find("websocket");
        ws = web.GetComponent<websocket>();
        ssm = Skill_Scene_Manager.GetInstance();
        skillimage = GetComponent<Image>();
    }

    //On_learn_Button表示点击按钮
    //技能操作总包括两次点击，例如先点击未学习技能再点击已学习技能来替换，  mousetype表示两次点击中的第几次点击，并执行相应操作
    async public void On_Learn_Button()
    {
        int MouseType = ssm.handletype;
        if (MouseType == 0)
        {
            //改变按钮image
            skillimage.sprite = Resources.Load<Sprite>("Bag/skillyes");
            /*skillimage.sprite = Resources.Load<Sprite>("PokemonSprite/" + playerpokemon.pokemon.image.ToString() + "c");*/

            ssm.handletype = mouse_type;
            ssm.previousbuttonskill = pokemonskill;
            ssm.previousindex = index;
            
            //将技能存入previous技能
            //将按钮index存到静态类 previousindex中
        }
        else if (MouseType != 0)
        {
            if (ssm.previousindex == 0)
            {
                await ws.sendMsgAsync("learn_skill\n" + pokemonID + " " + index + " " + ssm.previousbuttonskill.id);
            }
            else {
                await ws.sendMsgAsync("swap_skill\n" + pokemonID + " " + index + " " + ssm.previousindex);
            }
            
            //操作结束
            await ws.receiveMsgAsync();
            ssm.handletype = 0;
            //刷新技能
            await GameObject.Find("SkillLoad").GetComponent<SkillLoad>().PokemonSkillLoad(pokemonID);
            await GameObject.Find("SkillLoad").GetComponent<SkillLoad>().PokemonUnlearnSkillLoad(pokemonID);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
