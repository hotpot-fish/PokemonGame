package com.example.pokemonbackend.dao;

import com.example.pokemonbackend.entity.databaseEntity.Player;

public interface PlayerDao {
    Player findByAccountAndPassword(String account, String password);

    void save(Player player);

    Player findByAccount(String account);

    Player findById(int id);
}
