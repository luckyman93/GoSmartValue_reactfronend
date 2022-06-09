#!/bin/bash
envsubst < /app/ClientApp/dist/vantageproperties/assets/env.template.js > /app/ClientApp/dist/vantageproperties/assets/env.js

dotnet GoSmartValue.Web.dll