using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlemsg;
//挂在MainCamera上，界面初始化
public class LoadPokemon : MonoBehaviour
{
    // Start is called before the first frame update
    private bool hasbar1 = false;
    private bool hasbar2 = false;
    private int i = 0;
    private int j = 0;
    GameObject skill1;
    public GameObject battle;
    public GameObject pokemon1;
    public GameObject pokemon2;
    public void loadMypokemon(PokemonInBattle my) {
        int image = my.pokemon.pokemon.image;
        switch (image)
        {
            case 1:
                pokemon1 = (GameObject)Resources.Load("FightPrefabs/Bee");
                break;
            case 2:
                pokemon1 = (GameObject)Resources.Load("FightPrefabs/Spider King");
                break;
            case 3:
                pokemon1 = (GameObject)Resources.Load("FightPrefabs/Bat Lord");
                break;
            case 4:
                pokemon1 = (GameObject)Resources.Load("FightPrefabs/Spook");
                break;
            case 5:
                pokemon1 = (GameObject)Resources.Load("FightPrefabs/Toadstool");
                break;
            case 6:
                pokemon1 = (GameObject)Resources.Load("FightPrefabs/Shadow");
                break;
            case 7:
                pokemon1 = (GameObject)Resources.Load("FightPrefabs/Bird");
                break;
            case 8:
                pokemon1 = (GameObject)Resources.Load("FightPrefabs/Chick");
                break;
            case 9:
                pokemon1 = (GameObject)Resources.Load("FightPrefabs/Seed");
                break;
            default:
                pokemon1 = (GameObject)Resources.Load("FightPrefabs/Bee");
                break;
        }
        pokemon1 = Instantiate(pokemon1, new Vector3(-2.4f, -0.7f, -895), Quaternion.Euler(0f, 380f, 0f));
        pokemon1.tag = "user1_pokemon";
        battle = GameObject.Find("Battle");
        battle.GetComponent<BattleController>().mypokemon = pokemon1;
        GameObject life1 = (GameObject)Resources.Load("UserBar/User1_Bar");
        if (hasbar1 == false) {
            life1 = Instantiate(life1);
            hasbar1 = true;
        }
        if (i == 0) {
            GameObject.Find("Battle").GetComponent<BattleController>().hp1 = life1;
            skill1 = GameObject.Find("skillImage");
            i++;
        }
        skill1.GetComponent<Skillupdate>().Skillchange(my);
    }
    public void loadEnemypokemon(PokemonInBattle enemy)
    {
        int image = enemy.pokemon.pokemon.image;//种类 小蜜蜂1 蜘蛛王 2 
        switch (image)
        {
            case 1:
                pokemon2 = (GameObject)Resources.Load("FightPrefabs/Bee");
                break;
            case 2:
                pokemon2 = (GameObject)Resources.Load("FightPrefabs/Spider King");
                break;
            case 3:
                pokemon2 = (GameObject)Resources.Load("FightPrefabs/Bat Lord");
                break;
            case 4:
                pokemon2 = (GameObject)Resources.Load("FightPrefabs/Spook");
                break;
            case 5:
                pokemon2 = (GameObject)Resources.Load("FightPrefabs/Toadstool");
                break;
            case 6:
                pokemon2 = (GameObject)Resources.Load("FightPrefabs/Shadow");
                break;
            case 7:
                pokemon2 = (GameObject)Resources.Load("FightPrefabs/Bird");
                break;
            case 8:
                pokemon2 = (GameObject)Resources.Load("FightPrefabs/Chick");
                break;
            case 9:
                pokemon2 = (GameObject)Resources.Load("FightPrefabs/Seed");
                break;
            default:
                pokemon2 = (GameObject)Resources.Load("FightPrefabs/Bee");
                break;
        }
        pokemon2 = Instantiate(pokemon2, new Vector3(3.3f, 0.4f, -892.5f), Quaternion.Euler(0f, 230f, 0f));
        pokemon2.tag = "user2_pokemon";
        battle = GameObject.Find("Battle");
        battle.GetComponent<BattleController>().enemypokemon = pokemon2;
        GameObject life2 = (GameObject)Resources.Load("UserBar/User2_Bar");
        if (hasbar2 == false) {
            life2 = Instantiate(life2);
            hasbar2 = true;
        }
        if (j == 0) {
            GameObject.Find("Battle").GetComponent<BattleController>().hp2 = life2;
            j++;
        }

    }
    void Start()
    {
        /*Dictionary<int, int> openWith = new Dictionary<int, int>();
        openWith.Add(1, 1);
        openWith.Add(2, 1);
        GameObject pokemon1 = (GameObject)Resources.Load("Bee1");
        pokemon1 = Instantiate(pokemon1, new Vector3(-2.4f, -0.7f, -895), Quaternion.Euler(0f, 380f, 0f));
        pokemon1.tag = "user1_pokemon";
        GameObject life1 = (GameObject)Resources.Load("User1_Bar");
        life1 = Instantiate(life1);
        life1.tag = "user1_life";
        GameObject pokemon2 = (GameObject)Resources.Load("Spider King");
        pokemon2 = Instantiate(pokemon2, new Vector3(3.3f, 0.4f, -892.5f), Quaternion.Euler(0f, 230f, 0f));
        pokemon2.tag = "user2_pokemon";
        GameObject life2 = (GameObject)Resources.Load("User2_Bar");
        life2 = Instantiate(life2);
        life2.tag = "user2_life";

        GameObject b = GameObject.FindWithTag("user1_state");
         b.GetComponent<bufupdate>().Buffupdate(openWith);*/
        battle = GameObject.Find("Battle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
