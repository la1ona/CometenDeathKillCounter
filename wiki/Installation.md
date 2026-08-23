# Installation

## Requirements

- Streamer.bot
- OBS Studio
- Streamer.bot WebSocket server enabled
- A modern desktop browser for the WebAdmin panel

Default Streamer.bot WebSocket endpoint:

```text
ws://127.0.0.1:8081/
```

## Recommended installation

Download and extract:

```text
release/CometenDeathKillCounter-v1.11.0.zip
```

In Streamer.bot, use **Import** and select:

```text
CometenDeathKillCounter_1.11.0.sb
```

The import creates `Cometen Death Counter` and the chat-command trigger for:

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

If any of these commands already exist, review the imported command before using it.

## Manual fallback

Create an action named exactly `Cometen Death Counter`, add `Core -> C# -> Execute C# Code`, paste the complete contents of `src/CometenDeathCounter.cs`, then compile and save.

## OBS overlay

Add `overlay/death_counter_overlay.html` as a local OBS Browser Source using:

```text
Width: 1920
Height: 1080
```

After updating the overlay, refresh the Browser Source cache in OBS.

## WebAdmin panel

Open:

```text
web/death_counter_panel.html
```

The panel should connect to the Streamer.bot WebSocket and display the current counter state.

## Full guide

The maintained repository guide is [docs/INSTALLATION.md](https://github.com/la1ona/CometenDeathKillCounter/blob/main/docs/INSTALLATION.md).
