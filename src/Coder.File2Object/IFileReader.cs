namespace Coder.File2Object
{
    public interface IFileReader<TCell>
    {
        void Open(string file);
        void Close();
        bool TryRead(int row, int cellIndex, out TCell cell);
        void Write(string file);

        void WriteTo(int row, int cellIndex, string value);

        string Convert(TCell cell);
    }
}