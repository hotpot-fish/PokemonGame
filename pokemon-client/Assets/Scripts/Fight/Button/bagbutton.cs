using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����bagImage�ϣ���ʾ�����������
public class bagbutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (websocket.fightInfo == "PVP")
        {
            GameObject.Find("ballButton").SetActive(false);
        }
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Change()
    {
        if (gameObject.activeSelf == false) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }
}
