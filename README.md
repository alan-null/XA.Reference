# SXA Reference
**SXA Reference** is an example Sitecore project build on top of **Sitecore Experience Accelerator**.

The main goal of this reference project is to show **SXA** features and give guidelines on how to build/configure new sites.


# Installation
## Clone Repository
Open command line and run following command
```
clone https://github.com/alan-null/XA.Reference.git
```
## Install Sitecore & SXA
The next step is to create a new Sitecore instnace and then install SXA package.

Current **XA.Reference** project is compatible with:

| Product   |      Version      |  Revision |
|----------|:-------------:|:------:|
| Sitecore |  **8.2** | rev. 161221 |
| SXA  |  **1.2** | rev. 161205 |


## Environment specific configs

1. Open repository root.
2. Make a copy of following files and remove `.example` from the file name:
   * `publishsettings.targets.example`,
   * `zzz.Sxa.Reference.config.example`
3. Fill file content of each file with your settings
   * `publishsettings.targets` - set **publishUrl**. This is your site name (IIS site name),
   * `zzz.Sxa.Reference.config` - set value of Sitecore variable named **sourceFolder**. Variable should point to the *src* folder from **XA.Reference** repository


## Build and publish
- Open *PowerShell Console* as **Administrator**
- Navigate to **XA.Reference** repository root
- Execute

 ```powershell
.\publish.ps1
```

**Alternative way:**

You can build whole solution using Visual Studio and publish selected projects using *WebPublish* (hit `Alt + B + H` and select publish).