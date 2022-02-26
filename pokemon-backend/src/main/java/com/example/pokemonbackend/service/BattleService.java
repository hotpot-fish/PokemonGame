package com.example.pokemonbackend.service;

import com.example.pokemonbackend.entity.Battle;
import com.fasterxml.jackson.databind.JsonNode;

import java.util.List;

public interface BattleService {
    List<JsonNode>[] calcBattle(Battle battle);

    void AIMove(Battle battle);
}
