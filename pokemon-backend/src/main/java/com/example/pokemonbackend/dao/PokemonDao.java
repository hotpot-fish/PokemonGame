package com.example.pokemonbackend.dao;

import com.example.pokemonbackend.entity.databaseEntity.Pokemon;

public interface PokemonDao {
    Pokemon findById(int pokemonId);
}
