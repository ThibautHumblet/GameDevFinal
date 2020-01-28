using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThibautHumblet_GameDev_Final.Components;
using ThibautHumblet_GameDev_Final.Crystals;
using ThibautHumblet_GameDev_Final.Interfaces;
using ThibautHumblet_GameDev_Final.Sprites;

namespace ThibautHumblet_GameDev_Final.Map
{
    public class PlayingState: State
    {
        private LevelModel _level;

        private ObservableCollection<Component> _components;

        private IEnumerable<IMoveable> _worldObjects;

        private IEnumerable<Sprite> _sprites;

        private TileTypes _tileType;

        private Texture2D _texture;

        private Crystal _crystal = new Crystal();

        private List<string> _map
        {
            get
            {
                switch (Game1.Level)
                {
                    case 0:
                        return new List<string>()
                {
                    "00000000000000000000000110000000000000",
                    "04000010000000000000011000000000000000",
                    "0$0010000$02000000011000000000000$0000",
                    "11111111111111111110000000000111111111",
                    "11112221121112211112211111112211111211",
                    "66622211655521551152566215662215111211"
                };
                    case 1:
                        return new List<string>()
                {
                            "00000000000000000",
                            "00000000000000000",
                            "4444004001401001$"
                };
                    case 2:
                        return new List<string>()
                {
                            "0000000$000000000",
                            "111111111144111111"
                };
                    default:
                        return new List<string>()
                        {
                            "10000",
                            "0001100000",
                            "0010000000",
                            "11111100111",
                            "444114!!144"
                        };
                }

            }
        }

        public PlayingState(GameModel gameModel, LevelModel level)
          : base(gameModel)
        {
            _level = level;
        }

        public override void LoadContent()
        {
            _components = new ObservableCollection<Component>();
            _components.CollectionChanged += UpdateWorldObjects;

            _components.Add(_level.Player);

            foreach (var parallax in _level.Parallaxes)
                _components.Add(parallax);

            int y = 1;
            foreach (var line in _map)
            {
                int x = 1;
                foreach (var character in line)
                {
                    x++;

                    switch (character)
                    {
                        case '1':
                            _texture = _content.Load<Texture2D>("ground05");
                            _tileType = TileTypes.Safe;
                            break;
                        case '2':
                            _texture = _content.Load<Texture2D>("ground06");
                            _tileType = TileTypes.Safe;
                            break;
                        case '3':
                            _texture = _content.Load<Texture2D>("leafy_ground01");
                            _tileType = TileTypes.Safe;
                            break;
                        case '4':
                            _texture = _content.Load<Texture2D>("leafy_ground02");
                            _tileType = TileTypes.Safe;
                            break;
                        case '5':
                            _texture = _content.Load<Texture2D>("rocky01");
                            _tileType = TileTypes.Safe;
                            break;
                        case '6':
                            _texture = _content.Load<Texture2D>("rocky03");
                            _tileType = TileTypes.Safe;
                            break;
                        case '?':
                            _texture = _content.Load<Texture2D>("board01");
                            _tileType = TileTypes.Safe;
                            break;
                        case '>':
                            _texture = _content.Load<Texture2D>("board03");
                            _tileType = TileTypes.Safe;
                            break;
                        case '^':
                            _texture = _content.Load<Texture2D>("board04");
                            _tileType = TileTypes.Safe;
                            break;
                        case '!':
                            _texture = _content.Load<Texture2D>("spikes");
                            _tileType = TileTypes.Spike;
                            break;
                        case '$':
                            if (!Crystal.gotCrystal1)
                            {
                                _texture = _content.Load<Texture2D>("crystal01");;
                                _tileType = TileTypes.Crystal;
                            }
                            else
                                continue;
                            break;
                        default:
                            continue;
                    }

                    var platform = new Tile(_texture)
                    {
                        Position = new Vector2(x * 128, y * 128),
                        Layer = 0.999f,
                        TileType = _tileType,
                    };

                    _components.Add(platform);
                }
                y++;
            }
        }

        private void UpdateWorldObjects(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            _worldObjects = _components.Where(c => c is IMoveable).Cast<IMoveable>();
            _sprites = _components.Where(c => c is Sprite).Cast<Sprite>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);


            foreach (var worldObject in _worldObjects)
            {
                worldObject.Velocity = new Vector2(-_level.Player.Velocity.X, worldObject.Velocity.Y);
            }

            PostUpdate(gameTime);
        }

        public void PostUpdate(GameTime gameTime)
        {
            foreach (var spriteA in _sprites)
            {
                // Don't do anything if they're the same sprite!
                if (spriteA == _level.Player)
                    continue;

                if (_level.Player.IsTouching(spriteA))
                {
                    if (spriteA is Tile)
                    {
                        var platform = (Tile)spriteA;

                        if (platform.TileType == TileTypes.Crystal)
                        {
                            NextLevel();
                        }

                    }


                    _level.Player.OnCollide(spriteA);
                }
            }

            _level.Player.ApplyVelocity(gameTime);
        }

        public void NextLevel()
        {
            Game1.Level++;
            Game1.Player.Position = new Vector2(0, 0);
            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Draw(gameTime, _spriteBatch);
        }

    }
}
