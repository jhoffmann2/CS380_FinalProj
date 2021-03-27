using System;
using System.Collections.Generic;
using UnityEngine;

namespace Planning
{
  public class Blackboard
  {
    private readonly Dictionary<string, object> state
      = new Dictionary<string, object>();

    public Blackboard()
    {}

    public Blackboard(Blackboard other)
    {
      state = new Dictionary<string, object>(other.state);
    }

    public int GetInt(string key)
    {
      if (state.TryGetValue(key, out object v))
        return (int)v;
      state[key] = 0;
      return 0;
    }

    public void SetInt(string key, int val)
    {
      state[key] = val;
    }

    public bool GetBool(string key)
    {
      if (state.TryGetValue(key, out object v))
        return (bool)v;
      state[key] = false;
      return false;
    }

    public void SetBool(string key, bool val)
    {
      state[key] = val;
    }

    public float GetFloat(string key)
    {
      if (state.TryGetValue(key, out object v))
        return (float)v;
      state[key] = 0f;
      return 0f;
    }

    public void SetFloat(string key, float val)
    {
      state[key] = val;
    }

    public string GetString(string key)
    {
      if (state.TryGetValue(key, out object v))
        return (string)v;
      state[key] = "";
      return "";
    }

    public void SetString(string key, string val)
    {
      state[key] = val;
    }

    public object Get(string key)
    {
      if (state.TryGetValue(key, out object v))
        return v;
      state[key] = null;
      return null;
    }

    public void Set(string key, object val)
    {
      state[key] = val;
    }
  }
}