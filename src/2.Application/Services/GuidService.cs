namespace Application.Services
{
    public static class GuidService
    {
        public static bool IsNullOrEmpty(Guid? id)
        {
            return id == null || id == Guid.Empty;
        }
    }
}
