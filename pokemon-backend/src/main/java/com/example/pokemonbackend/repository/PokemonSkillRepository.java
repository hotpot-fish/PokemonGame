package com.example.pokemonbackend.repository;

import com.example.pokemonbackend.entity.databaseEntity.PokemonSkill;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import java.util.List;

public interface PokemonSkillRepository extends JpaRepository<PokemonSkill, Integer> {
    @Query("from PokemonSkill ps " +
            "where ps.pokemon.id = :pokemonId and ps.skill.learnLevel <= :level")
    List<PokemonSkill> listAvailSkill(int pokemonId, int level);
}
