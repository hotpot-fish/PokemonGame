package com.example.pokemonbackend.serviceImpl;

import com.example.pokemonbackend.service.ElementService;
import org.springframework.stereotype.Service;

@Service
public class ElementServiceImpl implements ElementService {
    @Override
    public double getElementRelation(String attacker, String defender){
        switch (attacker) {
            case "飞行":
                switch (defender) {
                    case "草": return 0.5;
                    case "岩": return 1.5;
                    default:return 1;
                }
            case "虫":
                switch (defender) {
                    case "草": return 0.5;
                    case "恶魔": case "岩": return  1.5;
                    default: return 1;
                }
            case "恶魔":
                switch (defender) {
                    case "草": return 1.5;
                    default: return 1;
                }
            case "草":
                switch (defender) {
                    case "岩": return 0.5;
                    default: return 1;
                }
            default: return 1;
        }
    }
}
