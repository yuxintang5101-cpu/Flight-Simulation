# F-16C HUD Runtime Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Add a reusable, self-building, cockpit-only F-16C NAV/Landing HUD driven exclusively by `HudState` and explicit cockpit context.

**Architecture:** Pure layout math feeds a retained vector command buffer and fixed TMP label pool. A single controller creates and owns the generated uGUI hierarchy, updates it from pushed contract data, and gates the entire display.

**Tech Stack:** Unity 2022.3, C# 9-compatible syntax, uGUI, `MaskableGraphic`, `VertexHelper`, TextMeshPro 3.0.7, NUnit EditMode tests.

## Global Constraints

- Create files only under `Assets/FlightSimPlatform/Presentation/**`.
- Do not edit Contracts, Core, Integration, Unity/legacy code, existing tests, or scenes.
- Consume only `Contracts.HudState` plus an explicit cockpit-active flag and camera.
- Do not use `OnGUI`, input polling, `Rigidbody`, or `Camera.main`.
- Configure CanvasScaler to 1920x1080 Scale With Screen Size.
- Preserve safe-area aspect and scale line/text dimensions from the minimum landscape dimension.

---

### Task 1: Layout Mathematics and Tests

**Files:**
- Create: `Assets/FlightSimPlatform/Presentation/FlightSim.Presentation.asmdef`
- Create: `Assets/FlightSimPlatform/Presentation/Runtime/F16HudLayoutMath.cs`
- Create: `Assets/FlightSimPlatform/Presentation/Tests/EditMode/FlightSim.Presentation.EditModeTests.asmdef`
- Create: `Assets/FlightSimPlatform/Presentation/Tests/EditMode/F16HudLayoutMathTests.cs`

**Interfaces:**
- Consumes: `FlightSim.Platform.Contracts.HudState`
- Produces: `F16HudLayoutMath.IsDisplayable`, `CalculateSafeRect`, `ProjectAngle`, `WrapDegrees`, and SI/display conversion methods.

- [ ] Write tests for valid/off/NaN state gating, 16:9/ultrawide/4:3 safe rectangles, projection bounds, heading wrapping, knots/feet/fpm/nm conversion.
- [ ] Run Unity EditMode tests and confirm failure because the runtime types do not exist.
- [ ] Add the assembly and minimal layout math implementation.
- [ ] Run tests and confirm the layout-math suite passes.

### Task 2: Vector and Label Primitives

**Files:**
- Create: `Assets/FlightSimPlatform/Presentation/Runtime/HudVectorCommandBuffer.cs`
- Create: `Assets/FlightSimPlatform/Presentation/Runtime/HudVectorGraphic.cs`
- Create: `Assets/FlightSimPlatform/Presentation/Runtime/HudLabelPool.cs`
- Create: `Assets/FlightSimPlatform/Presentation/Tests/EditMode/HudPrimitiveTests.cs`

**Interfaces:**
- Consumes: layout-space points, line widths, colors, text requests.
- Produces: bounded retained vector commands, one `MaskableGraphic`, and a fixed-capacity TMP pool.

- [ ] Write failing tests for command reset/capacity and label-pool reuse.
- [ ] Implement solid/dashed lines, circles, boxes, diamonds, crosses, and label acquisition.
- [ ] Run the primitive tests and complete the red-green cycle.

### Task 3: F-16C Composer

**Files:**
- Create: `Assets/FlightSimPlatform/Presentation/Runtime/F16HudComposer.cs`
- Create: `Assets/FlightSimPlatform/Presentation/Tests/EditMode/F16HudComposerTests.cs`

**Interfaces:**
- Consumes: valid `HudState`, combiner rect, vector buffer, and label sink.
- Produces: all NAV/Landing vector and text symbology with deterministic capacity bounds.

- [ ] Write failing coverage tests for required NAV symbols, Landing-only AoA, warnings, and arm/store/nav labels.
- [ ] Implement fixed layout zones and the DCS-inspired symbology.
- [ ] Run composer tests and verify capacity remains within fixed bounds.

### Task 4: Self-Building Controller

**Files:**
- Create: `Assets/FlightSimPlatform/Presentation/Runtime/F16HudController.cs`
- Create: `Assets/FlightSimPlatform/Presentation/Tests/EditMode/F16HudControllerTests.cs`

**Interfaces:**
- Consumes: `SetCockpitContext(bool, Camera)` and `SetHudState(HudState)`.
- Produces: generated Canvas/Scaler/mask/graphic/labels and complete visibility gating.

- [ ] Write failing controller tests for hierarchy configuration, explicit-camera assignment, and full deactivation gates.
- [ ] Implement generated hierarchy ownership, refresh, resize handling, and public push API.
- [ ] Run controller and full Presentation EditMode tests.

### Task 5: Verification and Review

- [ ] Compile the Presentation assembly in Unity batch mode.
- [ ] Scan owned source for forbidden APIs and direct non-contract dependencies.
- [ ] Check file ownership and confirm no files outside Presentation changed.
- [ ] Review each requested HUD symbol against composer coverage and report compile limitations.
