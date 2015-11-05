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
                throw new InvalidOperationException("Keyboard definition xml not valid!");
            }

            CreateLayout(rootElement, keyboard);
        }

        private void CreateLayout(XElement rootElement, Keyboard keyboard)
        {
            var keyElements = rootElement.Elements("Key");
            if (keyElements == null)
            {
                throw new InvalidOperationException("Keyboard definition xml not valid!");
            }

            try
            {
                foreach (var key in keyElements)
                {
                    var keyboardKey = new KeyboardKey();
                    var stateElements = key.Elements("State");
                    foreach (var state in stateElements)
                    {
                        keyboardKey.AddState(GetKeyState(state));
                    }
                    keyboard.LayoutManager.AddCell(keyboardKey, GetLocation(key), GetSize(key));
                    keyboard.Controls.Add(keyboardKey);
                }

                keyboard.LayoutManager.Rows = Convert.ToInt16(rootElement.Attribute("Rows").Value);
                keyboard.LayoutManager.Cols = Convert.ToInt16(rootElement.Attribute("Cols").Value);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Keyboard definition xml can't read!", ex);
            }

            keyboard.LayoutManager.PerformLayout();
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
                KeyCode = GetAttributeValueOrDefault(stateElement, "KeyCode"),
                KeyCodeDeadCircumflex = GetAttributeValueOrDefault(stateElement, "KeyCodeDeadCircumflex"),
                KeyCodeDeadAcute = GetAttributeValueOrDefault(stateElement, "KeyCodeDeadAcute"),
                KeyCodeDeadDiaeresis = GetAttributeValueOrDefault(stateElement, "KeyCodeDeadDiaeresis"),
                KeyCodeDeadGrave = GetAttributeValueOrDefault(stateElement, "KeyCodeDeadGrave"),
                KeyCodeDeadTilde = GetAttributeValueOrDefault(stateElement, "KeyCodeDeadTilde")
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