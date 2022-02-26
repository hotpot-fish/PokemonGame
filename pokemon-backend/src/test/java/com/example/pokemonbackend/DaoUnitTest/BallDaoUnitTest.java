package com.example.pokemonbackend.DaoUnitTest;

import com.example.pokemonbackend.dao.BallDao;
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
public class BallDaoUnitTest {
    @Autowired
    private BallDao ballDao;

    @Test
    @Transactional
    public void TestFind(){
        Assertions.assertEquals("初级精灵球", ballDao.findById(1).getName());
        Assertions.assertEquals("中级精灵球", ballDao.findById(2).getName());
        Assertions.assertEquals("高级精灵球", ballDao.findById(3).getName());
        Assertions.assertEquals("大师精灵球", ballDao.findById(4).getName());
    }
}
