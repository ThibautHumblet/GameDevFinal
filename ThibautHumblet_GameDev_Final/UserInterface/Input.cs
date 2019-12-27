using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ThibautHumblet_GameDev_Final.UserInterface
{
    class Input
    {
        public KeyboardState keyboard, oldKeyboard;
        public bool shift_down, control_down, alt_down;
        public bool shift_press, control_press, alt_press;
        public bool old_shift_down, old_control_down, old_alt_down;

        public Input()
        {

        }

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
            // vorige staat onthouden
            old_shift_down = shift_down;
            old_alt_down = alt_down;
            old_control_down = control_down;
            oldKeyboard = keyboard;

            keyboard = Keyboard.GetState();

            // reset toetsaanslag
            shift_down = false;
            shift_press = false;
            alt_down = false;
            alt_press = false;
            control_down = false;
            control_press = false;

            // Het maakt niet uit of LSHIFT of RSHIFT ect. gebruikt worden
            if (keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift))
                shift_down = true;
            if (keyboard.IsKeyDown(Keys.LeftAlt) || keyboard.IsKeyDown(Keys.RightAlt))
                shift_down = true;
            if (keyboard.IsKeyDown(Keys.LeftControl) || keyboard.IsKeyDown(Keys.RightControl))
                shift_down = true;

            // controle of toetsen kort worden ingedrukt ipv ingehouden
            if (shift_down && !old_shift_down)
                shift_press = true;
            if (alt_down && !old_alt_down)
                alt_press = true;
            if (control_down && !old_control_down)
                control_press = true;
        }
    }
}
