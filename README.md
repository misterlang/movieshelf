# Movie Shelf

A practical MCP (Model Context Protocol) workshop project built with C# and .NET.

Movie Shelf demonstrates how to build an MCP server that exposes a movie collection to AI clients such as GitHub Copilot.

## Overview

The project provides a small movie collection stored in a JSON file and exposes functionality through MCP tools.

The goal is to explore:

* Building an MCP server with C# and .NET
* Exposing application functionality as MCP tools
* Integrating an MCP server with Visual Studio Code and GitHub Copilot
* Designing tools that can be discovered and invoked by an AI client
* Testing MCP tools and their underlying application logic

## Architecture

```text
GitHub Copilot
      │
      │ MCP
      ▼
┌───────────────────┐
│   MovieShelf MCP  │
├───────────────────┤
│    MovieTools     │
├───────────────────┤
│   MovieService    │
├───────────────────┤
│    movies.json    │
└───────────────────┘
```

The MCP layer is intentionally kept thin. Business and data-access logic remains in the existing application services.

## MCP Tools

The following MCP tools are currently available:

| Tool                   | Description                             |
| ---------------------- | --------------------------------------- |
| `find_movies_by_genre` | Finds movies matching a specific genre. |
| `get_movie_by_title`   | Finds a movie by its title.             |
| `get_all_movies`       | Returns the complete movie collection.  |

## Requirements

* .NET SDK
* Visual Studio Code
* GitHub Copilot
* MCP support in Visual Studio Code

## Running the MCP Server

Clone the repository and open the project in Visual Studio Code.

Build the project:

```powershell
dotnet build
```

Run the MCP server:

```powershell
dotnet run --project MovieShelf/MovieShelf.csproj
```

The server uses the MCP STDIO transport and is intended to be started by an MCP-compatible client such as Visual Studio Code.

## VS Code MCP Configuration

Create or update `.vscode/mcp.json`:

```json
{
  "servers": {
    "movieshelf": {
      "type": "stdio",
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "${workspaceFolder}/MovieShelf/MovieShelf.csproj"
      ]
    }
  }
}
```

After starting the server through VS Code, the available MovieShelf MCP tools can be used from GitHub Copilot Agent mode.

## Project Status

Movie Shelf is primarily a learning and experimentation project for MCP development with C# and .NET.

The project is intentionally small so that MCP concepts, tool design, integration, and testing can be explored without unnecessary infrastructure.

## License

This project is licensed under the MIT License.
