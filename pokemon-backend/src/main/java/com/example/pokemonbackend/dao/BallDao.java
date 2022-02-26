package com.example.pokemonbackend.dao;

import com.example.pokemonbackend.entity.databaseEntity.Ball;

public interface BallDao {
    Ball findById(int ballId);
}
