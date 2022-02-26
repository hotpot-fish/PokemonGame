//����pokemonwarehouse ������PokemonInfor �µ� Skillimage �µ� �ĸ�skillbutton�� �Ǿ�����ѧϰ�ļ���
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
    public int pokemonID;//��ǰ����ID
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

    //On_learn_Button��ʾ�����ť
    //���ܲ����ܰ������ε���������ȵ��δѧϰ�����ٵ����ѧϰ�������滻��  mousetype��ʾ���ε���еĵڼ��ε������ִ����Ӧ����
    async public void On_Learn_Button()
    {
        int MouseType = ssm.handletype;
        if (MouseType == 0)
        {
            //�ı䰴ťimage
            skillimage.sprite = Resources.Load<Sprite>("Bag/skillyes");
            /*skillimage.sprite = Resources.Load<Sprite>("PokemonSprite/" + playerpokemon.pokemon.image.ToString() + "c");*/

            ssm.handletype = mouse_type;
            ssm.previousbuttonskill = pokemonskill;
            ssm.previousindex = index;
            
            //�����ܴ���previous����
            //����ťindex�浽��̬�� previousindex��
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
            
            //��������
            await ws.receiveMsgAsync();
            ssm.handletype = 0;
            //ˢ�¼���
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
