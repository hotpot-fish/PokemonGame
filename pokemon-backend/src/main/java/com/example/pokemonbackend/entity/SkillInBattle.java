package com.example.pokemonbackend.entity;

import com.example.pokemonbackend.entity.databaseEntity.Skill;

public class SkillInBattle {
    private Skill skill;
    private int curPP;

    public Skill getSkill() {
        return skill;
    }

    public void setSkill(Skill skill) {
        this.skill = skill;
    }

    public int getCurPP() {
        return curPP;
    }

    public void setCurPP(int curPP) {
        this.curPP = curPP;
    }

    public boolean check() {
        return skill != null &&
                (skill.getMaxPP() == -1 || (0 <= curPP && curPP < skill.getMaxPP()));
    }

    public int computeDamage(PokemonDigitalAttribution selfAttr, PokemonDigitalAttribution targetAttr) {
        return skill.getPower() * (selfAttr.getAttack() + 100) / (targetAttr.getDefence() + 100);
    }
}