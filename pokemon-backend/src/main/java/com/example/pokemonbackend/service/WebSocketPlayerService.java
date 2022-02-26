package com.example.pokemonbackend.service;

import javax.websocket.Session;

public interface WebSocketPlayerService {
    void setImage(Session session, String msg);
}
