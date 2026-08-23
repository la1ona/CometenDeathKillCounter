# CometenDeathKillCounter

A customizable death and kill counter for **Streamer.bot** and **OBS Studio**.

The project includes a movable browser overlay, a local WebAdmin panel, persistent stream and lifetime totals, chat-command support, Stream Deck-friendly operations, and a ready-to-import Streamer.bot package.

## Features

- Stream Deaths
- Stream Kills
- Total Deaths
- Total Kills
- Show or hide each counter independently
- Movable and scalable OBS browser overlay
- Custom title, colors, width, scale, and background opacity
- Persistent settings and totals through Streamer.bot global variables
- Chat commands and Stream Deck operations
- Separate reset controls for stream counters, kills, deaths, or everything
- Local HTML files with no external JavaScript dependencies
- Ready-to-import Streamer.bot `.sb` file

## Current version

**v1.11.0**

## Download

The complete ready-to-use package is:

```text
release/CometenDeathKillCounter-v1.11.0.zip
```

The standalone Streamer.bot import is also available as:

```text
release/CometenDeathKillCounter_1.11.0.sb
```

## Quick setup

1. Download and extract `CometenDeathKillCounter-v1.11.0.zip`.
2. In Streamer.bot, use **Import** and select `CometenDeathKillCounter_1.11.0.sb`.
3. The import creates the action `Cometen Death Counter` and its chat-command trigger.
4. Enable the Streamer.bot WebSocket server on port `8081`.
5. Open `web/death_counter_panel.html` in a browser.
6. Add `overlay/death_counter_overlay.html` as a local OBS Browser Source.
7. Set the OBS Browser Source size to `1920 x 1080`.

Imported chat commands:

```text
!kill
!death
!rkill
!rdeath
!reset
!resetkills
!resetdeaths
!resetall
```

The C# source remains included in `src/CometenDeathCounter.cs` as a manual-install fallback.

The panel should show:

```text
Tilkoblet - C# V1.11.0
```

See [docs/INSTALLATION.md](docs/INSTALLATION.md) for the complete setup.

## Repository structure

```text
release/CometenDeathKillCounter_1.11.0.sb  Streamer.bot import
src/CometenDeathCounter.cs                 Streamer.bot C# source fallback
web/death_counter_panel.html               Local WebAdmin panel
overlay/death_counter_overlay.html         OBS browser overlay
docs/INSTALLATION.md                       Installation and setup guide
release/CometenDeathKillCounter-v1.11.0.zip Complete package
```

## Stream Deck operations

Use `Set Argument` with variable name `operation`, followed by `Run Action` for `Cometen Death Counter` with **Run Action Immediately** enabled.

Supported operation values:

```text
addkill
adddeath
removekill
removedeath
resetstream
resetkills
resetdeaths
resetall
```

## Persistent global variables

| Variable | Purpose |
|---|---|
| `death` | Stream Deaths |
| `kills` | Stream Kills |
| `tottal` | Total Kills - original spelling retained for compatibility |
| `totaldeath` | Total Deaths |

The project also stores overlay appearance, position, title, and visibility settings in `DC_*` global variables.

## Updating

Replace/import the files from the latest package, reopen the WebAdmin panel, and refresh the OBS Browser Source cache after replacing the overlay.

## Documentation

- [GitHub Wiki](https://github.com/la1ona/CometenDeathKillCounter/wiki)
- [Documentation index](docs/README.md)
- [Installation](docs/INSTALLATION.md)
- [Changelog](CHANGELOG.md)
- [Security](SECURITY.md)
- [License](LICENSE)

The Markdown files in `wiki/` are the maintained source for the GitHub Wiki and are published by the `Sync Wiki` workflow.

## Security

The project does not require cloud credentials or API keys. Keep local production secrets and private environment details out of the repository. See [SECURITY.md](SECURITY.md).

## License

Released under the [MIT License](LICENSE).
