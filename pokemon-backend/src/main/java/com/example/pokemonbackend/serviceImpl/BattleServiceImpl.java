package com.example.pokemonbackend.serviceImpl;

import com.example.pokemonbackend.config.JsonAnnotation;
import com.example.pokemonbackend.dao.PlayerBallDao;
import com.example.pokemonbackend.dao.PlayerDao;
import com.example.pokemonbackend.dao.PlayerPotionDao;
import com.example.pokemonbackend.entity.*;
import com.example.pokemonbackend.entity.databaseEntity.*;
import com.example.pokemonbackend.service.BattleService;
import com.example.pokemonbackend.service.ElementService;
import com.example.pokemonbackend.service.PlayerItemService;
import com.example.pokemonbackend.service.PlayerPokemonService;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.*;

@Service
public class BattleServiceImpl implements BattleService {
    @Autowired
    private PlayerPotionDao playerPotionDao;
    @Autowired
    private PlayerBallDao playerBallDao;
    @Autowired
    private PlayerPokemonService playerPokemonService;
    @Autowired
    private ElementService elementService;
    @Autowired
    private PlayerItemService playerItemService;
    @Autowired
    private PlayerDao playerDao;

    private void checkBattleEnd(Battle battle) {
        for (int i = 0; i < 2; ++i) {
            Team team = battle.getTeams()[i];
            if (Arrays.stream(team.getPokemon()).allMatch(pokemonInBattle ->
                    pokemonInBattle == null || pokemonInBattle.getCurAttribution().getHP() == 0)) {
                battle.winnerIndex = i ^ 1;
                return;
            }
        }
    }

    private void roundStart(Battle battle) {
        for (int i = 0; i < 2; ++i) {
            Team team = battle.getTeams()[i];
            team.clearNewStatus();
            String[] ops = team.getReceiveBuffer().split(" ");
            if (ops[0].equals("0")) {
                if (ops.length != 4) {
                    battle.winnerIndex = i ^ 1;
                    return;
                }
                changeTeamPokemon(battle, i, Integer.parseInt(ops[1]));
                team.setReceiveBuffer(ops[2] + " " + ops[3]);
            }
        }
        Arrays.stream(battle.getTeams()).map(Team::getCurrentPokemon).forEach(pokemon ->
                pokemon.getCurAttribution().setCanMove(true));
    }

    private void roundEnd(Battle battle) {
        if (battle.winnerIndex != null) {
            return;
        }
        Arrays.stream(battle.getTeams()).forEach(team -> {
            PokemonDigitalAttribution attr = team.getCurrentPokemon().getCurAttribution();
            if (attr.getDodgeRoundCnt() > 0) {
                attr.setDodgeRoundCnt(attr.getDodgeRoundCnt() - 1);
            }
            List<Buff> buffs = team.getCurrentPokemon().getBuffs();
            buffs.forEach(Buff::roundEnd);
            buffs.removeIf(Buff::isOutOfDate);
        });
    }

    private void changeTeamPokemon(Battle battle, int teamIndex, int index) {
        Team team = battle.getTeams()[teamIndex];
        if (team.getPokemon()[index].getCurAttribution().getHP() == 0) {
            battle.winnerIndex = teamIndex ^ 1;
            return;
        }
        team.clearNewStatus();
        team.getCurrentPokemon().getBuffs().clear();
        team.setCurrentPokemonIndex(index);
        team.setAnimation("2 " + team.getCurrentPokemon().getPokemon().getPokemon().getName());
    }

