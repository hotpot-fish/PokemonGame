package com.example.pokemonbackend.daoImpl;

import com.example.pokemonbackend.dao.PokemonSkillDao;
import com.example.pokemonbackend.entity.databaseEntity.PokemonSkill;
import com.example.pokemonbackend.repository.PokemonSkillRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public class PokemonSkillDaoImpl implements PokemonSkillDao {
    @Autowired
    private PokemonSkillRepository pokemonSkillRepository;

    @Override
    public List<PokemonSkill> listAvailSkill(int pokemonId, int level) {
        return pokemonSkillRepository.listAvailSkill(pokemonId, level);
    }

    @Override
    public PokemonSkill findById(int id) {
        return pokemonSkillRepository.findById(id).orElse(null);
    }
}
