package com.example.pokemonbackend.DaoUnitTest;

import com.example.pokemonbackend.dao.PlayerPotionDao;
import com.example.pokemonbackend.entity.databaseEntity.PlayerPotion;
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
public class PlayerPotionDaoUnitTest {
    @Autowired
    private PlayerPotionDao playerPotionDao;

    @Test
    @Transactional
    public void TestFind() {
        Assertions.assertEquals(5, playerPotionDao.findByPlayerIdAndPotionId(1, 1).getNum());
    }

    @Test
    @Transactional
    public void TestSave() {
        PlayerPotion playerPotion = playerPotionDao.findByPlayerIdAndPotionId(1, 1);
        playerPotion.setNum(100);
        playerPotionDao.save(playerPotion);
        Assertions.assertEquals(100, playerPotionDao.findByPlayerIdAndPotionId(1, 1).getNum());
    }

    @Test
    @Transactional
    public void TestDelete() {
        PlayerPotion playerPotion = playerPotionDao.findByPlayerIdAndPotionId(1, 1);
        playerPotionDao.delete(playerPotion);
        Assertions.assertNull(playerPotionDao.findByPlayerIdAndPotionId(1, 1));
    }
}
