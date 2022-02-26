package com.example.pokemonbackend.DaoUnitTest;

import com.example.pokemonbackend.dao.PokemonDao;
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
public class PokemonDaoUnitTest {
    @Autowired
    private PokemonDao pokemonDao;

    @Test
    @Transactional
    public void TestFind() {
        Assertions.assertEquals("小蜜蜂", pokemonDao.findById(1).getName());
    }
}
