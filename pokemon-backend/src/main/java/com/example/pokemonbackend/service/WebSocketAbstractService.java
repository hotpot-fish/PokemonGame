package com.example.pokemonbackend.service;

import javax.websocket.Session;

public interface WebSocketAbstractService {
    void onOpen(Session session);

    void onClose(Session session);
}
