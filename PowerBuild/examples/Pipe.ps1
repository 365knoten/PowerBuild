#
# Pipe.ps1
#



Pipe-Start -pattern **/*.css -Base "C:\PowerBuildDemo\src" | Pipe-Replace | Pipe-Minimize | Pipe-Write -Base $env:TEMP/$(Get-BuildVar appname)

