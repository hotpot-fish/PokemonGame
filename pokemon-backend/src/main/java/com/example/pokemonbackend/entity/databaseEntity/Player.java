package com.example.pokemonbackend.entity.databaseEntity;

import com.example.pokemonbackend.config.JsonAnnotation;
import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name = "player")
@JsonIgnoreProperties(value = {"handler", "hibernateLazyInitializer", "fieldHandler"})
public class Player {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    private String name;
    private String image;
    @JsonIgnore
    private String account;
    @JsonIgnore
    private String password;
    @JsonIgnore
    private String email;
    @Column(name = "rank_point")
    private int rank = 1000;

    @OneToMany(fetch = FetchType.LAZY, cascade = CascadeType.REMOVE)
    @JoinColumn(name = "player_id")
    @JsonAnnotation.BattleIgnore
    @JsonAnnotation.PlayerInfoIgnore
    List<PlayerPokemon> pokemonList;

    @OneToMany(fetch = FetchType.LAZY, cascade = CascadeType.REMOVE)
    @JoinColumn(name = "player_id")
    @JsonAnnotation.PlayerInfoIgnore
    List<PlayerBall> ballList;

    @OneToMany(fetch = FetchType.LAZY, cascade = CascadeType.REMOVE)
    @JoinColumn(name = "player_id")
    @JsonAnnotation.PlayerInfoIgnore
    List<PlayerPotion> potionList;

    public Player() {}

    public Player(String name, String account, String password, String email) {
        this.name = name;
        this.account = account;
        this.password = password;
        this.email = email;
    }

    public List<PlayerBall> getBallList() {
        return ballList;
    }

    public void setBallList(List<PlayerBall> ballList) {
        this.ballList = ballList;
    }

    public List<PlayerPotion> getPotionList() {
        return potionList;
    }

    public void setPotionList(List<PlayerPotion> potionList) {
        this.potionList = potionList;
    }

    public List<PlayerPokemon> getPokemonList() {
        return pokemonList;
    }

    public void setPokemonList(List<PlayerPokemon> pokemons) {
        this.pokemonList = pokemons;
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

    public String getImage() {
        return image;
    }

    public void setImage(String image) {
        this.image = image;
    }

    public String getAccount() {
        return account;
    }

    public void setAccount(String account) {
        this.account = account;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public int getRank() {
        return rank;
    }

    public void setRank(int rank) {
        this.rank = rank;
    }
}
