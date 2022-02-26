package com.example.pokemonbackend.DaoUnitTest;

import com.example.pokemonbackend.dao.PlayerDao;
import com.example.pokemonbackend.entity.databaseEntity.Player;
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
public class PlayerDaoUnitTest {
    @Autowired
    private PlayerDao playerDao;

    @Test
    @Transactional
    public void TestFind() {
        Assertions.assertEquals("test user 001", playerDao.findById(1).getName());
        Assertions.assertEquals("test user 001", playerDao.findByAccount("t001").getName());
        Assertions.assertEquals("test user 001", playerDao.findByAccountAndPassword("t001", "t001t001").getName());
    }

    @Test
    @Transactional
    public void TestSave() {
        playerDao.save(new Player("abc", "abc", "abc", "abc@qq.com"));
        Assertions.assertEquals("abc", playerDao.findByAccountAndPassword("abc", "abc").getName());
    }
}
