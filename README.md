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

# A new version is being developed (a total rework). Please be patient.

N/A

Usage
--------------

To start using Vlc.DotNet in your project you need to set the VlcContext **LibVlcDllsPath** and **LibVlcPluginsPath** paths to:

- If you have VLC installed on your machine, use the default paths found in the CommonStrings class
- If you don't have VLC installed on your machine, you need the libvlc.dll, libvlccore.dll and vlc.exe executables. Set the properties to their physical locations on your hard drive.

*Note*: To use the WPF control you need to have >= 1.2 VLC libraries.

Examples
--------------

You can view our full #documentation, on our [site] (https://rexgrammer.github.io/Vlc.DotNet/doc/index.html)

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
    VlcContext.StartupOptions.LogOptions.Verbosity = VlcLogVerbosities.Debug;
 
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

- If you're good at **documenting**, you can help us write better documentation and make the awesome project even more awesome!

- If you like **writing**, you can write articles, blog posts or any other form of content about the project. Be sure to send us an e-mail so we can promote your post!

- If you like **testing** stuff you can test Vlc.DotNet and report any bugs and misbehaviours found through the **GitHub Issues** interface.

- If you have a means to **promote or feature** Vlc.DotNet on your website/blog/anything please feel free to send us an e-mail for more info and material.

- If you have an **improvement** or a **new feature** that you can implement fork the project, implement/improve it and submit a **pull request**!

Typos And Grammatical Errors
--------------

If you found a typo or a grammatical error please send me an e-mail, so I can fix it. Thank you for your support!

License
----

MIT

[1]:https://www.videolan.org/vlc/features.html