    private void usePotion(Battle battle, int teamIndex, int potionId) {
        Team team = battle.getTeams()[teamIndex];
        int playerId = team.getPlayer().getId();
        // refresh players to put they in Hibernate cache
        team.setPlayer(playerDao.findById(playerId));
        PlayerPotion playerPotion = playerItemService.findPotion(playerId, potionId);
        if (playerPotion == null) {
            battle.winnerIndex = teamIndex ^ 1;
            return;
        }
        Potion potion = playerPotion.getPotion();
        PokemonInBattle pokemon = team.getCurrentPokemon();
        switch (potion.getType()) {
            case "PP":
                Arrays.stream(pokemon.getSkills()).forEach(skillInBattle -> {
                            if (skillInBattle != null) {
                                skillInBattle.setCurPP(Math.min(
                                        skillInBattle.getSkill().getMaxPP(),
                                        skillInBattle.getCurPP() + potion.getData()));
                            }
                        });
                break;
            case "HP":
                pokemon.getCurAttribution().setHP(Math.min(
                        pokemon.getInitAttribution().getHP(),
                        pokemon.getCurAttribution().getHP() + potion.getData()
                ));
                team.setDeltaHP(potion.getData());
                break;
        }
        playerItemService.usePotion(playerPotion);
        team.setAnimation("3 " + potion.getName());
    }

    private void useBall(Battle battle, int teamIndex, int ballId) {
        if (battle.getTeams()[1].getPlayer() != null) { // in PVP battle
            battle.winnerIndex = teamIndex ^ 1;
            return;
        }
        if (Arrays.stream(battle.getTeams()[1].getPokemon()).filter(Objects::nonNull).count() != 1) {
            battle.winnerIndex = teamIndex ^ 1;
            return;
        }
        Team team = battle.getTeams()[teamIndex];
        int playerId = team.getPlayer().getId();
        // refresh players to put they in Hibernate cache
        team.setPlayer(playerDao.findById(playerId));
        PlayerBall playerBall = playerItemService.findBall(playerId, ballId);
        if (playerBall == null) {
            battle.winnerIndex = teamIndex ^ 1;
            return;
        }
        Ball ball = playerBall.getBall();
        if (playerBall.getNum() == 0) {
            battle.winnerIndex = teamIndex ^ 1;
            return;
        }
        playerItemService.useBall(playerBall);
        PokemonInBattle targetPokemon = battle.getTeams()[teamIndex ^ 1].getCurrentPokemon();
        double successRate = ball.getPossibility() *
                (1 - (double) targetPokemon.getCurAttribution().getHP()
                        / targetPokemon.getInitAttribution().getHP());
        if (Math.random() < successRate) {
            playerPokemonService.createPokemonAndSave(
                    targetPokemon.getPokemon().getPokemon(),
                    team.getPlayer(),
                    targetPokemon.getPokemon().getLevel()
            );
            battle.winnerIndex = teamIndex;
        }
        team.setAnimation("4 " + ball.getName() + " " + (battle.winnerIndex != null));
    }

    private void calcUseSkill(Battle battle, SkillInBattle skill, int teamIndex) {
        if (skill == null) {
            return;
        }
        if (battle.winnerIndex != null) {
            battle.getTeams()[0].clearNewStatus();
            battle.getTeams()[1].clearNewStatus();
            return;
        }
        Team selfTeam = battle.getTeams()[teamIndex], targetTeam = battle.getTeams()[teamIndex ^ 1];
        selfTeam.clearNewStatus();
        targetTeam.clearNewStatus();
        if (skill.getCurPP() <= 0) {
            battle.winnerIndex = teamIndex ^ 1;
            return;
        }
        PokemonInBattle pokemon = selfTeam.getCurrentPokemon();
        PokemonInBattle targetPokemon = targetTeam.getCurrentPokemon();
        if (pokemon.getCurAttribution().getHP() <= 0 || !pokemon.getCurAttribution().getCanMove()
                || targetPokemon.getCurAttribution().getHP() <= 0) {
            return;
        }

        skill.setCurPP(skill.getCurPP() - 1);
        boolean canDodge = targetPokemon.getCurAttribution().getDodgeRoundCnt() > 0;
        int damage;
        if (canDodge) {
            damage = 0;
        } else {
            damage = (int) (skill.computeDamage(
                                pokemon.getCurAttribution(),
                                targetPokemon.getCurAttribution()
                        )
                                * elementService.getElementRelation(
                                skill.getSkill().getElement(),
                                targetPokemon.getPokemon().getPokemon().getElement()
                        ));
        }
        selfTeam.setDamage(damage);

        PokemonDigitalAttribution targetAttr = targetPokemon.getCurAttribution();
        targetAttr.setHP(Math.max(targetAttr.getHP() - damage, 0));

        targetTeam.setDeltaHP(-damage);
        for (SkillBuff skillBuff : skill.getSkill().getBuffs()) {
            if (skillBuff.getPossibility() <= Math.random())
                continue;
            Buff buff = skillBuff.getBuff();
            if (buff.getTargetSelf()) {
                if (buff.addToBuff(pokemon.getBuffs())) {
                    selfTeam.getNewBuffs().add(buff);
                }
            } else {
                if (!canDodge && buff.addToBuff(targetPokemon.getBuffs())) {
                    targetTeam.getNewBuffs().add(buff);
                }
            }
        }
        selfTeam.setAnimation("5 " + skill.getSkill().getName() + " " + skill.getSkill().getAction());

        checkBattleEnd(battle);
    }

