using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameGame
{
  public class CellLookup
  {
    private Dictionary<Tuple<int, int>, Cell> lookup = new Dictionary<Tuple<int, int>, Cell>();

    public Cell this[int row, int column]
    {
      get
      {
        var key = CreateKey(row, column);
        Cell cell;
        if (!lookup.TryGetValue(key, out cell))
        {
          cell = null;
        }
        return cell;
      }
      set
      {
        var key = CreateKey(row, column);
        lookup[key] = value;
      }
    }

    private Tuple<int, int> CreateKey(int row, int column)
    {
      return Tuple.Create(row, column);
    }

    public bool Exists(int row, int column)
    {
      return lookup.ContainsKey(CreateKey(row, column));
    }

    public void GetCoordinate(Cell value, out int row, out int column)
    {
      var k = lookup.Single(l => l.Value == value);
      row = k.Key.Item1;
      column = k.Key.Item2;
    }

    public void Move(int oldRow, int oldColumn, int newRow, int newColumn)
    {
      var oldKey = CreateKey(oldRow, oldColumn);
      var newKey = CreateKey(newRow, newColumn);
      var cell = lookup[oldKey];
      lookup.Remove(oldKey);
      lookup.Add(newKey, cell);
    }

    public void Remove(Cell cell)
    {
      var k = lookup.Single(l => l.Value == cell);
      lookup.Remove(k.Key);
    }
  }
}
