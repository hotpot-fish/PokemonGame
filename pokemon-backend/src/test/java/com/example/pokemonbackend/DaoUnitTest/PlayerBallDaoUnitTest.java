package com.example.pokemonbackend.DaoUnitTest;

import com.example.pokemonbackend.dao.BallDao;
import com.example.pokemonbackend.dao.PlayerBallDao;
import com.example.pokemonbackend.dao.PlayerDao;
import com.example.pokemonbackend.entity.databaseEntity.Ball;
import com.example.pokemonbackend.entity.databaseEntity.Player;
import com.example.pokemonbackend.entity.databaseEntity.PlayerBall;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.jdbc.AutoConfigureTestDatabase;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.stereotype.Repository;

import javax.transaction.Transactional;

@DataJpaTest(includeFilters = @ComponentScan.Filter(classes = Repository.class))
@AutoConfigureTestDatabase(replace = AutoConfigureTestDatabase.Replace.NONE)
public class PlayerBallDaoUnitTest {
    @Autowired
    private PlayerBallDao playerBallDao;
    @Autowired
    private PlayerDao playerDao;
    @Autowired
    private BallDao ballDao;

    @Test
    @Transactional
    public void TestFind(){
        Assertions.assertEquals(5, playerBallDao.findByPlayerIdAndBallId(1, 4).getNum());
    }

    @Test
    @Transactional
    public void TestSave(){
        playerBallDao.save(new PlayerBall(playerDao.findById(1), ballDao.findById(1), 1));
        Assertions.assertEquals(1, playerBallDao.findByPlayerIdAndBallId(1, 1).getNum());
    }

    @Test
    @Transactional
    public void TestDelete(){
        playerBallDao.delete(playerBallDao.findByPlayerIdAndBallId(1, 4));
        Assertions.assertNull(playerBallDao.findByPlayerIdAndBallId(1, 4));
    }
}
