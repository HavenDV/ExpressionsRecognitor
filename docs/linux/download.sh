#!/bin/bash
rm -r -f ExpressionsRecognitor
git clone https://github.com/HavenDV/ExpressionsRecognitor
nuget restore ExpressionsRecognitor/ExpressionsRecognitor.sln
./build.sh
