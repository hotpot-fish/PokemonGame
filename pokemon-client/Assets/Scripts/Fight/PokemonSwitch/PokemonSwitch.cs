using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonSwitch : MonoBehaviour
{
    bool flag = true;
    GameObject controller;
    string path;
    // Start is called before the first frame update
    public void Clicked()
    {
        controller.GetComponent<Pokemonupdate>().choosePokemon(path);
    }
    void Setflag(bool a)
    {
        flag = a;
    }
    void Start()
    {
        controller = GameObject.Find("pokemonImage");
        switch (gameObject.name)
        {
            case "B1":
                path = "battle\n" + "2 0";
                break;
            case "B2":
                path = "battle\n" + "2 1";
                break;
            case "B3":
                path = "battle\n" + "2 2";
                break;
            case "B4":
                path = "battle\n" + "2 3";
                break;
            case "B5":
                path = "battle\n" + "2 4";
                break;
            case "B6":
                path = "battle\n" + "2 5";
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
