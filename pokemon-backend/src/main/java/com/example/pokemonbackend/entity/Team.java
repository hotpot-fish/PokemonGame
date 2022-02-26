package com.example.pokemonbackend.entity;

import com.example.pokemonbackend.entity.databaseEntity.*;
import com.fasterxml.jackson.annotation.JsonIgnore;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

public class Team {
    private Player player;
    private PokemonInBattle[] pokemon;
    private int currentPokemonIndex;

    @JsonIgnore
    private String receiveBuffer;
    @JsonIgnore
    private int damage;

    @JsonIgnore
    private final List<Buff> newBuffs;
    private int deltaHP;
    private String animation;

    public Team() {
        newBuffs = new ArrayList<>();
    }

    public Player getPlayer() {
        return player;
    }

    public void setPlayer(Player player) {
        this.player = player;
    }

    public PokemonInBattle[] getPokemon() {
        return pokemon;
    }

    public void setPokemon(PokemonInBattle[] pokemon) {
        this.pokemon = pokemon;
    }

    public int getCurrentPokemonIndex() {
        return currentPokemonIndex;
    }

    public void setCurrentPokemonIndex(int currentPokemonIndex) {
        this.currentPokemonIndex = currentPokemonIndex;
    }

    public boolean check() {
        return pokemon.length == 6 &&
                0 <= currentPokemonIndex && currentPokemonIndex < pokemon.length;
    }

    public int getDeltaHP() {
        return deltaHP;
    }

    public void setDeltaHP(int deltaHP) {
        this.deltaHP = deltaHP;
    }

    public String getAnimation() {
        return animation;
    }

    public void setAnimation(String animation) {
        this.animation = animation;
    }

    public String getReceiveBuffer() {
        return receiveBuffer;
    }

    public void setReceiveBuffer(String receiveBuffer) {
        this.receiveBuffer = receiveBuffer;
    }

    @JsonIgnore
    public PokemonInBattle getCurrentPokemon() {
        return pokemon[currentPokemonIndex];
    }

    public List<Buff> getNewBuffs() {
        return newBuffs;
    }

    public List<AbilityBuff> getNewAbilityBuffs() {
        return newBuffs.stream().filter(buff -> buff instanceof AbilityBuff)
                .map(buff -> (AbilityBuff) buff)
                .collect(Collectors.toList());
    }

    public List<ControlBuff> getNewControlBuffs() {
        return newBuffs.stream().filter(buff -> buff instanceof ControlBuff)
                .map(buff -> (ControlBuff) buff)
                .collect(Collectors.toList());
    }

    public List<HPBuff> getNewHPBuffs() {
        return newBuffs.stream().filter(buff -> buff instanceof HPBuff)
                .map(buff -> (HPBuff) buff)
                .collect(Collectors.toList());
    }

    public void clearNewStatus() {
        animation = null;
        deltaHP = 0;
        newBuffs.clear();
    }

    public int getDamage() {
        return damage;
    }

    public void setDamage(int damage) {
        this.damage = damage;
    }

    private static class filterEnemyTeamFlag {}

    private Team(Team toFilterEnemyTeam, filterEnemyTeamFlag filterFlag) {
        player = toFilterEnemyTeam.player;
        deltaHP = toFilterEnemyTeam.deltaHP;
        animation = toFilterEnemyTeam.animation;
        newBuffs = toFilterEnemyTeam.newBuffs;
        currentPokemonIndex = toFilterEnemyTeam.currentPokemonIndex;

        pokemon = new PokemonInBattle[6];
        pokemon[currentPokemonIndex] =
                toFilterEnemyTeam.getCurrentPokemon().filterEnemyCurPokemon();
    }

    Team filterSelfTeam() {
        return this;
    }

    Team filterEnemyTeam() {
        return new Team(this, null);
    }
}