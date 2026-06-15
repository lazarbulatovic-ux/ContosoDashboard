<!--
Sync Impact Report

- Version change: unknown -> 0.1.0
- Modified principles: placeholders replaced with concrete principles (Security, Test-First,
	Observability, Versioning, Simplicity)
- Added/updated sections: Development Workflow and Technology Constraints clarified
- Removed sections: none
- Templates reviewed: .specify/templates/plan-template.md (✅ updated),
	.specify/templates/spec-template.md (✅ updated), .specify/templates/tasks-template.md (✅ updated)
- Follow-up TODOs:
	- RATIFICATION_DATE: TODO(RATIFICATION_DATE): original ratification date unknown; please
		confirm and replace the placeholder.
-->

# ContosoDashboard Constitution

## Core Principles

### Security First (NON-NEGOTIABLE)
All code MUST follow least-privilege principles. Secrets and credentials MUST NOT be
committed to version control. All inputs coming from external sources MUST be validated
and sanitized. Vulnerability fixes for direct dependencies MUST be addressed within 7 days
or escalated.

### Test-First (TDD where practical)
Every new feature MUST include automated tests: unit tests for business logic, integration
tests for service boundaries, and acceptance tests for user stories. Tests SHOULD be
authored before implementation when feasible; failing tests MUST drive implementation.

### Observability & Logging
Services and long-running processes MUST emit structured logs, expose key metrics, and
include tracing for critical user flows. Production-grade alerts MUST exist for major
failure modes so incidents are detectable and actionable.

### API Compatibility & Versioning
Public and cross-repository APIs MUST follow semantic versioning. Breaking changes MUST
be communicated in advance and accompanied by migration guidance and compatibility
tests where applicable.

### Simplicity & Minimal Surface
Prefer simple, well-documented interfaces. Avoid premature generalization or large
abstractions. Code MUST aim for clarity and minimal public surface area (YAGNI).

## Technology Constraints

This project is implemented as a .NET Blazor application (C#). The following constraints
apply:

- Target frameworks: .NET 8/10 where supported.
- Use Entity Framework Core migrations for schema changes; ensure migrations are
	reviewed in PRs.
- CI pipelines MUST run unit and integration tests and enforce static analysis/linting.

## Development Workflow

- Feature branches per work item, named `feature/short-description`.
- Pull requests MUST include tests and a brief description of the user scenario.
- All PRs require at least one approving review from a maintainer and a green CI run.
- Commits SHOULD be small and focused; prefer multiple small PRs over a single large one.

## Governance

Amendments to this constitution MUST be proposed via a pull request that documents the
change, the rationale, and a migration plan for any required updates. A constitutional
change requires approval from at least two maintainers. Versioning follows semantic
rules: MAJOR for incompatible governance changes, MINOR for added principles or
material guidance, PATCH for editorial/clarifying updates.

**Version**: 0.1.0 | **Ratified**: TODO(RATIFICATION_DATE): please confirm | **Last Amended**: 2026-06-15

