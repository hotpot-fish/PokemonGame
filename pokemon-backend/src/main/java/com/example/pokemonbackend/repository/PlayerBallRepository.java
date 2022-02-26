package com.example.pokemonbackend.repository;

import com.example.pokemonbackend.entity.databaseEntity.PlayerBall;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PlayerBallRepository extends JpaRepository<PlayerBall, Integer> {
    PlayerBall findByPlayerIdAndBallId(int playerId, int ballId);
}
