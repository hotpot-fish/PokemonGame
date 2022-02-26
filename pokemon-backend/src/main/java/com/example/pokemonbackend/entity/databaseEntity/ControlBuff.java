package com.example.pokemonbackend.entity.databaseEntity;

import com.example.pokemonbackend.entity.PokemonDigitalAttribution;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import java.util.List;
import java.util.Objects;

@Entity
@DiscriminatorValue(value = "控制")
public class ControlBuff extends Buff {
    private int lasting;

    @Override
    public String getDiscType() {
        return "控制";
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
        ControlBuff that = (ControlBuff) o;
        return super._equals(that) && getEffect().equals(that.getEffect()) && lasting == that.lasting;
    }

    @Override
    public int hashCode() {
        return Objects.hash(super._hashCode(), lasting);
    }

    @Override
    public ControlBuff clone() throws CloneNotSupportedException {
        ControlBuff ret = (ControlBuff) super.clone();
        ret.lasting = lasting;
        return ret;
    }

    @Override
    public boolean addToBuff(List<Buff> buffs) {
        switch (getEffect()) {
            case "失明":
            case "晕眩":
            case "疲惫":
                for (Buff _buff : buffs) {
                    if (_buff instanceof ControlBuff && _buff.getEffect().equals("免控")) {
                        return false;
                    }
                }
                break;
        }
        for (Buff _buff : buffs) {
            if (_buff instanceof ControlBuff) {
                ControlBuff buff = (ControlBuff) _buff;
                if (getEffect().equals(buff.getEffect())) {
                    if (buff.lasting < lasting)
                        buff.lasting = lasting;
                    return true;
                }
            }
        }
        try {
            ControlBuff thisClone = clone();
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
            case "失明":
                if (Math.random() < 0.5)
                    break;
            case "晕眩":
            case "疲惫":
                curAttr.setCanMove(false);
                break;
            case "免控":
                break;
            case "闪避":
                curAttr.setDodgeRoundCnt(Math.max(lasting, curAttr.getDodgeRoundCnt()));
                break;
        }
        return 0;
    }

    @Override
    public void roundEnd() {
        lasting--;
    }

    @Override
    public boolean isOutOfDate() {
        return lasting == 0;
    }
}
