# What is this project
This project is a sample of distributed architecture with [Dapr](https://dapr.io/)
It is constituted of 3 containerized apps:
- A Blazor Frontend
- An AspNetCore Backend
- An AspNetCore minimal api used as test entry point

Other containers:
- 3 dapr SideCar (one by app) they will contains informations about dapr components and global configuration.
- A kafka broker (co) it is consumed by the Backend and for now is feed by the minimal api.
- A Kafdrop services, it is connected to the kafka broker and expose a WebUi to monitor and manage the topics. [Kafdrop documentation](https://github.com/obsidiandynamics/kafdrop)
- A Zipkin service, it will monitor everything transiting through the sidecars. [Zipkin documentation](https://zipkin.io/)

# Reference material
## Dapr
- Get Started => https://learn.microsoft.com/en-us/dotnet/architecture/dapr-for-net-developers/getting-started
- Dapr .Net => https://docs.dapr.io/developing-applications/sdks/dotnet/
## Azure Container App
- ContainerApp - Dapr => https://learn.microsoft.com/fr-fr/azure/container-apps/dapr-overview?tabs=bicep1%2Cyaml#dapr-apis
- Deploy Dapr on Container App => https://learn.microsoft.com/fr-fr/azure/container-apps/microservices-dapr?tabs=bash

# Getting Started
## Preqrequisites:
### Windows
- VisualStudio 2022
- Docker Desktop or RancherDesktop (not tested)
### Linux
Not Tested

## Launch project
- Open the soution DaprContainerApp.sln
- Set docker-compose as startup project
- Launch the project, ⚠️it might take some time on the first run due to the pulling of docker images
- Kafdrop can be found at http://localhost:9000 **when kafdrop is up we need toc reate a new topic called messages**
- Zipkin can be found at http://localhost:19411
- The publish minimal api used to publish messages to kafka can be found at http://localhost:60005/swagger
  - To check it it's working the messages sent can be viewed in the kafdrop topic page and zipkin should show the traces
