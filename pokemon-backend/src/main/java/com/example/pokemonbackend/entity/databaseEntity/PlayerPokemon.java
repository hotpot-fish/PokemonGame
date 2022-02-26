package com.example.pokemonbackend.entity.databaseEntity;

import com.example.pokemonbackend.config.JsonAnnotation;
import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import javax.persistence.*;

@Entity
@Table(name = "player_pokemon")
@JsonIgnoreProperties(value = {"handler", "hibernateLazyInitializer", "fieldHandler"})
public class PlayerPokemon {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    @ManyToOne(fetch = FetchType.EAGER, cascade = CascadeType.DETACH)
    @JoinColumn(name = "pokemon_id")
    private Pokemon pokemon;

    @JsonIgnore
    @ManyToOne(fetch = FetchType.EAGER, cascade = CascadeType.DETACH)
    @JoinColumn(name = "player_id")
    private Player player;

    @ManyToOne(fetch = FetchType.EAGER, cascade = CascadeType.DETACH)
    @JoinColumn(name = "skill1_id")
    @JsonIgnore
    private Skill skill1;

    @ManyToOne(fetch = FetchType.EAGER, cascade = CascadeType.DETACH)
    @JoinColumn(name = "skill2_id")
    @JsonIgnore
    private Skill skill2;

    @ManyToOne(fetch = FetchType.EAGER, cascade = CascadeType.DETACH)
    @JoinColumn(name = "skill3_id")
    @JsonIgnore
    private Skill skill3;

    @ManyToOne(fetch = FetchType.EAGER, cascade = CascadeType.DETACH)
    @JoinColumn(name = "skill4_id")
    @JsonIgnore
    private Skill skill4;

    @Column(name = "level")
    private int level;

    @Column(name = "cur_attack")
    @JsonAnnotation.BattleIgnore
    private int curAttack;

    @Column(name = "cur_defence")
    @JsonAnnotation.BattleIgnore
    private int curDefence;

    @Column(name = "cur_HP")
    @JsonAnnotation.BattleIgnore
    private int curHP;

    @Column(name = "cur_speed")
    @JsonAnnotation.BattleIgnore
    private int curSpeed;

    @Column(name = "bag_index")
    @JsonAnnotation.BattleIgnore
    private int bagIndex;

    private int exp;
    @Column(name = "cur_exp")
    private int curExp;

    public void setBagIndex(int bagIndex) {
        this.bagIndex = bagIndex;
    }

    public int getExp() {
        return exp;
    }

    public void setExp(int exp) {
        this.exp = exp;
    }

    public int getCurExp() {
        return curExp;
    }

    public void setCurExp(int curExp) {
        this.curExp = curExp;
    }

    public int getBagIndex() {
        return bagIndex;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public Pokemon getPokemon() {
        return pokemon;
    }

    public void setPokemon(Pokemon pokemon) {
        this.pokemon = pokemon;
    }

    public Player getPlayer() {
        return player;
    }

    public void setPlayer(Player player) {
        this.player = player;
    }

    public Skill getSkill1() {
        return skill1;
    }

    public void setSkill1(Skill skill1) {
        this.skill1 = skill1;
    }

    public Skill getSkill2() {
        return skill2;
    }

    public void setSkill2(Skill skill2) {
        this.skill2 = skill2;
    }

    public Skill getSkill3() {
        return skill3;
    }

    public void setSkill3(Skill skill3) {
        this.skill3 = skill3;
    }

    public Skill getSkill4() {
        return skill4;
    }

    public void setSkill4(Skill skill4) {
        this.skill4 = skill4;
    }

    public int getLevel() {
        return level;
    }

    public void setLevel(int level) {
        this.level = level;
    }

    public int getCurAttack() {
        return curAttack;
    }

    public void setCurAttack(int curAttack) {
        this.curAttack = curAttack;
    }

    public int getCurDefence() {
        return curDefence;
    }

    public void setCurDefence(int curDefence) {
        this.curDefence = curDefence;
    }

    public int getCurHP() {
        return curHP;
    }

    public void setCurHP(int curHP) {
        this.curHP = curHP;
    }

    public int getCurSpeed() {
        return curSpeed;
    }

    public void setCurSpeed(int curSpeed) {
        this.curSpeed = curSpeed;
    }

    @JsonAnnotation.BattleIgnore
    public Skill[] getSkillList() {
        return new Skill[]{skill1, skill2, skill3, skill4};
    }
}