    private void calcBuff(Battle battle, String time, boolean shouldClearNewStatus) {
        if (battle.winnerIndex != null) {
            battle.getTeams()[0].clearNewStatus();
            battle.getTeams()[1].clearNewStatus();
            return;
        }
        int[] damage = {battle.getTeams()[0].getDamage(), battle.getTeams()[1].getDamage()};
        for (int i = 0; i < 2; ++i) {
            Team team = battle.getTeams()[i];
            if (shouldClearNewStatus)
                team.clearNewStatus();
            int selfDamage = damage[i], enemyDamage = damage[i ^ 1];
            PokemonInBattle pokemon = team.getCurrentPokemon();
            PokemonDigitalAttribution initAttr = pokemon.getInitAttribution(),
                    curAttr = pokemon.getCurAttribution();
            if (time.equals("立即")) {
                curAttr.copyBaseAttribution(initAttr);
            }
            int deltaHP = pokemon.getBuffs().stream().mapToInt(buff ->
                    buff.calc(time, initAttr, curAttr, selfDamage, enemyDamage)).sum();
            team.setDeltaHP(deltaHP);
            curAttr.setHP(Math.min(Math.max(curAttr.getHP() + deltaHP, 0), initAttr.getHP()));
        }

        checkBattleEnd(battle);
    }

    private void teamMove(Battle battle, int teamIndex, int[] ops) {
        if (ops[0] <= 1 || ops[0] > 6 || ops.length != 2) {
            battle.winnerIndex = teamIndex ^ 1;
            return;
        }
        switch (ops[0]) {
            case 2:
                changeTeamPokemon(battle, teamIndex, ops[1]);
                break;
            case 3:
                usePotion(battle, teamIndex, ops[1]);
                break;
            case 4:
                useBall(battle, teamIndex, ops[1]);
                break;
            case 5:
                SkillInBattle[] skillList = battle.getTeams()[teamIndex].getCurrentPokemon().getSkills();
                if (ops[1] < 0 || ops[1] >= skillList.length) {
                    calcUseSkill(battle, null, teamIndex);
                } else {
                    calcUseSkill(battle, skillList[ops[1]], teamIndex);
                }
                break;
        }
    }

    private List<JsonNode>[] initBattleMessage() {
        List<JsonNode>[] ret = new List[2];
        ret[0] = new ArrayList<>();
        ret[1] = new ArrayList<>();
        return ret;
    }

    private void dumpBattleMessage(List<JsonNode>[] json, Battle battle, ObjectMapper battleMapper) {
        json[0].add(battleMapper.valueToTree(battle.filterMsg(0)));
        json[1].add(battleMapper.valueToTree(battle.filterMsg(1)));
    }

