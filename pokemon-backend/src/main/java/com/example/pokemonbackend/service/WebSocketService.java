package com.example.pokemonbackend.service;

import javax.websocket.Session;

public interface WebSocketService extends WebSocketAbstractService {
    void auth(Session session, String authMsg);

    void pairing(Session session);

    void startPVE(Session session, String msg);

    void battle(Session session, String msg);

    void cancelPairing(Session session);

    void listBag(Session session);

    void listMyPokemon(Session session);

    void listMyPokemonNotInBag(Session session);

    void modifyBagIndex(Session session, String msg);

    void register(Session session, String msg);

    void setImage(Session session, String msg);

    void chooseInitPokemon(Session session, String msg);

    void getPlayerPokemon(Session session, String msg);

    void getUnlearnSkill(Session session, String msg);

    void learnSkill(Session session, String msg);

    void swapSkill(Session session, String msg);
}
