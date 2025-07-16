using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Xunit;

public class AppSettingsHashTests
{
    static readonly Dictionary<string, string> ExpectedHashes = new()
    {
        { "appsettings.json",             "C56F586710A59B0BCAE315246ED064E49505B213A84E32850BD3AF9B6D108343" },
        { "appsettings.Development.json",  "73F95F9E0CEB205FC1C4DC50C07697FCFA29D7087868C2AEF1D504CB38C771EC" },
    };

    public static IEnumerable<object[]> HashData =>
        ExpectedHashes
            .Select(kvp => new object[] { kvp.Key, kvp.Value });

    [Theory]
    [MemberData(nameof(HashData))]
    public void AppSettings_Icerigi_Degismemeli(string fileName, string expectedHash)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        Assert.True(File.Exists(path), $"{fileName} bulunamadı: {path}");

        var content    = File.ReadAllText(path, Encoding.UTF8);
        var actualHash = ComputeHash(content);

        Assert.Equal(expectedHash, actualHash);
    }

    private static string ComputeHash(string content)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(content);
        var hash  = sha.ComputeHash(bytes);
        return string.Concat(hash.Select(b => b.ToString("X2")));
    }
}
