﻿using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using InstaAPI.Services.BusinessLogicServices;
using InstaAPI.Services.BusinessLogicServices.Interfaces;

namespace InstaAPI.Helpers
{
    public class AutofacBootstrapper
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();                        
            AddRegisterations(builder);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            // Set the dependency resolver for Web API.
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;            
        }
        
        private static void AddRegisterations(ContainerBuilder builder)
        {            
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();
            builder.RegisterType<InstagramApiService>().As<IInstagramApiService>().InstancePerRequest();
        }
    }
}