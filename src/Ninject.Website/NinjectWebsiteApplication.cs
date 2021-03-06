#region License
// 
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2009, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 
#endregion
#region Using Directives
using System;
using CommonServiceLocator.NinjectAdapter;
using Microsoft.Practices.ServiceLocation;
using Ninject.Web.Mvc;
using Ninject.Website.Framework;
using Ninject.Website.Services.Persistence;
using Spark.Web.Mvc;
#endregion

namespace Ninject.Website
{
	public class NinjectWebsiteApplication : NinjectHttpApplication
	{
		protected override void OnApplicationStarted()
		{
			var serviceLocator = new NinjectServiceLocator(Kernel);
			ServiceLocator.SetLocatorProvider(() => serviceLocator);

			RegisterAllControllersIn("Ninject.Website");

			var bootstrapper = Kernel.Get<Bootstrapper>();

			bootstrapper.SetUpViewEngine();
			bootstrapper.RegisterRoutes();
			bootstrapper.GenerateJavascriptForRouting();
		}

		protected override IKernel CreateKernel()
		{
			var kernel = new StandardKernel();

			kernel.Load<PersistenceModule>();

			return kernel;
		}
	}
}