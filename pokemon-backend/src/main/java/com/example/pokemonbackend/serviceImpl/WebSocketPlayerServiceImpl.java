package com.example.pokemonbackend.serviceImpl;

import com.example.pokemonbackend.entity.databaseEntity.Player;
import com.example.pokemonbackend.service.PlayerService;
import com.example.pokemonbackend.service.WebSocketPlayerService;
import com.example.pokemonbackend.util.MyWSException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.websocket.Session;
import java.io.IOException;

@Service
public class WebSocketPlayerServiceImpl implements WebSocketPlayerService {
    @Autowired
    private PlayerService playerService;

    @Override
    public void setImage(Session session, String msg){
        int playerId = (int) session.getUserProperties().get("playerId");
        Player player = playerService.getPlayer(playerId);
        playerService.setImage(player, msg);
        try {
            session.getBasicRemote().sendText("set_image_success");
        } catch (IOException e) {
            throw new MyWSException();
        }
    }
}
