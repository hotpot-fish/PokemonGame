package com.example.pokemonbackend.controller;

import com.example.pokemonbackend.service.HealthCheckService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

import javax.servlet.http.HttpServletRequest;
import java.util.Map;

@RestController
public class CheckHealthController {
    @Autowired
    private HealthCheckService healthCheckService;

    @GetMapping("/health")
    public Map<String, Object> reportHealth(HttpServletRequest request) {
        String host = request.getHeader("host");
        if (host != null && host.split(":")[0].equals("localhost")) {
            return healthCheckService.reportStatus();
        }
        return null;
    }
}
