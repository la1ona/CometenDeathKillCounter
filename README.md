# CometenDeathKillCounter

A customizable death and kill counter for **Streamer.bot** and **OBS Studio**.

The project includes a movable browser overlay, a local WebAdmin panel, persistent stream and lifetime totals, chat-command support, and Stream Deck-friendly operations.

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

## Current version

**v1.11.0**

## Download

The ready-to-use package is available in:

```text
release/CometenDeathKillCounter-v1.11.0.zip
```

The repository also includes the complete source, WebAdmin panel and OBS overlay as separate files.

## Repository structure

```text
src/CometenDeathCounter.cs              Streamer.bot C# action
web/death_counter_panel.html            Local WebAdmin panel
overlay/death_counter_overlay.html      OBS browser overlay
docs/INSTALLATION.md                    Installation and setup guide
release/CometenDeathKillCounter-v1.11.0.zip
```

## Quick setup

1. Create a Streamer.bot action named exactly `Cometen Death Counter`.
2. Add an **Execute C# Code** sub-action.
3. Paste the complete contents of `src/CometenDeathCounter.cs`.
4. Compile and save the C# action.
5. Enable the Streamer.bot WebSocket server on port `8081`.
6. Open `web/death_counter_panel.html` in a browser.
7. Add `overlay/death_counter_overlay.html` as a local OBS Browser Source.
8. Set the OBS Browser Source size to `1920 x 1080`.

The panel should show:

```text
Tilkoblet - C# V1.11.0
```

See [docs/INSTALLATION.md](docs/INSTALLATION.md) for the complete setup.

## Chat commands

| Command | Action |
|---|---|
| `!kill` | Add one Stream Kill and one Total Kill |
| `!death` | Add one Stream Death and one Total Death |
| `!rkill` | Remove one Stream Kill and one Total Kill |
| `!rdeath` | Remove one Stream Death and one Total Death |
| `!reset` | Reset Stream Deaths and Stream Kills only |
| `!resetkills` | Reset Stream Kills and Total Kills |
| `!resetdeaths` | Reset Stream Deaths and Total Deaths |
| `!resetall` | Reset all four counters |

Command triggers must be attached directly to the `Cometen Death Counter` action.

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

Replace all three runtime files when updating:

- `src/CometenDeathCounter.cs`
- `web/death_counter_panel.html`
- `overlay/death_counter_overlay.html`

Compile and save the C# action again, reopen the new WebAdmin panel, and refresh the OBS Browser Source cache.

## Documentation

- [GitHub Wiki](https://github.com/la1ona/CometenDeathKillCounter/wiki)
- [Documentation index](docs/README.md)
- [Installation](docs/INSTALLATION.md)
- [Changelog](CHANGELOG.md)
- [Security](SECURITY.md)

The Markdown files in `wiki/` are the maintained source for the GitHub Wiki and are published by the `Sync Wiki` workflow.

## Security

The project does not require cloud credentials or API keys. Keep local production secrets and private environment details out of the repository. See [SECURITY.md](SECURITY.md).

## License

Released under the [GNU General Public License v3.0](LICENSE).
