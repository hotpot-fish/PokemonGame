using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageValue : MonoBehaviour
{
    public void Valueupdate(int a)
    {
        if (a > 0)
        {
            gameObject.GetComponent<Text>().text = "<color=#00FF00>" + "+" + a.ToString() + "</color>";
        }
        if (a == 0)
        {
            gameObject.GetComponent<Text>().text = "";
        }
        if (a < 0)
        {
            gameObject.GetComponent<Text>().text = " <color=#FF0000>" + a.ToString() + "</color>";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
