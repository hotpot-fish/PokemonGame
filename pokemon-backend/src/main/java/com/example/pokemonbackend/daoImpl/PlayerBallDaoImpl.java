package com.example.pokemonbackend.daoImpl;

import com.example.pokemonbackend.dao.PlayerBallDao;
import com.example.pokemonbackend.entity.databaseEntity.PlayerBall;
import com.example.pokemonbackend.repository.PlayerBallRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class PlayerBallDaoImpl implements PlayerBallDao {
    @Autowired
    private PlayerBallRepository playerBallRepository;

    @Override
    public void delete(PlayerBall playerBall) {
        playerBallRepository.delete(playerBall);
    }

    @Override
    public PlayerBall save(PlayerBall playerBall) {
        return playerBallRepository.save(playerBall);
    }

    @Override
    public PlayerBall findByPlayerIdAndBallId(int playerId, int ballId) {
        return playerBallRepository.findByPlayerIdAndBallId(playerId, ballId);
    }
}
