using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//随机生成怪物，挂在空enemy上
public class BornBotDesert : MonoBehaviour
{
    public bool on = true;
    //该出生点生成的怪物
    public GameObject targetEnemy;
    //生成怪物的总数量
    public int enemyTotalNum = 1000;
    //生成怪物的时间间隔
    public float intervalTime = 2;
    //生成怪物的计数器
    public int enemyCounter;

    // Start is called before the first frame update
    void Start()
    {
        //初始时，怪物计数为0；
        enemyCounter = 0;
        //重复生成怪物
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

        //生成一只怪物
        Instantiate(targetEnemy, suiji, Quaternion.identity);
        enemyCounter++;
        //如果计数达到最大值
        if (enemyCounter == enemyTotalNum)
        {
            //停止刷新
            CancelInvoke();
            on = false;
        }
    }
}
