package com.example.pokemonbackend.serviceImpl;

import com.example.pokemonbackend.dao.PlayerDao;
import com.example.pokemonbackend.entity.databaseEntity.Player;
import com.example.pokemonbackend.service.PlayerService;
import com.example.pokemonbackend.service.WebSocketBattleService;
import com.example.pokemonbackend.service.WebSocketPairingService;
import com.example.pokemonbackend.util.MyWSException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.websocket.Session;
import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

@Service
public class WebSocketPairingServiceImpl implements WebSocketPairingService {
    @Autowired
    private WebSocketBattleService battleService;
    @Autowired
    private PlayerDao playerDao;
    @Autowired
    private PlayerService playerService;

    static final Map<String, Session> pairing = new HashMap<>();

    private static boolean canPair(Player p1, Player p2) {
        return Math.abs(p1.getRank() - p2.getRank()) <= 1000;
    }

    private boolean removePairing(Session session) {
        Session s = pairing.remove(session.getId());
        session.getUserProperties().remove("player");
        return s != null;
    }

    private void startPVPBattle(Session s1, Session s2) {
        battleService.startPVPBattle(s1, s2);
        removePairing(s1);
        removePairing(s2);
    }

    @Override
    public void pairing(Session session) {
        int newPlayerId = (int) session.getUserProperties().get("playerId");
        if (playerService.getPokemonInBag(newPlayerId).size() == 0) {
            return;
        }
        Player newPlayer = playerDao.findById(newPlayerId);
        session.getUserProperties().put("player", newPlayer);
        synchronized (this) {
            for (Map.Entry<String, Session> entry : pairing.entrySet()) {
                if (canPair(newPlayer, (Player) entry.getValue().getUserProperties().get("player"))) {
                    startPVPBattle(entry.getValue(), session);
                    return;
                }
            }
            pairing.put(session.getId(), session);
        }
    }

    @Override
    public void cancelPairing(Session session) {
        boolean cancelResult;
        synchronized (this) {
            cancelResult = removePairing(session);
        }
        if (cancelResult) {
            try {
                session.getBasicRemote().sendText("cancel_success");
            } catch (IOException e) {
                throw new MyWSException();
            }
        }
    }


    @Override
    public void onOpen(Session session) {
    }

    @Override
    public void onClose(Session session) {
        cancelPairing(session);
    }
}
