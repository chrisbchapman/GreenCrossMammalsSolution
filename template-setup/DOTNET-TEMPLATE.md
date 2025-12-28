# Creating a `dotnet new` Template

## Template Structure

Create a `.template.config` folder in your solution root with `template.json`:

```json
{
  "$schema": "http://json.schemastore.org/template",
  "author": "Your Name",
  "classifications": ["Solution", "Clean Architecture", "WinForms"],
  "identity": "YourCompany.CleanArchitecture.WinForms",
  "name": "Clean Architecture WinForms Solution",
  "shortName": "cleanarch-winforms",
  "tags": {
    "language": "C#",
    "type": "solution"
  },
  "sourceName": "GreenCross.App",
  "preferNameDirectory": true,
  "symbols": {
    "namespace": {
      "type": "parameter",
      "datatype": "text",
      "replaces": "GreenCross.App",
      "defaultValue": "MyApp"
    }
  }
}
```

## Install Template Locally

```bash
# From template directory
dotnet new install .

# Create new solution from template
dotnet new cleanarch-winforms -n MyCompany.MyApp -o C:\Projects\MyNewApp
```

## Package and Distribute

```bash
# Pack as NuGet
dotnet pack template.csproj

# Publish to NuGet.org or private feed
dotnet nuget push YourTemplate.1.0.0.nupkg --source nuget.org
```

## Install from NuGet

```bash
dotnet new install YourCompany.CleanArchitecture.Template
```

## Uninstall Template

```bash
dotnet new uninstall YourCompany.CleanArchitecture.Template
```
