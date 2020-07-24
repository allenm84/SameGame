using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Common.Extensions
{
  public static partial class CollectionExtensions
  {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IEnumerable<T> Enumerate<T>(this T value)
    {
      yield return value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="range"></param>
    public static void RemoveRange<T>(this IList<T> list, IEnumerable<T> range)
    {
      foreach (var value in range)
      {
        list.Remove(value);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="range"></param>
    public static void AddRange<T>(this IList<T> list, IEnumerable<T> range)
    {
      foreach (var value in range)
      {
        list.Add(value);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
    {
      return GetValueOrDefault(dictionary, key, default(TValue));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
    {
      return GetValueOrDefault(dictionary, key, () => defaultValue);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> getDefault)
    {
      TValue value;
      if (!dictionary.TryGetValue(key, out value))
      {
        value = getDefault();
        dictionary[key] = value;
      }
      return value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> values)
    {
      return new HashSet<T>(values);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static IEnumerable<T> Iterate<T>(this IList<T> list)
    {
      foreach (var value in list)
      {
        yield return value;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sequence"></param>
    /// <param name="action"></param>
    public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
    {
      foreach (var value in sequence)
      {
        action(value);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sequence"></param>
    /// <returns></returns>
    public static double StandardDev(this IEnumerable<double> values)
    {
      var avg = values.Average();
      var sum = values.Sum(d => Math.Pow(d - avg, 2));
      return Math.Sqrt((sum) / (values.Count() - 1));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sequence"></param>
    /// <param name="start"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static IEnumerable<T> Range<T>(this IEnumerable<T> sequence, int start, int count)
    {
      if (sequence is IList<T>)
      {
        return Range((IList<T>)sequence, start, count);
      }
      return sequence.Skip(start).Take(count);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="start"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static IEnumerable<T> Range<T>(this IList<T> list, int start, int count)
    {
      for (var i = 0; i < count; ++i)
      {
        yield return list[i + start];
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool Remove<T>(this IList<T> list, Func<T, bool> predicate)
    {
      var removed = false;
      for (var i = 0; !removed && i < list.Count; ++i)
      {
        var item = list[i];
        if (predicate(item))
        {
          list.RemoveAt(i);
          removed = true;
          --i;
        }
      }
      return removed;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="list"></param>
    /// <param name="getKey"></param>
    public static void Sort<TValue, TKey>(this IList<TValue> list, Func<TValue, TKey> getKey)
    {
      var comparer = Comparer<TKey>.Default;
      list.QuickSort((a, b) => comparer.Compare(getKey(a), getKey(b)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static int FirstIndex<T>(this IList<T> list, Func<T, bool> predicate)
    {
      var index = -1;
      for (var i = 0; index == -1 && i < list.Count; ++i)
      {
        if (predicate(list[i]))
        {
          index = i;
        }
      }
      return index;
    }

    /// <summary>
    /// Adds an item to the collection if (and only if) it isn't null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="item"></param>
    public static void AddIff<T>(this ICollection<T> collection, T item) where T : class
    {
      if (item != null)
      {
        collection.Add(item);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sequence"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool Exists<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
    {
      return sequence.Count(predicate) > 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static IEnumerable<T> Ignore<T>(this IList<T> list, int index)
    {
      for (var i = 0; i < list.Count; ++i)
      {
        if (i == index) { continue; }
        yield return list[i];
      }
    }

    /// <summary>
    /// Evaluates if every element in the list is the same.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lst"></param>
    /// <returns>True if all the elements are the same; otherwise false.</returns>
    public static bool Same<T>(this IList<T> lst) where T : IEquatable<T>
    {
      if (lst.Count < 1) { return true; }

      var c = lst[0];
      var count = 1;

      for (var i = 1; i < lst.Count; ++i)
      {
        if (lst[i].Equals(c))
        {
          ++count;
        }
      }

      return (count == lst.Count);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bucket"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static IEnumerable<T[]> Choose<T>(this IList<T> bucket, int count)
    {
      return Choose(bucket, count, false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bucket"></param>
    /// <param name="count"></param>
    /// <param name="duplicates"></param>
    /// <returns></returns>
    public static IEnumerable<T[]> Choose<T>(this IList<T> bucket, int count, bool duplicates)
    {
      var running = new List<T>(bucket.Count << 1);
      return choose(count, 0, duplicates, running, bucket);
    }

    private static IEnumerable<T[]> choose<T>(int count, int i, bool duplicates, List<T> running, IList<T> bucket)
    {
      for (; i < bucket.Count; ++i)
      {
        var index = running.Count;
        running.Add(bucket[i]);

        if (count > 1)
        {
          var next = i + (duplicates ? 0 : 1);
          foreach (var t in choose(count - 1, next, duplicates, running, bucket))
          {
            yield return t;
          }
        }
        else
        {
          yield return running.ToArray();
        }
        running.RemoveAt(index);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static decimal Multiply(this IEnumerable<decimal> source)
    {
      decimal retval = 1;
      foreach (var d in source)
      {
        retval *= d;
      }
      return retval;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static double Multiply(this IEnumerable<double> source)
    {
      double retval = 1;
      foreach (var d in source)
      {
        retval *= d;
      }
      return retval;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static float Multiply(this IEnumerable<float> source)
    {
      float retval = 1;
      foreach (var d in source)
      {
        retval *= d;
      }
      return retval;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int Multiply(this IEnumerable<int> source)
    {
      var retval = 1;
      foreach (var d in source)
      {
        retval *= d;
      }
      return retval;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static long Multiply(this IEnumerable<long> source)
    {
      long retval = 1;
      foreach (var d in source)
      {
        retval *= d;
      }
      return retval;
    }

    private static Comparison<T> CreateComparison<T>() where T : IComparable<T>
    {
      return (T a, T b) => { return a.CompareTo(b); };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="comparer"></param>
    public static void QuickSort<T>(this IList<T> list, IComparer<T> comparer)
    {
      qs(list, 0, list.Count - 1, comparer.Compare);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="comparison"></param>
    public static void QuickSort<T>(this IList<T> list, Func<T, T, int> comparison)
    {
      qs(list, 0, list.Count - 1, comparison);
    }

    private static void qs<T>(IList<T> items, int left, int right, Func<T, T, int> compare)
    {
      if (items.Count == 0) { return; }

      int i, j;
      T x, y;

      i = left;
      j = right;
      x = items[(left + right) / 2];

      do
      {
        while (compare(items[i], x) < 0 && (i < right)) { i++; }
        while (compare(x, items[j]) < 0 && (j > left)) { j--; }

        if (i <= j)
        {
          y = items[i];
          items[i] = items[j];
          items[j] = y;
          i++;
          j--;
        }
      } while (i <= j);

      if (left < j) { qs(items, left, j, compare); }
      if (i < right) { qs(items, i, right, compare); }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="items"></param>
    public static void TransferTo<T>(this IList<T> source, IList<T> destination, IEnumerable<T> items)
    {
      foreach (var value in items)
      {
        source.Remove(value);
      }

      foreach (var value in items)
      {
        destination.Add(value);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lst"></param>
    public static void InsertionSort<T>(this IList<T> lst) where T : IComparable<T>
    {
      lst.InsertionSort(CreateComparison<T>());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lst"></param>
    /// <param name="comparison"></param>
    public static void InsertionSort<T>(this IList<T> lst, Comparison<T> comparison)
    {
      for (var i = 1; i < lst.Count; ++i)
      {
        var value = lst[i];
        var j = i - 1;

        while (j >= 0 && comparison(lst[j], value) > 0)
        {
          lst[j + 1] = lst[j];
          --j;
        }

        lst[j + 1] = value;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lst"></param>
    public static void BubbleSort<T>(this IList<T> lst) where T : IComparable<T>
    {
      lst.BubbleSort(CreateComparison<T>());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lst"></param>
    /// <param name="comparison"></param>
    public static void BubbleSort<T>(this IList<T> lst, Comparison<T> comparison)
    {
      var n = lst.Count;
      var swapped = false;
      do
      {
        swapped = false;
        --n;

        for (var i = 0; i <= (n - 1); ++i)
        {
          if (comparison(lst[i], lst[i + 1]) > 0)
          {
            var tmp = lst[i];
            lst[i] = lst[i + 1];
            lst[i + 1] = tmp;
            swapped = true;
          }
        }
      } while (swapped);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lst"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static T Pop<T>(this IList<T> lst, int index)
    {
      var item = lst[index];
      lst.RemoveAt(index);
      return item;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lst"></param>
    /// <param name="popIfTrue"></param>
    /// <returns></returns>
    public static IEnumerable<T> PopAll<T>(this IList<T> lst, Func<T, bool> popIfTrue)
    {
      for (var i = 0; i < lst.Count; ++i)
      {
        var item = lst[i];
        if (popIfTrue(item))
        {
          lst.RemoveAt(i--);
          yield return item;
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T PopMin<T>(this IList<T> list) where T : IComparable<T>
    {
      var minIndex = 0;
      var minValue = list[0];

      for (var i = 1; i < list.Count; ++i)
      {
        var value = list[i];
        if (value.CompareTo(minValue) < 0)
        {
          minValue = value;
          minIndex = i;
        }
      }

      list.RemoveAt(minIndex);
      return minValue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
      var rand = new Random((int)DateTime.Now.Ticks);
      return source
        .Select(t => new {Rank = rand.Next(), Value = t})
        .OrderBy(p => p.Rank)
        .Select(p => p.Value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int BinarySearch<T>(this IList<T> list, T value) where T : IComparable<T>
    {
      var low = 0;
      var high = list.Count - 1;

      while (low <= high)
      {
        var midpoint = low + (high - low) / 2;
        var comparison = list[midpoint].CompareTo(value);

        if (comparison == 0)
        {
          return midpoint;
        }
        if (comparison > 0)
        {
          high = midpoint - 1;
        }
        else
        {
          low = midpoint + 1;
        }
      }
      return ~low;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="value"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static int BinarySearch<T>(this IList<T> list, T value, IComparer<T> comparer)
    {
      var low = 0;
      var high = list.Count - 1;

      while (low <= high)
      {
        var midpoint = low + (high - low) / 2;
        var comparison = comparer.Compare(list[midpoint], value);

        if (comparison == 0)
        {
          return midpoint;
        }
        if (comparison > 0)
        {
          high = midpoint - 1;
        }
        else
        {
          low = midpoint + 1;
        }
      }
      return ~low;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="value"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static int BinarySearch<T>(this IList<T> list, T value, Comparison<T> comparer)
    {
      var low = 0;
      var high = list.Count - 1;

      while (low <= high)
      {
        var midpoint = low + (high - low) / 2;
        var comparison = comparer(list[midpoint], value);

        if (comparison == 0)
        {
          return midpoint;
        }
        if (comparison > 0)
        {
          high = midpoint - 1;
        }
        else
        {
          low = midpoint + 1;
        }
      }
      return ~low;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TList"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="list"></param>
    /// <param name="value"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static int BinarySearch<TList, TValue>(this IList<TList> list, TValue value, Func<TList, TValue, int> comparer)
    {
      var low = 0;
      var high = list.Count - 1;

      while (low <= high)
      {
        var midpoint = low + (high - low) / 2;
        var comparison = comparer(list[midpoint], value);

        if (comparison == 0)
        {
          return midpoint;
        }
        if (comparison > 0)
        {
          high = midpoint - 1;
        }
        else
        {
          low = midpoint + 1;
        }
      }
      return ~low;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool BinaryContains<T>(this IList<T> list, T value)
    {
      return BinaryContains(list, value, Comparer<T>.Default);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="value"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static bool BinaryContains<T>(this IList<T> list, T value, IComparer<T> comparer)
    {
      var low = 0;
      var high = list.Count - 1;

      while (low <= high)
      {
        var midpoint = low + (high - low) / 2;
        var comparison = comparer.Compare(list[midpoint], value);

        if (comparison == 0)
        {
          return midpoint > -1;
        }
        if (comparison > 0)
        {
          high = midpoint - 1;
        }
        else
        {
          low = midpoint + 1;
        }
      }
      return false;
    }
  }
}