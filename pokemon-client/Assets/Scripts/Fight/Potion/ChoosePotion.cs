using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePotion : MonoBehaviour
{
    GameObject battle;
    string path;
    // Start is called before the first frame update
    void Start()
    {
        battle = GameObject.Find("Battle");
        switch (gameObject.name)
        {
            case "Hp1":
                path = "battle\n3 1";
                break;
            case "Hp2":
                path = "battle\n3 2";
                break;
            case "Hp3":
                path = "battle\n3 3";
                break;
            case "Pp1":
                path = "battle\n3 4";
                break;
            case "Pp2":
                path = "battle\n3 5";
                break;
            case "Pp3":
                path = "battle\n3 6";
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void choosePotion()
    {
        battle.GetComponent<BattleController>().Receivechoose(path);
    }
}
