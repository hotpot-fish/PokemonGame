package com.example.pokemonbackend.dao;

import com.example.pokemonbackend.entity.databaseEntity.PlayerBall;

public interface PlayerBallDao {
    void delete(PlayerBall playerBall);

    PlayerBall save(PlayerBall playerBall);

    PlayerBall findByPlayerIdAndBallId(int playerId, int ballId);
}
