namespace Coder.Object2Report
{
    public interface IRender
    {
        void Write(Point currentPosition, object v);
    }

    public struct Point
    {
        public int Cell { get; set; }
        public int Row { get; set; }
    }
}