package com.example.pokemonbackend.serviceImpl;

import com.example.pokemonbackend.dao.PlayerDao;
import com.example.pokemonbackend.dao.PlayerPokemonDao;
import com.example.pokemonbackend.entity.databaseEntity.Player;
import com.example.pokemonbackend.entity.databaseEntity.PlayerPokemon;
import com.example.pokemonbackend.service.PlayerService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Comparator;
import java.util.List;

@Service
public class PlayerServiceImpl implements PlayerService {
    @Autowired
    private PlayerDao playerDao;
    @Autowired
    private PlayerPokemonDao playerPokemonDao;

    @Override
    public List<PlayerPokemon> getPokemonInBag(int playerId) {
        List<PlayerPokemon> ret = playerPokemonDao.findByPlayerIdAndInBag(playerId);
        ret.sort(Comparator.comparingInt(PlayerPokemon::getBagIndex));
        return ret;
    }

    @Override
    public List<PlayerPokemon> getPokemonNotInBag(int playerId) {
        List<PlayerPokemon> ret = playerPokemonDao.findByPlayerIdAndNotInBag(playerId);
        ret.sort(Comparator.comparingInt(PlayerPokemon::getId));
        return ret;
    }

    @Override
    public Player getPlayer(int id) {
        return playerDao.findById(id);
    }

    @Override
    public void save(Player player) {
        playerDao.save(player);
    }

    @Override
    public void setImage(Player player, String image) {
        player.setImage(image);
        playerDao.save(player);
    }
}
