package com.example.pokemonbackend.service;

import com.example.pokemonbackend.entity.databaseEntity.Player;
import com.example.pokemonbackend.entity.databaseEntity.PlayerBall;
import com.example.pokemonbackend.entity.databaseEntity.PlayerPotion;

public interface PlayerItemService {
    PlayerPotion findPotion(int playerId, int potionId);

    PlayerBall findBall(int playerId, int ballId);

    void usePotion(PlayerPotion playerPotion);

    void useBall(PlayerBall playerBall);

    PlayerPotion acquirePotion(int playerId, int potionId);

    PlayerBall acquireBall(int playerId, int ballId);
}
