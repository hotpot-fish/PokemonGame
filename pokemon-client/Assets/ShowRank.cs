using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRank : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject web = GameObject.Find("websocket");
        gameObject.GetComponent<Text>().text = "≈≈Œª∑÷ ˝£∫" + websocket.rank;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
