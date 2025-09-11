# StackNucleus

## NuGet Packages

[![StackNucleus.DDD.Events.WolverineFx](https://img.shields.io/nuget/v/StackNucleus.DDD.Events.WolverineFx)](https://www.nuget.org/packages/StackNucleus.DDD.Events.WolverineFx)

![logo](https://github.com/user-attachments/assets/e3b349a6-0c3f-4fba-9f02-b4a94790d445)

## Overview

**StackNucleus** provides modular building blocks for **Domain-Driven Design (DDD)** in .NET — including core patterns like entities, aggregates, repositories, and domain services. It's designed to streamline the development of enterprise-grade applications by providing reusable, composable components grounded in tactical DDD.

Whether you're building a greenfield system or refactoring a legacy monolith, StackNucleus helps enforce consistency and clarity across your domain model.

---

## Features

- Entity and AggregateRoot base types with domain event support
- Repository abstractions for persistence decoupling
- Domain service scaffolding for encapsulating business logic
- Rich support for validation and value object modeling
- Minimal dependencies, works with any persistence provider
- Reusable components and jobs for an easy start or ease of use

---

## How to Create and Deploy a NuGet Package

Follow these steps to create, version, and deploy your NuGet package for StackNucleus projects:

1. **Add your code changes**  
   Make all necessary code updates or feature additions in your local branch.

2. **Increase the NuGet version**  
   Update the version number inside the `Directory.Build.props` file to reflect your new release.

3. **Update the changelog**  
   Add a version description to the `CHANGELOG.md` file under each NuGet project, documenting your changes.

4. **(Optional) Update the README**  
   Modify the `README.md` under each NuGet project if you want to update usage instructions or examples.

5. **Create a pull request**  
   Push your changes and create a pull request targeting the `master` branch.

6. **Merge and tag**  
   After your PR is merged to `master`, create a Git tag on GitHub corresponding to the new NuGet package version (e.g., `v1.2.3`).

7. **Wait for deployment**  
   The CI/CD pipeline will automatically build and deploy the package to the NuGet feed. Wait until the package is available on [nuget.org](https://www.nuget.org/).

8. **Update the NuGet API key (every 3 months)**  
   For security, update the NuGet API key periodically (once every three months) in your CI/CD environment to maintain deployment access.
