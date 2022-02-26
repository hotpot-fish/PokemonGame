package com.example.pokemonbackend.serviceImpl;

import com.example.pokemonbackend.dao.PlayerBallDao;
import com.example.pokemonbackend.dao.PlayerDao;
import com.example.pokemonbackend.dao.PlayerPotionDao;
import com.example.pokemonbackend.entity.databaseEntity.Player;
import com.example.pokemonbackend.entity.databaseEntity.PlayerBall;
import com.example.pokemonbackend.entity.databaseEntity.PlayerPotion;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import javax.transaction.Transactional;
import java.util.Date;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
public class PlayerItemServiceUnitTest {
    @Autowired
    private PlayerItemServiceImpl playerItemService;
    @Autowired
    private PlayerBallDao playerBallDao;
    @Autowired
    private PlayerPotionDao playerPotionDao;
    @Autowired
    private PlayerDao playerDao;

    private int playerId = 0;

    Player registerUser() {
        Player player = new Player();
        Date date = new Date();
        String strDate = date.toString();
        player.setAccount("test account at " + strDate);
        player.setEmail("test_" + strDate + "@test.local");
        player.setName("test " + strDate);
        player.setPassword("TEST:" + strDate);
        playerDao.save(player);
        return player;
    }

    void checkPlayerBall(PlayerBall playerBall, int num, int ballId, int playerId) {
        Assertions.assertNotNull(playerBall);
        Assertions.assertEquals(num, playerBall.getNum());
        Assertions.assertEquals(ballId, playerBall.getBall().getId());
        Assertions.assertEquals(playerId, playerBall.getPlayer().getId());
    }

    void checkPlayerPotion(PlayerPotion playerPotion, int num, int potionId, int playerId) {
        Assertions.assertNotNull(playerPotion);
        Assertions.assertEquals(num, playerPotion.getNum());
        Assertions.assertEquals(potionId, playerPotion.getPotion().getId());
        Assertions.assertEquals(playerId, playerPotion.getPlayer().getId());
    }

    @BeforeEach
    public void setUp() {
        if (playerId == 0) {
            playerId = registerUser().getId();
        }
    }

    @Test
    @Transactional
    public void findNonExistTest() {
        Assertions.assertNull(playerItemService.findBall(playerId, 1));
        Assertions.assertNull(playerItemService.findPotion(playerId, 1));
    }

    @Test
    @Transactional
    public void acquireNewItemTest() {
        playerItemService.acquireBall(playerId, 1);
        checkPlayerBall(playerItemService.findBall(playerId, 1),
                1, 1, playerId);

        playerItemService.acquirePotion(playerId, 1);
        checkPlayerPotion(playerItemService.findPotion(playerId, 1),
                1, 1, playerId);

        Assertions.assertNull(playerItemService.findBall(playerId, 2));
        Assertions.assertNull(playerItemService.findPotion(playerId, 2));
    }

    @Test
    @Transactional
    public void acquireMultipleItem() {
        playerItemService.acquireBall(playerId, 1);
        playerItemService.acquireBall(playerId, 1);
        playerItemService.acquireBall(playerId, 2);
        checkPlayerBall(playerItemService.findBall(playerId, 1),
                2, 1, playerId);
        checkPlayerBall(playerItemService.findBall(playerId, 2),
                1, 2, playerId);

        playerItemService.acquirePotion(playerId, 1);
        playerItemService.acquirePotion(playerId, 1);
        playerItemService.acquirePotion(playerId, 2);
        checkPlayerPotion(playerItemService.findPotion(playerId, 1),
                2, 1, playerId);
        checkPlayerPotion(playerItemService.findPotion(playerId, 2),
                1, 2, playerId);

        Assertions.assertNull(playerItemService.findBall(playerId, 3));
        Assertions.assertNull(playerItemService.findPotion(playerId, 3));
    }

    @Test
    @Transactional
    public void useItemTest() {
        playerItemService.acquireBall(playerId, 1);
        playerItemService.acquireBall(playerId, 1);
        checkPlayerBall(playerItemService.findBall(playerId, 1),
                2, 1, playerId);
        playerItemService.useBall(playerItemService.findBall(playerId, 1));
        checkPlayerBall(playerItemService.findBall(playerId, 1),
                1, 1, playerId);
        playerItemService.useBall(playerItemService.findBall(playerId, 1));
        Assertions.assertNull(playerItemService.findBall(playerId, 1));

        playerItemService.acquirePotion(playerId, 1);
        playerItemService.acquirePotion(playerId, 1);
        checkPlayerPotion(playerItemService.findPotion(playerId, 1),
                2, 1, playerId);
        playerItemService.usePotion(playerItemService.findPotion(playerId, 1));
        checkPlayerPotion(playerItemService.findPotion(playerId, 1),
                1, 1, playerId);
        playerItemService.usePotion(playerItemService.findPotion(playerId, 1));
        Assertions.assertNull(playerItemService.findPotion(playerId, 1));
    }
}
