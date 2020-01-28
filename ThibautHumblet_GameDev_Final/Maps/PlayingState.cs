using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThibautHumblet_GameDev_Final.Components;
using ThibautHumblet_GameDev_Final.Interfaces;
using ThibautHumblet_GameDev_Final.Sprites;

namespace ThibautHumblet_GameDev_Final.Maps
{
    public class PlayingState : State
    {
        private LevelModel _level;

        private ObservableCollection<Component> _components;

        private IEnumerable<IMoveable> _worldObjects;

        private IEnumerable<Sprite> _sprites;

        private TileTypes _tileType;

        private Texture2D _texture;

        private List<string> _map
        {
            get
            {
                if (!_level.Player.WorldShift)
                {
                    switch (Game1.Level)
                    {
                        case 0:
                            return new List<string>()
                {
                                "0000000000000130000000000000000000000000000000000000005565500000000",
                                "00000000>000021333300000000000003440000033000100030000065600000$000",
                                "0000000012430155651300000000340000000000561000000003000560000211000",
                                "!!!!!!!!6655!5555666!!!!!!6656!!!!!!!!!!565!!!!!!!!5556655566556!!!",
                                "4431212165566666656555665666665556655566556555656555555566565656566",
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
                else
                {
                    switch (Game1.Level)
                    {
                        case 0:
                            return new List<string>()
                {
                                "0000000000000000000000000000000000000340000000000000005565500000000000000",
                                "00000000>000000343300000000000000040000030000003000000000000000$000000000",
                                "0000000012430031251300430000340000000000530000000003000000000211000000000",
                                "!!!!!!!!6655!!655666!!66666656!!!!!!!!!!56!!!!!!!!!5556655566556!!!!!!!!!",
                                "4431212165566666656555665666665556655566556555656555555566565656566431212",
                };
                        case 1:
                            return new List<string>()
                {
                                "0000000000000000000000000000000000000000000000000000000000000000000000000",
                                "0000000000000000000000000000000000000000000000000000000000000000000000000",
                                "0000000000000000000000000000000000000000000000000000000000042113000000000",
                                "!!!!!!!!11111111111111111111111111111111111111111111111111111111!!!!!!!!!",
                                "4431212165566666656555665666665556655566556555656555555566565656566431212",
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
                                _texture = _content.Load<Texture2D>("crystal01"); ;
                                _tileType = TileTypes.Crystal;
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

            if (_level.Player.WorldShift)
            {
                LoadContent();
            }
        }

        public void NextLevel()
        {
            Game1.Level++;
            Game1.Player.Position = new Vector2(0, 0);
            Game1.AchtergrondPositie.X = 0;
            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Draw(gameTime, _spriteBatch);
        }

    }
}
