package com.example.pokemonbackend.dao;

import com.example.pokemonbackend.entity.databaseEntity.PokemonSkill;

import java.util.List;

public interface PokemonSkillDao {
    List<PokemonSkill> listAvailSkill(int pokemonId, int level);

    PokemonSkill findById(int id);
}
