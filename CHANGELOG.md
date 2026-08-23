# Changelog

All notable changes to CometenDeathKillCounter are documented here.

## Unreleased

### Added

- Ready-to-import `CometenDeathKillCounter_1.11.0.sb` Streamer.bot package
- Complete v1.11.0 ZIP containing the Streamer.bot import, source, WebAdmin panel, OBS overlay, documentation, security policy and MIT license
- Imported chat-command trigger for `!kill`, `!death`, `!rkill`, `!rdeath`, `!reset`, `!resetkills`, `!resetdeaths` and `!resetall`

### Changed

- Streamer.bot import is now the recommended installation method
- Manual C# installation remains available as a fallback
- Project license changed from GNU General Public License v3.0 to MIT

## [1.11.0] - 2026-07-21

### Added

- Total Deaths counter
- Independent visibility control for Total Deaths
- Manual Total Deaths value in the WebAdmin panel
- `resetkills`, `resetdeaths`, and `resetall` operations
- Separate reset buttons for stream counters, kill counters, death counters, and all counters

### Changed

- `!death` now increments both Stream Deaths and Total Deaths
- `!rdeath` now decrements both Stream Deaths and Total Deaths
- Reset Stream preserves both lifetime totals
- Visibility and layout settings use verified persistent storage

### Fixed

- Chat-command and Stream Deck operation detection
- Settings being overwritten by synchronization calls
- Visibility settings resetting after command or argument execution
- WebAdmin position and drag behavior
