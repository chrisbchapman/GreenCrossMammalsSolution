# Setting Up This Repository as a GitHub Template

## Steps to Publish

1. **Create GitHub Repository**
   - Go to GitHub and create a new repository
   - Name it: `dotnet-clean-architecture-template` (or your preferred name)
   - Don't initialize with README (you already have one)

2. **Mark as Template**
   - Go to repository **Settings**
   - Check **"Template repository"** option
   - Save changes

3. **Push Your Code**
```bash
git init
git add .
git commit -m "Initial template commit"
git branch -M main
git remote add origin https://github.com/YOUR_USERNAME/dotnet-clean-architecture-template.git
git push -u origin main
```

4. **Add Topics** (Optional but Recommended)
   - In repository settings, add topics:
   - `dotnet`, `template`, `clean-architecture`, `repository-pattern`, `entity-framework`, `winforms`, `csharp`

## Creating New Projects from Template

### Using GitHub UI
1. Navigate to template repository
2. Click **"Use this template"** → **"Create a new repository"**
3. Name your new project
4. Clone and run rename script

### Using GitHub CLI
```bash
gh repo create my-new-app --template YOUR_USERNAME/dotnet-clean-architecture-template --private
cd my-new-app
.\template-setup\rename-template.ps1 -NewName "MyCompany.MyApp"
```

### Using dotnet new (Advanced)
See DOTNET-TEMPLATE.md for creating a `dotnet new` template
