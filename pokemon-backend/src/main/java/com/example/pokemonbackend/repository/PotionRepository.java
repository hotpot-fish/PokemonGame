package com.example.pokemonbackend.repository;

import com.example.pokemonbackend.entity.databaseEntity.Potion;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PotionRepository extends JpaRepository<Potion, Integer> {
}
