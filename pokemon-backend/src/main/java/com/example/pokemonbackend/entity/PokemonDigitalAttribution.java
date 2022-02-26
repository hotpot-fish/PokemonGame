package com.example.pokemonbackend.entity;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonProperty;

public class PokemonDigitalAttribution implements Cloneable {
    private int attack;
    private int defence;
    @JsonProperty(value = "HP")
    private int HP;
    private int speed;

    @JsonIgnore
    private boolean canMove;
    @JsonIgnore
    private int dodgeRoundCnt;

    public PokemonDigitalAttribution(int attack, int defence, int HP, int speed) {
        this.attack = attack;
        this.defence = defence;
        this.HP = HP;
        this.speed = speed;
    }

    @Override
    public PokemonDigitalAttribution clone() {
        PokemonDigitalAttribution ret = null;
        try {
            ret = (PokemonDigitalAttribution) super.clone();
            ret.attack = attack;
            ret.defence = defence;
            ret.HP = HP;
            ret.speed = speed;
        } catch (CloneNotSupportedException e) {
            e.printStackTrace();
        }
        return ret;
    }

    public int getAttack() {
        return attack;
    }

    public void setAttack(int attack) {
        this.attack = attack;
    }

    public int getDefence() {
        return defence;
    }

    public void setDefence(int defence) {
        this.defence = defence;
    }

    public int getHP() {
        return HP;
    }

    public void setHP(int HP) {
        this.HP = HP;
    }

    public int getSpeed() {
        return speed;
    }

    public void setSpeed(int speed) {
        this.speed = speed;
    }

    public boolean getCanMove() {
        return canMove;
    }

    public void setCanMove(boolean canMove) {
        this.canMove = canMove;
    }

    public void copyBaseAttribution(PokemonDigitalAttribution initAttribution) {
        attack = initAttribution.attack;
        defence = initAttribution.defence;
        speed = initAttribution.speed;
    }

    public int getDodgeRoundCnt() {
        return dodgeRoundCnt;
    }

    public void setDodgeRoundCnt(int dodgeRoundCnt) {
        this.dodgeRoundCnt = dodgeRoundCnt;
    }
}
