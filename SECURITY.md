# Security

CometenDeathKillCounter is a local Streamer.bot and OBS integration. It does not require cloud credentials, API keys or external JavaScript dependencies.

## Sensitive information

Do not commit or publish local configuration containing:

- private LAN addresses that identify a production setup
- usernames or absolute user-profile paths
- Streamer.bot authentication secrets
- OBS WebSocket passwords
- platform tokens, webhook URLs or API keys

The project defaults to the local Streamer.bot WebSocket endpoint `ws://127.0.0.1:8081/`. This is a localhost address and is safe to publish.

## Network exposure

If Streamer.bot WebSocket is made available to other devices on a LAN, users are responsible for securing the host, firewall and network. Do not expose the Streamer.bot WebSocket directly to the public Internet.

## Reporting a security issue

If you discover a security issue, avoid posting credentials or private environment details in a public issue. Report the problem without including secrets, and rotate any credential that may have been exposed.
