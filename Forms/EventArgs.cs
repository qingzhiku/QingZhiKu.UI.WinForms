using System;
using System.Collections.Generic;
using System.Text;

namespace System
{

    #region NC

    /// <summary>
    /// 非客户区激活
    /// </summary>
    public class NCActivateEventArgs : EventArgs
    {
        public NCActivateEventArgs(bool activate)
        {
            Activate = activate;
        }

        public bool Activate { get; set; }
    }

    #endregion

    #region HotKey

    public class HotKeyEventArgs
    {
        public HotKey HotKey { get; private set; }

        public HotKeyEventArgs(HotKey hotKey)
        {
            HotKey = hotKey;
        }
    }


    public class HotKeyEventArgs<T> : HotKeyEventArgs
    {
        public T Data { get; private set; }

        public HotKeyEventArgs(HotKey hotKey, T data)
            : base(hotKey)
        {
            Data = data;
        }
    }

    public delegate void HotKeyEventHandler(object sender, HotKeyEventArgs e);
    public delegate void HotKeyEventHandler<T>(object sender, HotKeyEventArgs<T> e);

    public class HotKey : IEquatable<HotKey>
    {
        internal static int CalculateID(ModifierKeys modifiers, Keys key)
        {
            return (int)modifiers + ((int)key << 4);
        }

        public readonly ModifierKeys ModifierKey;
        public readonly Keys Key;
        public readonly int id;

        internal HotKey(ModifierKeys modifiers, Keys key)
        {
            this.ModifierKey = modifiers;
            this.Key = key;
            this.id = HotKey.CalculateID(modifiers, key);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (obj is HotKey) && this.Equals(obj as HotKey);
        }

        public bool Equals(HotKey other)
        {
            return other != null && other.id == this.id;
        }

        public override string ToString()
        {
            return $"{ModifierKey}  {Key}";
        }
    }


    /// <summary>
    /// Specifies the set of modifier keys.
    /// flags enum, 4 bits, 0 == none
    /// 1111
    /// </summary>
    [Flags]
    public enum ModifierKeys
    {
        //
        // Summary:
        //     No modifiers are pressed.
        None = 0,
        //
        // Summary:
        //     The ALT key.
        Alt = 1,
        //
        // Summary:
        //     The CTRL key.
        Control = 2,
        //
        // Summary:
        //     The SHIFT key.
        Shift = 4,
        //
        // Summary:
        //     The Windows logo key.
        Windows = 8
    }


    #endregion


    #region Size

    public class SizeEventArgs : EventArgs
    {
        public SizeEventArgs(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }
    }

    #endregion


}
