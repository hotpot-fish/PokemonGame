package com.example.pokemonbackend.serviceImpl;

import com.example.pokemonbackend.dao.PlayerDao;
import com.example.pokemonbackend.dao.PlayerPokemonDao;
import com.example.pokemonbackend.dao.PokemonDao;
import com.example.pokemonbackend.entity.databaseEntity.Player;
import com.example.pokemonbackend.entity.databaseEntity.PlayerPokemon;
import com.example.pokemonbackend.service.PlayerPokemonService;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import javax.transaction.Transactional;
import java.util.*;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
public class PlayerServiceUnitTest {
    @Autowired
    private PlayerServiceImpl playerService;
    @Autowired
    private PlayerDao playerDao;
    @Autowired
    private PlayerPokemonDao playerPokemonDao;
    @Autowired
    private PlayerPokemonService playerPokemonService;
    @Autowired
    private PokemonDao pokemonDao;

    private String randomBase64(int byteCnt) {
        byte[] bytes = new byte[byteCnt];
        new Random().nextBytes(bytes);
        return Base64.getEncoder().encodeToString(bytes);
    }

    Player createUser() {
        Player player = new Player();
        Date date = new Date();
        String strDate = date.toString();
        player.setAccount("test account at " + strDate);
        player.setEmail("test_" + strDate + "@test.local");
        player.setName("test " + strDate);
        player.setPassword("TEST:" + strDate);
        return player;
    }

    Player createUserAndSave() {
        Player player = new Player();
        Date date = new Date();
        String strDate = date.toString();
        player.setAccount("test account at " + strDate);
        player.setEmail("test_" + strDate + "@test.local");
        player.setName("test " + strDate);
        player.setPassword("TEST:" + strDate);
        playerService.save(player);
        return player;
    }

    @Test
    @Transactional
    public void saveTest() {
        String password = randomBase64(20);
        Player realPlayer = createUser();
        realPlayer.setPassword(password);

        playerService.save(realPlayer);
        int playerId = realPlayer.getId();

        Player gotPlayer = playerService.getPlayer(playerId);
        Assertions.assertNotNull(gotPlayer);
        Assertions.assertEquals(playerId, gotPlayer.getId());
        Assertions.assertEquals(password, gotPlayer.getPassword());
    }

    @Test
    @Transactional
    public void setImageTest() {
        String image = randomBase64(5);
        Player realPlayer = createUserAndSave();
        playerService.setImage(realPlayer, image);

        Player gotPlayer = playerService.getPlayer(realPlayer.getId());
        Assertions.assertNotNull(gotPlayer);
        Assertions.assertEquals(image, gotPlayer.getImage());
    }

    @Test
    @Transactional
    public void getPokemonTest() {
        Player player = createUserAndSave();
        PlayerPokemon pokemon5 =
                playerPokemonService.createPokemon(1, player, 1);
        pokemon5.setBagIndex(5);
        playerPokemonDao.save(pokemon5);

        PlayerPokemon pokemon2 =
                playerPokemonService.createPokemon(4, player, 3);
        pokemon2.setBagIndex(2);
        playerPokemonDao.save(pokemon2);

        PlayerPokemon pokemon0 =
                playerPokemonService.createPokemon(7, player, 100);
        pokemon0.setBagIndex(0);
        playerPokemonDao.save(pokemon0);

        List<PlayerPokemon> inBag = playerService.getPokemonInBag(player.getId()),
                notInBag = playerService.getPokemonNotInBag(player.getId());
        Assertions.assertArrayEquals(new Object[]{pokemon2, pokemon5}, inBag.toArray());
        Assertions.assertArrayEquals(new Object[]{pokemon0}, notInBag.toArray());
    }
}
