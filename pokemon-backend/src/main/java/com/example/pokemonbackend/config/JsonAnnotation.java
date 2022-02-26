package com.example.pokemonbackend.config;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.introspect.AnnotatedMember;
import com.fasterxml.jackson.databind.introspect.JacksonAnnotationIntrospector;

import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;

public class JsonAnnotation {
    @Retention(RetentionPolicy.RUNTIME)
    public @interface BattleIgnore {}
    @Retention(RetentionPolicy.RUNTIME)
    public @interface PlayerInfoIgnore {}

    private static ObjectMapper battleMapper = null, playerMapper = null;

    public static ObjectMapper getBattleMapper() {
        if (battleMapper == null) {
            battleMapper = new ObjectMapper();
            battleMapper.setAnnotationIntrospector(new JacksonAnnotationIntrospector() {
                @Override
                public boolean hasIgnoreMarker(AnnotatedMember m) {
                    return _findAnnotation(m, BattleIgnore.class) != null
                            || super.hasIgnoreMarker(m);
                }
            });
        }
        return battleMapper;
    }

    public static ObjectMapper getPlayerMapper() {
        if (playerMapper == null) {
            playerMapper = new ObjectMapper();
            playerMapper.setAnnotationIntrospector(new JacksonAnnotationIntrospector() {
                @Override
                public boolean hasIgnoreMarker(AnnotatedMember m) {
                    return _findAnnotation(m, PlayerInfoIgnore.class) != null
                            || super.hasIgnoreMarker(m);
                }
            });
        }
        return playerMapper;
    }
}
