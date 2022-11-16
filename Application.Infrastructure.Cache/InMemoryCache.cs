using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Application.CodeChallenge.Infrastructure.Cache
{
    //This memory implementation was created only for test, .NET already have build-in cache.
    public class InMemoryCache
    {
        private readonly ConcurrentDictionary<string, object> _dictionary;
        private readonly LinkedList<string> _trackList = new();

        public int? EntryCountLimit { get; }

        public int EntryCount => _dictionary.Count;

        public InMemoryCache()
        {
            _dictionary = new();
        }

        public InMemoryCache(int entryCountLimit)
        {
            EntryCountLimit = entryCountLimit;
            _dictionary = new(Environment.ProcessorCount, entryCountLimit);
        }

        public object Get(string key)
        {
            return _dictionary.TryGetValue(key, out var entry) ? entry : null;
        }

        public void Add(string key, object entry)
        {
            lock (_trackList)
            {
                if (_dictionary.ContainsKey(key))
                {
                    _dictionary[key] = entry;
                    _trackList.Remove(key);
                }
                else
                {
                    if (EntryCountLimit is { } && EntryCount == EntryCountLimit)
                    {
                        var oldestEntryKey = _trackList.First();
                        _trackList.RemoveFirst();
                        _dictionary.TryRemove(oldestEntryKey, out _);
                    }

                    _dictionary.TryAdd(key, entry);
                }

                _trackList.AddLast(key);
            }
        }
    }
}