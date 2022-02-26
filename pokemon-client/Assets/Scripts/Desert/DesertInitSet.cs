using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//����ɳĮEnvironment�ϣ�����ɳĮ��ʼ��Ϣ
public class DesertInitSet : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    public AudioSource au;
    public AudioClip ac;
    public Method method;

    public GameObject sendButton;
    public GameObject inputId;
    public bool isOpen;
    public GameObject Confirm;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Player.GetComponent<MoveController>().moveMent = 0f;
        isOpen = false;

        method = GameObject.Find("Method").GetComponent<Method>();
        if (method.taTaKai)
        {//��Player��λ������Ϊ����ս��ǰ��λ��
            method.InitPosition();
            method.taTaKai = false;
        }
        else
        {
            Vector3 position = new Vector3(182f, 5f, 205f);
            if (Player.GetComponent<SingleInstanceGhost>().path == "ToDesert")
            {
                Player.transform.position = position;
            }
            else if (Player.GetComponent<SingleInstanceGhost>().path == "Bag")
            {
                method.InitPosition();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void findClick()
    {
        isOpen = !isOpen;
        sendButton.SetActive(isOpen);
        inputId.SetActive(isOpen);
    }
}
