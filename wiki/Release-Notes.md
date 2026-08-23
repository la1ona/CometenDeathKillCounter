# Release Notes

## v1.11.0 - 2026-07-21

### Added

- Total Deaths counter
- Independent visibility control for Total Deaths
- Manual Total Deaths value in the WebAdmin panel
- `resetkills`, `resetdeaths` and `resetall` operations
- Separate reset buttons for stream, kill, death and all counters

### Changed

- `!death` increments Stream Deaths and Total Deaths
- `!rdeath` decrements Stream Deaths and Total Deaths
- Reset Stream preserves lifetime totals
- Visibility and layout settings use verified persistent storage

### Fixed

- Chat-command and Stream Deck operation detection
- Settings being overwritten by synchronization calls
- Visibility settings resetting after command or argument execution
- WebAdmin position and drag behavior

See [CHANGELOG.md](https://github.com/la1ona/CometenDeathKillCounter/blob/main/CHANGELOG.md) for the maintained changelog.
