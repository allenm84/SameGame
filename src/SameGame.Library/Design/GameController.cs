using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Common.Extensions;

namespace SameGame
{
  public class GameController
  {
    private int rows;
    private int columns;
    private GamePiece[,] board;
    private Random random = new Random();
    private GameColor[] colors;
    private int score ;
    private int[][] mods;
    private bool gameWon;

    public int Rows { get { return rows; } }
    public int Columns { get { return columns; } }

    public GamePiece this[int row, int column]
    {
      get { return board[row, column]; }
    }

    public int Score { get { return score; } }
    public bool GameWon { get { return gameWon; } }

    public GameController(int rows = 10, int columns = 10)
    {
      this.rows = rows;
      this.columns = rows;

      mods = new[]
      {
        new[] { -1, 0 },
        new[] { 0, -1 },
        new[] { 1, 0 },
        new[] { 0, 1 },
      };

      colors = new GameColor[] { GameColor.Goldenrod, GameColor.Red, GameColor.DarkGreen };
      //colors = new GameColor[] { GameColor.Goldenrod, GameColor.DarkGreen };
      ResetBoard();
    }

    public void ResetBoard()
    {
      score = 0;
      board = new GamePiece[rows, columns];
      gameWon = false;

      int r, c, len = colors.Length;
      for (r = 0; r < rows; ++r)
      {
        for (c = 0; c < columns; ++c)
        {
          board[r, c] = new GamePiece
          {
            Color = colors[random.Next(len)],
            Column = c,
            Row = r,
          };
        }
      }
    }

    public bool Click(GamePiece piece)
    {
      int r, c;
      for (r = 0; r < rows; ++r)
      {
        for (c = 0; c < columns; ++c)
        {
          var current = board[r, c];
          if (current == null) continue;
          if (current == piece)
          {
            return Click(r, c);
          }
        }
      }

      throw new Exception("The piece could not be found");
    }

    public bool Click(int r, int c)
    {
      if (!IsValidPiece(r, c)) return false;
      bool[,] visited = new bool[rows, columns];

      int matches = CountMatches(r, c, visited, board[r, c].Color);
      if (matches == 1) return false;

      int counter = matches - 1;
      score = ((score++) + ((counter - 2) * (counter - 2)));

      InternalClick(r, c, board[r, c].Color);
      PerformShift();

      bool gameOver = IsGameOver();
      bool cleared = true;

      if (gameOver)
      {
        for (r = 0; cleared && r < rows; ++r)
        {
          for (c = 0; cleared && c < columns; ++c)
          {
            if (board[r, c] != null)
            {
              cleared = false;
            }
          }
        }

        if (cleared)
        {
          score *= 5;
          gameWon = true;
        }
      }

      return gameOver;
    }

    private bool IsGameOver()
    {
      int r, c;

      for (r = 0; r < rows; ++r)
      {
        for (c = 0; c < columns; ++c)
        {
          var piece = board[r, c];
          if (piece == null) continue;
          if (GetNeighbors(r, c).Any(p => p.Color == piece.Color))
          {
            return false;
          }
        }
      }

      return true;
    }

    private IEnumerable<GamePiece> GetNeighbors(int r, int c)
    {
      int sr, sc;
      for (int i = 0; i < 4; ++i)
      {
        var mod = mods[i];
        sr = r + mod[0];
        sc = c + mod[1];

        if (IsValidPiece(sr, sc))
        {
          yield return board[sr, sc];
        }
      }
    }

    private int CountMatches(int r, int c, bool[,] visited, GameColor color)
    {
      int count = 0;

      // if this is the same color, then clear the piece
      if (IsValidPiece(r, c) && board[r, c].Color == color && !visited[r,c])
      {
        // update the count
        ++count;

        // make sure we mark this as visited
        visited[r, c] = true;

        // check the neighbors
        int sr, sc;
        for (int i = 0; i < 4; ++i)
        {
          var mod = mods[i];
          sr = r + mod[0];
          sc = c + mod[1];
          count += CountMatches(sr, sc, visited, color);
        }
      }

      return count;
    }

    private void PerformShift()
    {
      int r, c;

      // shift down
      for (c = 0; c < columns; ++c)
      {
        var column = GetPiecesInColumn(c).ToList();
        for (r = rows - 1; r > -1; --r)
        {
          if (column.Count == 0)
          {
            board[r, c] = null;
          }
          else
          {
            board[r, c] = column.Pop(column.Count - 1);
          }
        }
      }

      // shift left
      for (r = 0; r < rows; ++r)
      {
        var row = GetPiecesInRow(r).ToList();
        for (c = 0; c < columns; ++c)
        {
          if (row.Count == 0)
          {
            board[r, c] = null;
          }
          else
          {
            board[r, c] = row.Pop(0);
          }
        }
      }
    }

    private IEnumerable<GamePiece> GetPiecesInColumn(int c)
    {
      for (int r = 0; r < rows; ++r)
      {
        var piece = board[r, c];
        if (piece == null) continue;
        yield return piece;
      }
    }

    private IEnumerable<GamePiece> GetPiecesInRow(int r)
    {
      for (int c = 0; c < columns; ++c)
      {
        var piece = board[r, c];
        if (piece == null) continue;
        yield return piece;
      }
    }

    private bool IsValidPiece(int r, int c)
    {
      if (!(-1 < r && r < rows)) return false;
      if (!(-1 < c && c < columns)) return false;
      if (board[r, c] == null) return false;
      return true;
    }

    private void InternalClick(int r, int c, GameColor color)
    {
      if (!IsValidPiece(r, c)) return;

      // if this is the same color, then clear the piece
      if (board[r, c].Color == color)
      {
        board[r, c] = null;

        // click the neighbors
        int sr, sc;
        for (int i = 0; i < 4; ++i)
        {
          var mod = mods[i];
          sr = r + mod[0];
          sc = c + mod[1];
          InternalClick(sr, sc, color);
        }
      }
    }
  }
}
