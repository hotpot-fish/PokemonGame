package com.example.pokemonbackend.entity.databaseEntity;

import com.example.pokemonbackend.config.JsonAnnotation;
import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import javax.persistence.*;
import java.math.BigDecimal;
import java.util.List;

@Entity
@Table(name = "pokemon")
@JsonIgnoreProperties(value = {"handler", "hibernateLazyInitializer", "fieldHandler"})
public class Pokemon {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    private String name;
    private int image;
    private String element;

    @OneToMany(fetch = FetchType.LAZY, cascade = CascadeType.REMOVE)
    @JoinColumn(name = "pokemon_id")
    @JsonIgnore
    List<PokemonSkill> skillList;

    @Column(name = "base_attack")
    @JsonAnnotation.BattleIgnore
    private int baseAttack;

    @Column(name = "base_defence")
    @JsonAnnotation.BattleIgnore
    private int baseDefence;

    @Column(name = "base_HP")
    @JsonAnnotation.BattleIgnore
    private int baseHP;

    @Column(name = "base_speed")
    @JsonAnnotation.BattleIgnore
    private int baseSpeed;

    @Column(name = "grow_attack")
    @JsonAnnotation.BattleIgnore
    private BigDecimal growAttack;

    @Column(name = "grow_defence")
    @JsonAnnotation.BattleIgnore
    private BigDecimal growDefence;

    @Column(name = "grow_HP")
    @JsonAnnotation.BattleIgnore
    private BigDecimal growHP;

    @Column(name = "grow_speed")
    @JsonAnnotation.BattleIgnore
    private BigDecimal growSpeed;

    public List<PokemonSkill> getSkillList() {
        return skillList;
    }

    public void setSkillList(List<PokemonSkill> skillList) {
        this.skillList = skillList;
    }

    public String getElement() {
        return element;
    }

    public void setElement(String element) {
        this.element = element;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getImage() {
        return image;
    }

    public void setImage(int image) {
        this.image = image;
    }

    public int getBaseAttack() {
        return baseAttack;
    }

    public void setBaseAttack(int baseAttack) {
        this.baseAttack = baseAttack;
    }

    public int getBaseDefence() {
        return baseDefence;
    }

    public void setBaseDefence(int baseDefence) {
        this.baseDefence = baseDefence;
    }

    public int getBaseHP() {
        return baseHP;
    }

    public void setBaseHP(int baseHP) {
        this.baseHP = baseHP;
    }

    public int getBaseSpeed() {
        return baseSpeed;
    }

    public void setBaseSpeed(int baseSpeed) {
        this.baseSpeed = baseSpeed;
    }

    public BigDecimal getGrowAttack() {
        return growAttack;
    }

    public void setGrowAttack(BigDecimal growAttack) {
        this.growAttack = growAttack;
    }

    public BigDecimal getGrowDefence() {
        return growDefence;
    }

    public void setGrowDefence(BigDecimal growDefence) {
        this.growDefence = growDefence;
    }

    public BigDecimal getGrowHP() {
        return growHP;
    }

    public void setGrowHP(BigDecimal growHP) {
        this.growHP = growHP;
    }

    public BigDecimal getGrowSpeed() {
        return growSpeed;
    }

    public void setGrowSpeed(BigDecimal growSpeed) {
        this.growSpeed = growSpeed;
    }
}
