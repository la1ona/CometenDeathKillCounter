# Changelog

All notable changes to CometenDeathKillCounter are documented here.

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
