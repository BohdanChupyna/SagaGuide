using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SagaGuide.Infrastructure.JsonConverters;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace SagaGuide.IntegrationTests.Setup;

public abstract class TestBase : IAsyncLifetime
{
    public TestBase(ServerFixture serverFixture, ITestOutputHelper testOutputHelper)
    {
        ServerFixture = serverFixture;
        TestOutputHelper = testOutputHelper;

        serverFixture.ChangeTestOutputHelper(testOutputHelper);
    }

    protected ServerFixture ServerFixture { get; }
    protected ITestOutputHelper TestOutputHelper { get; }
    
    // public byte[] ExtractResource(string filename)
    // {
    //     using var resFileStream = ExtractResourceAsStream(filename);
    //     if (resFileStream == null)
    //         return null;
    //     var ba = new byte[resFileStream.Length];
    //     resFileStream.Read(ba, 0, ba.Length);
    //     return ba;
    // }
    //
    // public Stream ExtractResourceAsStream(string filename)
    // {
    //     var a = Assembly.GetExecutingAssembly();
    //     var resources = GetType().Assembly.GetManifestResourceNames();
    //     var fileNameResource = resources.FirstOrDefault(x => x.EndsWith(filename));
    //     if (fileNameResource == null)
    //         throw new Exception("Filename not found in embedded resource (set build action)" + filename);
    //     return a.GetManifestResourceStream(fileNameResource);
    // }

    // public ulong ExtractResourceSize(string filename)
    // {
    //     using var resFileStream = ExtractResourceAsStream(filename);
    //     if (resFileStream == null)
    //         return 0;
    //
    //     Debug.Assert(resFileStream.Length >= 0);
    //     return (ulong)resFileStream.Length;
    // }

    protected async Task<HttpResponseMessage> ClientDeleteAsync(string url, HttpStatusCode expectedStatusCode)
    {
        TestOutputHelper.WriteLine($"Delete {url}");
        var webResponse = await ServerFixture.Client.DeleteAsync(url);
        webResponse.StatusCode.Should().Be(expectedStatusCode);
        await PrettyWriteContent(webResponse);
        return webResponse;
    }

    protected async Task<T> ClientDeleteAsync<T>(string url, HttpStatusCode expectedStatusCode)
    {
        TestOutputHelper.WriteLine($"Delete {url}");
        var webResponse = await ServerFixture.Client.DeleteAsync(url);
        webResponse.StatusCode.Should().Be(expectedStatusCode);
        
        await PrettyWriteContent(webResponse);

        var vm = await DeserializeHttpResponseBodyAsync<T>(webResponse);
        vm.Should().NotBeNull();
        return vm!;
    }
    
    protected async Task<HttpResponseMessage> ClientPostAsync<T>(string url, T postData, HttpStatusCode expectedStatusCode)
    {
        TestOutputHelper.WriteLine($"Post {url}");
        
        var json = JsonConverterWrapper.Serialize(postData, JsonSettingsWrapper.Create());
        var webResponse = await ServerFixture.Client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
        await PrettyWriteContent(webResponse);
        
        webResponse.StatusCode.Should().Be(expectedStatusCode);
        
        await PrettyWriteContent(webResponse);
        return webResponse;
    }
    
    protected async Task<TR> ClientPostAsync<T, TR>(string url, T postData, HttpStatusCode expectedStatusCode)
    {
        TestOutputHelper.WriteLine($"Post {url}");
        
        var json = JsonConverterWrapper.Serialize(postData, JsonSettingsWrapper.Create());
        var webResponse = await ServerFixture.Client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
        await PrettyWriteContent(webResponse);
        
        webResponse.StatusCode.Should().Be(expectedStatusCode);
        
        await PrettyWriteContent(webResponse);

        var vm = await DeserializeHttpResponseBodyAsync<TR>(webResponse);
        vm.Should().NotBeNull();
        return vm!;
    }

    protected async Task<TR> ClientPostEmptyBodyAsync<TR>(string url, HttpStatusCode expectedStatusCode)
    {
        TestOutputHelper.WriteLine($"Post {url}");
        
        var webResponse = await ServerFixture.Client.PostAsync(url, new StringContent("", Encoding.UTF8, "application/json"));
        await PrettyWriteContent(webResponse);
        
        webResponse.StatusCode.Should().Be(expectedStatusCode);
        
        await PrettyWriteContent(webResponse);

        var vm = await DeserializeHttpResponseBodyAsync<TR>(webResponse);
        vm.Should().NotBeNull();
        return vm!;
    }
    
    protected async Task<TR> ClientPutAsync<T, TR>(string url, T putData, HttpStatusCode expectedStatusCode)
    {
        TestOutputHelper.WriteLine($"Put {url}");

        var json = JsonConverterWrapper.Serialize(putData, JsonSettingsWrapper.Create());
        var webResponse = await ServerFixture.Client.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
        await PrettyWriteContent(webResponse);
        
        webResponse.StatusCode.Should().Be(expectedStatusCode);
        
        var vm = await DeserializeHttpResponseBodyAsync<TR>(webResponse);
        vm.Should().NotBeNull();
        return vm!;
    }
    
    protected async Task<HttpResponseMessage> ClientPutAsync<T>(string url, T putData, HttpStatusCode expectedStatusCode)
    {
        TestOutputHelper.WriteLine($"Put {url}");

        var json = JsonConverterWrapper.Serialize(putData, JsonSettingsWrapper.Create());
        var webResponse = await ServerFixture.Client.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
        await PrettyWriteContent(webResponse);
        
        webResponse.StatusCode.Should().Be(expectedStatusCode);
        return webResponse;
    }
    
    protected async Task<T> ClientGetAsync<T>(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK) => await ClientGetAsync<T>(new Uri(url, UriKind.Relative), expectedStatusCode);

    protected async Task<T> ClientGetAsync<T>(Uri url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    {
        if (ServerFixture.Client == null)
            throw new NullReferenceException(nameof(ServerFixture));
        TestOutputHelper.WriteLine($"Get {url}");
        var webResponse = await ServerFixture.Client.GetAsync(url);
        webResponse.StatusCode.Should().Be(expectedStatusCode);
        
        await PrettyWriteContent(webResponse);
        
        var vm = await DeserializeHttpResponseBodyAsync<T>(webResponse);
        vm.Should().NotBeNull();
        return vm!;
    }

    protected async Task<HttpResponseMessage> ClientGetAsync(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    {
        if (ServerFixture.Client == null)
            throw new NullReferenceException(nameof(ServerFixture));
        TestOutputHelper.WriteLine($"Get {url}");
        var webResponse = await ServerFixture.Client.GetAsync(url);
        webResponse.StatusCode.Should().Be(expectedStatusCode);
        return webResponse;
    }

    private async Task PrettyWriteContent(HttpResponseMessage webResponse)
    {
        var content = await webResponse.Content.ReadAsStringAsync();

        foreach (var item in content.Split("\\n").SelectMany(x => x.Split("\",\""))) TestOutputHelper.WriteLine(item);
    }

    protected async Task<T?> DeserializeHttpResponseBodyAsync<T>(HttpResponseMessage httpResponseMessage)
    {
        var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
        var response = JsonConverterWrapper.Deserialize<T>(responseString, JsonSettingsWrapper.Create());
        return response;
    }
    
    public async Task InitializeAsync() => await ServerFixture.ResetDbAsync();

    public Task DisposeAsync() => Task.CompletedTask;
}