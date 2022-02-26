package com.example.pokemonbackend.serviceImpl;

import com.example.pokemonbackend.dao.PlayerDao;
import com.example.pokemonbackend.dao.PlayerPokemonDao;
import com.example.pokemonbackend.dao.PokemonDao;
import com.example.pokemonbackend.entity.databaseEntity.Player;
import com.example.pokemonbackend.entity.databaseEntity.PlayerPokemon;
import com.example.pokemonbackend.entity.databaseEntity.Pokemon;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import javax.transaction.Transactional;
import java.util.Date;
import java.util.List;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
public class PlayerPokemonServiceUnitTest {
    @Autowired
    private PlayerPokemonServiceImpl playerPokemonService;
    @Autowired
    private PlayerDao playerDao;
    @Autowired
    private PokemonDao pokemonDao;
    @Autowired
    private PlayerPokemonDao playerPokemonDao;

    Player player;

    @BeforeEach
    void registerUser() {
        player = new Player();
        Date date = new Date();
        String strDate = date.toString();
        player.setAccount("test account at " + strDate);
        player.setEmail("test_" + strDate + "@test.local");
        player.setName("test " + strDate);
        player.setPassword("TEST:" + strDate);
        playerDao.save(player);
    }

    @Test
    @Transactional
    public void createPokemonTest() {
        PlayerPokemon playerPokemon =
                playerPokemonService.createPokemon(1, player, 100);
        Assertions.assertEquals(player, playerPokemon.getPlayer());
        Assertions.assertEquals(1, playerPokemon.getPokemon().getId());
        Assertions.assertEquals(100, playerPokemon.getLevel());
        Assertions.assertEquals(0,
                playerPokemonDao.findByPlayerIdAndNotInBag(player.getId()).size());
    }

    @Test
    @Transactional
    public void createPokemonAndSaveTest() {
        Pokemon pokemon = pokemonDao.findById(1);
        playerPokemonService.createPokemonAndSave(pokemon, player, 100);
        List<PlayerPokemon> pokemonList =
                playerPokemonDao.findByPlayerIdAndNotInBag(player.getId());
        Assertions.assertEquals(1, pokemonList.size());
        PlayerPokemon playerPokemon = pokemonList.get(0);
        Assertions.assertEquals(player, playerPokemon.getPlayer());
        Assertions.assertEquals(pokemon, playerPokemon.getPokemon());
        Assertions.assertEquals(100, playerPokemon.getLevel());
    }

    @Test
    @Transactional
    public void createPokemonExceptionTest() {
        Assertions.assertThrows(NullPointerException.class,
                () -> playerPokemonService.createPokemon(0, player, 5));
    }
}
