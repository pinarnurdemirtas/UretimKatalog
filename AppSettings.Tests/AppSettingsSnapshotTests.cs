using System.IO;
using Xunit;
using Snapshooter.Xunit;  

public class AppSettingsSnapshotTests
{
    [Theory]
    [InlineData("appsettings.json")]
    [InlineData("appsettings.Development.json")]
    public void AppSettingsJson_Degismemeli(string fileName)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        Assert.True(File.Exists(path), $"{fileName} bulunamadı: {path}");

        var content = File.ReadAllText(path);

        Snapshot.Match(content, fileName);
    }
}
