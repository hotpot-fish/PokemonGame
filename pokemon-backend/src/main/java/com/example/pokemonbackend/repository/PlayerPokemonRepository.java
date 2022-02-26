package com.example.pokemonbackend.repository;

import com.example.pokemonbackend.entity.databaseEntity.PlayerPokemon;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import java.util.List;

public interface PlayerPokemonRepository extends JpaRepository<PlayerPokemon, Integer> {
    @Query(value = "SELECT * FROM player_pokemon pp " +
            "WHERE pp.player_id = :playerId AND pp.bag_index > 0", nativeQuery = true)
    List<PlayerPokemon> findByPlayerIdAndInBag(int playerId);

    @Query(value = "SELECT * FROM player_pokemon pp " +
            "WHERE pp.player_id = :playerId AND pp.bag_index = 0", nativeQuery = true)
    List<PlayerPokemon> findByPlayerIdAndNotInBag(int playerId);

    PlayerPokemon findByPlayerIdAndBagIndex(int playerId, int bagIndex);
}