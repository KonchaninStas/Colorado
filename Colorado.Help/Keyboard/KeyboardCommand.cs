using System;
using System.Windows.Forms;

namespace Colorado.Help.Keyboard
{
    public interface IKeyboardCommand
    {
        Keys Key { get; }

        string Name { get; }

        string Description { get; }

        bool IsPressed { get; }
    }

    public class KeyboardCommand : IKeyboardCommand, IEquatable<KeyboardCommand>
    {
        public KeyboardCommand(Keys key, string name, string description)
        {
            Key = key;
            Name = name;
            Description = description;
        }

        public Keys Key { get; }

        public string Name { get; }

        public string Description { get; }

        public bool IsPressed { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is KeyboardCommand))
            {
                return false;
            }

            KeyboardCommand other = (KeyboardCommand)obj;
            return Equals(other);
        }

        public bool Equals(KeyboardCommand other)
        {
            return Name == other.Name && Description == other.Description && Key == other.Key;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Description.GetHashCode() ^ Key.GetHashCode();
        }
    }
}
