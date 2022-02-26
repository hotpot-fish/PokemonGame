package com.example.pokemonbackend.repository;

import com.example.pokemonbackend.entity.databaseEntity.Player;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PlayerRepository extends JpaRepository<Player, Integer> {
    Player findByAccountAndPassword(String account, String password);

    Player findByAccount(String account);
}
