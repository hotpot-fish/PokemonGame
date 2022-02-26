using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Battlemsg;
//挂在User1Bar上，己方血条控制
public class User1Bar : MonoBehaviour
{
    Image user1_img1;
    Image user1_img2;
    float user1_imm_bar_amount, user1_slow_bar_amount;
    float user1_current_life=180, user1_all_life=180;
    GameObject hptext;

    public void hpchange(PokemonInBattle my)
    {
        user1_current_life = my.curAttribution.HP;
        user1_all_life = my.initAttribution.HP;
    }

    // Start is called before the first frame update
    void Start()
    {
        //User1_Bar(Clone)
        hptext = GameObject.Find("HpText1");
        user1_img1 = GameObject.Find("User1_ImmBar").GetComponent<Image>();//User1_ImmBar
        user1_img2 = GameObject.Find("User1_SlowBar").GetComponent<Image>();//User1_SlowBar
    }


    // Update is called once per frame
    void Update()
    {
        //
        //血条动画
        //
        hptext.GetComponent<Text>().text = user1_current_life + "/" + user1_all_life;
        user1_img1.fillAmount = (user1_current_life / user1_all_life);
        user1_imm_bar_amount = user1_img1.fillAmount;
        user1_slow_bar_amount = user1_img2.fillAmount;
        if (user1_imm_bar_amount < user1_slow_bar_amount)
        {
            user1_img2.fillAmount = user1_slow_bar_amount - (Time.deltaTime * 0.5f);
        }
        if (user1_imm_bar_amount > user1_slow_bar_amount)
        {
            user1_img2.fillAmount = user1_slow_bar_amount + (Time.deltaTime * 0.15f);
        }
    }
}


