package com.example.pokemonbackend.daoImpl;

import com.example.pokemonbackend.dao.PlayerPotionDao;
import com.example.pokemonbackend.entity.databaseEntity.PlayerPotion;
import com.example.pokemonbackend.repository.PlayerPotionRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class PlayerPotionDaoImpl implements PlayerPotionDao {
    @Autowired
    private PlayerPotionRepository playerPotionRepository;

    @Override
    public PlayerPotion save(PlayerPotion playerPotion) {
        return playerPotionRepository.save(playerPotion);
    }

    @Override
    public void delete(PlayerPotion playerPotion) {
        playerPotionRepository.delete(playerPotion);
    }

    @Override
    public PlayerPotion findByPlayerIdAndPotionId(int playerId, int potionId) {
        return playerPotionRepository.findByPlayerIdAndPotionId(playerId, potionId);
    }
}
