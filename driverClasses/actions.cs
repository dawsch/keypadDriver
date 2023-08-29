using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;

namespace driverClasses
{
    public delegate void pressKeyEventHandler(WindowsInput.Native.VirtualKeyCode key);
    public delegate void pressShortcutEventHandler(List<WindowsInput.Native.VirtualKeyCode> keys);
    public delegate void runProgramEventHandler(string path);
    public delegate void writeTextEventHandler(string path);
    public delegate void nextLayerEventHandler();

    public enum actionsE
    {
        pressKey = 0,
        pressShortcut = 1,
        runProgram = 2,
        writeText = 3,
        nextLayer = 4
    }

    public class actions
    {
        public nextLayerEventHandler nextLayerExe;
        InputSimulator input;
        KeyboardSimulator keyboard;
        public actions()
        {
            input = new InputSimulator();
            keyboard = new KeyboardSimulator(input);
        }
        public void pressKey(WindowsInput.Native.VirtualKeyCode key)
        {
            keyboard.KeyPress(key);
        }
        public void pressShortCut(List<WindowsInput.Native.VirtualKeyCode> keys)
        {
            foreach (WindowsInput.Native.VirtualKeyCode key in keys)
            {
                keyboard.KeyDown(key);
            }
            foreach (WindowsInput.Native.VirtualKeyCode key in keys)
            {
                keyboard.KeyUp(key);
            }
        }
        public void runProgram(string path)
        {
            System.Diagnostics.Process.Start(path);
        }
        public void write(string text)
        {
            input.Keyboard.TextEntry(text);
        }
        public void nextLayer()
        {
            nextLayerExe();
        }
    }
}
