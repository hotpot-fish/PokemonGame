//搭载在uimanger上 与pokemonmanager作用类似，储存 mousetype（第几次点击） 以及第一次点击按钮所携带的skill属性或pokemon属性以便与第二次点击的按钮进行属性交换
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