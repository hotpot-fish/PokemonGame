package com.example.pokemonbackend.serviceImpl;

import com.example.pokemonbackend.dao.*;
import com.example.pokemonbackend.entity.databaseEntity.PlayerBall;
import com.example.pokemonbackend.entity.databaseEntity.PlayerPotion;
import com.example.pokemonbackend.service.PlayerItemService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class PlayerItemServiceImpl implements PlayerItemService {
    @Autowired
    private PlayerDao playerDao;
    @Autowired
    private PlayerPotionDao playerPotionDao;
    @Autowired
    private PlayerBallDao playerBallDao;
    @Autowired
    private PotionDao potionDao;
    @Autowired
    private BallDao ballDao;

    @Override
    public PlayerPotion findPotion(int playerId, int potionId) {
        return playerPotionDao.findByPlayerIdAndPotionId(playerId, potionId);
    }

    @Override
    public PlayerBall findBall(int playerId, int ballId) {
        return playerBallDao.findByPlayerIdAndBallId(playerId, ballId);
    }

    @Override
    public void usePotion(PlayerPotion playerPotion) {
        if (playerPotion.getNum() == 1) {
            playerPotionDao.delete(playerPotion);
        } else {
            playerPotion.setNum(playerPotion.getNum() - 1);
            playerPotionDao.save(playerPotion);
        }
    }

    @Override
    public void useBall(PlayerBall playerBall) {
        if (playerBall.getNum() == 1) {
            playerBallDao.delete(playerBall);
        } else {
            playerBall.setNum(playerBall.getNum() - 1);
            playerBallDao.save(playerBall);
        }
    }

    @Override
    public PlayerPotion acquirePotion(int playerId, int potionId) {
        PlayerPotion playerPotion = findPotion(playerId, potionId);
        if (playerPotion == null) {
            playerPotion = new PlayerPotion();
            playerPotion.setPlayer(playerDao.findById(playerId));
            playerPotion.setPotion(potionDao.findById(potionId));
            playerPotion.setNum(1);
        } else {
            playerPotion.setNum(playerPotion.getNum() + 1);
        }
        playerPotionDao.save(playerPotion);
        return playerPotion;
    }

    @Override
    public PlayerBall acquireBall(int playerId, int ballId) {
        PlayerBall playerBall = findBall(playerId, ballId);
        if (playerBall == null) {
            playerBall = new PlayerBall();
            playerBall.setPlayer(playerDao.findById(playerId));
            playerBall.setBall(ballDao.findById(ballId));
            playerBall.setNum(1);
        } else {
            playerBall.setNum(playerBall.getNum() + 1);
        }
        playerBallDao.save(playerBall);
        return playerBall;
    }
}
