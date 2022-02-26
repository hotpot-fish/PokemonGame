package com.example.pokemonbackend.util;

import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.ScheduledThreadPoolExecutor;
import java.util.concurrent.TimeUnit;

public class MyTimer {
    private static final ScheduledThreadPoolExecutor executor
            = new ScheduledThreadPoolExecutor(1);
    private ScheduledFuture<?> future = null;

    public MyTimer() {}

    public void startTimeout(Runnable runnable, long millis) {
        cancelTimeout();
        future = executor.schedule(runnable, millis, TimeUnit.MILLISECONDS);
    }

    public boolean cancelTimeout() {
        if (future != null) {
            return future.cancel(false);
        }
        return false;
    }
}
