# FlightSim Platform V2

## Start Here

- Open `Assets/FlightSimPlatform/Samples/Scenes/FlightSim_KTEX_V2.unity`.
- The V2 scene is first in Build Settings. The legacy `FlightSim_KTEX` scene is preserved.
- Default preset: `RunwayReady` at KTEX Runway 27, heading 285 degrees true.
- Default view: F-35 visual/cockpit eye point with an F-16C-style HUD. Cesium terrain may need a few seconds to stream.

## Command Line Development

The project wrapper uses Unity CLI `1.0.0-beta.2` together with the pinned Unity Editor at `D:\unity\2022.3.62f2c1\Editor\Unity.exe`. Save and close the Unity Editor before running batch actions against this project.

```powershell
.\Tools\UnityCli.ps1 doctor
.\Tools\UnityCli.ps1 scene
.\Tools\UnityCli.ps1 test
.\Tools\UnityCli.ps1 test -TestMode EditMode
.\Tools\UnityCli.ps1 build
.\Tools\UnityCli.ps1 mission-batch -Profile smoke -Mission all -Output Artifacts/Missions
.\Tools\UnityCli.ps1 mission-batch -Profile regression -Mission all -Runs 5 -SeedStart 1000 -Output Artifacts/Missions
.\Tools\UnityCli.ps1 mission-docs
.\Tools\UnityCli.ps1 terrain-cache
.\Tools\UnityCli.ps1 distribution-docs
.\Tools\UnityCli.ps1 distribution-validate
.\Tools\UnityCli.ps1 distribution -Output Artifacts/Distribution
.\Tools\UnityCli.ps1 all
.\Tools\UnityCli.ps1 open
```

Logs and NUnit XML reports are written to `Artifacts/CLI`. The Windows player is written to `Builds/FlightSim_KTEX_V2`. The experimental `com.unity.pipeline` package is intentionally not installed because its current release requires Unity 6000.0; Unity CLI instead launches the pinned 2022.3 Editor in batch mode.

## Controls

| Input | Action |
|---|---|
| `W` / `S` | Increase / decrease throttle |
| Mouse or arrows | Pitch and roll |
| `A` / `D` | Roll fallback |
| `Q` / `E` | Rudder |
| `Space` | Wheel brakes |
| `B` | Speed brake |
| `G` | Landing gear |
| `I` | Engine start command |
| `P` | Primary systems toggle |
| `C` | Cockpit / chase / free camera |
| `R` | Reset scenario |
| `F8` | Toggle manual / automated pilot |
| `Tab` | Select next air-to-air target |
| `X` | Select next air-to-air weapon |
| `M` | Cycle SAFE / SIM / ARM |
| `Left Ctrl` | Release selected weapon |

## Assemblies

- `FlightSim.Contracts`: versioned commands, snapshots, events, fixed-capacity tactical/store data.
- `FlightSim.Core`: deterministic 100 Hz F-16-style six-degree-of-freedom simulation.
- `FlightSim.Missions`: mission definitions, control arbitration, tactical AI, deterministic engagements and headless batch execution.
- `FlightSim.Data`: canonical read-only snapshots, mission state, health and bounded events.
- `FlightSim.Integration`: in-process simulation service, publication cadence, formation AI, read-only UDP v2 and TCP NDJSON telemetry.
- `FlightSim.Unity`: input, timing, Cesium visual synchronization, camera and terrain query bridge.
- `FlightSim.Presentation`: cockpit-only F-16C NAV/landing HUD driven only by `HudState`.

`FlightSim.Core` does not reference Unity, Cesium, PhysX or scene objects. ECEF position, velocity and attitude are authoritative. The aircraft GameObject is a kinematic visual without a `Rigidbody`.

## In-Process API

Use `IFlightSimulationService` for trusted in-process Unity integration and `IFlightDataHub` for read-only UI/data access. All core quantities are SI and include units in field names. `AircraftSnapshot` contains fast state, systems state and tactical state; the authoritative HUD payload is `snapshot.Systems.Avionics.Hud`.

Publication rates:

- Fast flight state: 50 Hz
- Systems state: 10 Hz
- Tactical picture: 5 Hz
- Discrete events: immediate

Trusted in-process commands are queued and applied on the next 100 Hz simulation tick. Supported high-level commands include reset, startup preset, system switches, landing gear, loadout configuration, formation control, store release and failure injection. These methods are not exposed through the external UDP or TCP gateways.

## UDP V2

- Bind: `127.0.0.1:49000`
- Telemetry target: `127.0.0.1:49001`
- Maximum packet size: 1200 bytes
- Header: `FSIM`, contract version, message type, sequence, tick, fixed aircraft ID, payload length and CRC-32
- Transport is nonblocking and best effort. Missing receivers and packet loss do not stop the simulation.

`FlightSimulationHost` creates the outbound-only UDP publisher by default. It has no receive loop, command parser or acknowledgement encoder. Change the serialized ports or disable UDP on the host when embedding multiple simulator instances in one process.

Mission, actor, objective, combat, automation and engagement event packets use message types 10 through 16. See `Docs/Integration/udp-v2.md` and `Docs/Integration/data-dictionary-v2.md` for the field-level contract.

## TCP NDJSON

- Listen: `127.0.0.1:46001`
- UTF-8, one JSON object per line
- First envelope: `hello`; subsequent envelopes: `data` or `event`
- Every telemetry envelope carries a concrete `domain`, versions, tick, simulation time, aircraft ID, validity and payload
- Inbound bytes are ignored and cannot become simulation commands

## Missions And Automation

The built-in catalog contains `KTEX_SCRAMBLE_01`, `KTEX_CAP_01`, `KTEX_ESCORT_01` and `KTEX_EMERGENCY_RTB_01`. The briefing panel can launch each mission in manual or visible automated mode. Headless runs execute the same 100 Hz core without creating Cesium, cameras, HUD or aircraft GameObjects.

Batch output includes per-run JSON and CSV logs plus `batch-summary.csv`, `batch-summary.json` and JUnit XML. Tactical mission failure is a valid dataset result; exceptions, non-finite state, timeouts and determinism mismatches are infrastructure failures.

## Cesium And Terrain

- `CesiumOriginShift` is on the Main Camera.
- The aircraft uses `CesiumGlobeAnchor` to consume ECEF snapshots.
- Cesium physics meshes are queried only on the dedicated `FlightTerrainQuery` layer for AGL and terrain alerts.
- Dynamic Cesium tile colliders never produce aircraft forces or determine runway contact.
- KTEX runway contact uses the persistent analytic runway and three-point landing-gear model in `FlightSim.Core`.

## Scope And Licensing

This is a semi-real platform model, not a certified F-16 training model. Radar electromagnetic propagation, weapon guidance, ballistic hit evaluation, damage and signal-level electronic warfare are intentionally out of scope.

The developer source project may still contain FlightGear F-16C GPL-2.0 prototype assets outside `Assets/FlightSimPlatform`. They are not referenced by or included in the portable distribution. The distributed visual model is the authorized F-35 asset under `Assets/FlightSimPlatform/ThirdParty/F35`; retain the delivery license record supplied by the owner.
