//   7/28脚本修改
//   45-47   146-149

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//挂在userState上，buff栏更新

public class Bufupdate : MonoBehaviour
{
    GameObject canvas;
    GameObject buf1;
    GameObject buf2;
    GameObject buf3;
    GameObject buf4;
    GameObject buf5;
    GameObject buf6;
    GameObject buf7;
    GameObject buf8;
    GameObject buf9;
    GameObject buf10;
    GameObject buf11;
    GameObject buf12;
    GameObject buf13;
    //Using int for buff type , 1 烧伤 2 冻伤 3 中毒 4 晕眩 5 疲惫 6 失明 7 强化攻击 8 强化防御 9 强化速度 10 弱化攻击 11 弱化防御 12 弱化速度 13 免控
    public void Buffupdate(Dictionary<int, int> buf)
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
            buf1 = (GameObject)Resources.Load("Buff/burn");
            buf2 = (GameObject)Resources.Load("Buff/frostbite");
            buf3 = (GameObject)Resources.Load("Buff/poisoning");
            buf4 = (GameObject)Resources.Load("Buff/vertigo");
            buf5 = (GameObject)Resources.Load("Buff/exhausted");
            buf6 = (GameObject)Resources.Load("Buff/blindness");
            buf7 = (GameObject)Resources.Load("Buff/powerup");
            buf8 = (GameObject)Resources.Load("Buff/defenseup");
            buf9 = (GameObject)Resources.Load("Buff/speedup");
            buf10 = (GameObject)Resources.Load("Buff/powerdown");
            buf11 = (GameObject)Resources.Load("Buff/defensedown");
            buf12 = (GameObject)Resources.Load("Buff/speeddown");
            buf13= (GameObject)Resources.Load("Buff/uncontrollable");
        int a = -445;
        int b = 162;
        

        foreach (KeyValuePair<int, int> kvp in buf)
        {
            Vector3 pos = transform.TransformPoint(a, b, 0);
            if (kvp.Key == 1)
            {
                buf1.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
                buf1 = Instantiate(buf1, pos, Quaternion.identity);
                buf1.transform.SetParent(gameObject.transform);

            }
            if (kvp.Key == 2)
            {
                buf2 = Instantiate(buf2, pos, Quaternion.identity);
                buf2.transform.SetParent(gameObject.transform);
                buf2.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 3)
            {
                buf3 = Instantiate(buf3, pos, Quaternion.identity);
                buf3.transform.SetParent(gameObject.transform);
                buf3.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 4)
            {
                buf4 = Instantiate(buf4, pos, Quaternion.identity);
                buf4.transform.SetParent(gameObject.transform);
                buf4.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 5)
            {
                buf5 = Instantiate(buf5, pos, Quaternion.identity);
                buf5.transform.SetParent(gameObject.transform);
                buf5.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 6)
            {
                buf6 = Instantiate(buf6, pos, Quaternion.identity);
                buf6.transform.SetParent(gameObject.transform);
                buf6.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 7)
            {
                buf7 = Instantiate(buf7, pos, Quaternion.identity);
                buf7.transform.SetParent(gameObject.transform);
                buf7.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 8)
            {
                buf8 = Instantiate(buf8, pos, Quaternion.identity);
                buf8.transform.SetParent(gameObject.transform);
                buf8.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 9)
            {
                buf9 = Instantiate(buf9, pos, Quaternion.identity);
                buf9.transform.SetParent(gameObject.transform);
                buf9.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 10)
            {
                buf10 = Instantiate(buf10, pos, Quaternion.identity);
                buf10.transform.SetParent(gameObject.transform);
                buf10.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 11)
            {
                buf11 = Instantiate(buf11, pos, Quaternion.identity);
                buf11.transform.SetParent(gameObject.transform);
                buf11.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 12)
            {
                buf12 = Instantiate(buf12, pos, Quaternion.identity);
                buf12.transform.SetParent(gameObject.transform);
                buf12.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 13)
            {
                buf13 = Instantiate(buf13, pos, Quaternion.identity);
                buf13.transform.SetParent(gameObject.transform);
                buf13.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            a += 40;
        }
    }



    public void BuffupdateEnemy(Dictionary<int, int> buf)
    {
            buf1 = (GameObject)Resources.Load("Buff/burn");
            buf2 = (GameObject)Resources.Load("Buff/frostbite");
            buf3 = (GameObject)Resources.Load("Buff/poisoning");
            buf4 = (GameObject)Resources.Load("Buff/vertigo");
            buf5 = (GameObject)Resources.Load("Buff/exhausted");
            buf6 = (GameObject)Resources.Load("Buff/blindness");
            buf7 = (GameObject)Resources.Load("Buff/powerup");
            buf8 = (GameObject)Resources.Load("Buff/defenseup");
            buf9 = (GameObject)Resources.Load("Buff/speedup");
            buf10 = (GameObject)Resources.Load("Buff/powerdown");
            buf11 = (GameObject)Resources.Load("Buff/defensedown");
            buf12 = (GameObject)Resources.Load("Buff/speeddown");
            buf13 = (GameObject)Resources.Load("Buff/uncontrollable");
        int a = 273;
        int b = 282;
        foreach (KeyValuePair<int, int> kvp in buf)
        {
            Vector3 pos = transform.TransformPoint(a, b, 0);
            if (kvp.Key == 1)
            {
                buf1.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
                buf1 = Instantiate(buf1, pos, Quaternion.identity);
                buf1.transform.SetParent(gameObject.transform);
                //Debug.Log(buf1.transform.GetChild(1));

            }
            if (kvp.Key == 2)
            {
                buf2 = Instantiate(buf2, pos, Quaternion.identity);
                buf2.transform.SetParent(gameObject.transform);
                buf2.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 3)
            {
                buf3 = Instantiate(buf3, pos, Quaternion.identity);
                buf3.transform.SetParent(gameObject.transform);
                buf3.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 4)
            {
                buf4 = Instantiate(buf4, pos, Quaternion.identity);
                buf4.transform.SetParent(gameObject.transform);
                buf4.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 5)
            {
                buf5 = Instantiate(buf5, pos, Quaternion.identity);
                buf5.transform.SetParent(gameObject.transform);
                buf5.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 6)
            {
                buf6 = Instantiate(buf6, pos, Quaternion.identity);
                buf6.transform.SetParent(gameObject.transform);
                buf6.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 7)
            {
                buf7 = Instantiate(buf7, pos, Quaternion.identity);
                buf7.transform.SetParent(gameObject.transform);
                buf7.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 8)
            {
                buf8 = Instantiate(buf8, pos, Quaternion.identity);
                buf8.transform.SetParent(gameObject.transform);
                buf8.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 9)
            {
                buf9 = Instantiate(buf9, pos, Quaternion.identity);
                buf9.transform.SetParent(gameObject.transform);
                buf9.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 10)
            {
                buf10 = Instantiate(buf10, pos, Quaternion.identity);
                buf10.transform.SetParent(gameObject.transform);
                buf10.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 11)
            {
                buf11 = Instantiate(buf11, pos, Quaternion.identity);
                buf11.transform.SetParent(gameObject.transform);
                buf11.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 12)
            {
                buf12 = Instantiate(buf12, pos, Quaternion.identity);
                buf12.transform.SetParent(gameObject.transform);
                buf12.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            if (kvp.Key == 13)
            {
                buf13 = Instantiate(buf13, pos, Quaternion.identity);
                buf13.transform.SetParent(gameObject.transform);
                buf13.transform.GetChild(1).GetComponent<Text>().text = kvp.Value.ToString();
            }
            a += 40;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
