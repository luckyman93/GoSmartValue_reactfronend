FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
 # Setup NodeJs
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_14.x | bash - && \
    apt-get install -y build-essential nodejs
    # End setup
#RUN npm install -g @angular/cli@11.1.2

WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 4200 49153

ENV APP_DEBUG true
ENV APP_LOG errorLog
ENV APP_DOMAIN http://localhost:5000
ENV APP_NAME goSmartValue-dev
ENV ASPNETCORE_ENVIRONMENT Development
ENV AuthKey ""
ENV ApiSecretKey ""
ENV DB_Database ""
ENV DB_Host ""
ENV DB_Password ""
ENV DB_Port 3306
ENV APPINSIGHTS_INSTRUMENTATIONKEY ""
ENV Logging_LogLevel_Default "Warning"

EXPOSE ${SmtpSettings_AlternativePort}
EXPOSE ${SmtpSettings_FromEmailAddress}
EXPOSE ${SmtpSettings_Password}
EXPOSE ${SmtpSettings_Port}
EXPOSE ${SmtpSettings_SmtpMailServer}
EXPOSE ${SmtpSettings_UserName}
EXPOSE ${SmtpSettings_UseTLS}

EXPOSE ${PaymentGateways_Dpo_CreateTokenUrl} 
EXPOSE ${PaymentGateways_Dpo_PaymentPageUrl}
EXPOSE ${PaymentGateways_Dpo_RedirectURL}
EXPOSE ${PaymentGateways_Dpo_BackURL}
EXPOSE ${PaymentGateways_Dpo_CompanyToken}

#Tingg Africa / Cellulant Config
EXPOSE ${Cellulant_ServiceCode}
EXPOSE ${Cellulant_ClientCode}
EXPOSE ${Cellulant_IvKey}
EXPOSE ${Cellulant_SecretKey}
EXPOSE ${Cellulant_AccessKey}
EXPOSE ${Cellulant_CurrencyCode}
EXPOSE ${Cellulant_ServiceShortName}
EXPOSE ${Cellulant_CallBackUrl}
EXPOSE ${Cellulant_FailRedirectUrl}
EXPOSE ${Cellulant_SuccessRedirectUrl}
EXPOSE ${Cellulant_PendingRedirectUrl}
EXPOSE ${Flutterwave_PaymentUrl}
EXPOSE ${Flutterwave_Secret}

EXPOSE ${API_URL}
EXPOSE ${HOST}
EXPOSE ${API_KEY}
EXPOSE ${FIRE_API_KEY}
EXPOSE ${FIRE_AUTH_DOMAIN}
EXPOSE ${FIRE_PROJECT_ID}
EXPOSE ${FIRE_STORAGE_BUCKETID}
EXPOSE ${FIRE_MESSAGING_SENDER_ID}
EXPOSE ${FIRE_APP_ID}
EXPOSE ${FIRE_MEASUMENT_ID}

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .

FROM build AS publish
RUN dotnet publish "/src/GoSmartValue.Web" -c Release -o /app

FROM node:14 as nodebuilder
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app
ENV PATH /usr/src/app/node_modules/.bin:$PATH
COPY GoSmartValue.Web/ClientApp/package.json /usr/src/app/package.json

RUN npm install
COPY GoSmartValue.Web/ClientApp/. /usr/src/app

RUN npm install -g @angular/cli@12.2.8
RUN npm run build-prod

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
RUN mkdir -p /app/ClientApp/dist
COPY --from=nodebuilder /usr/src/app/dist/. /app/ClientApp/dist/
ENTRYPOINT ["dotnet", "GoSmartValue.Web.dll"]
