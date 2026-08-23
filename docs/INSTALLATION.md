# Installation

## Requirements

- Streamer.bot
- OBS Studio
- Streamer.bot WebSocket server enabled
- A modern desktop browser for the WebAdmin panel

The default WebSocket address is:

```text
ws://127.0.0.1:8081/
```

## 1. Download and extract the package

Download:

```text
release/CometenDeathKillCounter-v1.11.0.zip
```

Extract the ZIP to a permanent folder.

## 2. Import into Streamer.bot

In Streamer.bot, use **Import** and select:

```text
CometenDeathKillCounter_1.11.0.sb
```

The import creates the action:

```text
Cometen Death Counter
```

It also imports the chat-command trigger for:

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

If any of these commands already exist in your Streamer.bot setup, review the imported command before enabling or using it.

### Manual fallback

If you prefer a manual installation, create an action named exactly `Cometen Death Counter`, add **Core -> C# -> Execute C# Code**, paste the complete contents of `src/CometenDeathCounter.cs`, then compile and save.

## 3. Configure Streamer.bot WebSocket

Enable the Streamer.bot WebSocket server and use port `8081`.

The WebAdmin panel can use another host or port through its connection settings when needed.

## 4. Add the OBS browser overlay

Create a new OBS Browser Source and enable **Local file**.

Select:

```text
overlay/death_counter_overlay.html
```

Use these dimensions:

```text
Width: 1920
Height: 1080
```

After replacing the overlay during an update, use **Refresh cache of current page** in OBS.

## 5. Open the WebAdmin panel

Open:

```text
web/death_counter_panel.html
```

The connection status should show:

```text
Tilkoblet - C# V1.11.0
```

The panel controls:

- Counter title
- Visibility of each counter
- Position
- Scale and width
- Accent, title, value, label, and background colors
- Background opacity
- Exact counter values
- Reset operations

After saving, the panel should confirm that Streamer.bot saved and read the settings back.

## 6. Stream Deck setup

Create a separate wrapper action for each Stream Deck button.

Add these sub-actions in order:

### Set Argument

```text
Variable Name: operation
Value: adddeath
Auto Type: OFF
```

### Run Action

```text
Action: Cometen Death Counter
Run Action Immediately: ON
```

Change `adddeath` to the required operation:

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

## Reset behavior

| Operation | Stream Deaths | Stream Kills | Total Deaths | Total Kills |
|---|---:|---:|---:|---:|
| `resetstream` / `!reset` | Reset | Reset | Keep | Keep |
| `resetkills` | Keep | Reset | Keep | Reset |
| `resetdeaths` | Reset | Keep | Reset | Keep |
| `resetall` | Reset | Reset | Reset | Reset |

## Migration from versions before v1.11.0

`Total Deaths` did not exist before v1.11.0. When the `totaldeath` global variable does not exist, the current Stream Deaths value is used as the initial fallback.

Existing values are retained in:

```text
death
kills
tottal
```
