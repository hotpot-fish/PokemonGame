package com.example.pokemonbackend.DaoUnitTest;

import com.example.pokemonbackend.dao.PokemonSkillDao;
import com.example.pokemonbackend.entity.databaseEntity.PokemonSkill;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.jdbc.AutoConfigureTestDatabase;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.stereotype.Repository;

import javax.transaction.Transactional;
import java.util.List;

@DataJpaTest(includeFilters = @ComponentScan.Filter(classes = Repository.class))
@AutoConfigureTestDatabase(replace = AutoConfigureTestDatabase.Replace.NONE)
public class PokemonSkillDaoUnitTest {
    @Autowired
    private PokemonSkillDao pokemonSkillDao;

    @Test
    @Transactional
    public void TestFind() {
        List<PokemonSkill> pokemonSkills = pokemonSkillDao.listAvailSkill(1, 100);
        for(PokemonSkill pokemonSkill : pokemonSkills) {
            Assertions.assertEquals(pokemonSkill, pokemonSkillDao.findById(pokemonSkill.getId()));
        }
    }
}
