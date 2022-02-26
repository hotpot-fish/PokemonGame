using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
using System.Text.RegularExpressions;
using Battlemsg;
//����Main Camera�ϣ�ע�������ط���
public class Register : MonoBehaviour
{
    //ע����Ϣ
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
        //�п�+++
        if (inputNickName.text.Trim().ToString() == "")
        {
            //��ʾ�û���Ϊ��
            messageText.GetComponent<Text>().text = "�û���Ϊ��";
            messageBox.SetActive(true);
            return;
        }
        if (inputAccount.text.Trim().ToString() == "")
        {
            //��ʾ�˺�Ϊ��
            messageText.GetComponent<Text>().text = "�˺�Ϊ��";
            messageBox.SetActive(true);
            return;
        }
        if (inputPaswd.text.Trim().ToString() == "")
        {
            //��ʾ����Ϊ��
            messageText.GetComponent<Text>().text = "����Ϊ��";
            messageBox.SetActive(true);
            return;
        }
        if (inputEmail.text.Trim().ToString() == "")
        {
            //��ʾ����Ϊ��
            messageText.GetComponent<Text>().text = "����Ϊ��";
            messageBox.SetActive(true);
            return;
        }
        //�ж��ظ��Ƿ�һ��
        if (inputPaswd.text.Trim().ToString() != inputRepeatPaswd.text.Trim().ToString())
        {
            messageText.GetComponent<Text>().text = "������������벻һ��";
            messageBox.SetActive(true);
            return;
        }
        //�ж������ʽ
        Regex r = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");
        if (!r.IsMatch(inputEmail.text.Trim().ToString()))
        {
            messageText.GetComponent<Text>().text = "�����ʽ����ȷ";
            messageBox.SetActive(true);
            return;
        }
        //����Ҫ���������
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
            //��ʾ�˺��Ѵ���
            ws.remake();
            messageText.GetComponent<Text>().text = "�˺��Ѵ���";
            messageBox.SetActive(true);
            return;
        }
        Battlemsg.Player player = JsonMapper.ToObject<Battlemsg.Player>(message[1]);
        websocket.id = player.id;
        messageText.GetComponent<Text>().text = "ע��ɹ�";
        messageBox.SetActive(true);
        confirm.SetActive(false);
        succeed.SetActive(true);
        //����ѡ���������
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
