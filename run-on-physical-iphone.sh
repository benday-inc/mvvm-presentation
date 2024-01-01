#!/bin/bash

cd MauiApp1

dotnet build -t:Run -f net8.0-ios -p:RuntimeIdentifier=ios-arm64 -p:_DeviceName=00008130-0005443E2011401C

# pop back to original directory
cd -
