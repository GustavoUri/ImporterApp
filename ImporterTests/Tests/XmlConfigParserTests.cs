using ImporterCore.Core;

namespace ImporterTests.Tests;

using Xunit;

public class XmlConfigParserTests
{
    [Fact]
    public void ParseValidXmlReturnsCorrectConfiguration()
    {
        var xmlContent = @"
            <config>
                <name>Конфигурация 1</name>
                <description>Описание Конфигурации 1</description>
            </config>";
        
        var parser = new XmlConfigParser();

        var config = parser.Parse(xmlContent);

        Assert.NotNull(config);
        Assert.Equal("Конфигурация 1", config.Name);
        Assert.Equal("Описание Конфигурации 1", config.Description);
    }

    [Fact]
    public void ParseInvalidXmlThrowsException()
    {
        var invalidXmlContent = "<config><name>Конфигурация 1</name><description></config>";
        var parser = new XmlConfigParser();

        Assert.Throws<System.Xml.XmlException>(() => parser.Parse(invalidXmlContent));
    }
}