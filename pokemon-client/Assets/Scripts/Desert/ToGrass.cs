using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//����ɳĮ�л�����
public class ToGrass : MonoBehaviour
{
    public GameObject Player;
    private Vector3 m = new Vector3(0f, 0f, 0f);
    private Vector3 n = new Vector3(2.5f, 0f, 2.5f);
    public Method method;
    void OnTriggerEnter(Collider other)//�Ӵ�ʱ�������������
    {

        if (Player == null)
        {
            return;
        }
        if (Vector3.Distance(this.transform.position, Player.transform.position) < Vector3.Distance(m, n))
        {
            Player.GetComponent<SingleInstanceGhost>().path = "ToGrass";
            method.SetMapPath("Demo_1");
            SceneManager.LoadScene("Demo_1");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        method = GameObject.Find("Method").GetComponent<Method>();
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
