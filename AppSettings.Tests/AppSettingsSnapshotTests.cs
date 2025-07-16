using System.IO;
using Xunit;
using Snapshooter.Xunit;  // <--- Snapshooter namespace

public class AppSettingsSnapshotTests
{
    [Theory]
    [InlineData("appsettings.json")]
    [InlineData("appsettings.Development.json")]
    public void AppSettingsJson_Degismemeli(string fileName)
    {
        // ➊ Çalışma dizininden dosyanın yolunu oluştur
        var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        Assert.True(File.Exists(path), $"{fileName} bulunamadı: {path}");

        // ➋ Dosya içeriğini oku
        var content = File.ReadAllText(path);

        // ➌ Snapshooter ile snapshot testi
        Snapshot.Match(content, fileName);
    }
}
