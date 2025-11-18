namespace Application.Services;

public static class FileService
{
    public static readonly string[] DocumentsExtensions = new[]
        { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".txt" };

    public static readonly string[] ImagesExtensions = new[]
        { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" };

    public static readonly string[] VideosExtensions = new[]
        { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".mkv" };

    public static readonly string[] AudiosExtensions = new[] { ".mp3", ".wav", ".aac", ".flac", ".ogg" };
    public static readonly string[] CompressedExtensions = new[] { ".zip", ".rar", ".7z", ".tar", ".gz" };

    // Combinação estática que pode ser usada por atributos/validações
    public static readonly string[] AllowedExtensions = DocumentsExtensions
        .Concat(ImagesExtensions)
        .Concat(VideosExtensions)
        .Concat(AudiosExtensions)
        .Concat(CompressedExtensions)
        .ToArray();

    public static bool IsAllowedExtensions(string fileName)
    {
        var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();

        return AllowedExtensions.Contains(fileExtension);
    }

    public static bool IsValidFileSize(long fileSize, long maxFileSizeInBytes)
    {
        return fileSize <= maxFileSizeInBytes;
    }
}