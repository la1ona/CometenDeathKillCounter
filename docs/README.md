# Documentation

## Start here

- [Installation](INSTALLATION.md)
- [Main README](../README.md)
- [Changelog](../CHANGELOG.md)
- [Security](../SECURITY.md)
- [License](../LICENSE)

## Runtime components

```text
src/CometenDeathCounter.cs
web/death_counter_panel.html
overlay/death_counter_overlay.html
```

## Integration model

CometenDeathKillCounter uses one Streamer.bot action named `Cometen Death Counter` as the state authority. The WebAdmin panel and OBS browser overlay connect to Streamer.bot through WebSocket and receive the current counter state.

The project keeps stream counters, lifetime totals and appearance settings in Streamer.bot global variables so state survives browser refreshes and normal restarts.
