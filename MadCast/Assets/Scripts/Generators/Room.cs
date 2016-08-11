using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Models;
using UnityEngine;

namespace Assets.Scripts.Generators
{
    public class Room
    {
        private int MaxNumberOfTiles;
        private int MinNumberOfTiles;

        private string seed = "";
        private int cumulative;

        private string previousArrow;
        private Vector2 previousTile;

        private readonly bool useRandomSeed;
        private int rndNum;
        public List<Vector2> TilesList;

        private Direction[] directions = {
                new Direction(0F, 1F, 25, "up"),
                new Direction(0F, -1F, 25, "down"),
                new Direction(1F, 0F, 25, "right"),
                new Direction(-1F, 0F, 25, "left")
        };

        public Room()
        {

            useRandomSeed = true;
        }

        public List<Vector2> Generate(int maxNumberOfTiles)
        {
            TilesList = new List<Vector2>();
            TilesList.Add(Vector2.zero);

            //Use random seed to Generate the rooms
            if (useRandomSeed)
            {
                seed = Time.time.ToString();

            }

            System.Random randomNumber = new System.Random();

            previousTile = Vector2.zero;
            previousArrow = "";

            //Generate for the set of Number
            for (int i = 0; i < maxNumberOfTiles; i++)
            {
                //For every new random number
                rndNum = randomNumber.Next(1, 100);
                Vector2 vec2;

                //Rearrange Directionss' chance in ASC order
                directions = directions.OrderBy(x => x.Chance).ToArray();
                
                //Generate the tile
                GenerateTile(rndNum, out vec2);

                //Add to list
                TilesList.Add(vec2);
            }
            return TilesList;
        }

        public void GenerateTile(int ranomdNum, out Vector2 vec2)
        {
            vec2 = Vector2.zero;
            cumulative = 0;

            //Check every direction
            foreach (var direction in directions)
            {
                Vector2 currentDir = new Vector2(direction.X, direction.Y);
                Vector2 nextTile = previousTile + currentDir;

                //Cumaltively
                cumulative += direction.Chance;

                //Check random number if it is in chances' range
                if (ranomdNum <= cumulative)
                {
                    //If list not contains the next Step/Tile
                    if (!TilesList.Contains(nextTile))
                    {
                        //"Add to it later"
                        vec2 = nextTile;
                        //Substract from that directions' chance
                        direction.Chance -= 3;
                    }
                    //If the direction changed then set all chances to a arbitaryly default values
                    if (previousArrow != direction.Arrow)
                    {
                        foreach (var dir in directions)
                        {
                            dir.Chance = 26;
                        }

                        direction.Chance = 22;

                        //Set previous for next direction and return
                        previousArrow = direction.Arrow;
                        previousTile = nextTile;
                        break;
                    }

                    //Set previous for next direction and return
                    previousArrow = direction.Arrow;
                    previousTile = nextTile;
                    break;
                }
                //Add 1 to chance if that wasn't the way he steps to
                //This way we won't get a too narrow Passages
                direction.Chance += 1;
            }
        }
    }
}