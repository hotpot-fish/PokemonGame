using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//���ڲ�ԭ�л�����
public class ToDesert : MonoBehaviour
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
            Player.GetComponent<SingleInstanceGhost>().path = "ToDesert";
            method.SetMapPath("Demo_2");
            SceneManager.LoadScene("Demo_2");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        method = GameObject.Find("Method").GetComponent<Method>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
