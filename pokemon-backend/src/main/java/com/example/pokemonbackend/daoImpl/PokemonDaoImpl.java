package com.example.pokemonbackend.daoImpl;

import com.example.pokemonbackend.dao.PokemonDao;
import com.example.pokemonbackend.entity.databaseEntity.Pokemon;
import com.example.pokemonbackend.repository.PokemonRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class PokemonDaoImpl implements PokemonDao {
    @Autowired
    private PokemonRepository pokemonRepository;

    @Override
    public Pokemon findById(int pokemonId) {
        return pokemonRepository.findById(pokemonId).orElse(null);
    }
}
