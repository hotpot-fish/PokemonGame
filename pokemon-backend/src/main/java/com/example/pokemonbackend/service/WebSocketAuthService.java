package com.example.pokemonbackend.service;

import com.fasterxml.jackson.core.JsonProcessingException;

import javax.websocket.Session;

public interface WebSocketAuthService extends WebSocketAbstractService {
    void auth(Session session, String authMsg);

    void register(Session session, String registerMsg);
}
