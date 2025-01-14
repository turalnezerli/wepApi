﻿using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {//bura yazilanlari starup dada yazmaaq olar
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager,MemoryCacheManager>();
        }
    }
}
