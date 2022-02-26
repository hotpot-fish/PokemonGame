using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//≈≈∂”∞¥≈•

public class Gamestart : MonoBehaviour
{
    public GameObject CancelPairButton;
    public GameObject PairButton;
    public async void pairGame()
    {
        PairButton.SetActive(false);
        CancelPairButton.SetActive(true);
        Method method = GameObject.Find("Method").GetComponent<Method>();
        GameObject web = GameObject.Find("websocket");
        websocket ws = web.GetComponent<websocket>();
        await ws.sendMsgAsync("start_pairing");
        String answer = await ws.receiveMsgAsync();
        String[] message = answer.Split('\n');
        if (message[0] == "cancel_success")
        {
            PairButton.SetActive(true);
            CancelPairButton.SetActive(false);
        }
        else
        {
            websocket.fightInfo = "PVP";
            ws.setBattleMsg(message[1]);
            method.taTaKai = true;
            method.GetComponent<Method>().RecordPosition();
            method.GetComponent<Method>().SetPlayer(false);
            method.GetComponent<Method>().SetFmAndVm(false);
            SceneManager.LoadScene("FightScene");
        }
    }
    public async void cancelPairGame()
    {
        GameObject web = GameObject.Find("websocket");
        websocket ws = web.GetComponent<websocket>();
        await ws.sendMsgAsync("cancel_pairing");
    }
    // Start is called before the first frame update
    void Start()
    {
        CancelPairButton = GameObject.Find("CancelPair");
        CancelPairButton.SetActive(false);
        PairButton = GameObject.Find("Pair");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
