using System.Collections;
using UnityEngine;
using System;
using System.Text;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Collections.Generic;
//挂在websocket空对象上，websocket方法
public class websocket : MonoBehaviour
{
    //比赛性质
    public static String fightInfo;
    //玩家信息
    public static int rank=0;
    //
    public static System.Threading.CancellationToken token = new System.Threading.CancellationToken();
    public static ClientWebSocket client = new ClientWebSocket();
/*    public static System.Uri uri = new Uri("ws://121.37.182.57:38234/ws");*/
    public static System.Uri uri = new Uri("ws://121.37.182.57:38234/ws");
    //public static System.Uri uri = new Uri("ws://localhost:8080/ws");
    public static string battle_msg;
    public static int id { get; set; }
    public static String image { get; set; }
    private static websocket instance = null;
    
    // Start is called before the first frame update
    public void setBattleMsg(String msg) {
        battle_msg = msg;
    }
    public String getBattleMsg()
    {
        return battle_msg;
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (this != instance)
        {
            Destroy(gameObject);
            return;
        }
    }
    public async Task<String> receiveMsgAsync()
    {
        List<byte> allMsg = new List<byte>();
        byte[] Msg = new byte[10000];
        while (true)
        {
            ArraySegment<byte> msg= new ArraySegment<byte>(Msg);
            var result = await client.ReceiveAsync(msg, token);
            ArraySegment<byte> msg1 = new ArraySegment<byte>(Msg,0,result.Count);
            allMsg.AddRange(msg1);
            if (result.EndOfMessage)
            {
                break;
            }
        }
        return Encoding.UTF8.GetString(allMsg.ToArray());
    }

    public async Task connectAsync()
    {
        await client.ConnectAsync(uri, token);
    }

    public void remake()
    {
        client = new ClientWebSocket();
    }

    public async Task sendMsgAsync(String message)
    {
        byte[] Msg = Encoding.UTF8.GetBytes(message);
        await client.SendAsync(new ArraySegment<byte>(Msg),WebSocketMessageType.Text,true,token);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}