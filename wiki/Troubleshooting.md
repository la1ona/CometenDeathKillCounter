# Troubleshooting

## WebAdmin shows disconnected

Confirm that the Streamer.bot WebSocket server is enabled and that the configured host and port are correct. The default local endpoint is:

```text
ws://127.0.0.1:8081/
```

## The action cannot be found

The Streamer.bot action should be named exactly:

```text
Cometen Death Counter
```

The browser clients also attempt a fallback search for an action name containing both `death` and `counter`, but the exact name is recommended.

## Overlay does not update

- Confirm Streamer.bot is running.
- Confirm the WebSocket server is enabled.
- Verify the overlay host and port.
- Refresh the OBS Browser Source cache after replacing the overlay file.

## Settings revert

Use the matching v1.11.0 source, panel and overlay together. Do not update only one runtime component.

## Total Deaths after upgrading

Versions before v1.11.0 did not have `totaldeath`. If the variable does not yet exist, the current Stream Deaths value is used as its initial fallback.
