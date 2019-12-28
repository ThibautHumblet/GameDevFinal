using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThibautHumblet_GameDev_Final.Map
{
    public class GameModel
    {
        public ContentManager ContentManger { get; set; }

        public GraphicsDeviceManager GraphicsDeviceManager { get; set; }

        public SpriteBatch SpriteBatch { get; set; }
    }
}
