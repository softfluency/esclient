# The .NET esclient Project by Softfluency

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