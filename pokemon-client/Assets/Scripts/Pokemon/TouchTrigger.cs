using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����ÿ���������ϣ���ͼ�ж�������

public class TouchTrigger : MonoBehaviour
{
    //��������
    public GameObject Player;
    public bool on;
    private Animator anim;
    private Vector3 m = new Vector3(0f, 0f, 0f);
    private Vector3 n = new Vector3(2.5f, 0f, 2.5f);
    void OnTriggerEnter(Collider other)//�Ӵ�ʱ�������������
    {
        if (Player == null)
        {
            return;
        }
        on = Player.GetComponent<AttackButtonTrigger>().on;
        if (on &&Vector3.Distance(this.transform.position,Player.transform.position)<Vector3.Distance(m,n))
        {
            anim.SetTrigger("Attack");
            //anim.ResetTrigger("Idle");
            //anim.SetTrigger("Idle");
            this.tag = "Current";
        }
    }
    void OnTriggerExit(Collider other)
    {
        anim.ResetTrigger("Idle");
        anim.SetTrigger("Idle");
        this.tag = "Untagged";
    }


    
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.SetTrigger("Idle");
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
