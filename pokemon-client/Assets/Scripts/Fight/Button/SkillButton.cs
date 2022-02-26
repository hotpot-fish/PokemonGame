using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    bool flag=true;
    GameObject controller;
    public string path;
    // Start is called before the first frame update
    public void Clicked() {
        controller.GetComponent<BattleController>().Receivechoose(path);
    }
    void Setflag(bool a) {
        flag = a;
    }
    void Start()
    {
        controller = GameObject.Find("Battle");
        switch (gameObject.name)
        {
            case "skillButton1":
                path = "battle\n" + "5 0";
                break;
            case "skillButton2":
                path = "battle\n" + "5 1";
                break;
            case "skillButton3":
                path = "battle\n" + "5 2";
                break;
            case "skillButton4":
                path = "battle\n" + "5 3";
                break;
            default:
                path = "battle\n" + "5 0";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
