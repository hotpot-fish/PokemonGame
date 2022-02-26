package com.example.pokemonbackend.entity;

import com.example.pokemonbackend.entity.databaseEntity.*;
import com.fasterxml.jackson.annotation.JsonProperty;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

public class PokemonInBattle {
    private PlayerPokemon pokemon;
    private SkillInBattle[] skills;
    private final List<Buff> buffs;
    private PokemonDigitalAttribution initAttribution, curAttribution;

    public PlayerPokemon getPokemon() {
        return pokemon;
    }

    public void setPokemon(PlayerPokemon pokemon) {
        this.pokemon = pokemon;
    }

    public SkillInBattle[] getSkills() {
        return skills;
    }

    public void setSkills(SkillInBattle[] skills) {
        this.skills = skills;
    }

    public List<Buff> getBuffs() {
        return buffs;
    }

    public List<AbilityBuff> getAbilityBuffs() {
        return buffs.stream().filter(buff -> (buff instanceof AbilityBuff))
                .map(buff -> (AbilityBuff) buff)
                .collect(Collectors.toList());
    }

    public List<ControlBuff> getControlBuffs() {
        return buffs.stream().filter(buff -> (buff instanceof ControlBuff))
                .map(buff -> (ControlBuff) buff)
                .collect(Collectors.toList());
    }

    @JsonProperty(value = "HPBuffs")
    public List<HPBuff> getHPBuffs() {
        return buffs.stream().filter(buff -> (buff instanceof HPBuff))
                .map(buff -> (HPBuff) buff)
                .collect(Collectors.toList());
    }

    public boolean check() {
        return pokemon != null;
    }

    public PokemonDigitalAttribution getInitAttribution() {
        return initAttribution;
    }

    public void setInitAttribution(PokemonDigitalAttribution initAttribution) {
        this.initAttribution = initAttribution;
    }

    public PokemonDigitalAttribution getCurAttribution() {
        return curAttribution;
    }

    public void setCurAttribution(PokemonDigitalAttribution curAttribution) {
        this.curAttribution = curAttribution;
    }

    public PokemonInBattle() {
        buffs = new ArrayList<>();
    }

    private static class filterEnemyTeamFlag {}

    private PokemonInBattle(PokemonInBattle toFilterEnemyPokemon, filterEnemyTeamFlag flag) {
        pokemon = toFilterEnemyPokemon.getPokemon();
        skills = null;
        buffs = toFilterEnemyPokemon.getBuffs();
        initAttribution = toFilterEnemyPokemon.getInitAttribution();
        curAttribution = toFilterEnemyPokemon.getCurAttribution();
    }

    PokemonInBattle filterEnemyCurPokemon() {
        return new PokemonInBattle(this, null);
    }
}