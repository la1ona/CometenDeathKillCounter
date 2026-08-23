# Configuration and State

CometenDeathKillCounter keeps counters and appearance settings in Streamer.bot global variables.

## Counter variables

| Variable | Purpose |
|---|---|
| `death` | Stream Deaths |
| `kills` | Stream Kills |
| `tottal` | Total Kills - original spelling retained for compatibility |
| `totaldeath` | Total Deaths |

## Appearance variables

Appearance, position, title and visibility settings use `DC_*` global variables, including position, scale, width, background opacity and colors.

## WebAdmin behavior

The WebAdmin panel sends operations to the `Cometen Death Counter` Streamer.bot action and receives the authoritative state back from Streamer.bot.

Saving configuration reads the persisted globals back before confirming success, so the acknowledgement reflects what Streamer.bot actually stored.

## Overlay connection

The OBS overlay supports connection parameters such as:

```text
?host=<STREAMERBOT_HOST>&port=8081
```

and also accepts a complete WebSocket URL through `?ws=`.
