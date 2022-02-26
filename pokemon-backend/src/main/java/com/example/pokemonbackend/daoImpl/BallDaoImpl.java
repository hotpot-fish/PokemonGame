package com.example.pokemonbackend.daoImpl;

import com.example.pokemonbackend.dao.BallDao;
import com.example.pokemonbackend.entity.databaseEntity.Ball;
import com.example.pokemonbackend.repository.BallRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

@Repository
public class BallDaoImpl implements BallDao {
    @Autowired
    private BallRepository ballRepository;

    @Override
    public Ball findById(int ballId) {
        return ballRepository.findById(ballId).orElse(null);
    }
}
