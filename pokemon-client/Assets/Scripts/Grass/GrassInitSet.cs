using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//挂在草原Environment上，设置草原初始信息
public class GrassInitSet : MonoBehaviour
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
    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        if (Player == null)
        {
            Vector3 initialPosition = new Vector3(168.9409f, 30f, 291.1315f);
            GameObject web = GameObject.Find("websocket");
            websocket ws = web.GetComponent<websocket>();
            //加载人物模型
            Instantiate(Resources.Load("PlayerPrefabs/"+websocket.image), initialPosition, Quaternion.identity);
            Player = GameObject.FindWithTag("Player");
        }
    }
    void Start()
    {
        Player.GetComponent<MoveController>().moveMent = 0f;
        GameObject fm = GameObject.Find("CM FreeLook1");
        fm.GetComponent<CinemachineFreeLook>().LookAt = Player.transform;
        fm.GetComponent<CinemachineFreeLook>().Follow = Player.transform;
        isOpen = false;
        method = GameObject.Find("Method").GetComponent<Method>();
        if (method.taTaKai)
        {//将Player的位置设置为进入战斗前的位置
            method.InitPosition();
            method.taTaKai = false;
        }
        else
        {
            if (Player.GetComponent<SingleInstanceGhost>().path == "ToGrass")
            {
                Player.transform.position = new Vector3(203f, 19f, 303f);
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
