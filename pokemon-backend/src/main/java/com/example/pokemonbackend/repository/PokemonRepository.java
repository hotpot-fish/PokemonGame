package com.example.pokemonbackend.repository;

import com.example.pokemonbackend.entity.databaseEntity.Pokemon;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PokemonRepository extends JpaRepository<Pokemon, Integer> {
}
