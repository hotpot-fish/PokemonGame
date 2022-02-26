package com.example.pokemonbackend.serviceImpl;

import com.example.pokemonbackend.config.JsonAnnotation;
import com.example.pokemonbackend.dao.PlayerDao;
import com.example.pokemonbackend.entity.databaseEntity.Player;
import com.example.pokemonbackend.service.WebSocketAuthService;
import com.example.pokemonbackend.util.MyWSException;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.dao.DataIntegrityViolationException;
import org.springframework.stereotype.Service;

import javax.websocket.CloseReason;
import javax.websocket.Session;
import java.io.IOException;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

@Service
public class WebSocketAuthServiceImpl implements WebSocketAuthService {
    static final Map<Integer, Session> playerSessionMap = new ConcurrentHashMap<>();
    @Autowired
    private PlayerDao playerDao;

    @Override
    public void auth(Session session, String authMsg) {
        try {
            JsonNode json = (new ObjectMapper()).readTree(authMsg);
            Player player = playerDao.findByAccountAndPassword(
                    json.get("account").asText(),
                    json.get("password").asText()
            );
            if (player != null) {
                int playerId = player.getId();
                String chooseMsg;
                if (player.getImage() == null) {
                    chooseMsg = "choose_image";
                } else if (player.getPokemonList().size() == 0) {
                    chooseMsg = "choose_pokemon";
                } else {
                    chooseMsg = "finish_choose";
                }
                session.getUserProperties().put("playerId", playerId);
                session.setMaxIdleTimeout(0);
                String msg = "login_success\n"
                        + JsonAnnotation.getPlayerMapper().writeValueAsString(player) + "\n"
                        + chooseMsg;
                session.getBasicRemote().sendText(msg);
                playerSessionMap.put(playerId, session);
                return;
            }
        } catch (NullPointerException ignored) {
        } catch (Exception e) {
            e.printStackTrace();
        }
        try {
            session.getBasicRemote().sendText("login_fail");
            session.close(new CloseReason(
                    CloseReason.CloseCodes.NO_STATUS_CODE, "login_fail")
            );
        } catch (IOException e) {
            throw new MyWSException();
        }
    }

    @Override
    public void register(Session session, String registerMsg) {
        JsonNode json;
        try {
            json = (new ObjectMapper()).readTree(registerMsg);
        } catch (JsonProcessingException e) {
            throw new MyWSException();
        }
        Player player = new Player(
                json.get("name").asText(), json.get("account").asText(),
                json.get("password").asText(), json.get("email").asText());
        try {
            playerDao.save(player);
        } catch (DataIntegrityViolationException e) {
            try {
                session.getBasicRemote().sendText("register_fail");
                session.close(new CloseReason(
                        CloseReason.CloseCodes.NO_STATUS_CODE, "register_fail")
                );
            } catch (IOException ioException) {
                throw new MyWSException();
            }
            return;
        }
        int playerId = player.getId();
        session.getUserProperties().put("playerId", playerId);
        session.setMaxIdleTimeout(0);
        try {
            session.getBasicRemote().sendText("register_success\n"
                    + JsonAnnotation.getPlayerMapper().writeValueAsString(player));
        } catch (IOException e) {
            throw new MyWSException();
        }
        playerSessionMap.put(playerId, session);
    }

    @Override
    public void onOpen(Session session) {
        try {
            session.getBasicRemote().sendText("连接成功，请先登录");
        } catch (IOException e) {
            throw new MyWSException();
        }
        session.setMaxIdleTimeout(10000);
    }

    @Override
    public void onClose(Session session) {
        Integer playerId = (Integer) session.getUserProperties().get("playerId");
        if (playerId != null) {
            playerSessionMap.remove(playerId);
        }
    }
}
