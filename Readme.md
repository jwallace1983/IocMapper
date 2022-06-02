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
 
Easily integrate dependencies across multiple libraries whether using onion/clean architecture, layered architecture, or just separate libraries.

## A Note on Complexity

This tool is meant to make IOC mapping easier. It is not intended to cover all possible ways to register dependencies, otherwise it would just become the tool it is trying to simplify and become too complex itself.
