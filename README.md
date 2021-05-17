# SpeakNumbers
![image](https://user-images.githubusercontent.com/69211409/118551818-6be65c80-b75e-11eb-86a6-f1319d20ce0f.png)

Small program that I have build for my little sister to teach her about numbers. Originally I build a WinForms app with .NET Framework but I lost the source code, so I decided to make a new one with .NET 5.
- For reasons I don't know, System.Speech isn't shipped in .NET Core (or later). There are other options for this problem; I could use an Azure service, but I need a local speech synthesis API. Fortunately, I have Youtube [video](https://www.youtube.com/watch?v=GTCrPgrXV7w) providing a way to use powershell to execute .NET Framework code.
  - I didn't get all the details of his code but you should consider that PowerShell process leaves a file (of the command) in your Temp Folder, also, this code won't work on other platforms.
  - It goes without saying that the speech synthesis API quality is shit, but it is the only option for now.

I might a GUI later or update the whole code base if I find better alternatives.
