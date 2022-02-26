package com.example.pokemonbackend.serviceImpl;

import com.example.pokemonbackend.dao.PlayerPokemonDao;
import com.example.pokemonbackend.dao.PokemonDao;
import com.example.pokemonbackend.entity.databaseEntity.Player;
import com.example.pokemonbackend.entity.databaseEntity.PlayerPokemon;
import com.example.pokemonbackend.entity.databaseEntity.Pokemon;
import com.example.pokemonbackend.entity.databaseEntity.PokemonSkill;
import com.example.pokemonbackend.service.PlayerPokemonService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.math.BigDecimal;
import java.util.Comparator;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class PlayerPokemonServiceImpl implements PlayerPokemonService {
    @Autowired
    private PokemonDao pokemonDao;
    @Autowired
    private PlayerPokemonDao playerPokemonDao;

    @Override
    public PlayerPokemon createPokemon(int pokemonId, Player player, int level) {
        Pokemon pokemon = pokemonDao.findById(pokemonId);
        return createPokemon(pokemon, player, level);
    }

    @Override
    public void updateAttribution(PlayerPokemon playerPokemon, int level) {
        playerPokemon.setLevel(level);
        Pokemon pokemon = playerPokemon.getPokemon();
        playerPokemon.setCurAttack(pokemon.getBaseAttack()
                + pokemon.getGrowAttack().multiply(new BigDecimal(level)).intValue());
        playerPokemon.setCurDefence(pokemon.getBaseDefence()
                + pokemon.getGrowDefence().multiply(new BigDecimal(level)).intValue());
        playerPokemon.setCurHP(pokemon.getBaseHP()
                + pokemon.getGrowHP().multiply(new BigDecimal(level)).intValue());
        playerPokemon.setCurSpeed(pokemon.getBaseSpeed()
                + pokemon.getGrowSpeed().multiply(new BigDecimal(level)).intValue());
    }

    private PlayerPokemon createPokemon(Pokemon pokemon, Player player, int level) {
        PlayerPokemon ret = new PlayerPokemon();
        if (pokemon == null) {
            throw new NullPointerException();
        }
        ret.setPokemon(pokemon);
        ret.setPlayer(player);
        updateAttribution(ret, level);
        List<PokemonSkill> skills = pokemon.getSkillList().stream()
                .filter(skill -> level >= skill.getSkill().getLearnLevel())
                .sorted(Comparator.comparingInt(skill -> ((PokemonSkill) skill).getSkill().getLearnLevel()).reversed())
                .collect(Collectors.toList());
        if (skills.size() >= 1) {
            ret.setSkill1(skills.get(0).getSkill());
        }
        if (skills.size() >= 2) {
            ret.setSkill2(skills.get(1).getSkill());
        }
        if (skills.size() >= 3) {
            ret.setSkill3(skills.get(2).getSkill());
        }
        if (skills.size() >= 4) {
            ret.setSkill4(skills.get(3).getSkill());
        }
        return ret;
    }

    @Override
    public void createPokemonAndSave(Pokemon pokemon, Player player, int level) {
        PlayerPokemon ret = createPokemon(pokemon, player, level);
        playerPokemonDao.save(ret);
    }
}
