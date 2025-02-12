# GenAISearchQueryExpander

## Overview

GenAISearchQueryExpander is a .NET 8.0 application designed to expand search queries using AI. It leverages Azure Functions and various Azure services to provide enhanced search capabilities.

## Features

- Azure Functions integration
- AI-powered search query expansion
- Support for HTTP triggers
- Integration with Azure OpenAI
- JSON serialization with Newtonsoft.Json

## Prerequisites

- .NET 8.0 SDK
- Azure account
- Azure Functions Core Tools (for local development)

## Getting Started

### Clone the Repository

```
git clone https://github.com/yourusername/GenAISearchQueryExpander.git
cd GenAISearchQueryExpander
```

Build the Project
Run the Project Locally
To run the Azure Functions locally, use the following command:

```func start```

Deploy to Azure
Create a new Azure Function App in the Azure Portal.
Publish the project to Azure:
```
dotnet publish --output ./publish
func azure functionapp publish <YourFunctionAppName>
```
Project Structure
- `GenAISearchQueryExpander/`: Main project directory
  - `QueryExpander.cs`: Contains the main logic for expanding search queries
  - `host.json`: Configuration file for Azure Functions
  - `local.settings.json`: Local settings for Azure Functions (do not publish this file)
  - `Prompt.txt`: Contains the prompt used for AI query expansion

Dependencies
- `Microsoft.Azure.Functions.Worker` (v2.0.0)
- `Microsoft.Azure.Functions.Worker.Extensions.Http` (v3.2.0)
- `Microsoft.Azure.Functions.Worker.Extensions.Http.AspNetCore` (v2.0.0)
- `Microsoft.Azure.Functions.Worker.Sdk` (v2.0.0)
- `Azure.AI.OpenAI` (v2.0.0)
- `Microsoft.Azure.WebJobs` (v3.0.30)
- `Newtonsoft.Json` (v13.0.1)

Contributing
Contributions are welcome! Please open an issue or submit a pull request for any changes.

License
This project is licensed under the MIT License. See the LICENSE file for details.
