package com.example.pokemonbackend.daoImpl;

import com.example.pokemonbackend.dao.PlayerPokemonDao;
import com.example.pokemonbackend.entity.databaseEntity.PlayerPokemon;
import com.example.pokemonbackend.repository.PlayerPokemonRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public class PlayerPokemonDaoImpl implements PlayerPokemonDao {
    @Autowired
    private PlayerPokemonRepository playerPokemonRepository;

    @Override
    public List<PlayerPokemon> findByPlayerIdAndInBag(int playerId) {
        return playerPokemonRepository.findByPlayerIdAndInBag(playerId);
    }

    @Override
    public List<PlayerPokemon> findByPlayerIdAndNotInBag(int playerId) {
        return playerPokemonRepository.findByPlayerIdAndNotInBag(playerId);
    }

    @Override
    public PlayerPokemon save(PlayerPokemon playerPokemon) {
        return playerPokemonRepository.save(playerPokemon);
    }

    @Override
    public PlayerPokemon findById(Integer id) {
        return playerPokemonRepository.findById(id).orElse(null);
    }

    @Override
    public PlayerPokemon findByPlayerIdAndBagIndex(int playerId, Integer bagIndex) {
        return playerPokemonRepository.findByPlayerIdAndBagIndex(playerId, bagIndex);
    }
}
