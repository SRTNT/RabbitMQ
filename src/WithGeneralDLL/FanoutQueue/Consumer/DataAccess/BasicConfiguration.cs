using GeneralDLL.SRTExtensions;

namespace Consumer.DataAccess
{
    public static class BasicConfiguration
    {
        public static WebApplicationBuilder AddRepositoryInjection(this WebApplicationBuilder builder)
        {
            var targetNamespace = ".Repositories";
            var types = AppDomain.CurrentDomain
                                 .GetAssemblies()
                                 .SelectMany(a =>
                                 {
                                     try { return a.GetTypes(); }
                                     catch
                                     { return new List<Type> { typeof(string) }.ToArray(); }
                                 })
                                 .Where(t => t.IsClass &&
                                             !t.IsAbstract &&
                                             !t.Namespace.SRT_StringIsNullOrEmpty() &&
                                             t.Namespace!.Contains(targetNamespace) &&
                                             t.Name.EndsWith("Repo"))
                                 .ToList();

            foreach (var implementation in types)
            {
                var interfaceType = implementation.GetInterfaces().First();
                //var interfaceType = implementation.GetInterface("I" + implementation.Name);

                builder.Services.AddScoped(interfaceType, implementation);
            }

            return builder;
        }

        public static WebApplicationBuilder AddUnitOfWorkInjection(this WebApplicationBuilder builder)
        {
            var targetNamespace = ".UnitOfWork";
            var types = AppDomain.CurrentDomain
                                 .GetAssemblies()
                                 .SelectMany(a =>
                                 {
                                     try { return a.GetTypes(); }
                                     catch
                                     { return new List<Type> { typeof(string) }.ToArray(); }
                                 })
                                 .Where(t => t.IsClass &&
                                             !t.IsAbstract &&
                                             !t.Namespace.SRT_StringIsNullOrEmpty() &&
                                             t.Namespace!.Contains(targetNamespace) &&
                                             t.Name.EndsWith("UOW"))
                                 .ToList();

            foreach (var implementation in types)
            {
                var interfaceType = implementation.GetInterfaces().First();
                //var interfaceType = implementation.GetInterface("I" + implementation.Name);

                builder.Services.AddScoped(interfaceType, implementation);
            }

            // Background Job

            return builder;
        }
    }
}
