using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Battlemsg;
//挂在User2Bar上，敌方血条控制
public class User2Bar : MonoBehaviour
{
    Image user2_img1;
    Image user2_img2;
    float user2_imm_bar_amount, user2_slow_bar_amount;
    float user2_current_life = 180, user2_all_life = 180;
    GameObject hptext;

    public void hpchange(PokemonInBattle my)
    {
        user2_current_life = my.curAttribution.HP;
        user2_all_life = my.initAttribution.HP;
    }

    // Start is called before the first frame update
    void Start()
    {
        hptext = GameObject.Find("HpText2");
        user2_img1 = GameObject.Find("User2_ImmBar").GetComponent<Image>();
        user2_img2 = GameObject.Find("User2_SlowBar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        hptext.GetComponent<Text>().text = user2_current_life + "/" + user2_all_life;
        user2_img1.fillAmount = (user2_current_life / user2_all_life);
        user2_imm_bar_amount = user2_img1.fillAmount;
        user2_slow_bar_amount = user2_img2.fillAmount;
        if (user2_imm_bar_amount < user2_slow_bar_amount)
        {
            user2_img2.fillAmount = user2_slow_bar_amount - (Time.deltaTime * 0.5f);
        }
        if (user2_imm_bar_amount > user2_slow_bar_amount)
        {
            user2_img2.fillAmount = user2_slow_bar_amount + (Time.deltaTime * 0.15f);
        }
    }
}


