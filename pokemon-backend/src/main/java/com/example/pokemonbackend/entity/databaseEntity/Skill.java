package com.example.pokemonbackend.entity.databaseEntity;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name = "skill")
@JsonIgnoreProperties(value = {"handler", "hibernateLazyInitializer", "fieldHandler"})
public class Skill {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    private String name;
    private int power;
    private String element;
    private int priority;
    private int learnLevel;
    private String describe;
    private String action;

    @Column(name = "max_pp")
    private int maxPP;

    @OneToMany(fetch = FetchType.EAGER, cascade = CascadeType.REMOVE)
    @JoinColumn(name = "skill_id")
    @JsonIgnore
    private List<SkillBuff> buffs;

    public String getAction() {
        return action;
    }

    public void setAction(String action) {
        this.action = action;
    }

    public String getDescribe() {
        return describe;
    }

    public void setDescribe(String describe) {
        this.describe = describe;
    }

    public int getLearnLevel() {
        return learnLevel;
    }

    public void setLearnLevel(int learnLevel) {
        this.learnLevel = learnLevel;
    }

    public int getPriority() {
        return priority;
    }

    public void setPriority(int priority) {
        this.priority = priority;
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

    public int getPower() {
        return power;
    }

    public void setPower(int power) {
        this.power = power;
    }

    public int getMaxPP() {
        return maxPP;
    }

    public void setMaxPP(int maxPP) {
        this.maxPP = maxPP;
    }

    public List<SkillBuff> getBuffs() {
        return buffs;
    }

    public void setBuffs(List<SkillBuff> buffs) {
        this.buffs = buffs;
    }
}
