package com.example.pokemonbackend.repository;

import com.example.pokemonbackend.entity.databaseEntity.Skill;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SkillRepository extends JpaRepository<Skill, Integer> {
}