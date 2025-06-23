using System;
using System.Collections.Generic;
using ObservableCollections;
using R3;

namespace EventSystem
{
    public class EventBus : IDisposable
    {
        private readonly ObservableDictionary<Type, object> _streams = new ();
        public IReadOnlyObservableDictionary<Type, object> Streams => _streams;

        public IObservable<T> Receive<T>()
        {
            if (!_streams.TryGetValue(typeof(T), out var stream))
            {
                stream = new Subject<T>();
                _streams[typeof(T)] = stream;
            }

            return (IObservable<T>)stream;
        }

        public void Invoke<T>(T signal)
        {
            if (_streams.TryGetValue(typeof(T), out var stream))
            {
                ((ISubject<T>)stream).OnNext(signal);
            }
        }
        
        public void Dispose()
        {
            foreach (var stream in _streams)
            {
                if (stream.Value is IDisposable disposable)
                    disposable.Dispose();
            }

            _streams.Clear();
        }
    }
}