namespace NiuX.Csv.Quantum;

/// <summary>
/// 量子阅读器
/// </summary>
internal class QuantumReader
{
    public QuantumReader(TextReader tr, int bufsize)
    {
        _tr = tr;
        _bufsize = bufsize;
        _buffer = new char[bufsize];
    }

    /// <summary>
    /// 填充缓冲区
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    internal int FillBuffer(int offset)
    {
        var len = _bufsize - offset;
        var read = _tr.ReadBlock(_buffer, offset, len);
        read += offset;

        if (read != _bufsize) _eof = true;

        _bufidx = 0;
        return read;
    }

    /// <summary>
    /// 读取
    /// </summary>
    /// <returns></returns>
    internal QuantumSpan ReadLine()
    {
        if (_bufread == 0 || _bufidx >= _bufread)
        {
            if (_eof) return new QuantumSpan();

            _bufread = FillBuffer(0);

            if (_bufread == 0) return new QuantumSpan();
        }

        var start = _bufidx;
        var end = _bufidx;
        var qc = 0;
        var read = false;

        while (_bufidx < _bufread)
        {
            var c = _buffer[_bufidx++];

            if (c == '\"') qc++;

            if (c is not ('\r' or '\n') || qc % 2 != 0) continue;

            read = true;
            end = _bufidx - 1;

            if (_bufidx >= _bufread)
            {
                read = false;
                break;
            }

            c = _buffer[_bufidx++];

            if (c != '\r' && c != '\n') _bufidx--;

            break;
        }

        switch (_eof)
        {
            case true when end == start:
                end = _bufread;
                break;

            case false when read == false:
                {
                    reload++;

                    if (reload > 1) throw new Exception("line too long for buffer");

                    Array.Copy(_buffer, start, _buffer, 0, _bufsize - start);
                    var len = _bufsize - start;
                    _bufread = FillBuffer(len);

                    return _bufread == 0 ? new QuantumSpan() : ReadLine();
                }
        }

        reload = 0;
        return new QuantumSpan(_buffer, start, end - start);
    }

    #region fields

    private readonly TextReader _tr;
    private readonly int _bufsize;
    private int _bufread;
    private int _bufidx;
    private readonly char[] _buffer;
    private bool _eof;
    private int reload;

    #endregion fields
}