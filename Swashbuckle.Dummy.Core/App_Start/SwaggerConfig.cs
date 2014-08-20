﻿using System;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Application;
using Swashbuckle.Dummy.Controllers;
using Swashbuckle.Dummy.SwaggerExtensions;

namespace Swashbuckle.Dummy
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Swashbuckle.Bootstrapper.Init(config);

            SwaggerSpecConfig.Customize(c =>
                {
                    c.IgnoreObsoleteActions();

                    c.SupportMultipleApiVersions((apiDesc) =>
                        {
                            if (apiDesc.ActionDescriptor.ControllerDescriptor.ControllerName == "Products")
                                return new[] { "1.0", "2.0" };
                            return new[] { "2.0" };
                        });

                    //c.PolymorphicType<Animal>(ac => ac
                    //    .DiscriminateBy(a => a.Type)
                    //    .SubType<Kitten>());

                    c.OperationFilter<AddStandardResponseCodes>();
                    c.OperationFilter<AddAuthResponseCodes>();

                    c.IncludeXmlComments(GetXmlCommentsPath());
                });

            SwaggerUiConfig.Customize(c =>
            {
                c.SupportHeaderParams = true;
                c.DocExpansion = DocExpansion.List;
                c.SupportedSubmitMethods = new[] { HttpMethod.Get, HttpMethod.Post, HttpMethod.Put, HttpMethod.Head };
                c.EnableDiscoveryUrlSelector();
                //c.InjectJavaScript(typeof(SwaggerConfig).Assembly, "Swashbuckle.Dummy.SwaggerExtensions.testScript1.js");
                //c.InjectStylesheet(typeof(SwaggerConfig).Assembly, "Swashbuckle.Dummy.SwaggerExtensions.testStyles1.css");
            });

        }

        private static string GetXmlCommentsPath()
        {
            return String.Format(@"{0}\XmlComments.xml", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}