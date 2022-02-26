using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Battlemsg;
using System;
//挂在pokemonImage上，精灵背包按钮初始化
//玩家操作后需要设置不可点击
public class Pokemonupdate : MonoBehaviour
{
    public GameObject[] pokebuttons = new GameObject[6];
    public Sprite pokemonsprite;
    private bool noDie = true;
    private int i = 0;
    GameObject controller;
    public void Pokemonchange(PokemonInBattle[] pokemons)
    {
        if (i == 0) {
            pokebuttons[0] = GameObject.Find("B1");
            pokebuttons[1] = GameObject.Find("B2");
            pokebuttons[2] = GameObject.Find("B3");
            pokebuttons[3] = GameObject.Find("B4");
            pokebuttons[4] = GameObject.Find("B5");
            pokebuttons[5] = GameObject.Find("B6");
            i = 1;
        }
        //需要根据宝可梦信息设置显示
        foreach (GameObject pokebutton in pokebuttons)
        {
            pokebutton.SetActive(false);
        }
        int index = 0;
        //存在则显示
        foreach (PokemonInBattle pokemon in pokemons)
        {
            if (pokemon != null)
            {
                index++;
                string name = pokemon.pokemon.pokemon.name;
                int id = pokemon.pokemon.pokemon.id;
                int maxHP = pokemon.initAttribution.HP;
                int curHP = pokemon.curAttribution.HP;
                int image = pokemon.pokemon.pokemon.image;
                int level = pokemon.pokemon.level;
                pokemonsprite = Resources.Load<Sprite>("Bag/PokemonSprite/" + image.ToString());
                pokebuttons[index - 1].GetComponent<Image>().sprite = pokemonsprite;
                pokebuttons[index - 1].SetActive(true);
                pokebuttons[index - 1].transform.GetChild(0).GetComponent<Text>().text = "LV\n" + level;
                pokebuttons[index - 1].transform.GetChild(1).GetComponent<Text>().text = curHP + "/" + maxHP + "\n" + name;
                pokebuttons[index - 1].GetComponent<Button>().interactable = false;
            }
        }

    }
   public void setNodie(bool f)
    {
        noDie = f;
    }
    public void choosePokemon(string message)
    {
        if (noDie)
            controller.GetComponent<BattleController>().Receivechoose(message);
        else controller.GetComponent<BattleController>().chooseNewPokemon(Int32.Parse(message.Split(' ')[1]));
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Battle");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setflag(bool a)
    {//设置按钮是否可用
        for (int i = 0; i < 4; i++)
        {
            pokebuttons[i].GetComponent<Button>().interactable = a;
        }
    }
    public void Show(PokemonInBattle[] pokemons,int currentPokemonIndex)
    {//根据信息设置可用性
        int index = 0;
        foreach (PokemonInBattle pokemon in pokemons)
        {
            if (pokemon != null)
            {
                index++;
                int curHP = pokemon.curAttribution.HP;
                if (curHP == 0)
                {
                    pokebuttons[index - 1].GetComponent<Button>().interactable = false;
                }
                else
                {
                    pokebuttons[index - 1].GetComponent<Button>().interactable = true;
                }
                if (currentPokemonIndex == index - 1)
                {
                    pokebuttons[index - 1].GetComponent<Button>().interactable = false;
                }
            }
        }
    }
}
