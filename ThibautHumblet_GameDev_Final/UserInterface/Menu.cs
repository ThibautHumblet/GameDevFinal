using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThibautHumblet_GameDev_Final.Sprites;

namespace ThibautHumblet_GameDev_Final.UserInterface
{
    public class Menu
    {

        public static Texture2D titleScreen;
        public static SpriteFont gameOverFont;
        public static SpriteFont font;

        public static int _screenWidth;
        public static int _screenHeight;

        public Menu(int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }

        public void Load(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/Font");
            gameOverFont = content.Load<SpriteFont>("Fonts/GameOverFont");
            // title screen inladen
            titleScreen = content.Load<Texture2D>("TitleScreen");
        }

        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Game1.mainMenu)
            {
                spriteBatch.Draw(titleScreen, new Rectangle(0, 0, _screenWidth, _screenHeight), null, Color.White);
                if (Player.IsDead)
                    spriteBatch.DrawString(gameOverFont, "GAME OVER", new Vector2((_screenWidth / 2.7f), 300), Color.White);
                else
                    spriteBatch.DrawString(font, "Druk op ENTER om het spel te starten", new Vector2(90, 600), Color.White);
                spriteBatch.DrawString(font, "Druk op ESCAPE om het spel af te sluiten", new Vector2(90, 640), Color.White);
            }
        }
    }
}
