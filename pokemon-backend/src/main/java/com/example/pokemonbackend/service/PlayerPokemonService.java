package com.example.pokemonbackend.service;

import com.example.pokemonbackend.entity.databaseEntity.Player;
import com.example.pokemonbackend.entity.databaseEntity.PlayerPokemon;
import com.example.pokemonbackend.entity.databaseEntity.Pokemon;

public interface PlayerPokemonService {
    PlayerPokemon createPokemon(int pokemonId, Player player, int level);

    void createPokemonAndSave(Pokemon pokemon, Player player, int level);

    void updateAttribution(PlayerPokemon playerPokemon, int level);
}
