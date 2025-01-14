﻿using Cibertec.Repositories.Dapper.Northwind;
using Cibertec.UnitOfWork;
using log4net;
using log4net.Core;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Cibertec.MVC.App_Start
{
    public class DIConfig
    {
        public static void ConfigureInjector()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<IUnitOfWork>(() => new NorthwindUnitOfWork(ConfigurationManager.ConnectionStrings["NorthwindConnection"].ToString()));
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.RegisterConditional(typeof(ILog), c => typeof(Log4NetAdapter<>).MakeGenericType(c.Consumer.ImplementationType), Lifestyle.Singleton, c => true);

            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }

    public class Log4NetAdapter<T> : LogImpl
    {
        public Log4NetAdapter() : base(LogManager.GetLogger(typeof(T)).Logger) { }
    }
}