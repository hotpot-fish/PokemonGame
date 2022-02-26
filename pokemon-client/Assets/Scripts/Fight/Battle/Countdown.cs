using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    GameObject battlecontroller; 
    public bool startcount = false;
    private float remaintime = 0;
    void Start()
    {
        battlecontroller = GameObject.Find("Battle");
        startcount = false;
        // 3. ÉèÖÃÊ£ÓàÊ±¼ä
        remaintime = 11;
    }

    void Update()
    {
        if (startcount)
        {
            remaintime = remaintime - Time.deltaTime;
            if (remaintime > 0)
            {
                int second = (int)remaintime % 3600 % 60;
                gameObject.GetComponent<Text>().text = second.ToString();
            }
            else
            {
                gameObject.GetComponent<Text>().text = "";
                remaintime = 11;
                startcount = false;
                battlecontroller.GetComponent<BattleController>().Receivechoose("battle\n" + "5 4");
            }
        }
        if (!startcount) {
            gameObject.GetComponent<Text>().text = "";
            remaintime = 11;
        }
    }
}
