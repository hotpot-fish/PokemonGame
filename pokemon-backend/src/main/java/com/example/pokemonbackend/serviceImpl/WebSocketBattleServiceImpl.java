package com.example.pokemonbackend.serviceImpl;

import com.example.pokemonbackend.config.JsonAnnotation;
import com.example.pokemonbackend.dao.PlayerPokemonDao;
import com.example.pokemonbackend.entity.*;
import com.example.pokemonbackend.entity.databaseEntity.*;
import com.example.pokemonbackend.service.*;
import com.example.pokemonbackend.util.MyWSException;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.websocket.Session;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

@Service
public class WebSocketBattleServiceImpl implements WebSocketBattleService {
    @Autowired
    private PlayerService playerService;
    @Autowired
    private BattleService battleService;
    @Autowired
    private PlayerPokemonService playerPokemonService;
    @Autowired
    private PlayerPokemonDao playerPokemonDao;
    @Autowired
    private PlayerItemService playerItemService;

    static final Map<Integer, Battle> battleMap = new ConcurrentHashMap<>();

    private Runnable timeoutFunction(Battle battle) {
        int targetRound = ++battle.roundCnt;
        return () -> {
            synchronized (battle) {
                if (battle.roundCnt != targetRound) {
                    return;
                }
                if (battle.getTeams()[0].getReceiveBuffer() == null) {
                    endBattle(battle, 1);
                } else {
                    endBattle(battle, 0);
                }
            }
        };
    }

    private void addBattle(Battle battle) {
        Team[] teams = battle.getTeams();
        battleMap.put(teams[0].getPlayer().getId(), battle);
        //判断是否是人机玩家
        Player player2 = teams[1].getPlayer();
        if (player2 != null) {
            battleMap.put(player2.getId(), battle);
        }
        battle.startTimeout(timeoutFunction(battle));
    }

    private void deleteBattle(Battle battle) {
        battle.cancelTimeout();
        Team[] teams = battle.getTeams();
        Player player1 = teams[0].getPlayer(), player2 = teams[1].getPlayer();
        battleMap.remove(player1.getId());
        //判断是否是人机玩家
        if (player2 != null) {
            battleMap.remove(player2.getId());
            PVPFinalCalc(battle);
        } else {
            PVEFinalCalc(battle);
        }
    }

    private void sendPVPCalc(Player p, int newRank) {
        Session session = WebSocketAuthServiceImpl.playerSessionMap.get(p.getId());
        if (session != null && session.isOpen()) {
            String msg = "PVP_calc\n" + newRank;
            try {
                session.getBasicRemote().sendText(msg);
            } catch (IOException e) {
                throw new MyWSException();
            }
        }
    }

    private void calcRank(Player winner, Player loser) {
        int rankDiff = winner.getRank() - loser.getRank();
        int deltaRank = Math.max(50, (int) (100 * Math.exp(-rankDiff / 1000.0)));
        winner.setRank(winner.getRank() + deltaRank);
        loser.setRank(Math.max(0, loser.getRank() - deltaRank));
        playerService.save(winner);
        playerService.save(loser);
    }

    private void PVPFinalCalc(Battle battle) {
        Player p1 = battle.getTeams()[0].getPlayer(), p2 = battle.getTeams()[1].getPlayer();
        if (battle.winnerIndex == 0) {
            calcRank(p1, p2);
        } else {
            calcRank(p2, p1);
        }
        sendPVPCalc(p1, p1.getRank());
        sendPVPCalc(p2, p2.getRank());
    }

    private void sendPVECalc(Player p, List<String> acquiredItems, int incExp) {
        ObjectMapper mapper = new ObjectMapper();
        Session session = WebSocketAuthServiceImpl.playerSessionMap.get(p.getId());
        if (session != null && session.isOpen()) {
            String msg;
            try {
                msg = "PVE_calc\n" + mapper.writeValueAsString(acquiredItems) + "\n"
                        + mapper.writeValueAsString(incExp);
            } catch (JsonProcessingException e) {
                e.printStackTrace();
                return;
            }
            try {
                session.getBasicRemote().sendText(msg);
            } catch (IOException e) {
                throw new MyWSException();
            }
        }
    }

