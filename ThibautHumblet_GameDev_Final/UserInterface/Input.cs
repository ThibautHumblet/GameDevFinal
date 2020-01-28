using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ThibautHumblet_GameDev_Final.UserInterface
{
    public class Input
    {
        public KeyboardState keyboard, oldKeyboard;

        public Input() { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] //marcro gebruikt om de code iets compacter te maken
        public bool Keypress(Keys k)
        {
            if (keyboard.IsKeyDown(k) && oldKeyboard.IsKeyUp(k))
                return true;
            else
                return false;
        }
        public bool Keydown(Keys k)
        {
            if (keyboard.IsKeyDown(k))
                return true;
            else
                return false;
        }

        public void Update()
        {
            oldKeyboard = keyboard;

            keyboard = Keyboard.GetState();
        }
    }
}
