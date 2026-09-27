# Contributing

Thanks for your interest in contributing!

## Workflow

1. Fork or branch from `master`.
2. Make your changes on a feature branch (e.g. `feature/your-change`).
3. Open a pull request into `master`.
4. At least **1 approving review** is required before merge.
5. Direct pushes and force pushes to `master` are disabled — all changes must go through a PR.

## CI Checks

On push/PR: .NET build & test (`ci.yml`). Merging triggers auto-tag/release and NuGet package publish (`cd-on-commit.yml`, `cd-on-tag.yml`). Make sure the solution builds and tests pass locally.
