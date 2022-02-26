//������PokemonWarehome �ϵ� Viewport/content/ remove��ť�ϣ�������ť�������������еľ���Ż����ֿ⣬һ��Ϊ ��һ�ε�������еľ��鰴ť���ڶ��ε���˰�ť
using System.Collections;
using System.Collections.Generic;
using Game_Manager;
using UnityEngine;
using UnityEngine.UI;
using Battlemsg;
public class RemoveButton : MonoBehaviour
{
    GameObject web;
    websocket ws;

    private Game_Scene_Manager gsm;

    void Awake()
    {
        web = GameObject.Find("websocket");
        ws = web.GetComponent<websocket>();
        gsm = Game_Scene_Manager.GetInstance();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async public void On_Remove_Button()//����˰�ť��ǰ��ֻ�����ȵ�������о�����ٵ���˰�ť
    {
        int MouseType = gsm.mousetype;//����MouseType�ز�Ϊ0
        if (MouseType != 0)
        {
            await ws.sendMsgAsync("modify_bag_index\n" + gsm.previousbuttonpokemon.id + " " + "0");
            await ws.receiveMsgAsync();
            gsm.mousetype = 0;//��������
            //ˢ�±���
            await GameObject.Find("BagLoad").GetComponent<BagLoad>().PokemonLoad();
        }
    }
}
