package com.example.pokemonbackend.service;

import javax.websocket.Session;

public interface WebSocketPairingService extends WebSocketAbstractService {
    void pairing(Session session);

    void cancelPairing(Session session);
}
