package com.example.pokemonbackend.daoImpl;

import com.example.pokemonbackend.dao.PlayerDao;
import com.example.pokemonbackend.entity.databaseEntity.Player;
import com.example.pokemonbackend.repository.PlayerRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class PlayerDaoImpl implements PlayerDao {
    @Autowired
    private PlayerRepository playerRepository;

    @Override
    public Player findByAccountAndPassword(String account, String password) {
        return playerRepository.findByAccountAndPassword(account, password);
    }

    @Override
    public void save(Player player) {
        playerRepository.save(player);
    }

    @Override
    public Player findByAccount(String account) {
        return playerRepository.findByAccount(account);
    }

    @Override
    public Player findById(int id) {
        return playerRepository.findById(id).orElse(null);
    }
}
