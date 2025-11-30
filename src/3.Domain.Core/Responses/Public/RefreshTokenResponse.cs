using System.ComponentModel.DataAnnotations;
using Domain.Shared.Responses;

namespace Domain.Core.Responses.Public;

public class RefreshTokenResponse : CommandResponse
{
    /// <summary>
    /// Jwt access token - Expired in 1 hour
    /// </summary>
    /// <example> eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyQGV4YW1wbGUuY29tIiwianRpIjoiMTIzNDU2Nzg5MCIsIm5hbWUiOiJKb2huIERvZSIsImlhdCI6MTUxNjIzOTAyMn0.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c</example>
    [DataType(DataType.Text)]
    public string Token { get; set; }
    
    /// <summary>
    /// Refresh token - Expired in 30 days
    /// </summary>
    /// <example>Hz8/n9+d3s4k6m8n0p2q4r6s8t0u2v4w6x8y0z2A+B=</example>
    [DataType(DataType.Text)]
    public string RefreshToken { get; set; }
}