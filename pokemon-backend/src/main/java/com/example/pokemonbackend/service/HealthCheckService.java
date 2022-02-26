package com.example.pokemonbackend.service;

import java.util.Map;

public interface HealthCheckService {
    Map<String, Object> reportStatus();
}
