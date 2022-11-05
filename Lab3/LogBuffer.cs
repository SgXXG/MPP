using System.Collections.Concurrent;
using System.Text;

namespace MPP_Lab_3_2;

public class LogBuffer : IDisposable
{

    private readonly object _writerLocker = new();
    private readonly object _addingLocker = new();

    private readonly string _path;
    private readonly int _maxMessagesCount;
    private int _waitingMessages;

    private readonly ConcurrentQueue<string> _messages;

    public LogBuffer(string path, int maxMessagesCount = 10)
    {
        if (maxMessagesCount <= 0)
        {
            throw new ArgumentException("Max messages count cannot be less than or equals to zero.");
        }

        _path = path;
        _maxMessagesCount = maxMessagesCount;
        _waitingMessages = 0;

        _messages = new ConcurrentQueue<string>();
    }

    public void Add(string item)
    {
        lock (_addingLocker)
        {
            _messages.Enqueue(item);
            _waitingMessages++;

            if (_waitingMessages == _maxMessagesCount)
            {
                ThreadPool.QueueUserWorkItem(LogMessages, _waitingMessages);
                _waitingMessages -= _maxMessagesCount;
            }
        }
    }

    public void Dispose()
    {
        LogMessages(_waitingMessages);
    }

    private void LogMessages(object countObj)
    {
        var count = (int)countObj;

        lock (_writerLocker)
        {
            Console.WriteLine($"Loggin' {count} messages.");

            var sb = new StringBuilder();

            for (var i = 0; i < count; i++)
            {
                if (_messages.TryDequeue(out var message))
                {
                    sb.AppendLine(message);
                }
            }

            File.AppendAllText(_path, sb.ToString());
        }
    }
}