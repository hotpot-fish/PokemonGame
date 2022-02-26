package com.example.pokemonbackend.service;

import com.example.pokemonbackend.entity.databaseEntity.Player;
import com.example.pokemonbackend.entity.databaseEntity.PlayerPokemon;

import java.util.List;

public interface PlayerService {
    List<PlayerPokemon> getPokemonInBag(int playerId);

    List<PlayerPokemon> getPokemonNotInBag(int playerId);

    Player getPlayer(int id);

    void save(Player winner);

    void setImage(Player player, String image);
}
