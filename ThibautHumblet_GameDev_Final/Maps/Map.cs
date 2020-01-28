﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThibautHumblet_GameDev_Final.Maps
{
    class Map
    {
        private List<string> _map
        {
            get
            {
                if (!PlayingState._level.Player.WorldShift)
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
                                "0000000000000000000000000000000000000000343333000000000000000000000000",
                                "0000000000000000000000000000000000000000055600000000000000000000000000",
                                "0$00000014000000000000000000000000000000000004000000000000000000000000",
                                "0434000000000000000000000000000000000000000000000004000300040000000000",
                                "00000000011000000400003000003003456000000^00030000410000000002100300<0",
                                "!!!!!!!!!211343441!!!!2!!!!!1!!5565!!!!!!1!!!2!!!!21!!!!!!!!!11155!!2!",
                                "4431212165566666656555665666665556655566556555656555555566565656566555"
                                };
                        default:
                            return new List<string>()
                            {
                                "1"
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
                                "0000000000000000000000000000000000003400000330000000000000000000000000",
                                "0000000440000000000000000000000000042000005600030000000000000000000000",
                                "0$00000000000000000000000000000000310000000000000000000000000000000000",
                                "0000000001400000000000000000000003100000000000030000040004000000000000",
                                "00000000000000000003000004000003456000000^00030000400000000002100300<0",
                                "!!!!!!!!!21134344431!!!!!2!!!!!5565!!!!!!1!!!244442!!!!!!!!!!11155!!2!",
                                "4431212165566666656555665666665556655566556555656555555566565656566555"
                            };
                        default:
                            return new List<string>()
                            {
                                "1"
                            };
                    }

                }
            }
        }

        public List<string> Load()
        {
            return _map;
        }
    }
}
