using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����VM�ϣ�ʵ�ֵڶ����ӽǸ���
public class SecondCamera : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private Vector3 cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            cameraPosition = player.transform.position;
            cameraPosition.y = cameraPosition.y + 25f;
            transform.position = cameraPosition;
        }
    }
}
