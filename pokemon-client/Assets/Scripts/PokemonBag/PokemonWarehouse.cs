//搭载在PokemonWarehome下的pokemon button上 表示仓库内的精灵
using System.Collections;
using System.Collections.Generic;
using Game_Manager;
using UnityEngine;
using UnityEngine.UI;
using Battlemsg;

public class PokemonWarehouse : MonoBehaviour {
    private Game_Scene_Manager gsm;
    private Image pokemonimage;
    public int mouse_type;
    public Sprite pokemonsprite;
    public Sprite UIMask;
    public PlayerPokemon playerpokemon;
    GameObject web;
    GameObject currpokemon;
    websocket ws;

    void Awake() {
        web = GameObject.Find("websocket");
        ws = web.GetComponent<websocket>();
        gsm = Game_Scene_Manager.GetInstance();
        pokemonimage = GetComponent<Image>();
    }
    //On_Warehouse_Button表示点击按钮
    //精灵操作总包括两次点击，例如先点击背包精灵再点击仓库精灵来替换，  mousetype表示两次点击中的第几次点击，并执行相应操作
    async public void On_Warehouse_Button() {
        int MouseType = gsm.mousetype;
        if (pokemonimage.sprite != UIMask && MouseType == 0)
        {
            //改变按钮image
            pokemonimage.sprite = Resources.Load<Sprite>("Bag/PokemonSprite/" + playerpokemon.pokemon.image.ToString()+"c");
            
            //改变展示的宝可梦
            for (int i = 0; i < GameObject.Find("ActorCamera").transform.childCount; i++)
            {
                Destroy(GameObject.Find("ActorCamera").transform.GetChild(i).gameObject);
            }
            currpokemon = (GameObject)Resources.Load("Bag/BagPrefabs/" + playerpokemon.pokemon.image.ToString());
            GameObject a = Instantiate(currpokemon, new Vector3(-2, 0, 106.4f), Quaternion.Euler(0f, 200, 0f));
            a.transform.parent = GameObject.Find("ActorCamera").transform;
            //显示宝可梦属性
            GameObject.Find("BagLoad").GetComponent<BagLoad>().datatext.GetComponent<Text>().text = "Lv: " + playerpokemon.level + " " + playerpokemon.pokemon.name + "\n" + "攻击：" + playerpokemon.curAttack + " " + "防御： " +
                playerpokemon.curDefence + " " + "速度： " + playerpokemon.curSpeed + " " + "血量： " + playerpokemon.curHP;

            //刷新技能
            GameObject.Find("BagLoad").GetComponent<BagLoad>().changebutton.GetComponent<ChangeButton>().pokemonid = playerpokemon.id;
            GameObject.Find("BagLoad").GetComponent<BagLoad>().SetButtonfalse();
            GameObject.Find("BagLoad").GetComponent<BagLoad>().pokemoninfor.GetComponent<PokemonInfor>().SetTrue();
            await GameObject.Find("SkillLoad").GetComponent<SkillLoad>().PokemonSkillLoad(playerpokemon.id);
            
            gsm.mousetype = mouse_type;
            //将宝可梦存入全局变量中
            gsm.previousbuttonpokemon = playerpokemon;
            gsm.previousindex = 0;
            //仓库中精灵第一次点击时，不必再进行放入仓库操作
            //只需将精灵存入previous宝可梦
        }
        else if (MouseType != 0) //此判断触发条件为 先点击背包中精灵，再点击仓库中精灵
        {
            //将当前按钮宝可梦放入previousindex对应按钮中
            await ws.sendMsgAsync("modify_bag_index\n" + gsm.previousbuttonpokemon.id + " " + "0");
            //将previous宝可梦放入当前按钮
            await ws.sendMsgAsync("modify_bag_index\n" + playerpokemon.id + " " + gsm.previousindex);
            await ws.receiveMsgAsync();
            await ws.receiveMsgAsync();
            gsm.mousetype= 0;
            //刷新
            await GameObject.Find("BagLoad").GetComponent<BagLoad>().PokemonLoad();
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
