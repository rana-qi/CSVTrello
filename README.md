# CSVTrello

A .Net API  that reads data from a provided CSV file containing tender
details and uses this data to create or update cards in a Trello board.

## Prerequisites

Before you begin, ensure you have met the following requirements:

- **Trello Account**: You need a Trello account to create an API key and token.
- **Trello API Key and Token**: Follow the instructions [here](https://developer.atlassian.com/cloud/trello/guides/rest-api/api-introduction/) to generate your API key and token.
- **.NET Environment** (if your project is built with .NET): Ensure you have the .NET SDK installed to run the service.
- **Git**: For version control and cloning the repository.

## Setup

To install **CSVTrello**, follow these steps:

1. **Clone the repository**:
    ```bash
    git clone https://github.com/ranaberi/CSVTrello.git
    ```
2. **Navigate to the project directory**:
    ```bash
    cd CSVTrello
    ```
3. **Install dependencies** (if applicable):
    ```bash
    dotnet add package ExtendedNumeric.BigDecimal
    dotnet add package Manatee.Trello
    dotnet add package Microsoft.EntityFrameworkCore
    dotnet add package Microsoft.EntityFrameworkCore.InMemory
    
    ```
4. **Configure your Trello API Key and Token**:
    - Locate the configuration file (e.g., `appsettings.json`).
    - Replace the placeholders with your Trello API Key and Token.