    private int calcExp(Battle battle) {
        PokemonInBattle[] myPokemon = battle.getTeams()[0].getPokemon();
        int incExp = 200 *
                Arrays.stream(battle.getTeams()[1].getPokemon()).mapToInt(pokemon -> {
                    if (pokemon == null) {
                        return 0;
                    }
                    return pokemon.getPokemon().getLevel();
                }).sum();
        for (PokemonInBattle pokemon : myPokemon) {
            if (pokemon == null) {
                continue;
            }
            PlayerPokemon playerPokemon = pokemon.getPokemon();
            playerPokemon.setCurExp(playerPokemon.getCurExp() + incExp);
            int level = playerPokemon.getLevel();
            while (level != 100 && playerPokemon.getCurExp() >= playerPokemon.getExp()) {
                playerPokemon.setCurExp(playerPokemon.getCurExp() - playerPokemon.getExp());
                playerPokemon.setExp(playerPokemon.getExp() + 1000);
                level++;
            }
            playerPokemonService.updateAttribution(playerPokemon, level);
            playerPokemonDao.save(playerPokemon);
        }
        return incExp;
    }

    private void randomAcquirePotion(
            int playerId, double possibility, int potionId,
            List<String> acquiredItems
    ) {
        if (Math.random() < possibility) {
            PlayerPotion playerPotion = playerItemService.acquirePotion(playerId, potionId);
            acquiredItems.add(playerPotion.getPotion().getName());
        }
    }

    private void randomAcquireBall(
            int playerId, double possibility, int ballId,
            List<String> acquiredItems
    ) {
        if (Math.random() < possibility) {
            PlayerBall playerBall = playerItemService.acquireBall(playerId, ballId);
            acquiredItems.add(playerBall.getBall().getName());
        }
    }

    private List<String> calcItem(Battle battle) {
        List<String> acquiredItems = new ArrayList<>();

        int playerId = battle.getTeams()[0].getPlayer().getId();
        // TODO: avoid hard coded
        randomAcquirePotion(playerId, 0.1, 1, acquiredItems);
        randomAcquirePotion(playerId, 0.1, 2, acquiredItems);
        randomAcquirePotion(playerId, 0.1, 3, acquiredItems);
        randomAcquirePotion(playerId, 0.1, 4, acquiredItems);
        randomAcquirePotion(playerId, 0.1, 5, acquiredItems);
        randomAcquirePotion(playerId, 0.1, 6, acquiredItems);

        randomAcquireBall(playerId, 0.1, 1, acquiredItems);
        randomAcquireBall(playerId, 0.1, 2, acquiredItems);
        randomAcquireBall(playerId, 0.1, 3, acquiredItems);
        randomAcquireBall(playerId, 0.1, 4, acquiredItems);

        return acquiredItems;
    }

    private void PVEFinalCalc(Battle battle) {
        Player player = battle.getTeams()[0].getPlayer();
        if (battle.winnerIndex != 0) {
            sendPVECalc(player, new ArrayList<>(), 0);
        } else {
            List<String> acquiredItems = calcItem(battle);
            int incExp = calcExp(battle);
            sendPVECalc(player, acquiredItems, incExp);
        }
    }

    private void endBattle(Battle battle, int winnerIndex) {
        battle.winnerIndex = winnerIndex;
        String msg = "battle_end\n[]\n" + winnerIndex;
        Arrays.stream(battle.getTeams()).map(Team::getPlayer).forEach(player -> {
            if (player == null) {
                return;
            }
            Session session = WebSocketAuthServiceImpl.playerSessionMap.get(player.getId());
            if (session != null && session.isOpen()) {
                try {
                    session.getBasicRemote().sendText(msg);
                } catch (IOException e) {
                    throw new MyWSException();
                }
            }
        });
        deleteBattle(battle);
    }

    private Battle startPVPBattle(int player1Id, int player2Id) {
        Team[] teams = {getTeam(player1Id), getTeam(player2Id)};

        Battle battle = new Battle(teams);

        addBattle(battle);

        return battle;
    }

