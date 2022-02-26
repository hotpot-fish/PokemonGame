package com.example.pokemonbackend.dao;

import com.example.pokemonbackend.entity.databaseEntity.PlayerPokemon;

import java.util.List;

public interface PlayerPokemonDao {
    List<PlayerPokemon> findByPlayerIdAndInBag(int playerId);

    PlayerPokemon save(PlayerPokemon playerPokemon);

    PlayerPokemon findById(Integer id);

    PlayerPokemon findByPlayerIdAndBagIndex(int playerId, Integer bagIndex);

    List<PlayerPokemon> findByPlayerIdAndNotInBag(int playerId);
}
