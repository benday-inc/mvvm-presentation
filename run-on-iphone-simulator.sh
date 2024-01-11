#!/bin/bash

cd Benday.ControlsAndViewModelSample

dotnet build -t:Run -f net8.0-ios -p:_DeviceName=:v2:udid=7B0EA7AD-BC48-46B7-9D8E-27F6B3329DD3

# pop back to original directory
cd -
