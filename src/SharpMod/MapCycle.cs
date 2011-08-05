using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using SharpMod;

public class DefaultMapCycle : IEnumerable<string>
{
  public string MapCycleFile { get; protected set; }
  public string[] Maps { get; protected set; }

  public DefaultMapCycle()
    : this(Path.Combine(Server.GameDirectory, "mapcycle.txt"))
  {
  }

  public DefaultMapCycle(string mapCycleFile)
  {
    MapCycleFile = mapCycleFile;

    List<string> maps = new List<string>();
    using (StreamReader sr = new StreamReader(File.OpenRead(MapCycleFile)))
    {
      while (!sr.EndOfStream) {
        string line = sr.ReadLine();
        if (CheckMap(line)) {
          maps.Add(line);
        }
      }
    }

    Maps = maps.ToArray();
  }

  bool CheckMap(string map)
  {
    return map.Length > 0;
  }

  #region IEnumerable implementation
  public IEnumerator<string> GetEnumerator()
  {
    return Forever(Maps);
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }

  IEnumerator<T> Forever<T>(IEnumerable<T> list)
  {
    var enumerator = list.GetEnumerator();
    while (true) {
      if (enumerator.MoveNext()) {
        yield return enumerator.Current;
      } else {
        enumerator.Reset();
      }
    }
  }
  #endregion
}

public class MapCycle
{
  static MapCycle()
  {
    Reload();
  }

  public static DefaultMapCycle Default { get; set; }

  public static void Reload()
  {
    Default = new DefaultMapCycle();
  }

  public static void Reload(string map)
  {
    Default = new DefaultMapCycle(map);
  }

  static LinkedList<IEnumerator<string>> ll = new LinkedList<IEnumerator<string>>();

  public static void Add(IEnumerator<string> maplist)
  {
    ll.AddLast(maplist);
  }

  public static IEnumerator<string> Pop()
  {
    var result = ll.First.Value;
    ll.RemoveFirst();
    return result;
  }

  public static void Push(IEnumerator<string> maplist)
  {
    ll.AddFirst(maplist);
  }

  public static void Push(IEnumerator maplist)
  {
    Push((IEnumerator<string>)maplist);
  }

  public static void Push(string map)
  {
    var arr = new string[] { map };
    Push(arr.GetEnumerator());
  }

  public static IEnumerator<string> Peek()
  {
    if (ll.Count == 0) {
      return null;
    }
    return ll.First.Value;
  }

  public static string Next()
  {
    var map = NextMap();
    if (map == null) {
      var e = Default.GetEnumerator();
      e.MoveNext();
      return e.Current;
    }
    return map;
  }

  static string NextMap()
  {
    while (true) {
      var enumerator = Peek();
      if (enumerator == null) {
        return null;
      }
      if (!enumerator.MoveNext()) {
        Pop();
      } else {
        return enumerator.Current;
      }
    }
  }

  public static void Init()
  {
    Message.Intercept(30, (Action)ChangeMap);
  }

  public static void ChangeMap()
  {
    Server.Commands.ChangeLevel(Next());
  }
}
