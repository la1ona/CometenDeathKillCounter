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

## 1. Create the Streamer.bot action

Create an action named exactly:

```text
Cometen Death Counter
```

Add one sub-action:

```text
Core -> C# -> Execute C# Code
```

Open `src/CometenDeathCounter.cs`, copy the entire file, paste it into the C# editor, then press **Compile** and **Save**.

## 2. Configure Streamer.bot WebSocket

Enable the Streamer.bot WebSocket server and use port `8081`.

The WebAdmin panel can use another host or port through its connection settings when needed.

## 3. Add the OBS browser overlay

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

## 4. Open the WebAdmin panel

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

## 5. Add chat-command triggers

Add separate **Command Triggered** triggers directly to the `Cometen Death Counter` action.

Recommended commands:

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

The C# action reads `command`, `commandName`, and `rawInput`, and maps the commands to the appropriate operation.

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
