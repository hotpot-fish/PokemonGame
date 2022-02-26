package com.example.pokemonbackend.dao;

import com.example.pokemonbackend.entity.databaseEntity.Potion;

public interface PotionDao {
    Potion findById(int potionId);
}
