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

            for (int i = 0; i < maxNumberOfTiles; i++)
            {
                rndNum = randomNumber.Next(1, 100);
                Vector2 vec2;

                directions = directions.OrderBy(x => x.Chance).ToArray();
                GenerateTile( rndNum, out vec2);

                if (vec2 != Vector2.zero)
                {
                    TilesList.Add(vec2);
                }

            }
            return TilesList;
        }

        public void GenerateTile( int ranomdNum, out Vector2 vec2)
        {
            vec2 = Vector2.zero;
            cumulative = 0;

            foreach (var direction in directions)
            {
                Vector2 currentDir = new Vector2(direction.X, direction.Y);
                Vector2 nextTile = previousTile + currentDir;

                cumulative += direction.Chance;

                if (ranomdNum <= cumulative)
                {
                    // Debug.Log(nextTile);
                    if (!TilesList.Contains(nextTile))
                    {
                        vec2 = nextTile;
                        direction.Chance -= 3;
                    }

                    if (previousArrow != direction.Arrow)
                    {
                        foreach (var dir in directions)
                        {
                            dir.Chance = 26;
                        }

                        direction.Chance = 22;
                        previousArrow = direction.Arrow;
                        previousTile = nextTile;
                        break;
                    }

                    previousArrow = direction.Arrow;
                    previousTile = nextTile;
                    break;
                }

                direction.Chance += 1;
            }
        }
    }
}