//�� BagLoad�ն����ϣ���ʼ����������
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Battlemsg;
using System.Threading.Tasks;
using UnityEngine.UI;

public class BagLoad : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject pokemonbutton;
    GameObject web;
    websocket ws;
    PlayerPokemon[] pokemonInBag;
    PlayerPokemon[] pokemonInWarehouse;
    public GameObject[] skillbuttons;
    public GameObject skillbar;
    public GameObject changebutton;
    public GameObject pokemoninfor;
    public GameObject datatext;
    public Sprite pokemonsprite;
    public Sprite UIMask;

    void Awake()
    {
        datatext = GameObject.Find("DataText");
        changebutton = GameObject.Find("ChangeButton");
        skillbar = GameObject.Find("SkillChangeBar");
        skillbuttons = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            skillbuttons[i] = GameObject.Find("SkillButton" + (i + 1).ToString());
        }
        pokemoninfor = GameObject.Find("PokemonInfor");
    }
    async void Start()
    {
        pokemoninfor.GetComponent<PokemonInfor>().SetFalse();//���ܽ�������
        await PokemonLoad();
    }
    public void SetButtonfalse()
    {
        for (int i = 0; i < 4; i++)
        {
            skillbuttons[i].GetComponent<Button>().enabled = false;
        }
        skillbar.SetActive(false);
    }
    public void SetButtontrue()
    {
        for (int i = 0; i < 4; i++)
        {
            skillbuttons[i].GetComponent<Button>().enabled = true;
        }
        skillbar.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {


    }
    async public Task PokemonLoad() //���ر����Ͳֿ��еľ���
    {
        web = GameObject.Find("websocket");
        ws = web.GetComponent<websocket>();
        await ws.sendMsgAsync("list_bag");
        String answer = await ws.receiveMsgAsync();
        String[] message = answer.Split('\n');
        pokemonInBag = JsonMapper.ToObject<Battlemsg.PlayerPokemon[]>(message[1]);
        await ws.sendMsgAsync("list_my_pokemon_not_in_bag");
        answer = await ws.receiveMsgAsync();
        message = answer.Split('\n');
        pokemonInWarehouse = JsonMapper.ToObject<Battlemsg.PlayerPokemon[]>(message[1]);

        pokemonbutton = (GameObject)Resources.Load("Bag/PokemonButton");

        for (int i = 0; i < 6; i++)//����6����������
        {
            GameObject bagbutton = GameObject.Find("PokemonInBag" + (i + 1).ToString());
            bagbutton.GetComponent<Image>().sprite = UIMask;
            if (pokemonInBag[i] != null)
            {
                PlayerPokemon playerPokemon = pokemonInBag[i];
                bagbutton.GetComponent<PokemonInBag>().playerpokemon = playerPokemon;
                bagbutton.GetComponent<PokemonInBag>().mouse_type = playerPokemon.pokemon.image;
                bagbutton.GetComponent<PokemonInBag>().pokemonsprite = Resources.Load<Sprite>("Bag/PokemonSprite/" + playerPokemon.pokemon.image.ToString());
                pokemonsprite = Resources.Load<Sprite>("Bag/PokemonSprite/" + playerPokemon.pokemon.image.ToString());
                bagbutton.GetComponent<Image>().sprite = pokemonsprite;
                bagbutton.transform.GetChild(0).GetComponent<Text>().text = "Lv��" + playerPokemon.level;
            }
            else {
                PlayerPokemon playerPokemon = pokemonInBag[i];
                bagbutton.GetComponent<PokemonInBag>().playerpokemon = null;
                bagbutton.transform.GetChild(0).GetComponent<Text>().text =null;
            }
        }
        int j = 0;
        GameObject b;
        while ((b = GameObject.Find("Pokemon" + j.ToString())) != null) {//��Ϊ�ֿ��Ƕ�̬�ģ�ÿ�μ���ǰҪ���֮ǰ��
            Destroy(b);
            j++;
        }
        for (int i = 0; i < pokemonInWarehouse.Length; i++)//���زֿ⾫��
        {
            if (pokemonInWarehouse[i] != null)
            {
                GameObject a = Instantiate(pokemonbutton);
                a.transform.SetParent(GameObject.Find("Content").transform); 
                a.name = "Pokemon" + i.ToString();
                PlayerPokemon playerPokemon = pokemonInWarehouse[i];
                a.GetComponent<PokemonWarehouse>().mouse_type = playerPokemon.pokemon.image;
                a.GetComponent<PokemonWarehouse>().playerpokemon = playerPokemon;
                pokemonsprite = Resources.Load<Sprite>("Bag/PokemonSprite/" + playerPokemon.pokemon.image.ToString());
                a.GetComponent<Image>().sprite = pokemonsprite;
                a.transform.GetChild(0).GetComponent<Text>().text = "Lv��" + playerPokemon.level + "  " + playerPokemon.pokemon.name;
            }
        }
    }
}
