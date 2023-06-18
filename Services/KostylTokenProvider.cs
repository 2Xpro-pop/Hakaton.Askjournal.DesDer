namespace Destinationosh.Services
{
    /// <summary>
    /// YOU NEED TO GET RID OF THIS
    /// </summary>
    public class KostylTokenProvider
    {
        public static readonly TimeSpan ExpiredTime = TimeSpan.FromHours(1);
        public Dictionary<string, DateTime> Tokens { get; } = new();

        public void CreateToken()
        {
            foreach(var token in Tokens)
            {
                if(DateTime.Now - token.Value > ExpiredTime)
                {
                    Tokens.Remove(token.Key);
                }
            }

            Tokens.Add(Guid.NewGuid().ToString(), DateTime.Now);
        }
    }
}
