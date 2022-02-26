package com.example.pokemonbackend.serviceImpl;

import com.example.pokemonbackend.service.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.websocket.Session;

@Service
public class WebSocketServiceImpl implements WebSocketService {
    @Autowired
    private WebSocketAuthService authService;
    @Autowired
    private WebSocketBattleService battleService;
    @Autowired
    private WebSocketPairingService pairingService;
    @Autowired
    private WebSocketPlayerPokemonService playerPokemonService;
    @Autowired
    private WebSocketPlayerService playerService;

    @Override
    public void auth(Session session, String authMsg) {
        authService.auth(session, authMsg);
    }

    @Override
    public void pairing(Session session) {
        pairingService.pairing(session);
    }

    @Override
    public void startPVE(Session session, String msg) {
        battleService.startPVEBattle(session, msg);
    }

    @Override
    public void cancelPairing(Session session) {
        pairingService.cancelPairing(session);
    }

    @Override
    public void listBag(Session session) {
        playerPokemonService.listBag(session);
    }

    @Override
    public void listMyPokemon(Session session) {
        playerPokemonService.listMyPokemon(session);
    }

    @Override
    public void listMyPokemonNotInBag(Session session) {
        playerPokemonService.listMyPokemonNotInBag(session);
    }

    @Override
    public void modifyBagIndex(Session session, String msg) {
        playerPokemonService.modifyBagIndex(session, msg);
    }

    @Override
    public void battle(Session session, String msg) {
        battleService.battle(session, msg);
    }

    @Override
    public void onOpen(Session session) {
        authService.onOpen(session);
        battleService.onOpen(session);
        pairingService.onOpen(session);
        playerPokemonService.onOpen(session);
    }

    @Override
    public void onClose(Session session) {
        authService.onClose(session);
        battleService.onClose(session);
        pairingService.onClose(session);
        playerPokemonService.onClose(session);
    }

    @Override
    public void register(Session session, String msg){
        authService.register(session, msg);
    }

    @Override
    public void setImage(Session session, String msg) {
        playerService.setImage(session, msg);
    }

    @Override
    public void chooseInitPokemon(Session session, String msg) {
        playerPokemonService.chooseInitPokemon(session, msg);
    }

    @Override
    public void getPlayerPokemon(Session session, String msg) {
        playerPokemonService.getPlayerPokemon(session, msg);
    }

    @Override
    public void getUnlearnSkill(Session session, String msg) {
        playerPokemonService.getUnlearnSkill(session, msg);
    }

    @Override
    public void learnSkill(Session session, String msg) {
        playerPokemonService.learnSkill(session, msg);
    }

    @Override
    public void swapSkill(Session session, String msg) {
        playerPokemonService.swapSkill(session, msg);
    }
}
