namespace Application.Services
{
    public static class EnumService
    {
        public static string GetDescription<T>(this T enumerationValue) where T : Enum
        {
            var fieldInfo = enumerationValue.GetType().GetField(enumerationValue.ToString());
            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(System.ComponentModel.DescriptionAttribute), false) as System.ComponentModel.DescriptionAttribute[];
            if (descriptionAttributes == null) return enumerationValue.ToString();
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Description : enumerationValue.ToString();
        }
    }
}
