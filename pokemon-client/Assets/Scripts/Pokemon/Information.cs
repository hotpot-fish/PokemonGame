using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//挂在pokemon上，给pokemon对应编号
public class Information : MonoBehaviour
{
    public int pokemonId;
    public int pokemonLevel;
    // Start is called before the first frame update
    void Start()
    {
        switch (gameObject.name)
        {
            case "Bee(Clone)"://1-5
                pokemonId = 1;
                pokemonLevel = Random.Range(1, 5);
                break;
            case "Chick(Clone)"://1-5
                pokemonId = 8;
                pokemonLevel = Random.Range(1, 5);
                break;
            case "Seed(Clone)"://1-5
                pokemonId = 9;
                pokemonLevel = Random.Range(1, 5);
                break;
            case "Spider King(Clone)"://80-100
                pokemonId = 2;
                pokemonLevel = Random.Range(80, 100);
                break;
            case "Bat Lord(Clone)"://40-50
                pokemonId = 3;
                pokemonLevel = Random.Range(40, 50);
                break;
            case "Spook(Clone)"://6-10
                pokemonId = 4;
                pokemonLevel = Random.Range(6, 10);
                break;
            case "Toadstool(Clone)"://30-40
                pokemonId = 5;
                pokemonLevel = Random.Range(30, 40);
                break;
            case "Shadow(Clone)"://60-70
                pokemonId = 6;
                pokemonLevel = Random.Range(60, 70);
                break;
            case "Bird(Clone)"://11-30
                pokemonId = 7;
                pokemonLevel = Random.Range(11, 30);
                break;
            default:
                pokemonId = 1;
                pokemonLevel = Random.Range(1, 5);
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
