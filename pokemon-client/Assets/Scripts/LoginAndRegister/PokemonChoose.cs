using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//挂在Buttons上，选择初始宝可梦界面相关方法
public class PokemonChoose : MonoBehaviour
{
    public GameObject message;
    public GameObject messageBox;
    public string pokemon;
    public GameObject bee;
    public GameObject seed;
    public GameObject chick;
    public GameObject beeButton;
    public GameObject seedButton;
    public GameObject chickButton;
    public void cancel()
    {
        messageBox.SetActive(false);
    }
    //确认选择
    async public void confirm()
    {
        GameObject web = GameObject.Find("websocket");
        websocket ws = web.GetComponent<websocket>();
        await ws.sendMsgAsync("choose_init_pokemon\n" + pokemon);
        String answer = await ws.receiveMsgAsync();
        SceneManager.LoadScene("Demo_1");
    }
    public void chooseBee()
    {
        pokemon = "1";
        message.GetComponent<Text>().text = "是否选择小蜜蜂";
        messageBox.SetActive(true);
    }
    public void chooseChick()
    {
        pokemon = "8";
        message.GetComponent<Text>().text = "是否选择小鸡";
        messageBox.SetActive(true);
    }
    public void chooseSeed()
    {
        pokemon = "9";
        message.GetComponent<Text>().text = "是否选择小豆芽";
        messageBox.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        message = GameObject.Find("Message");
        messageBox = GameObject.Find("MessageBox");
        messageBox.SetActive(false);
        bee = GameObject.Find("Bee");
        seed = GameObject.Find("Seed");
        chick = GameObject.Find("Chick");
        beeButton = GameObject.Find("BeeButton");
        seedButton = GameObject.Find("SeedButton");
        chickButton = GameObject.Find("ChickButton");
        //
        beeButton.GetComponent<RectTransform>().position = getPosition(bee);
        //
        seedButton.GetComponent<RectTransform>().position = getPosition(seed);
        //
        chickButton.GetComponent<RectTransform>().position = getPosition(chick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Vector2 getPosition(GameObject target)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
        Vector2 uiPos = Vector2.zero;
        uiPos.x = screenPos.x; //* canvasRtm.sizeDelta.x / Screen.width;
        uiPos.y = screenPos.y + 55; //* canvasRtm.sizeDelta.y / Screen.height;
        return uiPos;
    }
}
