package com.example.pokemonbackend.repository;

import com.example.pokemonbackend.entity.databaseEntity.PlayerPotion;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PlayerPotionRepository extends JpaRepository<PlayerPotion, Integer> {
    PlayerPotion findByPlayerIdAndPotionId(int playerId, int potionId);
}
