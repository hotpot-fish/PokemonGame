using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//�����л���ť�ϣ�ʵ���ӽ��л�
public class VisualSwitch : MonoBehaviour
{
    private GameObject vm;
    private void SwitchClick()
    {
        vm.GetComponent<CinemachineVirtualCamera>().enabled = !vm.GetComponent<CinemachineVirtualCamera>().enabled;
    }
    // Start is called before the first frame update
    void Start()
    {
        vm = GameObject.Find("CM vcam1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
