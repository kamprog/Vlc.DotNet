dumpbin /exports libvlc.dll > libvlc.def
..\Utils\Vlc.DotNet.Utils.DefFileCorrector.exe /DEFFILE:libvlc.def
lib /def:libvlc.def /out:libvlc.lib /machine:x86