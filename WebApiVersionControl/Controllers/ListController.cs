using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebApiVersionControl.Controllers
{
    public class ListController : Controller
    {

        private IActionDescriptorCollectionProvider _actionProvider;

        public ListController(ApplicationPartManager applicationPartManager, IActionDescriptorCollectionProvider actionProvider)
        {
            _applicationPartManager = applicationPartManager;
            _actionProvider = actionProvider;
        }
        private ApplicationPartManager _applicationPartManager;

        [Route("api/test/list")]
        public IEnumerable<dynamic> List()
        {
            var controllerFeature = new ControllerFeature();
            _applicationPartManager.PopulateFeature(controllerFeature);
            var data = controllerFeature.Controllers.Select(x => new
            {
                Namespace = x.Namespace,
                Controller = x.FullName,
                ModuleName = x.Module.Name,
                Actions = x.DeclaredMethods.Where(m => m.IsPublic && !m.IsDefined(typeof(NonActionAttribute))).Select(y => new
                {
                    Name = y.Name,
                    ParameterCount = y.GetParameters().Length,
                    Parameters = y.GetParameters()
                      .Select(z => new
                      {
                          z.Name,
                          z.ParameterType.FullName,
                          z.Position,
                          Attrs = z.CustomAttributes.Select(m => new
                          {
                              FullName = m.AttributeType.FullName,
                          })
                      })
                }),
            });
            return data;
        }

        [Route("api/test2/list")]
        public IEnumerable<dynamic> ListTwoTest()
        {
            var actionDescs = _actionProvider.ActionDescriptors.Items.Cast<ControllerActionDescriptor>().Select(x => new
            {
                ControllerName = x.ControllerName,
                ActionName = x.ActionName,
                DisplayName = x.DisplayName,
                RouteTemplate = x.AttributeRouteInfo.Template,
                Attributes = x.MethodInfo.CustomAttributes.Select(z => new {
                    TypeName = z.AttributeType.FullName,
                    ConstructorArgs = z.ConstructorArguments.Select(v => new {
                        ArgumentValue = v.Value
                    }),
                    NamedArguments = z.NamedArguments.Select(v => new {
                        v.MemberName,
                        TypedValue = v.TypedValue.Value,
                    }),
                }),
                ActionId = x.Id,
                x.RouteValues,
                Parameters = x.Parameters.Select(z => new {
                    z.Name,
                    PropertyType = z.ParameterType.GetProperties().Select(y => new
                    { 
                         ArguPropertyName = y.Name,
                        ArguPropertyType = y.PropertyType
                    })
                })
            });

            return actionDescs;
        }
    }
}
