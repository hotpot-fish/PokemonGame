//����pokemonwarehouse ������PokemonInfor �µ� Skillchangebar�µ�viewport�µ�skill content�� �Ǿ���δѧϰ�ļ���
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
    public int pokemonID;//��ǰ����ID
    public int mouse_type;
    public Sprite skillsprite;
    public Sprite UIMask;
    public PokemonSkill pokemonskill;
    public PlayerPokemon playerpokemon;
    GameObject web;
    websocket ws;

    //On_Unlearn_Button��ʾ�����ť
    //���ܲ����ܰ������ε���������ȵ��δѧϰ�����ٵ����ѧϰ�������滻��  mousetype��ʾ���ε���еĵڼ��ε������ִ����Ӧ����
    async public void On_Unlearn_Button()
    {
        int MouseType = ssm.handletype;
        if (MouseType == 0)
        {
            //�ı䰴ťimage
            skillimage.sprite = Resources.Load<Sprite>("Bag/skillyes");
            ssm.handletype = mouse_type;
            //�����ܴ���ȫ�ֱ�����
            ssm.previousbuttonskill = pokemonskill;
            ssm.previousindex = 0;
           //ֻ�轫���ܴ���previous����
           //previousindex = 0 ��ʾ��һ�ε��Ϊδѧϰ����
        }
        else if (MouseType != 0) 
        {

            //�滻���ܣ���Ҫ֪���ϴβ�������ѧϰ���ܻ���δѧϰ����
            //��ѧϰ�ı��滻��δѧϰ�ĸ���ѡ�м���
            if (ssm.previousindex == 0)
            {
                ssm.previousbuttonskill = pokemonskill;
            }

            if (ssm.previousindex != 0) {
                await ws.sendMsgAsync("learn_skill\n" + pokemonID + " " + ssm.previousindex + " " + pokemonskill.id);
                String answer = await ws.receiveMsgAsync();
                //��������
                ssm.handletype = 0;
                //ˢ�¼���
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
