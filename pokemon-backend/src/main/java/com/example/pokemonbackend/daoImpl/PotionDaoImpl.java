package com.example.pokemonbackend.daoImpl;

import com.example.pokemonbackend.dao.PotionDao;
import com.example.pokemonbackend.entity.databaseEntity.Potion;
import com.example.pokemonbackend.repository.PotionRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class PotionDaoImpl implements PotionDao {
    @Autowired
    private PotionRepository potionRepository;

    @Override
    public Potion findById(int potionId) {
        return potionRepository.findById(potionId).orElse(null);
    }
}
