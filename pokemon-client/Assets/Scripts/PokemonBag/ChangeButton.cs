//�����ڴ�����PokemonInfor �µ� Skillimage �µ� changebutton�� �����滻���ܰ�ť��������Կ���δѧϰ����������ʾ������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButton : MonoBehaviour
{
    // Start is called before the first frame update
    public int pokemonid;
    int i = 0;
    void Start()
    {
        
    }

    async public void Loadunlearnskillbar() {
        if (i == 0) {
            GameObject.Find("BagLoad").GetComponent<BagLoad>().SetButtontrue();
            await GameObject.Find("SkillLoad").GetComponent<SkillLoad>().PokemonUnlearnSkillLoad(pokemonid);
            i = 1;
            return;
        }
        if (i == 1) {
            GameObject.Find("BagLoad").GetComponent<BagLoad>().SetButtonfalse();
            i = 0;
            return;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
