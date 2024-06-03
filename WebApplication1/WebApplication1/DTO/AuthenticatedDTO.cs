namespace WebApplication1.DTO;

public record AuthenticatedDTO(string Token, string RefreshToken, DateTime refreshTokenExpiration);