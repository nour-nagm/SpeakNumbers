
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ConsoleApp
{

    public static class Speech
    {
        public static void Speak(string text, bool wait = false)
        {
            ExecuteCommand(
            $@"Add-Type -AssemblyName System.speech; 
                $speak = New-Object System.Speech.Synthesis.SpeechSynthesizer; 
                $speak.Speak(""{text}"");", wait);

            void ExecuteCommand(string command, bool wait)
            {
                string path = Path.GetTempPath() + Guid.NewGuid() + ".ps1";

                using (var streamWriter = new StreamWriter(
                    path: path,
                    append: false,
                    encoding: Encoding.UTF8))
                {
                    streamWriter.Write(command);

                    var processStart = new ProcessStartInfo
                    {
                        FileName = @"C:\Windows\System32\windowspowershell\v1.0\powershell.exe",
                        LoadUserProfile = false,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        Arguments = $"-executionpolicy bypass -File {path}",
                        WindowStyle = ProcessWindowStyle.Hidden
                    };

                    var process = Process.Start(processStart);
                    if (wait)
                        process.WaitForExit();
                }
            }
        }


    }

}
