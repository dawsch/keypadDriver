using System.Collections.Generic;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace driverClasses
{
    public class Button
    {
        [JsonIgnore]
        public bool isPressed = false;

        public List<action> actions = new List<action>();

        public Button()
        {
            actions = new List<action>();
        }
        public Button(bool press)
        {
            actions = new List<action>();
            this.isPressed = press;
        }
        public void addAction(action a)
        {
            actions.Add(a);
        }
        public void removeAction(int index)
        {
            if (index < actions.Count)
            {
                actions.RemoveAt(index);
            }
        }

        public void press()
        {
            foreach (action a in actions)
            {
                a.execute();
            }
        }
        public string[] getLabeles()
        {
            string[] labeles = new string[actions.Count];
            int i = 0;
            foreach (action a in actions)
            {
                labeles[i] = a.getLabel();
                i++;
            }
            return labeles;
        }
        public Button copy()
        {
            Button button = new Button();
            button.isPressed = this.isPressed;
            foreach(action element in actions)
            {
                button.addAction(element.copy());
            }
            return button;
        }
        public string serialize()
        {
            string s = "";
            if (actions.Count == 0)
                return s;
            foreach (action a in actions)
            {
                s += a.serialize();
                s += "; ";
            }
            s = s.Remove(s.Length - 2);
            return s;
        }
    }
}