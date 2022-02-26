//搭载在skillload空组件上与 pokemonload作用类似，都是加载按钮并赋予按钮技能或精灵属性
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Battlemsg;
using System.Threading.Tasks;
using UnityEngine.UI;

public class SkillLoad : MonoBehaviour
{

    // Start is called before the first frame update
    GameObject skillbutton;
    GameObject web;
    websocket ws;
    PlayerPokemon playerpokemon;
    PokemonSkill[] unlearnSkill;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    async public Task PokemonSkillLoad(int playerpokemonid)
    {
        web = GameObject.Find("websocket");
        ws = web.GetComponent<websocket>();
        await ws.sendMsgAsync("get_player_pokemon\n" + playerpokemonid);
        String answer = await ws.receiveMsgAsync();
        String[] message = answer.Split('\n');
        playerpokemon = JsonMapper.ToObject<Battlemsg.PlayerPokemon>(message[1]);

        for (int i = 0; i < 4; i++)
        {
            GameObject skillbutton = GameObject.Find("SkillButton"+(i+1).ToString());
            skillbutton.GetComponent<LearnSkill>().index = i + 1;
            skillbutton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bag/skill");
            if (playerpokemon.skillList[i] != null)
            {
                Skill skill = playerpokemon.skillList[i];
                skillbutton.GetComponent<LearnSkill>().mouse_type = skill.id;
                skillbutton.GetComponent<LearnSkill>().pokemonskill.skill = skill;
                skillbutton.transform.GetChild(0).GetComponent<Text>().text = skill.name + "\n威力： " + skill.power + "\nPP: " + skill.maxPP + "/" + skill.maxPP;
            }
            else
            {
                skillbutton.GetComponent<LearnSkill>().mouse_type = 1;
                skillbutton.transform.GetChild(0).GetComponent<Text>().text = "待学习";
            }
            skillbutton.GetComponent<LearnSkill>().pokemonID = playerpokemonid;
        }

    }
    async public Task PokemonUnlearnSkillLoad(int playerpokemonid)
    {
        web = GameObject.Find("websocket");
        ws = web.GetComponent<websocket>();
        await ws.sendMsgAsync("get_unlearn_skill\n" + playerpokemonid);
        String answer = await ws.receiveMsgAsync();
        String[] message = answer.Split('\n');
        unlearnSkill = JsonMapper.ToObject<Battlemsg.PokemonSkill[]>(message[1]);
        skillbutton = (GameObject)Resources.Load("Bag/SkillButton");

        for (int i = 0; i < GameObject.Find("SkillContent").transform.childCount; i++)
        {
            Destroy(GameObject.Find("SkillContent").transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < unlearnSkill.Length; i++)
        {
            if (unlearnSkill[i] != null)
            {

                GameObject a = Instantiate(skillbutton);
                a.transform.SetParent(GameObject.Find("SkillContent").transform);
                a.name = "Skill" + i.ToString();
                Skill skill = unlearnSkill[i].skill;
                a.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bag/skill");
                a.GetComponent<UnlearnSkill>().mouse_type = skill.id;
                a.GetComponent<UnlearnSkill>().pokemonskill = unlearnSkill[i];
                a.GetComponent<UnlearnSkill>().pokemonID = playerpokemonid;
                a.transform.GetChild(0).GetComponent<Text>().text = skill.name + "\n威力： " + skill.power + "\nPP: " + skill.maxPP + "/" + skill.maxPP;
            }
        }
    }
}
