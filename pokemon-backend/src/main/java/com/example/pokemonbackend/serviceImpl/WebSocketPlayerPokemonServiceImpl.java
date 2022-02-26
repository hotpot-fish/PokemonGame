package com.example.pokemonbackend.serviceImpl;

import com.example.pokemonbackend.dao.PlayerPokemonDao;
import com.example.pokemonbackend.dao.PokemonDao;
import com.example.pokemonbackend.dao.PokemonSkillDao;
import com.example.pokemonbackend.entity.databaseEntity.Player;
import com.example.pokemonbackend.entity.databaseEntity.PlayerPokemon;
import com.example.pokemonbackend.entity.databaseEntity.PokemonSkill;
import com.example.pokemonbackend.entity.databaseEntity.Skill;
import com.example.pokemonbackend.service.PlayerPokemonService;
import com.example.pokemonbackend.service.PlayerService;
import com.example.pokemonbackend.service.WebSocketPlayerPokemonService;
import com.example.pokemonbackend.util.MyWSException;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.websocket.Session;
import java.io.IOException;
import java.util.*;
import java.util.stream.Collectors;
import java.util.stream.Stream;

@Service
public class WebSocketPlayerPokemonServiceImpl implements WebSocketPlayerPokemonService {
    @Autowired
    PlayerService playerService;
    @Autowired
    PlayerPokemonDao playerPokemonDao;
    @Autowired
    PokemonDao pokemonDao;
    @Autowired
    PlayerPokemonService playerPokemonService;
    @Autowired
    PokemonSkillDao pokemonSkillDao;

    private final Set<Integer> initialPokemonIdSet
            = Stream.of(1, 8, 9).collect(Collectors.toUnmodifiableSet());

    private void sendBack(Session session, String headerMsg, Object payload) {
        String json;
        try {
            json = (new ObjectMapper()).writeValueAsString(payload);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
            return;
        }
        String msg = headerMsg + "\n" + json;
        try {
            session.getBasicRemote().sendText(msg);
        } catch (IOException e) {
            throw new MyWSException();
        }
    }

    @Override
    public void onOpen(Session session) {
    }

    @Override
    public void onClose(Session session) {
    }

    @Override
    public void listBag(Session session) {
        int playerId = (int) session.getUserProperties().get("playerId");
        String json;
        PlayerPokemon[] bag = new PlayerPokemon[6];
        for (PlayerPokemon playerPokemon : playerService.getPokemonInBag(playerId)) {
            bag[playerPokemon.getBagIndex() - 1] = playerPokemon;
        }
        sendBack(session, "bag_pokemon", bag);
    }

    @Override
    public void listMyPokemon(Session session) {
        int playerId = (int) session.getUserProperties().get("playerId");
        sendBack(session, "my_pokemon",
                playerService.getPlayer(playerId).getPokemonList());
    }

    @Override
    public void listMyPokemonNotInBag(Session session) {
        int playerId = (int) session.getUserProperties().get("playerId");
        sendBack(session, "my_pokemon_not_in_bag",
                playerService.getPokemonNotInBag(playerId));
    }

    @Override
    public void modifyBagIndex(Session session, String msg) {
        int playerId = (int) session.getUserProperties().get("playerId");
        List<Integer> parsedMsg = Arrays.stream(msg.split(" ")).map(Integer::parseInt)
                .collect(Collectors.toList());
        if (parsedMsg.size() != 2 || parsedMsg.get(0) == null) {
            try {
                session.getBasicRemote().sendText("false");
            } catch (IOException e) {
                throw new MyWSException();
            }
            return;
        }
        PlayerPokemon playerPokemon = playerPokemonDao.findById(parsedMsg.get(0));
        Integer bagIndex = parsedMsg.get(1);
        if (playerPokemon == null || playerPokemon.getPlayer().getId() != playerId ||
                (bagIndex != 0
                        && (playerPokemonDao.findByPlayerIdAndBagIndex(playerId, bagIndex) != null ||
                        bagIndex <= 0 || bagIndex > 6)) ||
                (bagIndex == 0
                        && playerPokemonDao.findByPlayerIdAndInBag(playerId).size() == 1)
        ) {
            try {
                session.getBasicRemote().sendText("false");
            } catch (IOException e) {
                throw new MyWSException();
            }
            return;
        }
        playerPokemon.setBagIndex(parsedMsg.get(1));
        playerPokemonDao.save(playerPokemon);
        try {
            session.getBasicRemote().sendText("true");
        } catch (IOException e) {
            throw new MyWSException();
        }
    }

