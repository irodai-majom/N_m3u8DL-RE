using System.Reflection;
using FluentAssertions;
using N_m3u8DL_RE.Parser.Config;
using N_m3u8DL_RE.Parser.Extractor;


namespace N_m3u8DL_RE.Parser.Tests.Extractor;

[TestClass]
public class MSSExtractorTest
{
    [TestMethod]
    public void TestMethod1()
    {
        var uri = getResourceUri("Extractor/SuperSpeedway_720.ism.manifest.xml");
        var rawText = File.ReadAllText(uri.LocalPath);
        var parserConfig = new ParserConfig
        {
            Url = uri.AbsoluteUri,
            OriginalUrl = uri.AbsoluteUri
        };
        var extractor = new MSSExtractor(parserConfig);
        var streamSpecs = extractor.ExtractStreamsAsync(rawText).Result;
        streamSpecs.Should().HaveCount(9);
    }

    private Uri getResourceUri(string resourceName)
    {
        var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = System.IO.Path.Combine(directory, resourceName);
        return new Uri("file://" + path);
    }
}