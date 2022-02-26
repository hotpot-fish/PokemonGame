package com.example.pokemonbackend.entity.databaseEntity;

import com.example.pokemonbackend.entity.PokemonDigitalAttribution;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import javax.persistence.*;
import java.util.List;
import java.util.Objects;

@Entity
@Table(name = "buff")
@Inheritance(strategy = InheritanceType.SINGLE_TABLE)
@DiscriminatorColumn(name = "disc_type", discriminatorType = DiscriminatorType.STRING)
@JsonIgnoreProperties(value = {"handler", "hibernateLazyInitializer", "fieldHandler"})
public abstract class Buff implements Cloneable {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    @Column(name = "target_self")
    private boolean targetSelf;

    @Column(name = "settle_time")
    private String settleTime;

    private String effect;

    public String getEffect() {
        return effect;
    }

    public void setEffect(String effect) {
        this.effect = effect;
    }

    public String getSettleTime() {
        return settleTime;
    }

    public void setSettleTime(String settleTime) {
        this.settleTime = settleTime;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public boolean getTargetSelf() {
        return targetSelf;
    }

    public void setTargetSelf(boolean targetSelf) {
        this.targetSelf = targetSelf;
    }

    public int calc(
            String time,
            PokemonDigitalAttribution initAttr, PokemonDigitalAttribution curAttr,
            int selfDamage, int enemyDamage
    ) {
        if (!settleTime.equals(time)) {
            return 0;
        }
        return _calc(initAttr, curAttr, selfDamage, enemyDamage);
    }

    protected abstract int _calc(
            PokemonDigitalAttribution initAttr, PokemonDigitalAttribution curAttr,
            int selfDamage, int enemyDamage
    );

    public abstract String getDiscType();

    public abstract boolean addToBuff(List<Buff> buffs);

    public abstract void roundEnd();

    public abstract boolean isOutOfDate();

    protected int _hashCode() {
        return Objects.hash(id, targetSelf);
    }

    protected boolean _equals(Buff that) {
        return id == that.id && targetSelf == that.targetSelf && effect.equals(that.effect);
    }
}
