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
        public static SpriteFont controlsFont;
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
            controlsFont = content.Load<SpriteFont>("Fonts/ControlsFont");
            // title screen inladen
            titleScreen = content.Load<Texture2D>("TitleScreen");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Game1.mainMenu)
            {
                spriteBatch.Draw(titleScreen, new Rectangle(0, 0, _screenWidth, _screenHeight), null, Color.White);
                if (Player.IsDead)
                    spriteBatch.DrawString(gameOverFont, "GAME OVER", new Vector2((_screenWidth / 2.7f), 300), Color.White);
                else if (!Game1.gewonnen)
                {
                    spriteBatch.DrawString(font, "Druk op ENTER om het spel te starten", new Vector2(90, 600), Color.White);

                    spriteBatch.DrawString(controlsFont, "Controls:", new Vector2(90, 350), Color.White);
                    spriteBatch.DrawString(controlsFont, "-> : Links", new Vector2(90, 390), Color.White);
                    spriteBatch.DrawString(controlsFont, "<- : Rechts", new Vector2(90, 430), Color.White);
                    spriteBatch.DrawString(controlsFont, "^ of SPACE : Springen", new Vector2(90, 470), Color.White);
                    spriteBatch.DrawString(controlsFont, "CTRL : WereldShift", new Vector2(90, 510), Color.White);
                }
                else if (Game1.gewonnen)
                {
                    spriteBatch.DrawString(gameOverFont, "GEWONNEN!", new Vector2((_screenWidth / 2.7f), 300), Color.White);
                    spriteBatch.DrawString(font, "Bedankt om het spel te spelen", new Vector2((_screenWidth / 3.1f), 400), Color.White);
                }
                spriteBatch.DrawString(font, "Druk op ESCAPE om het spel af te sluiten", new Vector2(90, 640), Color.White);
            }
        }
    }
}
