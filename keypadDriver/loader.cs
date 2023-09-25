using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using driverClasses;

namespace driverClasses
{
    class loader
    {
        public static List<List<Button>> loadFromFile(changeLayerDel changeLayerEvent = null)
        {
            List<List<Button>> buttons = new List<List<Button>>();
            //Stream iconStream = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/config2.txt")).Stream;
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string file = System.IO.File.ReadAllText(exePath + @"config2.txt");

            file = file.Replace(" ", "");
            file = file.Replace("\r", "");
            file = file.Replace("\n", "");

            int chIndex = 0;
            string currentWord = "";
            int currentLayer = 0;

            while (chIndex < file.Length)
            {
                currentWord += file[chIndex];

                if (String.Equals(currentWord, "layer"))
                {
                    int layerIndex = int.Parse(file[chIndex + 1].ToString());
                    currentLayer = layerIndex;
                    if (buttons.Count < (layerIndex + 1))
                    {
                        for(int i = 0; buttons.Count < (layerIndex + 1); i++)
                        {
                            buttons.Add(new List<Button>());
                        }
                    }
                    chIndex += 3;
                    currentWord = "";
                    continue;
                }
                if (String.Equals(currentWord, "button"))
                {
                    currentWord = "";
                    int o = 1;
                    while (int.TryParse(file[chIndex + o].ToString(), out _))
                    {
                        currentWord += file[chIndex + o];
                        o++;
                    }
                    chIndex += currentWord.Length - 1;
                    int buttonIndex = int.Parse(currentWord);
                    chIndex += 3;
                    string buttonConfig = "";
                    while(!char.Equals(file[chIndex], '}'))
                    {
                        buttonConfig += file[chIndex];
                        chIndex++;
                    }
                    if (buttons[currentLayer].Count() < buttonIndex + 1)
                    {
                        for (int i = 0; buttons[currentLayer].Count < (buttonIndex + 1); i++)
                        {
                            buttons[currentLayer].Add(new Button());
                        }
                    }
                    buttons[currentLayer][buttonIndex] = createButton(buttonConfig, changeLayerEvent);
                    currentWord = "";
                    chIndex++;
                    continue;
                }
                if (currentWord == "}")
                    currentWord = "";
                chIndex++;
            }

            return buttons;
        }
        internal static Button createButton(string input, changeLayerDel changeLayerEvent)
        {
            Button b = new Button();

            int chIndex = 0;
            string currentWord = "";

            while (chIndex < input.Length)
            {
                currentWord += input[chIndex];

                if (String.Equals(currentWord, "pressKey"))
                {
                    chIndex += 2;
                    currentWord = "";
                    while (!char.Equals(input[chIndex], ')'))
                    {
                        currentWord += input[chIndex];
                        chIndex++;
                    }
                    keyPress kp = new keyPress(convertStringToEnum(currentWord));
                    b.addAction(kp);
                    //b.addKeyPress(convertStringToEnum(currentWord));
                    currentWord = "";
                    chIndex += 2;
                    continue;
                }
                if (String.Equals(currentWord, "keyDown"))
                {
                    chIndex += 2;
                    currentWord = "";
                    while (!char.Equals(input[chIndex], ')'))
                    {
                        currentWord += input[chIndex];
                        chIndex++;
                    }
                    keyDown kd = new keyDown(convertStringToEnum(currentWord));
                    b.addAction(kd);
                    //b.addKeyPress(convertStringToEnum(currentWord));
                    currentWord = "";
                    chIndex += 2;
                    continue;
                }
                if (String.Equals(currentWord, "keyUp"))
                {
                    chIndex += 2;
                    currentWord = "";
                    while (!char.Equals(input[chIndex], ')'))
                    {
                        currentWord += input[chIndex];
                        chIndex++;
                    }
                    keyUp ku = new keyUp(convertStringToEnum(currentWord));
                    b.addAction(ku);
                    //b.addKeyPress(convertStringToEnum(currentWord));
                    currentWord = "";
                    chIndex += 2;
                    continue;
                }
                if (String.Equals(currentWord, "pressShortcut"))
                {
                    chIndex += 2;
                    List<WindowsInput.Native.VirtualKeyCode> keys;
                    keys = new List<WindowsInput.Native.VirtualKeyCode>();
                    currentWord = "";
                    string label = "macro";

                    while(!char.Equals(input[chIndex], ']') && chIndex < input.Length)
                    {
                        currentWord += input[chIndex];
                        chIndex++;
                        if (chIndex >= input.Length)
                            break;
                    }
                    chIndex += 2;
                    if (currentWord != "")
                        label = currentWord;
                    currentWord = "";
                    while (!char.Equals(input[chIndex], ')') && chIndex < input.Length)
                    {
                        while(!char.Equals(input[chIndex], ',') && !char.Equals(input[chIndex], ')'))
                        {
                            currentWord += input[chIndex];
                            chIndex++;
                            if (chIndex >= input.Length)
                                break;
                        }
                        keys.Add(convertStringToEnum(currentWord));
                        currentWord = "";
                        chIndex++;
                        if (chIndex >= input.Length)
                            break;
                    }
                    if (keys.Count > 0)
                    {
                        label += '{';
                        foreach(WindowsInput.Native.VirtualKeyCode key in keys)
                        {
                            label = label + key.ToString();
                            label = label + ';';
                        }
                        label = label.Remove(label.Length - 1, 1);
                        label += '}';
                    }
                    shortCutPress scp = new shortCutPress(keys, label);
                    b.addAction(scp);
                    //b.addShortcutPress(keys);
                    chIndex += 2;
                    continue;
                }
                if (String.Equals(currentWord, "runProgram"))
                {
                    chIndex += 2;
                    currentWord = "";
                    while (!char.Equals(input[chIndex], ')'))
                    {
                        currentWord += input[chIndex];
                        chIndex++;
                    }
                    runProgram rp = new runProgram(currentWord);
                    b.addAction(rp);
                    //b.addRunProgram(currentWord);
                    currentWord = "";
                    chIndex += 2;
                    continue;
                }
                if (String.Equals(currentWord, "writeText"))
                {
                    chIndex += 2;
                    currentWord = "";
                    while (!char.Equals(input[chIndex], ')'))
                    {
                        currentWord += input[chIndex];
                        chIndex++;
                    }
                    write w = new write(currentWord);
                    b.addAction(w);
                    //b.addWriteText(currentWord);
                    currentWord = "";
                    chIndex += 2;
                    continue;
                }
                if (String.Equals(currentWord, "changeLayer"))
                {
                    chIndex += 2;
                    currentWord = "";
                    while (!char.Equals(input[chIndex], ')'))
                    {
                        currentWord += input[chIndex];
                        chIndex++;
                    }
                    int index;
                    if(!int.TryParse(currentWord, out index))
                    {
                        continue;
                    }

                    changeLayer cl = new changeLayer(changeLayerEvent, index);
                    b.addAction(cl);
                    currentWord = "";
                    chIndex += 2;
                    continue;
                }

                chIndex++;
            }

            return b;
        }
        static WindowsInput.Native.VirtualKeyCode convertStringToEnum(string input)
        {
            WindowsInput.Native.VirtualKeyCode output;
            Enum.TryParse(input, out output);
            return output;
        }
    }
}
