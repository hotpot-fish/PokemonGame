package com.example.pokemonbackend.repository;

import com.example.pokemonbackend.entity.databaseEntity.Ball;
import org.springframework.data.jpa.repository.JpaRepository;

public interface BallRepository extends JpaRepository<Ball, Integer> {
}
