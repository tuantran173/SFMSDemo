using System.Text.Json.Serialization;

namespace SFMSSolution.Domain.ValueObjects.Common
{
    public class RefreshToken
    {
        public string Token { get; }
        public DateTime? Expiry { get; }

        public RefreshToken(string token, DateTime? expiry)
        {
            Token = token ?? string.Empty;
            Expiry = expiry;
        }

        // Factory method nếu cần
        public static RefreshToken Create(string token, DateTime? expiry)
        {
            return new RefreshToken(token, expiry);
        }

        public bool Equals(RefreshToken other)
        {
            if (other is null)
                return false;
            return Token == other.Token && Nullable.Equals(Expiry, other.Expiry);
        }

        public override bool Equals(object obj) => Equals(obj as RefreshToken);

        public override int GetHashCode() => HashCode.Combine(Token, Expiry);

        public override string ToString() => Token;
    }
}

