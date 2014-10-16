VideoLan DotNet
=========

VideoLan DotNet provides the audio/video playback capabilities of the VLC Media Player inside .NET. It's available for Windows Forms, WPF and Silverlight 5.

Some of the defining characteristics are: 

  - Full extent VLC Media Player features inside .NET
  - Ease of use
  - Modularity
  - Extensibility 

You can see a full overview of VLC Media Player features over on the [VLC site] [1].

Version
----

2.0

Usage
--------------

To start using Vlc.DotNet in your project you need to set the VlcContext **LibVlcDllsPath** and **LibVlcPluginsPath** paths to:

- If you have VLC installed on your machine, use the default paths found in the CommonStrings class
- If you don't have VLC installed on your machine, you need the libvlc.dll, libvlccore.dll and vlc.exe executables. Set the properties to their physical locations on your hard drive.

*Note*: To use the WPF control you need to have >= 1.2 VLC libraries.

Examples
--------------

Here you can find usage examples of the VLC Player control.

###Windows Forms:

You must first initialize the **VlcContext** before using the **VlcControl** in your forms. You can use the VlcControl by designer or by code. Close the VlcContext when the application closes.

```csharp
[STAThread]
static void Main()
{
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
 
    //Set libvlc.dll and libvlccore.dll directory path
    VlcContext.LibVlcDllsPath = CommonStrings.LIBVLC_DLLS_PATH_DEFAULT_VALUE_AMD64;
    //Set the vlc plugins directory path
    VlcContext.LibVlcPluginsPath = CommonStrings.PLUGINS_PATH_DEFAULT_VALUE_AMD64;
 
    //Set the startup options
    VlcContext.StartupOptions.IgnoreConfig = true;
    VlcContext.StartupOptions.LogOptions.LogInFile = true;
    VlcContext.StartupOptions.LogOptions.ShowLoggerConsole = true;
    VlcContext.StartupOptions.LogOptions.Verbosity = VlcLogOptions.Verbosities.Debug;
 
    //Initialize the VlcContext
    VlcContext.Initialize();
 
    Application.Run(new Form1());
 
    //Close the VlcContext
    VlcContext.CloseAll();
}
```

###WPF:

Before loading the control in XAML you must first initialize the VlcContext:

```csharp
//Set libvlc.dll and libvlccore.dll directory path
VlcContext.LibVlcDllsPath = CommonStrings.LIBVLC_DLLS_PATH_DEFAULT_VALUE_AMD64;
//Set the vlc plugins directory path
VlcContext.LibVlcPluginsPath = CommonStrings.PLUGINS_PATH_DEFAULT_VALUE_AMD64;
 
//Set the startup options
VlcContext.StartupOptions.IgnoreConfig = true;
VlcContext.StartupOptions.LogOptions.LogInFile = true;
VlcContext.StartupOptions.LogOptions.ShowLoggerConsole = true;
VlcContext.StartupOptions.LogOptions.Verbosity = VlcLogVerbosities.Debug;
 
//Initialize the VlcContext
VlcContext.Initialize();
```
An example of using the VlcControl in XAML:

```xml
<Wpf:VlcControl x:Name="myVlcControl" />
 
<Grid Grid.Row="0">
     <Grid.Background>
         <VisualBrush Stretch="Uniform">
             <VisualBrush.Visual>
                 <Image Source="{Binding ElementName=myVlcControl, Path=VideoSource}" />
             </VisualBrush.Visual>
         </VisualBrush >
    </Grid.Background>
</Grid>
```
When you're finished with the control (typically when the application closes), close the VlcContext:

```csharp
VlcContext.CloseAll();
```

Participation
--------------

To participate in the project, just fork it on github, add your improvement and submit a pull request.

License
----

MIT

[1]:https://www.videolan.org/vlc/features.html