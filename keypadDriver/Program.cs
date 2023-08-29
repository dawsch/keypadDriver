using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Management;
using Microsoft.Win32;
using System.Reflection;
using keyboardWindow;
using driverClasses;
using System.Text.Json;

using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace driverClasses
{
    
    [Guid("5CDF2C82-841E-4546-9722-0CF74078229A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IAudioEndpointVolume
    {
        int _0(); int _1(); int _2(); int _3();
        int SetMasterVolumeLevelScalar(float fLevel, Guid pguidEventContext);
        int _5();
        int GetMasterVolumeLevelScalar(out float pfLevel);
        int _7(); int _8(); int _9(); int _10(); int _11(); int _12();
    }

    [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IMMDevice
    {
        int Activate(ref System.Guid id, int clsCtx, int activationParams, out IAudioEndpointVolume aev);
    }

    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IMMDeviceEnumerator
    {
        int _0();
        int GetDefaultAudioEndpoint(int dataFlow, int role, out IMMDevice endpoint);
    }

    [ComImport, Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")] class MMDeviceEnumeratorComObject { }

    public class Audio
    {
        private static readonly IAudioEndpointVolume _MMVolume;

        static Audio()
        {
            var enumerator = new MMDeviceEnumeratorComObject() as IMMDeviceEnumerator;
            enumerator.GetDefaultAudioEndpoint(0, 1, out IMMDevice dev);
            var aevGuid = typeof(IAudioEndpointVolume).GUID;
            dev.Activate(ref aevGuid, 1, 0, out _MMVolume);
        }

        public static int Volume
        {
            get
            {
                _MMVolume.GetMasterVolumeLevelScalar(out float level);
                return (int)(level * 100);
            }
            set
            {
                _MMVolume.SetMasterVolumeLevelScalar((float)value / 100, default);
            }
        }
    }


    class Program
    {
        static SerialPort serialPort;
        static int currentLayer = 0;
        static List<List<Button>> buttons, buttonsCopy;

        static void Main(string[] args)
        {

            //serialPort = new SerialPort();
            //serialPort.PortName = "COM5";
            //serialPort.BaudRate = 9600;
            //serialPort.Parity = Parity.None;
            //serialPort.StopBits = StopBits.One;
            //serialPort.Handshake = Handshake.None;
            //serialPort.DataBits = 8;
            //serialPort.DtrEnable = true;
            //serialPort.RtsEnable = true;
            //serialPort.Open();

            connectToDevise();


            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (key.GetValue("Keypad") == null)
            {
                key.SetValue("Keypad", Application.ExecutablePath);
            }

            buttons = new List<List<Button>>();
            
            buttons = loader.loadFromFile();

            buttonsCopy = copyButtonList.copy(buttons);
            Thread windowT = runWindow(buttonsCopy, resetKeylayoutFunc, saveKeylayoutFunc);
            
            //Thread thread = new Thread(new ThreadStart(runWindow));
            //thread.Start();

            int potentiometer;

            
            while (true)
            {
                string input = serialPort.ReadExisting();
                List<string> inputA = input.Split('\r').ToList<string>();
                inputA.Remove("\r");
                inputA.Remove("\n");
                for (int i = 0; i < inputA.Count; i++)
                {
                    inputA[i] = inputA[i].Trim();
                }

                for (int i = 0; i < inputA.Count; i++)
                {
                    string[] line = inputA[i].Split(' ');
                    if (line.Length > 0)
                    {
                        if (String.Equals(line[0], "Button"))
                        {
                            if (line.Length == 3)
                            {
                                int buttonIndex = int.Parse(line[2]);
                                if (String.Equals(line[1], "clicked"))
                                {
                                    if (currentLayer < buttons.Count){
                                        if (buttonIndex < buttons[currentLayer].Count){
                                            buttons[currentLayer][buttonIndex].isPressed = true;
                                            buttons[currentLayer][buttonIndex].press();
                                        }
                                    }
                                            
                                }
                                if (String.Equals(line[1], "release"))
                                {
                                    if (currentLayer < buttons.Count)
                                        if (buttonIndex < buttons[currentLayer].Count)
                                            buttons[currentLayer][buttonIndex].isPressed = false;
                                }
                            }
                        }
                        if (String.Equals(line[0], "potentiometer"))
                        {
                            if (line.Length == 2)
                            {
                                potentiometer = int.Parse(line[1]);
                                try
                                {
                                    setVolume(potentiometer, 1024);
                                }
                                catch { }
                            }
                        }
                    }
                }

                Thread.Sleep(50);
            }
        }
        static internal void connectToDevise()
        {
            string[] portList = SerialPort.GetPortNames();
            Thread thread;

            foreach (string port in portList)
            {
                Console.WriteLine("current port: " + port);
                serialPort = new SerialPort();
                serialPort.PortName = port;
                serialPort.BaudRate = 9600;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.Handshake = Handshake.None;
                serialPort.DataBits = 8;
                serialPort.DtrEnable = true;
                serialPort.RtsEnable = true;
                serialPort.ReadTimeout = 100;
                serialPort.WriteTimeout = 100;
                try
                {
                    serialPort.Open();
                }
                catch
                {
                    continue;
                }

                if (trySerialPort(port) == 1)
                    break;

            }
        }
        internal static int trySerialPort(string port)
        {
            int i = 0;
            while (i < 5)
            {
                string line = "none";
                try
                {
                    serialPort.WriteLine("hello123");
                }
                catch (Exception) { }

                try
                {
                    line = serialPort.ReadLine();
                }
                catch (Exception) { }

                if (line.Contains("keypad"))
                {
                    Console.WriteLine("connected to:" + line);
                    return 1;
                }
                i++;
            }
            return -1;
        }
        internal static void setVolume(int AnalogVolume, int max)
        {
            float x = 1f / max;
            double y = x * AnalogVolume * 100;
            Audio.Volume = (int)y;
        }
        internal static void nextLayer()
        {
            currentLayer++;
            if (currentLayer > 3)
                currentLayer = 0;
        }
        internal static Thread runWindow(List<List<Button>> buttonList, resetKeylayout rk, saveKeylayout sk)
        {
            Thread newWindowThread = new Thread(new ThreadStart(() =>
            {
                // create and show the window
                MainWindow obj = new MainWindow(buttonList, rk, sk);
                obj.Show();

                // start the Dispatcher processing  
                System.Windows.Threading.Dispatcher.Run();
            }));

            // set the apartment state  
            newWindowThread.SetApartmentState(ApartmentState.STA);

            // make the thread a background thread  
            newWindowThread.IsBackground = true;

            // start the thread  
            newWindowThread.Start();
            return newWindowThread;
        }
        internal static void test()
        {
            Console.WriteLine("XD coś tam jakiś teścik");
        }
        internal static List<List<Button>> resetKeylayoutFunc()
        {
            buttonsCopy = buttons;
            buttons = copyButtonList.copy(buttons);
            return buttonsCopy;
        }
        internal static void saveKeylayoutFunc()
        {
            string s = saver.save(buttonsCopy);
            buttons = copyButtonList.copy(buttonsCopy);
        }
    }

}
