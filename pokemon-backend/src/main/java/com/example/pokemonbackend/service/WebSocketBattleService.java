package com.example.pokemonbackend.service;

import javax.websocket.Session;

public interface WebSocketBattleService extends WebSocketAbstractService {
    void startPVPBattle(Session player1Session, Session player2Session);

    void startPVEBattle(Session session, String msg);

    void battle(Session session, String msg);
}
