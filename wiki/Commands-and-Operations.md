# Commands and Operations

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
| `!resetall` | Reset all counters |

Attach command triggers directly to the `Cometen Death Counter` action.

## Stream Deck operations

Use `Set Argument` with variable name `operation`, followed by `Run Action` for `Cometen Death Counter` with Run Action Immediately enabled.

Supported values:

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

- `resetstream` keeps lifetime totals.
- `resetkills` clears Stream Kills and Total Kills.
- `resetdeaths` clears Stream Deaths and Total Deaths.
- `resetall` clears all four counters.
