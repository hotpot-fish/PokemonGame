package com.example.pokemonbackend.entity.databaseEntity;

import com.example.pokemonbackend.entity.PokemonDigitalAttribution;
import org.slf4j.LoggerFactory;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import java.util.List;
import java.util.Objects;

@Entity
@DiscriminatorValue(value = "血量")
public class HPBuff extends Buff {
    private int data;
    private int lasting;

    public int getData() {
        return data;
    }

    public void setData(int data) {
        this.data = data;
    }

    public int getLasting() {
        return lasting;
    }

    public void setLasting(int lasting) {
        this.lasting = lasting;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        HPBuff that = (HPBuff) o;
        return super._equals(that) && data == that.data && lasting == that.lasting;
    }

    @Override
    public int hashCode() {
        return Objects.hash(super._hashCode(), data, lasting);
    }

    @Override
    public HPBuff clone() throws CloneNotSupportedException {
        HPBuff ret = (HPBuff) super.clone();
        ret.data = data;
        ret.lasting = lasting;
        return ret;
    }

    @Override
    public String getDiscType() {
        return "血量";
    }

    @Override
    public boolean addToBuff(List<Buff> buffs) {
        for (Buff _buff : buffs) {
            if (_buff instanceof HPBuff) {
                HPBuff buff = (HPBuff) _buff;
                if (getEffect().equals(buff.getEffect()) && data == buff.data) {
                    if (buff.lasting < lasting)
                        buff.lasting = lasting;
                    return true;
                }
            }
        }
        try {
            HPBuff thisClone = clone();
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
        if (lasting == 0) {
            return 0;
        }
        --lasting;
        switch (getEffect()) {
            case "烧伤":
            case "冻伤":
            case "中毒":
            case "百分比":
                return initAttr.getHP() * data / 100;
            case "固定":
                return data;
            case "敌方伤害百分比":
                return enemyDamage * data / 100;
            case "己方伤害百分比":
                return selfDamage * data / 100;
        }
        LoggerFactory.getLogger(Object.class).error(
                "this path shouldn't be reached in HPBuff {}", this
        );
        return 0;
    }

    @Override
    public void roundEnd() {
    }

    @Override
    public boolean isOutOfDate() {
        return lasting == 0;
    }
}
