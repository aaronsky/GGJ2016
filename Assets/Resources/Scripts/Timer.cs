using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Timer : MonoBehaviour {

    public int hours = 7;
    public int minutes = 30;
    public float tickInterval = 0.5f;
    private static Dictionary<int, Dictionary<int, List<Action<int, int>>>> eventTable = new Dictionary<int, Dictionary<int, List<Action<int, int>>>>();


    void Awake()
    {
        eventTable = new Dictionary<int, Dictionary<int, List<Action<int, int>>>>();
        eventTable.Add(-1, new Dictionary<int, List<Action<int, int>>>());
        eventTable[-1].Add(-1, new List<Action<int, int>>());
    }

    // Use this for initialization
    void Start ()
    {
        FireEventAt(hours, minutes);
        StartCoroutine("Tick");
    }

    private IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickInterval);
            minutes += 1;
            if (minutes > 59)
            {
                minutes = 0;
                hours += 1;
                if (hours > 23)
                {
                    hours = 0;
                }
            }
            FireEventAt(hours, minutes);
        }
    }

    private void FireEventAt(int hour, int minute)
    {
        Dictionary<int, List<Action<int, int>>> minuteTable;
        if (eventTable.TryGetValue(hour, out minuteTable))
        {
            List<Action<int, int>> events;
            if (minuteTable.TryGetValue(minute, out events))
            {
                if (events.Count > 0)
                {
                    foreach (var cb in events)
                    {
                        cb.Invoke(hour, minute);
                    }

                }
            }
        }
        var allTickEvents = eventTable[-1][-1];
        if (allTickEvents != null && allTickEvents.Count > 0)
        {
            foreach (var cb in allTickEvents)
            {
                cb.Invoke(hour, minute);
            }
        }
    }

    /// <summary>
    /// Fires every tick
    /// </summary>
    /// <param name="cb">A function that takes in an hour integer and a minute integer</param>
    public static void Subscribe(Action<int, int> cb)
    {
        Subscribe(cb, -1, -1);
    }

    /// <summary>
    /// Fires at the given hour/minute
    /// </summary>
    /// <param name="cb">A function that takes in an hour integer and a minute integer</param>
    /// <param name="expectedHour">The expected firing hour</param>
    /// <param name="expectedMinute">The expected firing minute</param>
    public static void Subscribe(Action<int, int> cb, int expectedHour, int expectedMinute)
    {
        Dictionary<int, List<Action<int, int>>> minuteTable;
        if (eventTable.TryGetValue(expectedHour, out minuteTable))
        {
            List<Action<int, int>> events;
            if (minuteTable.TryGetValue(expectedMinute, out events))
            {
                events.Add(cb);
            } else
            {
                minuteTable.Add(expectedMinute, new List<Action<int, int>>() { cb });
            }
        } else
        {
            eventTable.Add(expectedHour, new Dictionary<int, List<Action<int, int>>>());
            eventTable[expectedHour].Add(expectedMinute, new List<Action<int, int>>() { cb });
        }
    }
}