    private void sendMsg(Battle battle, String msg) {
        Arrays.stream(battle.getTeams()).map(Team::getPlayer).forEach(player -> {
            if (player != null) {
                Session session = WebSocketAuthServiceImpl.playerSessionMap.get(player.getId());
                try {
                    session.getBasicRemote().sendText(msg);
                } catch (IOException e) {
                    throw new MyWSException();
                }
            }
        });
    }

    private void sendStartMsg(Battle battle) {
        String jsonStr = null;
        ObjectMapper battleMapper = JsonAnnotation.getBattleMapper();
        try {
            jsonStr = battleMapper.writeValueAsString(battle);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }
        String msg = "start_battle\n" + jsonStr;
        sendMsg(battle, msg);
    }

    @Override
    public void startPVPBattle(Session player1Session, Session player2Session) {
        Battle battle = startPVPBattle(
                (int) player1Session.getUserProperties().get("playerId"),
                (int) player2Session.getUserProperties().get("playerId")
        );
        sendStartMsg(battle);
    }

    private PokemonInBattle buildPokemon(int pokemonId, int level) {
        PlayerPokemon playerPokemon =
                playerPokemonService.createPokemon(pokemonId, null, level);
        PokemonInBattle ret = new PokemonInBattle();
        ret.setPokemon(playerPokemon);
        PokemonDigitalAttribution initAttr = new PokemonDigitalAttribution(
                playerPokemon.getCurAttack(),
                playerPokemon.getCurDefence(),
                playerPokemon.getCurHP(),
                playerPokemon.getCurSpeed()
        );
        ret.setInitAttribution(initAttr);
        ret.setCurAttribution(initAttr.clone());
        SkillInBattle[] skillInBattles = new SkillInBattle[4];
        addSkill(playerPokemon.getSkill1(), 0, skillInBattles);
        addSkill(playerPokemon.getSkill2(), 1, skillInBattles);
        addSkill(playerPokemon.getSkill3(), 2, skillInBattles);
        addSkill(playerPokemon.getSkill4(), 3, skillInBattles);
        ret.setSkills(skillInBattles);
        return ret;
    }

    private Battle startPVEBattle(int playerId, int[][] envPokemonInfo) throws Exception {
        if (envPokemonInfo.length != 6) {
            throw new Exception();
        }
        PokemonInBattle[] envPokemon = new PokemonInBattle[6];
        for (int i = 0; i < 6; ++i) {
            int[] infoEntry = envPokemonInfo[i];
            if (infoEntry == null) {
                continue;
            }
            if (infoEntry.length != 2) {
                throw new Exception();
            }
            envPokemon[i] = buildPokemon(infoEntry[0], infoEntry[1]);
        }
        Team envTeam = new Team();
        envTeam.setPlayer(null);
        envTeam.setPokemon(envPokemon);
        envTeam.setCurrentPokemonIndex(0);
        Team[] teams = {getTeam(playerId), envTeam};
        if (teams[0].getPokemon()[0] == null) {
            throw new Exception();
        }
        Battle battle = new Battle(teams);

        addBattle(battle);

        return battle;
    }

    @Override
    public void startPVEBattle(Session session, String msg) {
        ObjectMapper mapper = new ObjectMapper();
        try {
            int[][] envPokemonInfo = mapper.readValue(msg, int[][].class);
            Battle battle = startPVEBattle(
                    (int) session.getUserProperties().get("playerId"),
                    envPokemonInfo
            );
            sendStartMsg(battle);
        } catch (JsonProcessingException e) {
            throw new MyWSException();
        } catch (Exception ignored) {
        }
    }

