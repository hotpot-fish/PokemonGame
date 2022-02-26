package com.example.pokemonbackend.DaoUnitTest;

import com.example.pokemonbackend.dao.PlayerDao;
import com.example.pokemonbackend.dao.PlayerPokemonDao;
import com.example.pokemonbackend.dao.PokemonDao;
import com.example.pokemonbackend.entity.databaseEntity.Player;
import com.example.pokemonbackend.entity.databaseEntity.PlayerPokemon;
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
public class PlayerPokemonDaoUnitTest {
    @Autowired
    private PlayerPokemonDao playerPokemonDao;

    @Test
    @Transactional
    public void TestFind() {
        Assertions.assertEquals(6, playerPokemonDao.findByPlayerIdAndInBag(2).size());
        Assertions.assertEquals(0, playerPokemonDao.findByPlayerIdAndNotInBag(2).size());
        Assertions.assertEquals("小蜜蜂", playerPokemonDao.findByPlayerIdAndBagIndex(2, 2).getPokemon().getName());
        Assertions.assertNull(playerPokemonDao.findById(-1));
    }

    @Test
    @Transactional
    public void TestSave() {
        PlayerPokemon playerPokemon = playerPokemonDao.findById(1);
        playerPokemon.setLevel(101);
        playerPokemonDao.save(playerPokemon);
        Assertions.assertEquals(101, playerPokemonDao.findById(1).getLevel());
    }
}