    @Override
    public void chooseInitPokemon(Session session, String msg) {
        int playerId = (int) session.getUserProperties().get("playerId");
        Player player = playerService.getPlayer(playerId);
        int pokemonId = Integer.parseInt(msg);
        if (!initialPokemonIdSet.contains(pokemonId)) {
            try {
                session.getBasicRemote().sendText("choose_init_pokemon_fail");
            } catch (IOException e) {
                throw new MyWSException();
            }
            return;
        }
        PlayerPokemon playerPokemon;
        try {
            playerPokemon = playerPokemonService.createPokemon(pokemonId, player, 1);
        } catch (NullPointerException e) {
            return;
        }
        playerPokemon.setBagIndex(1);
        playerPokemon.setExp(1000);
        playerPokemonDao.save(playerPokemon);
        try {
            session.getBasicRemote().sendText("choose_init_pokemon_success");
        } catch (IOException e) {
            throw new MyWSException();
        }
    }

    @Override
    public void getPlayerPokemon(Session session, String msg) {
        int playerId = (int) session.getUserProperties().get("playerId");
        int playerPokemonId = Integer.parseInt(msg);
        PlayerPokemon playerPokemon = playerPokemonDao.findById(playerPokemonId);
        if (playerPokemon == null || playerPokemon.getPlayer().getId() != playerId) {
            sendBack(session, "player_pokemon", null);
            return;
        }
        sendBack(session, "player_pokemon", playerPokemon);
    }

    private Set<Integer> getAlreadyLearnedSkillId(PlayerPokemon playerPokemon) {
        return Arrays.stream(playerPokemon.getSkillList())
                .filter(Objects::nonNull).map(Skill::getId)
                .collect(Collectors.toUnmodifiableSet());
    }
    @Override
    public void getUnlearnSkill(Session session, String msg) {
        int playerId = (int) session.getUserProperties().get("playerId");
        int playerPokemonId = Integer.parseInt(msg);
        PlayerPokemon playerPokemon = playerPokemonDao.findById(playerPokemonId);
        if (playerPokemon == null || playerPokemon.getPlayer().getId() != playerId) {
            sendBack(session, "available_skill", "[]");
            return;
        }
        Set<Integer> alreadyLearnedSkillId = getAlreadyLearnedSkillId(playerPokemon);
        List<PokemonSkill> payload = pokemonSkillDao.listAvailSkill(
                playerPokemon.getPokemon().getId(), playerPokemon.getLevel()
        )
                .stream()
                .filter(pokemonSkill ->
                        !alreadyLearnedSkillId.contains(pokemonSkill.getSkill().getId()))
                .collect(Collectors.toList());
        sendBack(session, "available_skill", payload);
    }

    private void setSkill(PlayerPokemon playerPokemon, int index, Skill skill) {
        switch (index) {
            case 1:
                playerPokemon.setSkill1(skill);
                break;
            case 2:
                playerPokemon.setSkill2(skill);
                break;
            case 3:
                playerPokemon.setSkill3(skill);
                break;
            case 4:
                playerPokemon.setSkill4(skill);
                break;
        }
        playerPokemonDao.save(playerPokemon);
    }

    @Override
    public void learnSkill(Session session, String msg) {
        int playerId = (int) session.getUserProperties().get("playerId");
        int[] param = Arrays.stream(msg.split(" ", 3)).mapToInt(Integer::parseInt)
                .toArray();
        int playerPokemonId = param[0], index = param[1], pokemonSkillId = param[2];
        PlayerPokemon playerPokemon = playerPokemonDao.findById(playerPokemonId);
        PokemonSkill pokemonSkill = pokemonSkillDao.findById(pokemonSkillId);
        if (playerPokemon == null || playerPokemon.getPlayer().getId() != playerId
                || pokemonSkill == null
                || pokemonSkill.getPokemon().getId() != playerPokemon.getPokemon().getId()
                || pokemonSkill.getSkill().getLearnLevel() > playerPokemon.getLevel()
                || getAlreadyLearnedSkillId(playerPokemon).contains(pokemonSkill.getId())
        ) {
            sendBack(session, "learn_skill", false);
            return;
        }
        setSkill(playerPokemon, index, pokemonSkill.getSkill());
        sendBack(session, "learn_skill", true);
    }

    @Override
    public void swapSkill(Session session, String msg) {
        int playerId = (int) session.getUserProperties().get("playerId");
        int[] param = Arrays.stream(msg.split(" ", 3)).mapToInt(Integer::parseInt)
                .toArray();
        int playerPokemonId = param[0], index1 = param[1], index2 = param[2];
        PlayerPokemon playerPokemon = playerPokemonDao.findById(playerPokemonId);
        if (playerPokemon == null || playerPokemon.getPlayer().getId() != playerId) {
            sendBack(session, "swap_skill", false);
            return;
        }
        Skill[] skillList = playerPokemon.getSkillList();
        Skill skill1 = skillList[index1 - 1], skill2 = skillList[index2 - 1];
        setSkill(playerPokemon, index1, skill2);
        setSkill(playerPokemon, index2, skill1);
        playerPokemonDao.save(playerPokemon);
        sendBack(session, "swap_skill", true);
    }
}
