# Introduction

Saga Guide is a web application designed to enhance the gaming experience for game masters and players of the GURPS Fourth Edition roleplaying game.
Currently, Saga Guide offers an interactive character sheet, with more exciting features on the horizon.

# Getting Started

The latest version of Saga Guide is available at [saga.guide](https://saga.guide)

# Build and Test

To build and run saga guide follow next steps:

Requirements:
1. IDE for C# and TypeScript (Rider, Visual Studio, Visual Code)
1. .net 7
1. Node.js
1. npm
1. docker or docker desktop

Build steps:
1. Navigate to ".\src\frontend" and run "npm install" in console or IDE terminal.
1. Run ".\src\docker-compose.yml" using IDE or docker
1. Wait and check that docker containers are up and running
1. In browser navigate to http://localhost:3000/home to access Saga Guide
1. Swagger api page for back-end is available at http://localhost:5258/swagger/index.html
1. Keycloak page is available at http://localhost:8080/

# Contribute

TODO: Explain how other users and developers can contribute to make your code better.

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:

- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)
