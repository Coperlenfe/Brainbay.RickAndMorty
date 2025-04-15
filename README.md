# Brainbay Rick and Morty App

A .NET 8 project with a Web API and a Blazor WebAssembly UI.

## Features

- `WebApp`: Fetches and stores Rick and Morty characters using an API (SQLite DB).
  - `GET /api/Character` – List all characters  
  - `GET /api/Character/{id}` – Get character by ID  
  - `POST /api/Character` – Add a new character  
  - `POST /api/Character/sync` – Sync characters from external API  
  
- `UI`: Lists and filters characters, and shows their details.

## How to Run

Run both projects simultaneously:

 Brainbay.WebApp
 Brainbay.UI
