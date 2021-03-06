﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

namespace TestProjectSavchenko.Infrastructure
{
    public class WindsorControllerFactory: DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            kernel.ReleaseComponent(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }
            return (IController)kernel.Resolve(controllerType);
        }
    }
}