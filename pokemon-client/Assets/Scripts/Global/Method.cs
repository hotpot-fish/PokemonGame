using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//挂在Method上，一些常用的方法
public class Method : MonoBehaviour
{
    public GameObject player;
    public GameObject fm;
    public GameObject vm;
    public Vector3 position;
    public string mapPath;
    public bool taTaKai=false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        vm = GameObject.Find("CM vcam1");
        fm = GameObject.Find("CM FreeLook1");
        mapPath = "Demo_1";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPlayer(bool a)
    {
        player.SetActive(a);
    }
    public void SetFmAndVm(bool a)
    {
        fm.SetActive(a);
        vm.SetActive(a);
    }
    public void RecordPosition()
    {//记录玩家进入战斗前的位置
        position = player.transform.position;
    }
    public void InitPosition()
    {//恢复玩家进入战斗前的位置
        player.transform.position = position;
    }
    public string GetMapPath()
    {
        return mapPath;
    }
    public void SetMapPath(string path)
    {
        mapPath = path;
    }
}
