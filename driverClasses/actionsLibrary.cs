using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace driverClasses
{
    public class keyPress : action
    {
        [JsonInclude]
        WindowsInput.Native.VirtualKeyCode key;
        InputSimulator input;
        KeyboardSimulator keyboard;
        string label = "none";

        public keyPress(WindowsInput.Native.VirtualKeyCode key, string label = "none")
        {
            this.key = key;
            input = new InputSimulator();
            keyboard = new KeyboardSimulator(input);
            this.label = label;
        }
        public keyPress(WindowsInput.Native.VirtualKeyCode key, ref KeyboardSimulator keyboardSimulator, string label = "none")
        {
            this.key = key;
            this.label = label;
            keyboard = keyboardSimulator;
            //key = WindowsInput.Native.VirtualKeyCode.SNAPSHOT
        }
        public override void execute()
        {
            keyboard.KeyPress(key);
        }
        public override string getLabel()
        {
            if (label != "none")
            {
                return label;
            }
            return "clikc: " + key.ToString();
        }
        public override action copy()
        {
            return new keyPress(key, ref keyboard, label);
        }
        public override string serialize()
        {
            string result = "pressKey";

            result += '(';
            result += key.ToString();
            result += ')';

            return result;
        }
    }
    public class keyDown : action
    {
        WindowsInput.Native.VirtualKeyCode key;
        InputSimulator input;
        KeyboardSimulator keyboard;
        string label = "none";

        public keyDown(WindowsInput.Native.VirtualKeyCode key, string label = "none")
        {
            this.key = key;
            input = new InputSimulator();
            keyboard = new KeyboardSimulator(input);
            this.label = label;
        }
        public keyDown(WindowsInput.Native.VirtualKeyCode key, ref KeyboardSimulator keyboardSimulator, string label = "none")
        {
            this.key = key;
            this.label = label;
            keyboard = keyboardSimulator;
            //key = WindowsInput.Native.VirtualKeyCode.SNAPSHOT
        }
        public override void execute()
        {
            keyboard.KeyDown(key);
        }
        public override string getLabel()
        {
            if (label != "none")
            {
                return label;
            }
            return "press: " + key.ToString();
        }
        public override action copy()
        {
            return new keyPress(key, ref keyboard, label);
        }
        public override string serialize()
        {
            string result = "keyDown";

            result += '(';
            result += key.ToString();
            result += ')';

            return result;
        }
    }
    public class keyUp : action
    {
        WindowsInput.Native.VirtualKeyCode key;
        InputSimulator input;
        KeyboardSimulator keyboard;
        string label = "none";

        public keyUp(WindowsInput.Native.VirtualKeyCode key, string label = "none")
        {
            this.key = key;
            input = new InputSimulator();
            keyboard = new KeyboardSimulator(input);
            this.label = label;
        }
        public keyUp(WindowsInput.Native.VirtualKeyCode key, ref KeyboardSimulator keyboardSimulator, string label = "none")
        {
            this.key = key;
            this.label = label;
            keyboard = keyboardSimulator;
            //key = WindowsInput.Native.VirtualKeyCode.SNAPSHOT
        }
        public override void execute()
        {
            keyboard.KeyUp(key);
        }
        public override string getLabel()
        {
            if (label != "none")
            {
                return label;
            }
            return "release: " + key.ToString();
        }
        public override action copy()
        {
            return new keyPress(key, ref keyboard, label);
        }
        public override string serialize()
        {
            string result = "keyUp";

            result += '(';
            result += key.ToString();
            result += ')';

            return result;
        }
    }
    public class shortCutPress : action
    {
        List<WindowsInput.Native.VirtualKeyCode> keys;
        InputSimulator input;
        KeyboardSimulator keyboard;
        string label = "none";

        public shortCutPress(List<WindowsInput.Native.VirtualKeyCode> keys, string label = "none")
        {
            this.keys = new List<WindowsInput.Native.VirtualKeyCode>(keys);
            input = new InputSimulator();
            keyboard = new KeyboardSimulator(input);
            this.label = label;
        }
        public shortCutPress(List<WindowsInput.Native.VirtualKeyCode> keys, ref KeyboardSimulator keyboardSimulator, string label = "none")
        {
            this.keys = new List<WindowsInput.Native.VirtualKeyCode>(keys);
            keyboard = keyboardSimulator;
            this.label = label;
        }
        public override void execute()
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
        public override string getLabel()
        {
            if (label != "none")
            {
                if (label.Contains("{") && label.Contains("}"))
                {
                    int start = label.IndexOf("{");
                    int end = label.IndexOf("}");
                    if (start < end)
                        return label.Remove(start, end - start + 1);
                }
                return label;
            }
            return "shortcut";
        }
        public override List<string> getAddInfo()
        {
            if (label != "none")
            {
                if (label.Contains("{") && label.Contains("}"))
                {
                    int start = label.IndexOf("{");
                    int end = label.IndexOf("}");
                    string info = label.Substring(start + 1, end - start - 2);
                    string[] infos = info.Split(';');
                    return infos.ToList();

                }
            }
            return base.getAddInfo();
        }
        public override action copy()
        {
            return new shortCutPress(new List<WindowsInput.Native.VirtualKeyCode>(keys), ref keyboard, label);
        }
        public override string serialize()
        {
            string result = "pressShortcut";
            result = result + '[' + getLabel() + ']';
            result += '(';
            foreach(WindowsInput.Native.VirtualKeyCode key in keys)
            {
                result += key.ToString();
                result += ", ";
            }
            result = result.Remove(result.Length - 2);
            result += ')';

            return result;
        }
    }
    public class runProgram : action
    {
        string path;
        public runProgram(string path)
        {
            this.path = path;
        }
        public override void execute()
        {
            string s = getLabel();
            System.Diagnostics.Process.Start(path);
        }
        public override string getLabel()
        {
            return "Run app: " + path.Split('\\').Last<string>();
        }
        public override action copy()
        {
            return new runProgram(path);
        }
        public override string serialize()
        {
            string result = "runProgram";

            result += '(';
            result += path;
            result += ')';

            return result;
        }
    }
    public class write : action
    {
        string text;
        InputSimulator input;
        KeyboardSimulator keyboard;
        public write(string text)
        {
            this.text = text;
            input = new InputSimulator();
            keyboard = new KeyboardSimulator(input);
        }
        public write(string text, ref KeyboardSimulator keyboardSimulator)
        {
            this.text = text;
            input = new InputSimulator();
            keyboard = keyboardSimulator;
        }
        public override void execute()
        {
            input.Keyboard.TextEntry(text);
        }
        public override string getLabel()
        {
            return "write: " + text;
        }
        public override action copy()
        {
            return new write(text, ref keyboard);
        }
        public override string serialize()
        {
            string result = "writeText";

            result += '(';
            result += text;
            result += ')';

            return result;
        }
    }
    public class openWebsite : action
    {
        string url;
        public openWebsite(string url)
        {
            this.url = url;
        }
        public override void execute()
        {
            System.Diagnostics.Process.Start(url);
        }
        public override string getLabel()
        {
            string[] s = url.Split('/');
            if (s.Length > 2)
            {
                string s2 = s[1] + "." + s[2];
                s2 = s2.Remove(s2.Length - 1);
                return s[2];
            }
            else
            {
                return "incorret URL";
            }
        }
        public override action copy()
        {
            return new openWebsite(url);
        }
        public override string serialize()
        {
            string result = "openWebsite";

            result += '(';
            result += url;
            result += ')';

            return result;
        }
    }

    public class nextLayer : action
    {
        string text;
        InputSimulator input;
        KeyboardSimulator keyboard;
        public nextLayer(string text)
        {
            this.text = text;
            input = new InputSimulator();
            keyboard = new KeyboardSimulator(input);
        }
        public nextLayer(string text, ref KeyboardSimulator keyboardSimulator)
        {
            this.text = text;
            keyboard = keyboardSimulator;
        }
        public override void execute()
        {
            input.Keyboard.TextEntry(text);
        }
        public override string getLabel()
        {
            return "nextLayer";
        }
        public override action copy()
        {
            return new nextLayer(text, ref keyboard);
        }
        public override string serialize()
        {
            return "nextLayer";
        }
    }
    
}
