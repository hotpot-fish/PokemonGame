//搭载在PokemonInfor组件上，用来控制精灵属性、技能总界面的显示
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonInfor : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTrue() {
        gameObject.SetActive(true);
    }
    public void SetFalse()
    {
        gameObject.SetActive(false);
    }
   
}
