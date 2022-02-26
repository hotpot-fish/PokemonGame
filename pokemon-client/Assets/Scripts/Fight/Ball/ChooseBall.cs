using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBall : MonoBehaviour
{
    GameObject battle;
    string path;
    // Start is called before the first frame update
    void Start()
    {
        battle = GameObject.Find("Battle");
        switch (gameObject.name)
        {
            case "Ball1":
                path = "battle\n4 1";
                break;
            case "Ball2":
                path = "battle\n4 2";
                break;
            case "Ball3":
                path = "battle\n4 3";
                break;
            case "Ball4":
                path = "battle\n4 4";
                break;
            default:
                Debug.Log("¾«ÁéÇò×Ö·û´®Îª¿Õ");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void chooseBall()
    {
        battle.GetComponent<BattleController>().Receivechoose(path);
    }
}
