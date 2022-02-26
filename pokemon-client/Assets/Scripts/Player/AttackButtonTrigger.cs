using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//挂在player上，攻击按钮可用性触发
public class AttackButtonTrigger : MonoBehaviour
{
    private GameObject Attack;
    public int a = 0;
    public bool on = false;
    void OnTriggerEnter(Collider other)//接触时触发，无需调用
    {
        if (a > 1)
        {
            if (Attack == null)
            {
                Attack = GameObject.Find("AttackButton");
            }
            Attack.GetComponent<Button>().interactable = true;
            on = true;
        }
        else
        {
            a++;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (Attack == null)
        {
            Attack = GameObject.Find("AttackButton");
        }
        Attack.GetComponent<Button>().interactable = false;
        on = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        Attack = GameObject.Find("AttackButton");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Current = GameObject.FindWithTag("Current");
        if (Attack == null)
        {
            Attack = GameObject.Find("AttackButton");
        }
        if (Current == null&&Attack!=null)
        {
            Attack.GetComponent<Button>().interactable = false;
            on = false;
        }
    }
}
