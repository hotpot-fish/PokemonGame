using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������ɹ�����ڿ�enemy��
public class BornBotDesert : MonoBehaviour
{
    public bool on = true;
    //�ó��������ɵĹ���
    public GameObject targetEnemy;
    //���ɹ����������
    public int enemyTotalNum = 1000;
    //���ɹ����ʱ����
    public float intervalTime = 2;
    //���ɹ���ļ�����
    public int enemyCounter;

    // Start is called before the first frame update
    void Start()
    {
        //��ʼʱ���������Ϊ0��
        enemyCounter = 0;
        //�ظ����ɹ���
        InvokeRepeating("CreatEnemy", 0.5F, intervalTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!on && enemyCounter < enemyTotalNum)
        {
            InvokeRepeating("CreatEnemy", 0.5F, intervalTime);
            on = true;
        }
    }
    private void CreatEnemy()
    {
        Vector3 suiji = this.transform.position;
        switch (Random.Range(0, 5) % 5)
        {
            case 0:
                targetEnemy = Resources.Load<GameObject>("DesertPrefabs/Bird");
                //targetEnemy = GameObject.Find("Bee");
                suiji.x = this.transform.position.x + Random.Range(30, 130);
                suiji.z = this.transform.position.z + Random.Range(0, 100);
                break;
            case 1:
                targetEnemy = Resources.Load<GameObject>("DesertPrefabs/Shadow");
                //targetEnemy = GameObject.Find("Bat Lord");
                suiji.x = this.transform.position.x + Random.Range(-30, 30);
                suiji.z = this.transform.position.z + Random.Range(0, 100);
                break;
            case 2:
                targetEnemy = Resources.Load<GameObject>("DesertPrefabs/Toadstool");
                //targetEnemy = GameObject.Find("Bat Lord");
                suiji.x = this.transform.position.x + Random.Range(-130, -30);
                suiji.z = this.transform.position.z + Random.Range(0, 100);
                break;
            case 3:
                targetEnemy = Resources.Load<GameObject>("DesertPrefabs/Spider King");
                //targetEnemy = GameObject.Find("Bat Lord");
                suiji.x = this.transform.position.x + Random.Range(0, 100);
                suiji.z = this.transform.position.z + Random.Range(-100, 0);
                break;
            case 4:
                targetEnemy = Resources.Load<GameObject>("DesertPrefabs/Bat Lord");
                //targetEnemy = GameObject.Find("Bat Lord");
                suiji.x = this.transform.position.x + Random.Range(-100, 0);
                suiji.z = this.transform.position.z + Random.Range(-100, 0);
                break;
        }

        //����һֻ����
        Instantiate(targetEnemy, suiji, Quaternion.identity);
        enemyCounter++;
        //��������ﵽ���ֵ
        if (enemyCounter == enemyTotalNum)
        {
            //ֹͣˢ��
            CancelInvoke();
            on = false;
        }
    }
}
