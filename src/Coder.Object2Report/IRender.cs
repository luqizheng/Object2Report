namespace Coder.Object2Report
{
    public interface IRender
    {
        void Write(int cell, int row, object v);
    }
}