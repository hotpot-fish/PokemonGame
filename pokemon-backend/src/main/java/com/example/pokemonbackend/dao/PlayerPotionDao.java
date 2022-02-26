package com.example.pokemonbackend.dao;

import com.example.pokemonbackend.entity.databaseEntity.PlayerPotion;

public interface PlayerPotionDao {
    PlayerPotion save(PlayerPotion playerPotion);

    void delete(PlayerPotion playerPotion);

    PlayerPotion findByPlayerIdAndPotionId(int playerId, int potionId);
}