    @Override
    public void battle(Session session, String msg) {
        int playerId = (int) session.getUserProperties().get("playerId");
        Battle battle = battleMap.get(playerId);
        if (battle == null) {
            return;
        }
        //noinspection SynchronizationOnLocalVariableOrMethodParameter
        synchronized (battle) {
            if (battle.winnerIndex != null) {
                return;
            }
            if (battle.getTeams()[0].getPlayer().getId() == playerId) {
                if (msg.equals("1 0") || battle.getTeams()[0].getReceiveBuffer() != null) {
                    endBattle(battle, 1);
                    return;
                }
                battle.getTeams()[0].setReceiveBuffer(msg);
            } else {
                if (msg.equals("1 0") || battle.getTeams()[1].getReceiveBuffer() != null) {
                    endBattle(battle, 0);
                    return;
                }
                battle.getTeams()[1].setReceiveBuffer(msg);
            }
            if (battle.getTeams()[1].getPlayer() == null) {
                battleService.AIMove(battle);
                ++battle.receiveCnt;
            }
            if (++battle.receiveCnt == 2) {
                if (!battle.cancelTimeout()) {
                    return;
                }
                if (Arrays.stream(battle.getTeams()).allMatch(team -> team.getReceiveBuffer().equals("5 4"))) {
                    endBattle(battle, 1);
                    return;
                }
                List<JsonNode>[] battleMsg = battleService.calcBattle(battle);
                battle.receiveCnt = 0;
                if (battle.winnerIndex == null) {
                    battle.startTimeout(timeoutFunction(battle));
                }
                sendCalcBattle(battle, battleMsg);
            }
        }
    }

    private void sendCalcBattle(Battle battle, List<JsonNode>[] jsonMsg) {
        for (int i = 0; i < 2; ++i) {
            Team team = battle.getTeams()[i];
            team.setReceiveBuffer(null);
            String msg;
            Player player = team.getPlayer();
            if (player == null) {
                continue;
            }
            Integer playerId = player.getId();
            Session session = WebSocketAuthServiceImpl.playerSessionMap.get(playerId);
            try {
                if (battle.winnerIndex == null) {
                    msg = "in_battle\n" + (new ObjectMapper()).writeValueAsString(jsonMsg[i]);

                } else {
                    msg = "battle_end\n" + (new ObjectMapper()).writeValueAsString(jsonMsg[i]) + "\n"
                            + battle.winnerIndex;
                }
            } catch (JsonProcessingException e) {
                e.printStackTrace();
                continue;
            }
            try {
                session.getBasicRemote().sendText(msg);
            } catch (IOException e) {
                throw new MyWSException();
            }
        }
        if (battle.winnerIndex != null) {
            deleteBattle(battle);
        }
    }

    private Team getTeam(int playerId) {
        Player player = playerService.getPlayer(playerId);
        Team team = new Team();
        team.setPlayer(player);
        team.setCurrentPokemonIndex(0);
        PokemonInBattle[] player1Pokemon = new PokemonInBattle[6];
        int pos = 0;
        List<PlayerPokemon> player1PokemonList = playerService.getPokemonInBag(playerId);
        for (PlayerPokemon pokemon : player1PokemonList) {
            PokemonInBattle pokemonInBattle = new PokemonInBattle();
            pokemonInBattle.setPokemon(pokemon);
            PokemonDigitalAttribution attribution = new PokemonDigitalAttribution(
                    pokemon.getCurAttack(),
                    pokemon.getCurDefence(),
                    pokemon.getCurHP(),
                    pokemon.getCurSpeed()
            );
            pokemonInBattle.setInitAttribution(attribution.clone());
            pokemonInBattle.setCurAttribution(attribution);
            SkillInBattle[] skills = new SkillInBattle[4];
            addSkill(pokemon.getSkill1(), 0, skills);
            addSkill(pokemon.getSkill2(), 1, skills);
            addSkill(pokemon.getSkill3(), 2, skills);
            addSkill(pokemon.getSkill4(), 3, skills);
            pokemonInBattle.setSkills(skills);
            player1Pokemon[pos] = pokemonInBattle;
            pos++;
        }
        team.setPokemon(player1Pokemon);
        return team;
    }

    private void addSkill(Skill skill, int pos, SkillInBattle[] skills) {
        if (skill != null) {
            SkillInBattle skillInBattle = new SkillInBattle();
            skillInBattle.setSkill(skill);
            skillInBattle.setCurPP(skill.getMaxPP());
            skills[pos] = skillInBattle;
        }
    }

    @Override
    public void onOpen(Session session) {
    }

    @Override
    public void onClose(Session session) {
        Integer playerId = (Integer) session.getUserProperties().get("playerId");
        if (playerId == null) {
            return;
        }
        Battle battle = battleMap.get(playerId);
        if (battle == null) {
            return;
        }
        if (battle.getTeams()[0].getPlayer().getId() == playerId) {
            endBattle(battle, 1);
        } else {
            endBattle(battle, 0);
        }
    }
}