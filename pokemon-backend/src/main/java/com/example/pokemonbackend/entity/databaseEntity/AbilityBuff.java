package com.example.pokemonbackend.entity.databaseEntity;

import com.example.pokemonbackend.entity.PokemonDigitalAttribution;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import java.util.List;
import java.util.Objects;

@Entity
@DiscriminatorValue(value = "能力")
public class AbilityBuff extends Buff {
    private int data;

    @Override
    public String getDiscType() {
        return "能力";
    }

    public int getData() {
        return data;
    }

    public void setData(int data) {
        this.data = data;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        AbilityBuff that = (AbilityBuff) o;
        return super._equals(that) && data == that.data;
    }

    @Override
    public int hashCode() {
        return Objects.hash(super._hashCode(), data);
    }

    @Override
    public AbilityBuff clone() throws CloneNotSupportedException {
        AbilityBuff ret = (AbilityBuff) super.clone();
        ret.data = data;
        return ret;
    }

    @Override
    public boolean addToBuff(List<Buff> buffs) {
        if (getEffect().equals("消强")) {
            buffs.removeIf(buff -> {
                if (!(buff instanceof AbilityBuff)) {
                    return false;
                }
                return ((AbilityBuff) buff).data > 0;
            });
            return true;
        }
        if (getEffect().equals("解弱")) {
            buffs.removeIf(buff -> {
                if (!(buff instanceof AbilityBuff)) {
                    return false;
                }
                return ((AbilityBuff) buff).data < 0;
            });
            return true;
        }
        for (Buff _buff : buffs) {
            if (_buff instanceof AbilityBuff) {
                AbilityBuff buff = (AbilityBuff) _buff;
                if (getEffect().equals(buff.getEffect())) {
                    buff.data += data;
                    if (buff.data == 0) {
                        buffs.remove(buff);
                    }
                    return true;
                }
            }
        }
        try {
            AbilityBuff thisClone = clone();
            buffs.add(thisClone);
        } catch (CloneNotSupportedException e) {
            e.printStackTrace();
        }
        return true;
    }

    @Override
    protected int _calc(
            PokemonDigitalAttribution initAttr, PokemonDigitalAttribution curAttr,
            int selfDamage, int enemyDamage
    ) {
        switch (getEffect()) {
            case "攻击":
                curAttr.setAttack(Math.max(-99,
                        curAttr.getAttack() + data * initAttr.getAttack() / 5
                ));
                break;
            case "防御":
                curAttr.setDefence(Math.max(-99,
                        curAttr.getDefence() + data * initAttr.getDefence() / 5
                ));
                break;
            case "速度":
                curAttr.setSpeed(
                        curAttr.getSpeed() + data * initAttr.getSpeed() / 5
                );
                break;
        }
        return 0;
    }

    @Override
    public void roundEnd() {
        /* do nothing */
    }

    @Override
    public boolean isOutOfDate() {
        return false;
    }
}
