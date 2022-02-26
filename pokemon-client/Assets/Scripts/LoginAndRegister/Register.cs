using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
using System.Text.RegularExpressions;
using Battlemsg;
//挂在Main Camera上，注册界面相关方法
public class Register : MonoBehaviour
{
    //注册信息
    public InputField inputNickName;
    public InputField inputAccount;
    public InputField inputPaswd;
    public InputField inputRepeatPaswd;
    public InputField inputEmail;

    public GameObject messageBox;
    public GameObject messageText;
    public GameObject succeed;
    public GameObject confirm;
    async public void OnClick()
    {
        GameObject web = GameObject.Find("websocket");
        websocket ws = web.GetComponent<websocket>();
        //判空+++
        if (inputNickName.text.Trim().ToString() == "")
        {
            //提示用户名为空
            messageText.GetComponent<Text>().text = "用户名为空";
            messageBox.SetActive(true);
            return;
        }
        if (inputAccount.text.Trim().ToString() == "")
        {
            //提示账号为空
            messageText.GetComponent<Text>().text = "账号为空";
            messageBox.SetActive(true);
            return;
        }
        if (inputPaswd.text.Trim().ToString() == "")
        {
            //提示密码为空
            messageText.GetComponent<Text>().text = "密码为空";
            messageBox.SetActive(true);
            return;
        }
        if (inputEmail.text.Trim().ToString() == "")
        {
            //提示邮箱为空
            messageText.GetComponent<Text>().text = "邮箱为空";
            messageBox.SetActive(true);
            return;
        }
        //判断重复是否一致
        if (inputPaswd.text.Trim().ToString() != inputRepeatPaswd.text.Trim().ToString())
        {
            messageText.GetComponent<Text>().text = "两次输入的密码不一致";
            messageBox.SetActive(true);
            return;
        }
        //判断邮箱格式
        Regex r = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");
        if (!r.IsMatch(inputEmail.text.Trim().ToString()))
        {
            messageText.GetComponent<Text>().text = "邮箱格式不正确";
            messageBox.SetActive(true);
            return;
        }
        //符合要求后发送请求
        JsonData json = new JsonData();
        json["name"] = inputNickName.text.Trim().ToString();
        json["account"] = inputAccount.text.Trim().ToString();
        json["password"] = inputPaswd.text.Trim().ToString();
        json["email"] = inputEmail.text.Trim().ToString();
        String registerMsg = "register\n" + json.ToJson();
        await ws.connectAsync();
        await ws.receiveMsgAsync();
        await ws.sendMsgAsync(registerMsg);
        String answer = await ws.receiveMsgAsync();
        String[] message = answer.Split('\n');
        if(message[0] == "register_fail")
        {
            //提示账号已存在
            ws.remake();
            messageText.GetComponent<Text>().text = "账号已存在";
            messageBox.SetActive(true);
            return;
        }
        Battlemsg.Player player = JsonMapper.ToObject<Battlemsg.Player>(message[1]);
        websocket.id = player.id;
        messageText.GetComponent<Text>().text = "注册成功";
        messageBox.SetActive(true);
        confirm.SetActive(false);
        succeed.SetActive(true);
        //进入选择人物界面
    }
    public void switchScene()
    {
        SceneManager.LoadScene("CharactorChoose");
    }
    public void loginClick()
    {
        SceneManager.LoadScene("Login");
    }
    public void confirmClick()
    {
        messageBox.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        messageBox = GameObject.Find("MessageBox");
        messageText = GameObject.Find("Message");
        confirm = GameObject.Find("Confirm");
        succeed = GameObject.Find("Succeed");
        messageBox.SetActive(false);
        succeed.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
