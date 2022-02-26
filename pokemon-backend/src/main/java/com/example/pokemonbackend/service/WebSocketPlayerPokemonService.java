package com.example.pokemonbackend.service;

import javax.websocket.Session;

public interface WebSocketPlayerPokemonService extends WebSocketAbstractService {
    void listBag(Session session);

    void listMyPokemon(Session session);

    void listMyPokemonNotInBag(Session session);

    void modifyBagIndex(Session session, String msg);

    void chooseInitPokemon(Session session, String msg);

    void getPlayerPokemon(Session session, String msg);

    void getUnlearnSkill(Session session, String msg);

    void learnSkill(Session session, String msg);

    void swapSkill(Session session, String msg);
}
