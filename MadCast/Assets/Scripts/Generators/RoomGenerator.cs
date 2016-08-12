using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Models;
using UnityEngine;

namespace Assets.Scripts.Generators
{
    public static class RoomGenerator
    {
        private static int MaxNumberOfTiles;
        private static int MinNumberOfTiles;
        private static int cumulative;
        private static int rndNum;
        private static string previousArrow;
        private static Vector2 previousTile;
        public static List<Vector2> TilesList;

        private static Direction[] directions = {
                new Direction(0F, 1F, 25, "up"),
                new Direction(0F, -1F, 25, "down"),
                new Direction(1F, 0F, 25, "right"),
                new Direction(-1F, 0F, 25, "left")
        };

        public static List<Vector2> Generate(int maxNumberOfTiles)
        {
            TilesList = new List<Vector2> {Vector2.zero};
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

        public static void GenerateTile( int ranomdNum, out Vector2 vec2)
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