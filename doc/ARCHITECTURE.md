# Pelikello Architecture & Design Rules

## General Description

**Pelikello** is a hybrid desktop-web application for parents to manage and limit their children's PC gaming time. It is privacy-first, open-source, and runs entirely on the local network with no external dependencies. The application tracks Steam game activity, enforces configurable time limits, and provides a web dashboard for remote control and monitoring.

---

## High-Level Architecture

### 1. Core Components

- **Game Session Tracker**: Monitors Steam game processes, records session durations, and associates them with user profiles.
- **Web Dashboard (Blazor Server)**: Provides a LAN-accessible interface for parents to view status, set limits, and trigger actions.
- **System Actions Module**: Executes system-level commands (shutdown, logout) securely and reliably.
- **Persistence Layer**: Stores session data, settings, and user profiles using SQLite or JSON files.
- **Authentication Layer**: Secures the web interface with optional API key or password/IP-based authentication.

### 2. Technology Stack

- **Frontend & Backend**: Blazor Server on .NET 9, ASP.NET Core
- **Storage**: SQLite (or JSON as fallback)
- **Hosting**: Kestrel (self-hosted, LAN-exposed)

---

## Guiding Principles & Rules

1. **Privacy & Local Control**: All functionality is LAN-only. No cloud, no external APIs, no third-party analytics. No open internet ports.
2. **Simplicity**: UI and UX are clear, family-friendly, and require minimal technical knowledge for basic use.
3. **Resilience**: The app should auto-restart on boot and recover gracefully from errors.
4. **Security**: Web access is LAN-restricted and protected with optional API key, password, or IP allowlist.
5. **Extensibility**: The architecture should allow for future features (e.g., scheduling, notifications, per-game rules).
6. **Cross-Platform Readiness**: While Windows is the primary target, code should be organized to allow future Linux/Mac support.
7. **Fail-Safe Enforcement**: System actions (shutdown/logout) must be reliable and handle edge cases (permissions, unsaved work, user tampering).
8. **Multi-User Support**: No multi-user support, just one shared state. This is run on the machine that is tracked.

---

## Key Flows

- **Game Detection**: Monitor running processes, match against known Steam games, and log session times.
- **Time Enforcement**: When the time limit is reached, warn the user and then trigger shutdown/logout.
- **Remote Management**: Parents access the dashboard from any LAN device to view status, set limits, and control sessions.

---

## Future-Proofing

- Modularize platform-specific code (e.g., process monitoring, system actions) for easier cross-platform support.
- Design data models to support multiple users, games, and custom rules.
- Design extension points for future features like scheduling, per-game rules, and push notifications.
