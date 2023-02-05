# YoutubeMPLoader
There are files to restore build. To do this, you need to:
<ol>
<li>Download these files</li>
<li>Create in Visual Studio (I used 2022) a project with the template "Binding library Xamarin"</li>
<li>Add file YoutubeExplode.cs into the project</li>
<li>Add nuget packages: Xamarin.MP4Transcoder and YoutubeExplode</li>
<li>Build project in the release mode. You will get in the bin folder the dll named like your project name</li>
<li>Create in Visual Studio (I used 2022) a project "Xamarin.Forms"</li>
<li>Add other files to the project</li>
<li>Replace or add from Android manifest raws with permissions and with necessary requests </li>
<li>Change in .cs files namespaces on your created project's namespace</li>
<li>Change in xaml namespaces (here was App8 namespace) on your created project's namespace</li>
<li>Add YoutubeExplodePackage if needed</li>
<li>Add dependence to created dll in step 5 for 'your-project-name'.Android project </li>
<li>Enjoy</li>
</ol>
