<p align="center"><img src="assets/logo.svg?raw=true" width="350" alt="Logo"></p>

<h1 align="center"> Linq </h1>  

<p align="center">  
<img src="https://dev.azure.com/dimenicsbe/Utilities/_apis/build/status/dimenics.linq?branchName=master"/>
<img src="https://feeds.dev.azure.com/dimenicsbe/_apis/public/Packaging/Feeds/a7b896fd-9cd8-4291-afe1-f223483d87f0/Packages/a4ea1a44-b4ee-49dd-ba2f-eff013a1c9ce/Badge"/>
<img src="https://img.shields.io/azure-devops/coverage/dimenicsbe/Utilities/155/master" />
<img src="https://github.com/dimenics/linq/workflows/CodeQL/badge.svg" />
<img src="https://img.shields.io/badge/License-MIT-blue.svg" />
<img src="https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square" />
</p>

Dime.Linq is a utility library which contains a set of extensions for the System.Linq namespace.

## Getting Started

- You must have Visual Studio 2019 Community or higher.
- The dotnet cli is also highly recommend

## About this project

Some interesting extension methods in this assembly include:

- `Fork`: Not only returns the filtered set but also the rejected pile of records.
- `DistinctBy`: returns a distinct list based on an expression condition.
- `Pipe`: an all purpose extension method which gives you access to the entire collection.
- `SelectTry`: Generic method to return a different type from the select statement.


## Build and Test

- Run dotnet restore
- Run dotnet build
- Run dotnet test

## Installation

Use the package manager NuGet to install Dime.Linq:

`dotnet add package Dime.Linq`

## Usage

``` csharp
using System.Linq;

public void MyMethod(IEnumerable<Customer> customerList)
{
     (IEnumerable<Customer> success, IEnumerable<Customer> failed)
       = customerList.Fork(x => x.Address == "New York");    
}
```

## Contributing

Pull requests are welcome. Please check out the contribution and code of conduct guidelines.