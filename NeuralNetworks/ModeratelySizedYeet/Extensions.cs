using GameLibrary;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_Solver
{
    public static class Extensions
    {
        public static void DrawBoard(this SpriteBatch spriteBatch, Board board, Texture2D tile, SpriteFont font, Dictionary<int, Color> squareColors)
        {
            int scale = 20;
            for (int x = 0; x < board.grid.GetLength(1); x++)
            {
                for (int y = 0; y < board.grid.GetLength(0); y++)
                {
                    spriteBatch.Draw(tile, position: new Vector2(x * tile.Width, y * tile.Height) * scale, color: squareColors[board.grid[y, x].value], scale: Vector2.One * scale);
                    if (board.grid[y, x].value != 0)
                    {
                        var size = font.MeasureString(board.grid[y, x].value.ToString());

                        spriteBatch.DrawString(font, board.grid[y, x].value.ToString(), new Vector2((x * tile.Width) + tile.Width / 2, (y * tile.Height) + tile.Height / 2) * scale - size / 2, Color.Black);
                    }
                }
            }
        }

        public static void DrawBoard(this SpriteBatch spriteBatch, Board board, Texture2D tile, SpriteFont font, Dictionary<int, Color> squareColors, double Scale, Vector2 position)
        {
            int scale = (int)(20 * Scale);
            for (int x = 0; x < board.grid.GetLength(1); x++)
            {
                for (int y = 0; y < board.grid.GetLength(0); y++)
                {
                    spriteBatch.Draw(tile, position: new Vector2(x * tile.Width, y * tile.Height) * scale + position, color: squareColors[board.grid[y, x].value], scale: Vector2.One * scale);
                    if (board.grid[y, x].value != 0)
                    {
                        var size = font.MeasureString(board.grid[y, x].value.ToString());

                        spriteBatch.DrawString(font, board.grid[y, x].value.ToString(), (new Vector2((x * tile.Width) + tile.Width / 2, (y * tile.Height) + tile.Height / 2) * scale + position) - size / 2, Color.Black);
                    }
                }
            }
        }
    }
}
