using System;
using System.Drawing;
using System.IO;
using System.Xml.Linq;

namespace OnScreenKeyboard
{
    internal class KeyboardBuilder : IKeyboardBuilder
    {
        public void Build(string path, Keyboard keyboard)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(path);
            }

            Build(GetKeyboardDefinition(path), keyboard);
        }

        public void Build(XDocument definition, Keyboard keyboard)
        {
            if (definition == null)
            {
                throw new ArgumentNullException("definition");
            }

            if (keyboard == null)
            {
                throw new ArgumentNullException("keyboard");
            }

            var rootElement = definition.Element("KeyboardDefinition");

            if (rootElement == null)
            {
                throw new InvalidOperationException("<KeyboardDefinition> element not found in definition!");
            }

            CreateLayout(rootElement, keyboard);
        }

        private void CreateLayout(XElement rootElement, Keyboard keyboard)
        {
            var keyElements = rootElement.Elements("Key");
            if (keyElements == null)
            {
                throw new InvalidOperationException("<Key> element not found in definition!");
            }

            try
            {
                foreach (var keyElement in keyElements)
                {
                    var keyboardKey = new KeyboardKey();
                    var stateElements = keyElement.Elements("State");
                    foreach (var stateElement in stateElements)
                    {
                        keyboardKey.AddState(GetKeyState(stateElement));
                    }
                    keyboard.AddKey(keyboardKey, GetLocation(keyElement), GetSize(keyElement));
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("<State> element can't read!", ex);
            }

            try
            {
                keyboard.SetGirdSize(Convert.ToInt16(rootElement.Attribute("Rows").Value), Convert.ToInt16(rootElement.Attribute("Cols").Value));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Rows Or Cols attiribute can't read!", ex);
            }

            keyboard.PerformKeyboardLayout();
        }

        private string GetAttributeValueOrDefault(XElement element, string name, string defaultValue = "")
        {
            var attribute = element.Attribute(name);
            return attribute == null ? defaultValue : attribute.Value;
        }

        private XDocument GetKeyboardDefinition(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Keyboard definition file not found!", path);
            }

            try
            {
                return XDocument.Load(path);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Keyboard definition can't load!", ex);
            }
        }

        private KeyboardKeyState GetKeyState(XElement stateElement)
        {
            return new KeyboardKeyState
            {
                Text = GetAttributeValueOrDefault(stateElement, "Text", string.Empty),
                StateAction = (KeyStateAction)Enum.Parse(typeof(KeyStateAction), GetAttributeValueOrDefault(stateElement, "Action", "Send")),
                Style = (KeyStateStyle)Enum.Parse(typeof(KeyStateStyle), GetAttributeValueOrDefault(stateElement, "Style", "Default")),
                KeyCode = GetAttributeValueOrDefault(stateElement, "Code"),
                KeyCodeDeadCircumflex = GetAttributeValueOrDefault(stateElement, "DeadCircumflex"),
                KeyCodeDeadAcute = GetAttributeValueOrDefault(stateElement, "DeadAcute"),
                KeyCodeDeadDiaeresis = GetAttributeValueOrDefault(stateElement, "DeadDiaeresis"),
                KeyCodeDeadGrave = GetAttributeValueOrDefault(stateElement, "DeadGrave"),
                KeyCodeDeadTilde = GetAttributeValueOrDefault(stateElement, "DeadTilde")
            };
        }

        private Point GetLocation(XElement keyElement)
        {
            return new Point(Convert.ToInt16(keyElement.Attribute("Left").Value), Convert.ToInt16(keyElement.Attribute("Top").Value));
        }

        private Size GetSize(XElement keyElement)
        {
            return new Size(Convert.ToInt16(keyElement.Attribute("Width").Value), Convert.ToInt16(keyElement.Attribute("Height").Value));
        }

    }
}