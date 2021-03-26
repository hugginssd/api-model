﻿using ApiModel.Domain;
using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiModel.Installers
{
    public class CosmosInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection service, IConfiguration configuration)
        {
            var comsosStoreSettings = new CosmosStoreSettings(
                configuration["CosmosSettings:DatabaseName"],
                configuration["CosmosSettings:AccountUri"],
                configuration["CosmosSettings:AccountKey"],
                new ConnectionPolicy { ConnectionMode = ConnectionMode.Direct, ConnectionProtocol = Protocol.Tcp});
            service.AddCosmosStore<CosmosPostDto>(comsosStoreSettings);
        }
    }
}
