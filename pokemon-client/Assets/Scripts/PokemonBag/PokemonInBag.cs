//搭载在PokemonInBag下的pokemon button上 表示背包内的精灵
using System.Collections;
using System.Collections.Generic;
using Game_Manager;
using UnityEngine;
using UnityEngine.UI;
using Battlemsg;

public class PokemonInBag : MonoBehaviour {
    private Game_Scene_Manager gsm;
    private Image pokemonimage;
    public int mouse_type;
    public int index;
    public PlayerPokemon playerpokemon;
    public Sprite pokemonsprite;
    public Sprite UIMask;
    GameObject web;
    GameObject currpokemon;
    websocket ws;

    void Awake() {
        web = GameObject.Find("websocket");
        ws = web.GetComponent<websocket>();
        gsm = Game_Scene_Manager.GetInstance();
        pokemonimage = GetComponent<Image>();
    }

    //on bag button表示点击按钮
    //精灵操作总包括两次点击，例如先点击背包精灵再点击仓库精灵来替换，  mousetype表示两次点击中的第几次点击，并执行相应操作
    async public void On_Bag_Button() {
        int MouseType = gsm.mousetype;
        if (pokemonimage.sprite == pokemonsprite && MouseType == 0) {
            //改变按钮image
            pokemonimage.sprite = Resources.Load<Sprite>("Bag/PokemonSprite/" + playerpokemon.pokemon.image.ToString() + "c");
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
            
            //将按钮index存到静态类 previousindex中
            gsm.mousetype = mouse_type;
            gsm.previousbuttonpokemon = playerpokemon;
            gsm.previousindex = index;
        } else if (MouseType != 0) {
            if(pokemonimage.sprite == UIMask){
                //此时放回仓库
                //如果当前按钮为空，直接将gsm中存的宝可梦放入此按钮
                await ws.sendMsgAsync("modify_bag_index\n" + gsm.previousbuttonpokemon.id + " " + "0");
                await ws.sendMsgAsync("modify_bag_index\n" + gsm.previousbuttonpokemon.id + " " + index);
                await ws.receiveMsgAsync();
                await ws.receiveMsgAsync();
            }
            if (pokemonimage.sprite != UIMask) {
                //此时放回仓库 ，并将按钮index存到静态类 previousindex中
                //将当前按钮宝可梦放入previousindex对应按钮中
                //将previous宝可梦放入当前按钮
                await ws.sendMsgAsync("modify_bag_index\n" + gsm.previousbuttonpokemon.id + " " + "0");
                await ws.sendMsgAsync("modify_bag_index\n" + playerpokemon.id + " " + gsm.previousindex);
                await ws.sendMsgAsync("modify_bag_index\n" + gsm.previousbuttonpokemon.id + " " + index);
                await ws.receiveMsgAsync();
                await ws.receiveMsgAsync();
                await ws.receiveMsgAsync();
            }
            gsm.mousetype = 0;//操作结束
            //刷新背包
            await GameObject.Find("BagLoad").GetComponent<BagLoad>().PokemonLoad();
        }
    }

    // Use this for initialization
	
	// Update is called once per frame
	void Update () {
        
    }
}
