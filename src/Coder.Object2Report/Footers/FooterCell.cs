namespace Coder.Object2Report.Footers
{
    public abstract class FooterCell

    {
        /// <summary>
        ///     Format for output
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public abstract object GetValue();

        /// <summary>
        ///     Calculate of c
        /// </summary>
        /// <param name="value"></param>
        public abstract void Merge(object value);
    }

    public class FooterComment : FooterCell
    {
        private readonly string _message;

        public FooterComment(string message)
        {
            _message = message;
        }

        public override object GetValue()
        {
            return _message;
        }

        public override void Merge(object value)
        {
        }
    }
}