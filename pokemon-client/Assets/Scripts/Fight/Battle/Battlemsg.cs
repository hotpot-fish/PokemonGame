using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//自定义域名，挂在Battle上
namespace Battlemsg
{
    public class Battle
    {
        public Team[] teams { get; set; }
    }
    public class Team
    {
        public Player player { get; set; }
        public PokemonInBattle[] pokemon { get; set; }
        public int currentPokemonIndex { get; set; }
        public AbilityBuff[] newAbilityBuffs { get; set; }
        public ControlBuff[] newControlBuffs { get; set; }
        public HPBuff[] newHPBuffs { get; set; }
        public int deltaHP { get; set; }
        public String animation { get; set; }
    }
    public class Player
    {
        public int id { get; set; }
        public String name { get; set; }
        public String image { get; set; }
        public String account { get; set; }
        public String password { get; set; }
        public String email { get; set; }
        public PlayerPotion[] potionList { get; set; }
        public PlayerBall[] ballList { get; set; }
        public int Rank { get; set; }
    }
    public class PlayerPokemon
    {
        public int id { get; set; }
        public Pokemon pokemon { get; set; }
        public Skill skill1 { get; set; }
        public Skill skill2 { get; set; }
        public Skill skill3 { get; set; }
        public Skill skill4 { get; set; }
        public int level { get; set; }
        public int curAttack { get; set; }
        public int curDefence { get; set; }
        public int curHP { get; set; }
        public int curSpeed { get; set; }
        public int bagIndex { get; set; }
        public Skill[] skillList { get; set; }
    }
    public class Pokemon
    {
        public int id { get; set; }
        public String name { get; set; }
        public int image { get; set; }
        public String element { get; set; }
        public PokemonSkill[] skillList { get; set; }
        public int baseAttack { get; set; }
        public int baseDefence { get; set; }
        public int baseHP { get; set; }
        public int baseSpeed { get; set; }
        public float growAttack { get; set; }
        public float growDefence { get; set; }
        public float growHP { get; set; }
        public float growSpeed { get; set; }
    }
    public class PokemonInBattle
    {
        public PlayerPokemon pokemon { get; set; }
        public SkillInBattle[] skills { get; set; }
        public AbilityBuff[] abilityBuffs { get; set; }
        public ControlBuff[] controlBuffs { get; set; }
        public HPBuff[] HPBuffs { get; set; }
        public PokemonDigitalAttribution initAttribution { get; set; }
        public PokemonDigitalAttribution curAttribution { get; set; }
    }
    public class SkillInBattle
    {
        public Skill skill { get; set; }
        public int curPP { get; set; }
    }
    public class Skill
    {
        public int id { get; set; }
        public String name { get; set; }
        public int power { get; set; }
        public String element { get; set; }

        public String describe { get; set; }
        public int priority { get; set; }
        public int learnLevel { get; set; }
        public int maxPP { get; set; }

        public string action { get; set; }
    }
    public class Buff
    {
        public int id { get; set; }
        public String discType { get; set; }
        public bool targetSelf { get; set; }
        public String settleTime { get; set; }
        public String effect { get; set; }
    }
    public class AbilityBuff : Buff
    {
        public int data { get; set; }
    }
    public class ControlBuff : Buff
    {
        public int lasting { get; set; }
    }
    public class HPBuff : Buff
    {
        public int data { get; set; }
        public int lasting { get; set; }
    }
    public class PokemonSkill
    {
        public int id { get; set; }
        public Skill skill { get; set; }
    }
    public class Potion
    {
        public int id { get; set; }
        public String name { get; set; }
        public String type { get; set; }
        public int data { get; set; }
    }
    public class PlayerPotion
    {
        public int id { get; set; }
        public Potion potion { get; set; }
        public int num { get; set; }
    }
    public class Ball
    {
        public int id { get; set; }
        public String name { get; set; }
        public int image { get; set; }
        public double possibility { get; set; }
    }
    public class PlayerBall
    {
        public int id { get; set; }
        public Ball ball { get; set; }
        public int num { get; set; }
    }
    public class PokemonDigitalAttribution
    {
        public int attack { get; set; }
        public int defence { get; set; }
        public int HP { get; set; }
        public int speed { get; set; }
    }
}
