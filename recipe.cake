#load nuget:?package=Cake.Recipe&version=2.2.1

//*************************************************************************************************
// Settings
//*************************************************************************************************

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.Issues.GitRepository",
    repositoryOwner: "cake-contrib",
    repositoryName: "Cake.Issues.GitRepository",
    appVeyorAccountName: "cakecontrib",
    shouldGenerateDocumentation: false,
    shouldRunIntegrationTests: true,
    integrationTestScriptPath: "./tests/integration/tests.cake");

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    dupFinderExcludePattern: new string[] { BuildParameters.RootDirectoryPath + "/src/Cake.Issues.GitRepository.Tests/*.cs" },
    testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[Cake.Issues]* -[Cake.Issues.Testing]* -[Shouldly]* -[DiffEngine]* -[EmptyFiles]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

//*************************************************************************************************
// Extensions
//*************************************************************************************************

Task("Prepare-Integration-Tests")
    .IsDependentOn("Create-NuGet-Packages")
    .Does<BuildVersion>((context, buildVersion) =>
{
    // Clean addin directory
    var addinDir = MakeAbsolute(Directory("./tools/Addins/" + BuildParameters.RepositoryName));
    if (DirectoryExists(addinDir))
    {
        DeleteDirectory(addinDir, new DeleteDirectorySettings {
            Recursive = true,
            Force = true
        });
    }

    // Unzip package from current build into addin directory
    var packagePath =
        BuildParameters.Paths.Directories.NuGetPackages.CombineWithFilePath("Cake.Issues.GitRepository." + buildVersion.SemVersion + ".nupkg");
    Unzip(packagePath, addinDir);
});

Task("Buildserver")
  .IsDependentOn("Run-Integration-Tests");

BuildParameters.Tasks.IntegrationTestTask.IsDependentOn("Prepare-Integration-Tests");

//*************************************************************************************************
// Execution
//*************************************************************************************************

Build.RunDotNetCore();
