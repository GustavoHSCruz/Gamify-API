using Domain.Shared.Entities;
using Domain.Shared.ValueObjects;

namespace Application.Services
{

    public static class PatcherService
    {
        public static void ApplyPatch<T>(T entity, List<PatchModel> patches) where T : Entity
        {
            foreach (var patch in patches)
            {
                var property = typeof(T).GetProperty(patch.Path.TrimStart('/'), System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                if (property != null && property.CanWrite)
                {
                    var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    var safeValue = (patch.Value == null) ? null : Convert.ChangeType(patch.Value, targetType);
                    property.SetValue(entity, safeValue, null);
                }
            }
        }
    }
}
