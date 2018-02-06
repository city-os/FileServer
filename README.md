# Simple File Server

## ======= **_This project is actually not production ready_** =======

This project aims to give the ability to run a simple file server who manage documents and images.

## Build status

Component|Build status
--- | ---
Core | [![Build status](https://ci.appveyor.com/api/projects/status/0qb358krhx4pyj1v?svg=true)](https://ci.appveyor.com/project/Authfix/fileserver)
Provider.FileSystem | [![Build status](https://ci.appveyor.com/api/projects/status/l4t0wun3wwe8m83r?svg=true)](https://ci.appveyor.com/project/Authfix/fileserver-n7n72)

## Getting Started

### Installing

#### Install the packages

To make things work, you need to install **two packages** first.

The first one :

```
Package-Install CityOs.SimpleFileServer
```

This is the base package. It will allow you to use the server. It contains the base classes/main mechanisms.

The second one depends of file provider. **Actually we have only file system provider**

```
Package-Install CityOs.SimpleFileServer.FileSystemProvider
```

#### Update your Startup.cs

After packages installation process, you can update your Startup.cs.

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddSimpleFileServer(options =>
    {
        options.UseFileSystem();
    });
}
```

```
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    // Register Mvc after the file server
    app.UseSimpleFileServer();

    app.UseMvcWithDefaultRoute();
}
```

And that's it, your server is working.

## Running the tests

To run the tests, you just have to launch Visual Studio ;)

## Built With

* [Dotnet Core](https://www.microsoft.com/net/) - The base framework used
* [Image Sharp](https://github.com/SixLabors/ImageSharp) - Used to manipulate images

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/city-os/FileServer/tags). 

## Authors

* **Thomas Bailly** - *Initial work* - [Authfix](https://github.com/Authfix)

See also the list of [contributors](https://github.com/city-os/FileServer/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details