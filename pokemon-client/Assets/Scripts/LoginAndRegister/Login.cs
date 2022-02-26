using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
using System;
using UnityEngine.SceneManagement;
using Battlemsg;
//挂在Main Camera上，登录相关方法
public class Login : MonoBehaviour 
{
	
	//登录信息
	public InputField inputAccount;
	public InputField inputPaswd;
    public GameObject messageBox;
    public GameObject messageText;

    async public void OnCLick()
	{
        GameObject web = GameObject.Find("websocket");
        websocket ws = web.GetComponent<websocket>();
        if (inputAccount.text.Trim().ToString() == "")
        {
            //提示账号为空
            messageText.GetComponent<Text>().text = "账号为空";
            messageBox.SetActive(true);
            return;
        }
        if(inputPaswd.text.Trim().ToString() == "")
        {
            //提示密码为空
            messageText.GetComponent<Text>().text = "密码为空";
            messageBox.SetActive(true);
            return;
        }
        JsonData json = new JsonData();
        json["account"] = inputAccount.text.Trim().ToString();
        json["password"] = inputPaswd.text.Trim().ToString();
        String loginMsg = "auth\n" + json.ToJson();
		await ws.connectAsync();
        await ws.receiveMsgAsync();
		await ws.sendMsgAsync(loginMsg);

		String answer = await ws.receiveMsgAsync();
		String[] message = answer.Split('\n');
        if (message[0] == "login_success")
        {
            Battlemsg.Player player = JsonMapper.ToObject<Battlemsg.Player>(message[1]);
            websocket.id = player.id;
            websocket.image = player.image;
            websocket.rank = player.Rank;
            if (message[2] == "choose_image")
            {//需要完成选择人物和初始宝可梦
                SceneManager.LoadScene("CharactorChoose");
            }
            else if (message[2] == "choose_pokemon")
            {//需要完成选择初始宝可梦
                SceneManager.LoadScene("PokemonChoose");
            }
            else if (message[2] == "finish_choose")
            {
                SceneManager.LoadScene("Demo_1");
            }
        }
        else
        {
            //添加提示++++
            ws.remake();
            messageText.GetComponent<Text>().text = "账号或密码错误";
            messageBox.SetActive(true);
            return;
        }
		//Debug.Log(answer);
	}

    public void registerClick()
    {
        SceneManager.LoadScene("Register");
    }
    public void confirmClick()
    {
        messageBox.SetActive(false);
    }
    private void Start()
    {
        messageBox = GameObject.Find("MessageBox");
        messageText = GameObject.Find("Message");
        messageBox.SetActive(false);
    }
    // Update is called once per frame
    void Update () 
	{

	}

	void regist()
	{
		//如果可以的或直接将数据写入数据库在这里我们仅仅模拟下功能就行了
		if(inputAccount.text!=""&&inputPaswd.text!="")
		{
			Debug.Log("注册成功");
		}
		else
		{
			Debug.Log ("请输入注册信息");
		}
	}
}
