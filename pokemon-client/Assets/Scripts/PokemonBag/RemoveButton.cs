//搭载在PokemonWarehome 上的 Viewport/content/ remove按钮上，待命按钮，用来将背包中的精灵放回至仓库，一般为 第一次点击背包中的精灵按钮，第二次点击此按钮
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

    async public void On_Remove_Button()//点击此按钮的前提只能是先点击背包中精灵后再点击此按钮
    {
        int MouseType = gsm.mousetype;//所以MouseType必不为0
        if (MouseType != 0)
        {
            await ws.sendMsgAsync("modify_bag_index\n" + gsm.previousbuttonpokemon.id + " " + "0");
            await ws.receiveMsgAsync();
            gsm.mousetype = 0;//操作结束
            //刷新背包
            await GameObject.Find("BagLoad").GetComponent<BagLoad>().PokemonLoad();
        }
    }
}
