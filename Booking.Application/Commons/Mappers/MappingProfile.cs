using AutoMapper;
using System.Reflection;

namespace Booking.Application.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var typeIMapping = typeof(IMapping);
            var types = assembly.GetExportedTypes()
                .Where(t => typeIMapping.IsAssignableFrom(t) && !t.IsInterface)
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
