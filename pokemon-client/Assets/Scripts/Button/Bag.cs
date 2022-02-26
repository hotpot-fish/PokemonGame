using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Bag : MonoBehaviour
{
    public GameObject player;
    public Method method;
    // Start is called before the first frame update
    void Start()
    {
        method = GameObject.Find("Method").GetComponent<Method>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void bagButton()
    {
        player.GetComponent<SingleInstanceGhost>().path = "Bag";
        method.RecordPosition();
        method.SetPlayer(false);
        method.SetFmAndVm(false);
        SceneManager.LoadScene("BagScene");
    }
}
