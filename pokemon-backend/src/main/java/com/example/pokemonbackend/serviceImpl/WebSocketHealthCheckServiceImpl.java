package com.example.pokemonbackend.serviceImpl;

import com.example.pokemonbackend.service.HealthCheckService;
import org.springframework.stereotype.Service;

import java.util.HashMap;
import java.util.Map;

@Service
public class WebSocketHealthCheckServiceImpl implements HealthCheckService {
    @Override
    public Map<String, Object> reportStatus() {
        Map<String, Object> ret = new HashMap<>();
        ret.put("auth_count", WebSocketAuthServiceImpl.playerSessionMap.size());

        ret.put("pairing_count", WebSocketPairingServiceImpl.pairing.size());

        ret.put("battle_count", WebSocketBattleServiceImpl.battleMap.size());

        return ret;
    }
}
