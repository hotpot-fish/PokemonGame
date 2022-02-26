package com.example.pokemonbackend.controller;

import com.example.pokemonbackend.service.WebSocketService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.PostConstruct;
import javax.websocket.*;
import javax.websocket.server.ServerEndpoint;
import java.io.IOException;

@ServerEndpoint("/ws")
@Component
public class WebSocketController {
    @Autowired
    private WebSocketService _webSocketService;

    private static WebSocketService webSocketService;

    @PostConstruct
    public void postConstruct() {
        webSocketService = _webSocketService;
    }

    @OnOpen
    public void onOpen(Session session) {
        webSocketService.onOpen(session);
    }

    @OnMessage
    public void onMessage(String msg, Session session) throws IOException {
        String[] msgLine = msg.split("\n", 2);
        if (msgLine[0].equals("auth")) {
            webSocketService.auth(session, msgLine[1]);
            return;
        } else if (msgLine[0].equals(("register"))) {
            webSocketService.register(session, msgLine[1]);
            return;
        } else {
            if (!session.getUserProperties().containsKey("playerId")) {
                session.close(new CloseReason(
                        CloseReason.CloseCodes.NO_STATUS_CODE, "请先登录")
                );
                return;
            }
        }
        switch (msgLine[0]) {
            case "start_pairing":
                webSocketService.pairing(session);
                break;
            case "cancel_pairing":
                webSocketService.cancelPairing(session);
                break;
            case "start_PVE":
                webSocketService.startPVE(session, msgLine[1]);
                break;
            case "battle":
                webSocketService.battle(session, msgLine[1]);
                break;
            case "list_bag":
                webSocketService.listBag(session);
                break;
            case "list_my_pokemon":
                webSocketService.listMyPokemon(session);
                break;
            case "list_my_pokemon_not_in_bag":
                webSocketService.listMyPokemonNotInBag(session);
                break;
            case "modify_bag_index":
                webSocketService.modifyBagIndex(session, msgLine[1]);
                break;
            case "choose_image":
                webSocketService.setImage(session, msgLine[1]);
                break;
            case "choose_init_pokemon":
                webSocketService.chooseInitPokemon(session, msgLine[1]);
                break;
            case "get_player_pokemon":
                webSocketService.getPlayerPokemon(session, msgLine[1]);
                break;
            case "get_unlearn_skill":
                webSocketService.getUnlearnSkill(session, msgLine[1]);
                break;
            case "learn_skill":
                webSocketService.learnSkill(session, msgLine[1]);
                break;
            case "swap_skill":
                webSocketService.swapSkill(session, msgLine[1]);
                break;
        }
    }

    @OnError
    public void onError(Session session, Throwable throwable) {
        try {
            session.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @OnClose
    public void onClose(Session session) {
        webSocketService.onClose(session);
    }
}
