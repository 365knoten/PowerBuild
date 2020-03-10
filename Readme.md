# A Powershell based build system for javascript and css

Prototype

## Example

Get all css files from "C:\PowerBuildDemo\src", replace Tokens, minimize and write it to a temp folder with the appname

```powershell
Pipe-Start -pattern **/*.css -Base "C:\PowerBuildDemo\src" | Pipe-Replace | Pipe-Minimize | Pipe-Write -Base $env:TEMP/$(Get-BuildVar appname)
```
