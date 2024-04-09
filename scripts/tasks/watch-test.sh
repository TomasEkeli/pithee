#!/bin/bash

## this should be run from the root of the project (not the workspace)
## this script will watch for changes in the test project and run the tests
## it wil also generate coverage files

dotnet watch test \
    --nologo \
    --no-restore \
    /property:GenerateFullPaths=true \
    /p:CollectCoverage=true \
    /p:CoverletOutputFormat=cobertura \
    /p:CoverletOutput=./TestResults/ \