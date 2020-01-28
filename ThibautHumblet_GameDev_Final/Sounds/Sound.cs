using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThibautHumblet_GameDev_Final.Sounds
{
    class Sound
    {
        public static Song music;
        public static SoundEffect jump;
        public static SoundEffect worldShift;

        public static void Load(ContentManager content)
        {
            jump = content.Load<SoundEffect>("Sound/Jump");
            worldShift = content.Load<SoundEffect>("Sound/WorldShift");

            music = content.Load<Song>("Music/Music1");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.4f;
        }
    }
}
