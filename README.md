# bluehands Log Extensions
This repo contains the source code for the bluehands log extensions. The goal is to help developers to be able to debug there application with log files. The main purpose of this project is:
* Add advance traceability. 
* Easy to use
* No need to add class and method name in to the log message
* Support of correlation in distributed systems

# Basic Usage

Get an instance of the logger by declaring a log member in a class.

```csharp
public class MyWorkerClass
{
    private readonly Log<MyWorkerClass> m_Log = new Log<MyWorkerClass>();
```
In your log output you can refer to the class name and will get *MyWorkerClass*. 

For basic logging with a log level use 
```csharp
m_Log.Debug("Some debug messages");
```
The name of the calling method can also be referred in the log output.

Some times it takes a long time to evaluate the output variable. For example if you want to print out a xml fragment, it takes time to get the string representation. For this you can check if the log level is enabled with
```csharp
    if (m_Log.IsDebugEnabled)
    {
 ```
A better approach is to use lambda expressions. The underlaying system will do the check. So use
```csharp
    m_Log.Debug(() => $"The text parameter is '{myXmlFragment}'");
```
For automatic enter and leave tracing of methods use the AutoTrace feature.
```csharp
    using (m_Log.AutoTrace(() => $"The text parameter is '{text}'"))
    {
```
In distributed systems you want to trace correlated method call. On incoming method calls add the Correlation to the log
```csharp
    m_Log.Correlation = *Your correlation id goes here* 
```
The underlying system will add the correlation to the logical call context.

# Log output
The log extensions are using NLog as logging engine. So all NLog targets can be used. We add some custom properties to the layout which can be referred:
* Type: The class type
* Class: The class name of the type
* Method: The name of the method
* CallContext: A logical stack of method calls
* Correlation: A string to trace correlated operations over different systems
* indent: An amount of whitespace based on the logical call stack. Helpful for formatting log output

A typical layout of a target can be for example:

<code>layout="${longdate} ${level:uppercase=true}  ${event-properties:item=Correlation} ${event-properties:item=CallContext} ${event-properties:item=Class}:${event-properties:item=Method} ${message} ${exception:format=tostring}"</code>

# Example
The call to *MyComposingMethod* in this example 
```csharp
    public class MyClass
    {
        private readonly MyWorkerClass m_MyWorkerClass;
        private readonly Log<MyClass> m_Log = new Log<MyClass>();

        public MyClass()
        {
            m_MyWorkerClass = new MyWorkerClass();
        }

        public async Task<string> MyComposingMethod(string text)
        {
            m_Log.Correlation = Guid.NewGuid().ToString();
            using (m_Log.AutoTrace(() => $"The text parameter is '{text}'"))
            {
                try
                {
                    await m_MyWorkerClass.MyAsyncWorkerMethod(text);
                    m_MyWorkerClass.MySyncWorkerMethod(text);
                    return text;
                }
                catch (Exception ex)
                {
                    m_Log.Error("Something goes wrong", ex);
                    throw;
                }
            }
        }
    }

    public class MyWorkerClass
    {
        private readonly Log<MyWorkerClass> m_Log = new Log<MyWorkerClass>();
        public void MySyncWorkerMethod(string text)
        {
            using (m_Log.AutoTrace())
            {
                m_Log.Debug("Some debug messages");
                File.WriteAllText("MyFile.txt", text);
            }
        }

        public Task MyAsyncWorkerMethod(string text)
        {
            using (m_Log.AutoTrace())
            {
                m_Log.Info("Some information messages");
                var t = Task.Run(() => File.WriteAllText("MyFile.txt", text));
                return t;
            }
        }
    }
```
will produce this output.

2018-10-14 17:59:11.3247 TRACE  3ccf6cf6-dc2b-4f26-aa91-6f4fb562d19e MyClass:MyComposingMethod The text parameter is 'Hello logging' [Enter]<br> 
2018-10-14 17:59:11.3571 TRACE  3ccf6cf6-dc2b-4f26-aa91-6f4fb562d19e &nbsp;4da6 MyWorkerClass:MyAsyncWorkerMethod  [Enter]<br> 
2018-10-14 17:59:11.4705 INFO&nbsp;&nbsp;  3ccf6cf6-dc2b-4f26-aa91-6f4fb562d19e &nbsp;&nbsp;4da6_d20d MyWorkerClass:MyAsyncWorkerMethod Some information messages<br> 
2018-10-14 17:59:11.5079 TRACE  3ccf6cf6-dc2b-4f26-aa91-6f4fb562d19e &nbsp;4da6 MyWorkerClass:MyAsyncWorkerMethod  [Leave 136.2438ms]<br> 
2018-10-14 17:59:11.5079 TRACE  3ccf6cf6-dc2b-4f26-aa91-6f4fb562d19e &nbsp;4da6 MyWorkerClass:MySyncWorkerMethod  [Enter]<br> 
2018-10-14 17:59:11.5400 DEBUG  3ccf6cf6-dc2b-4f26-aa91-6f4fb562d19e &nbsp;&nbsp;4da6_2c08 MyWorkerClass:MySyncWorkerMethod Some debug messages<br> 
2018-10-14 17:59:11.5784 TRACE  3ccf6cf6-dc2b-4f26-aa91-6f4fb562d19e &nbsp;4da6 MyWorkerClass:MySyncWorkerMethod  [Leave 36.0404ms]<br> 
2018-10-14 17:59:11.6139 TRACE  3ccf6cf6-dc2b-4f26-aa91-6f4fb562d19e MyClass:MyComposingMethod The text parameter is 'Hello logging' [Leave 270.8393ms]<br> 

# Ilogger support

The logging methods exists for convenience in both namespaces: *Bluehands.Diagnostics.LogExtensions* and *Microsoft.Extensions.Logging*. They differs in naming, e.g. *log.Error* vs *log.LogError* or *log.AutoTrace* vs *log.LogScoped*. Internally they use the same structures and have the same functionality. If you do not want a dependency to the bluehands namespace use the Microsoft.Extensions.Logging methods.

To start using the Extensions in your .Net core application add a reference to *Bluehands.Diagnostics.LogExtensions* and *Bluehands.Diagnostics.LogExtensions.Web* NuGet package. In your *startup.cs* class add 

```csharp
services.AddLogging(
    builder =>
    {
        builder.AddLogEnhancementWithNLog();  
    }
);
```

Now let the DI inject a logger in to your controller class

```csharp
private readonly ILogger<WeatherForecastController> _logger;

public WeatherForecastController(ILogger<WeatherForecastController> logger)
{
    _logger = logger;
}
```

> **_NOTE:_** You must use the generic *ILogger<>* to get advantage of the logging enhancements.

# Correlation in web applications

When an incoming request has a *x-correlation* Header the value is automatically set in to the log correlation buffer. Also add tracing for the requests.

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    var log=new Log<Startup>();
    app.UseLogCorrelation(log);
    app.UseRequestLogTracing(log);
}
```

# Contribution
Please feel free to send us a pull request. The [bluehands development guidelines](https://github.com/bluehands/development-guidelines) are part of this project.