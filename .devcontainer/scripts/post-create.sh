#!/bin/bash

# make all scripts executable
chmod +x ./scripts/**/*.sh

# output dotnet version
dotnet --version

# restore nuget packages
dotnet restore
