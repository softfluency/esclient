# The .NET esclient Project by Softfluency

## New version on NuGet

1. In .csproj file, under \<PropertyGroup\> in \<Version\>1.0.0.0\<Version\> - change version  
2. From terminal in project  
dotnet pack --configuration Release (a new folder in structure is created)  
The folder can be default (bin -> Release...) or named folder from \<PackageOutputPath\> tag, under \<PropertyGroup\> of .csproj  
3. From terminal in project  
dotnet nuget push *Path to .nupkg packaged version (.\\.nupkg\projectName.1.0.0.0.nupkg)* --source https://api.nuget.org/v3/index.json --api-key *uniqueApiKey*  