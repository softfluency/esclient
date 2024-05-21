# The .NET esclient Project by Softfluency

## Usage  

Esclient is an application for checking the status of Elasticsearch, its availability, indices and single indexes.  
The application is used from cmd or terminal, by entering verbs and options:  
**Verbs**  
status - for ES server status  
indices - for listing all indexes written in ES  
index - for single index info  
**Options**  
-u - required option, ES server url  
-i - option used with index, for getting info on a certain index  

## Installation  

The application is available as a .NET tool you can install and call from the shell/command line.  
*dotnet tool install --global esclient --version*

**Usage examples**  
esclient status -u https://[username]:[password]@yourelasticsearch.com - returns status of ES server  
esclient indices -u https://[username]:[password]@yourelasticsearch.com - returns all indices  
esclient index -u https://[username]:[password]@yourelasticsearch.com -i [indexName] returns info on entered index

## New version on NuGet

1. In .csproj file, under \<PropertyGroup\> in \<Version\>1.0.0.0\<Version\> - change version  
2. From terminal in project  
dotnet pack --configuration Release (a new folder in structure is created)  
The folder can be default (bin -> Release...) or named folder from \<PackageOutputPath\>, under \<PropertyGroup\> of .csproj  
3. From terminal in project  
dotnet nuget push *Path to .nupkg packaged version (.\\.nupkg\projectName.1.0.0.0.nupkg)* --source https://api.nuget.org/v3/index.json --api-key *uniqueApiKey*  

## How to publish to local folder

From terminal in project  
dotnet publish -c Release -r *win-x64* --self-contained  
This command will build the application in Release mode, for the Windows x64 platform, and include all required dependencies.  
For other platforms: *win-x86*, *linux-x64*...  
The *--self-contained* option will include the .NET Core runtime with the application, so it won't have to be installed separately. 