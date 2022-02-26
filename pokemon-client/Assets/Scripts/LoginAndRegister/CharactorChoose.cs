using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//挂在Buttons上，选择人物界面相关方法

public class CharactorChoose : MonoBehaviour
{
    public GameObject message;
    public GameObject messageBox;
    public string charactor;
    public GameObject robot1;
    public GameObject boy1;
    public GameObject boy2;
    public GameObject boy3;
    public GameObject ninja1;
    public GameObject ninja2;
    public GameObject ninja3;
    public GameObject ninja4;
    public GameObject robot1Button;
    public GameObject boy1Button;
    public GameObject boy2Button;
    public GameObject boy3Button;
    public GameObject ninja1Button;
    public GameObject ninja2Button;
    public GameObject ninja3Button;
    public GameObject ninja4Button;
    public void cancel()
    {
        messageBox.SetActive(false);
    }
    //确认选择
    async public void confirm()
    {
        GameObject web = GameObject.Find("websocket");
        websocket ws = web.GetComponent<websocket>();
        await ws.sendMsgAsync("choose_image\n" + charactor);
        websocket.image = charactor;
        String answer = await ws.receiveMsgAsync();
        SceneManager.LoadScene("PokemonChoose");
    }
    public void chooseRobot1()
    {
        charactor = "Robot1";
        message.GetComponent<Text>().text = "是否选择机器人1号";
        messageBox.SetActive(true);
    }
    public void chooseBoy1()
    {
        charactor = "Boy1";
        message.GetComponent<Text>().text = "是否选择小男孩1号";
        messageBox.SetActive(true);
    }
    public void chooseBoy2()
    {
        charactor = "Boy2";
        message.GetComponent<Text>().text = "是否选择小男孩2号";
        messageBox.SetActive(true);
    }
    public void chooseBoy3()
    {
        charactor = "Boy3";
        message.GetComponent<Text>().text = "是否选择小男孩3号";
        messageBox.SetActive(true);
    }
    public void chooseNinja1()
    {
        charactor = "Ninja1";
        message.GetComponent<Text>().text = "是否选择忍者1号";
        messageBox.SetActive(true);
    }
    public void chooseNinja2()
    {
        charactor = "Ninja2";
        message.GetComponent<Text>().text = "是否选择忍者2号";
        messageBox.SetActive(true);
    }
    public void chooseNinja3()
    {
        charactor = "Ninja3";
        message.GetComponent<Text>().text = "是否选择忍者3号";
        messageBox.SetActive(true);
    }
    public void chooseNinja4()
    {
        charactor = "Ninja4";
        message.GetComponent<Text>().text = "是否选择忍者4号";
        messageBox.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        message = GameObject.Find("Message");
        messageBox = GameObject.Find("MessageBox");
        messageBox.SetActive(false);
        robot1=GameObject.Find("Robot1");
        boy1=GameObject.Find("Boy1");
        boy2=GameObject.Find("Boy2");
        boy3=GameObject.Find("Boy3");
        ninja1=GameObject.Find("Ninja1");
        ninja2=GameObject.Find("Ninja2");
        ninja3=GameObject.Find("Ninja3");
        ninja4=GameObject.Find("Ninja4");
        robot1Button=GameObject.Find("Robot1Button");
        boy1Button=GameObject.Find("Boy1Button");
        boy2Button=GameObject.Find("Boy2Button");
        boy3Button=GameObject.Find("Boy3Button");
        ninja1Button=GameObject.Find("Ninja1Button");
        ninja2Button=GameObject.Find("Ninja2Button");
        ninja3Button=GameObject.Find("Ninja3Button");
        ninja4Button=GameObject.Find("Ninja4Button");
        robot1Button.GetComponent<RectTransform>().position = getPosition(robot1);
        boy1Button.GetComponent<RectTransform>().position = getPosition(boy1);
        boy2Button.GetComponent<RectTransform>().position = getPosition(boy2);
        boy3Button.GetComponent<RectTransform>().position = getPosition(boy3);
        ninja1Button.GetComponent<RectTransform>().position = getPosition(ninja1);
        ninja2Button.GetComponent<RectTransform>().position = getPosition(ninja2);
        ninja3Button.GetComponent<RectTransform>().position = getPosition(ninja3);
        ninja4Button.GetComponent<RectTransform>().position = getPosition(ninja4);
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
        uiPos.y = screenPos.y + 41; //* canvasRtm.sizeDelta.y / Screen.height;
        return uiPos;
    }
}