    @Override
    public List<JsonNode>[] calcBattle(Battle battle) {
        ObjectMapper battleMapper = JsonAnnotation.getBattleMapper();
        List<JsonNode>[] jsonMsg = initBattleMessage();
        roundStart(battle);
        calcBuff(battle, "开始", false);
        dumpBattleMessage(jsonMsg, battle, battleMapper);
        boolean team0first = true;
        int[][] input = new int[2][];
        for (int i = 0; i < 2; ++i) {
            input[i] = Arrays.stream(battle.getTeams()[i].getReceiveBuffer().split(" "))
                    .mapToInt(Integer::parseInt).toArray();
            if (input[i].length != 2) {
                battle.winnerIndex = i ^ 1;
                return jsonMsg;
            }
        }
        if (input[0][0] != input[1][0]) {
            team0first = input[0][0] < input[1][0];
        } else if (input[0][0] == 5) {
            SkillInBattle[] skill = new SkillInBattle[2];
            for (int i = 0; i < 2; ++i) {
                Team team = battle.getTeams()[i];
                int index = input[i][1];
                SkillInBattle[] skillList = team.getCurrentPokemon().getSkills();
                if (index >= 0 && index < skillList.length) {
                    skill[i] = skillList[index];
                } else {
                    skill[i] = null;
                }
            }
            if (skill[0] != null && skill[1] != null) {
                int[] priority = {
                        skill[0].getSkill().getPriority(),
                        skill[1].getSkill().getPriority()
                };
                if (priority[0] != priority[1]) {
                    team0first = priority[0] > priority[1];
                } else {
                    team0first =
                            battle.getTeams()[0].getCurrentPokemon().getCurAttribution().getSpeed()
                                    > battle.getTeams()[1].getCurrentPokemon().getCurAttribution().getSpeed();
                }
            }
        }
        if (team0first) {
            teamMove(battle, 0, input[0]);
        } else {
            teamMove(battle, 1, input[1]);
        }
        dumpBattleMessage(jsonMsg, battle, battleMapper);
        calcBuff(battle, "立即", true);
        dumpBattleMessage(jsonMsg, battle, battleMapper);
        if (team0first) {
            teamMove(battle, 1, input[1]);
        } else {
            teamMove(battle, 0, input[0]);
        }
        dumpBattleMessage(jsonMsg, battle, battleMapper);
        calcBuff(battle, "立即", true);
        dumpBattleMessage(jsonMsg, battle, battleMapper);
        calcBuff(battle, "结束", true);
        roundEnd(battle);
        dumpBattleMessage(jsonMsg, battle, battleMapper);
        return jsonMsg;
    }

    public void AIMove(Battle battle) {
        Team team = battle.getTeams()[1];
        PokemonInBattle pokemon = team.getCurrentPokemon();
        if (pokemon.getCurAttribution().getHP() == 0) {
            List<Integer> availPokemonIndex = new ArrayList<>();
            PokemonInBattle[] allPokemon = team.getPokemon();
            for (int i = 0; i < allPokemon.length; ++i) {
                PokemonInBattle _pokemon = allPokemon[i];
                if (_pokemon != null && _pokemon.getCurAttribution().getHP() > 0) {
                    availPokemonIndex.add(i);
                }
            }
            int chooseIndex = availPokemonIndex.get((int) (Math.random() * availPokemonIndex.size()));
            team.setReceiveBuffer("0 " + chooseIndex);
            pokemon = allPokemon[chooseIndex];
        }
        List<Integer> skillIndex = new ArrayList<>();
        SkillInBattle[] skills = pokemon.getSkills();
        for (int i = 0; i < skills.length; ++i) {
            SkillInBattle skill = skills[i];
            if (skill != null && skill.getCurPP() > 0) {
                skillIndex.add(i);
            }
        }
        int useSkill;
        if (skillIndex.isEmpty()) {
            useSkill = 4;
        } else {
            useSkill = skillIndex.get((int) (Math.random() * skillIndex.size()));
        }
        String prevBuffer = team.getReceiveBuffer();
        if (prevBuffer == null) {
            team.setReceiveBuffer("5 " + useSkill);
        } else {
            team.setReceiveBuffer(prevBuffer + " 5 " + useSkill);
        }
    }
}
