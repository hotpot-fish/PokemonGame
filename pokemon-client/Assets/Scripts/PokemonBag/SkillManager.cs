//������uimanger�� ��pokemonmanager�������ƣ����� mousetype���ڼ��ε���� �Լ���һ�ε����ť��Я����skill���Ի�pokemon�����Ա���ڶ��ε���İ�ť�������Խ���
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlemsg;

public class SkillManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

namespace Skill_Manager
{

    public class Skill_Scene_Manager : System.Object
    {
        private static Skill_Scene_Manager instance;
        public int handletype=0;
        public int previousindex;
        public PokemonSkill previousbuttonskill;
        public PlayerPokemon lastbuttonpokemon;
        public static Skill_Scene_Manager GetInstance()
        {
            if (instance == null)
            {
                instance = new Skill_Scene_Manager();
            }
            return instance;
        }
    }

}