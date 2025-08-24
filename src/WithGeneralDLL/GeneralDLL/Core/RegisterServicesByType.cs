using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core
{
    internal static class RegisterServicesByType
    {
        #region Register Services With Type Dynamically
        public static IServiceCollection SRT_RegisterServices<TInterface, TInterfaceSearchInChild>(this IServiceCollection services)
        {
            var types = SRT_GetTypes<TInterface, TInterfaceSearchInChild>();

            var interfaceType = typeof(TInterface);
            foreach (var type in types)
            {
                services.AddScoped(interfaceType, type);
            }

            return services;
        }
        #endregion

        #region Get Types
        public static List<Type> SRT_GetTypes<TInterface, TInterfaceSearchInChild>()
        {
            var interfaceTypeSearchInChild = typeof(TInterfaceSearchInChild);
            var namespacePrefixSearchInChild = interfaceTypeSearchInChild.Namespace;

            return SRT_GetTypes<TInterface>(namespacePrefixSearchInChild);
        }
        public static List<Type> SRT_GetTypes<TInterface>(string namespacePrefixSearchInChild)
        {
            var interfaceType = typeof(TInterface);

            var types = AppDomain.CurrentDomain
                                 .GetAssemblies()
                                 .SelectMany(a =>
                                 {
                                     try { return a.GetTypes(); }
                                     catch
                                     { return new List<Type> { typeof(string) }.ToArray(); }
                                 })
                                 .Where(t => t != null &&
                                             t.IsClass &&
                                             !t.IsAbstract &&
                                             t.GetInterfaces().Contains(interfaceType) &&
                                             t.Namespace != null &&
                                             t.Namespace.StartsWith(namespacePrefixSearchInChild))
                                 .ToList();

            return types;
        }
        #endregion
    }
}
