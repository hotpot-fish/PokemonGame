using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlemsg;
using LitJson;
using UnityEngine.SceneManagement;
//挂在战斗按钮上，触发战斗
public class AttackButton : MonoBehaviour
{
    public Method method;
    public GameObject Current;
    public int pokemonId;
    public int pokemonLevel;
    public GameObject messageBox;

    public void attackClick()
    {
        messageBox.SetActive(true);
    }
    async public void confirm()
    {
        //向后端发送AI信息
        method.taTaKai = true;
        Current = GameObject.FindWithTag("Current");
        GameObject web = GameObject.Find("websocket");
        websocket ws = web.GetComponent<websocket>();
        if (Current != null)
        {
            pokemonId = Current.GetComponent<Information>().pokemonId;
            pokemonLevel = Current.GetComponent<Information>().pokemonLevel;
            int[][] enemy = new int[6][];
            enemy[0] = new int[2];//第一个是宝可梦id，第二个是等级
            enemy[0][0] = pokemonId;
            enemy[0][1] = pokemonLevel;
            string msg = "start_PVE\n" + LitJson.JsonMapper.ToJson(enemy);
            await ws.sendMsgAsync(msg);
            string answer = await ws.receiveMsgAsync();
            String[] message = answer.Split('\n');
            if (message[0] != "start_battle")
            {
                Debug.Log(answer);
                return;
            }
            ws.setBattleMsg(message[1]);
            websocket.fightInfo = "PVE";
            method.GetComponent<Method>().RecordPosition();
            method.GetComponent<Method>().SetPlayer(false);
            method.GetComponent<Method>().SetFmAndVm(false);
            SceneManager.LoadScene("FightScene");
        }
        /*
        if (Current != null)
        {
            Current.GetComponent<Animator>().SetTrigger("Die");
            Invoke("DestoryOne", 3);
            GameObject Enemy = GameObject.Find("Enemy");
            if (Enemy.GetComponent<BornBotGrass>() != null)
            {
                Enemy.GetComponent<BornBotGrass>().enemyCounter--;
            }
            if (Enemy.GetComponent<BornBotDesert>() != null)
            {
                Enemy.GetComponent<BornBotDesert>().enemyCounter--;
            }
        }
        */
    }
    public void cancel()
    {
        messageBox.SetActive(false);
    }
    public void DestoryOne()
    {
        DestroyImmediate(Current);
    }
    // Start is called before the first frame update
    void Start()
    {
        method = GameObject.Find("Method").GetComponent<Method>();
        messageBox = GameObject.Find("MessageBox");
        messageBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
