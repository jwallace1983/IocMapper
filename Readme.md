# IocMapper

## Overview

The IocMapper is a tool to simplify mapping interfaces and implementations for dependency injection and IOC (Inversion of Control).

For instance, with Microsoft Dependency Injection, the following code is very common for IOC mappings:

    builder.Services
        .AddTransient<IUserService, UserService>()
        .AddTransient<IAuthService, AuthService>()
        .AddTransient<IDataService, DataService>()
        .AddTransient<ISecurityService, TokenService>()
        .AddTransient<IEmailService, EmailService>()
        .AddTransient<IEncryptionService, EncryptionService>();

The IocMapper will make this easier by using a simple class attribute on each implementation class:

    [Ioc]
    public class UserService : IUserService { }

Then, all mappings can be automatically added with much simpler statement:

    builder.Services.AddIocMappings();

## Key Benefits

Attribute-driven IOC mappings will simplify day-to-day coding.

First, keep code that changes together close together:
* Save time with mappings near the implementation, not some shared file in a completely separate part of project structure

Also, avoid constantly changing a central configuration file as new services are added:
* Simplify change histories of a single, global configuration file
* Avoid merge conflicts with multiple teams or developers constantly changing the same file

## Lifetime Control

Natively define and control lifetimes of implementation objects directly in the mapping:

    [Ioc(Lifetimes.Singleton)]
    public class SecurityService : ISecurityService { }

## Map Specific Implementations

Map to specific interfaces, even when classes inherit base classes or implement multiple interfaces:

    [Ioc(Target = typeof(IUserService))]
    public class UserService : ServiceBase, IUserProxy, IUserService { }

Also, map a class to itself to inject specific class without a specific interface implementation:

    [Ioc(Lifetimes.Singleton, Target = typeof(Settings))]
    public class Settings { }

## Map Across Libraries

Add external libraries in addition to calling library with just a type from each library to include:

    builder.Services.AddIocMappings(
        typeof(IExternalService),
        typeof(IOtherService));
 
Easily integrate dependencies across multiple libraries whether using onion/clean architecture,
layered architecture, or just separate libraries.

## A Note on Complexity

This tool is meant to make IOC mapping easier. It is not intended to cover all possible ways to
register dependencies, otherwise it would just become the tool it is trying to simplify and become
too complex itself.

## Mediator

In line with the goal of simplifying IOC mapping, the IocMapper also includes a simple mediator
pattern. First, implement one or more requests and request handlers. They can be in the same file or in
entirely different libraries (as long as all libraries are added to the configuration above).

To create a simple request (no response is expected), then just implement the IRequest interface:

    public class SimpleRequest : IRequest { ... }

The handler can then be created to handle any requests from the mediator (such as Onion Architecture).

    public class SimpleRequestHandler : IRequestHandler<SimpleRequest>
    {
        public async Task Handle(SimpleRequest request, CancellationToken cancellationToken)
        {
            // Do something
        }
    }

The handler can also just be defined within the request class itself if not needing to separate
the request and handler to separate libraries or files.

    public class SimpleRequest : IRequest
    {
        public class Handler : IRequestHandler<SimpleRequest>
        {
            public async Task Handle(SimpleRequest request, CancellationToken cancellationToken)
            {
                // Do something
            }
        }
    }

A request can also have a response.

    public class AddRequest(int value1, int value2) : IRequest<int>
    {
        public int Value1 { get; } = value1;
        public int Value2 { get; } = value2;

        public class Handler : IRequestHandler<AddRequest, int>
        {
            public async Task<int> Handle(AddRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(request.Value1 + request.Value2);
            }
        }
    }

Finally, inject the `IMediator` into anything needing to send requests.

    public class TestService(IMediator mediator)
    {
        private readonly IMediator _mediator = mediator;
        
        public async Task<int> Add(int a, int b)
            => await _mediator.Send(new AddRequest(a, b));
    }
