package com.example.pokemonbackend.entity;

import com.example.pokemonbackend.util.MyTimer;
import com.fasterxml.jackson.annotation.JsonIgnore;

public class Battle {
    private static final long timeoutMillis = 18000;
    @JsonIgnore
    private final MyTimer timer = new MyTimer();
    @JsonIgnore
    public int receiveCnt = 0;
    @JsonIgnore
    public int roundCnt = 0;

    private final Team[] teams;

    @JsonIgnore
    public Integer winnerIndex = null;

    public Battle(Team[] teams) {
        this.teams = teams;
    }

    public Team[] getTeams() {
        return teams;
    }

    public boolean check() {
        return teams.length == 2 &&
                teams[0] != null && teams[1] != null &&
                teams[0].getPlayer() != null;
    }

    public Battle filterMsg(int selfTeamIndex) {
        Battle ret = new Battle(new Team[2]);
        ret.teams[selfTeamIndex] = teams[selfTeamIndex].filterSelfTeam();
        int enemyTeamIndex = selfTeamIndex ^ 1;
        ret.teams[enemyTeamIndex] = teams[enemyTeamIndex].filterEnemyTeam();
        return ret;
    }

    public void startTimeout(Runnable runnable) {
        timer.startTimeout(runnable, timeoutMillis);
    }

    public boolean cancelTimeout() {
        return timer.cancelTimeout();
    }
}
