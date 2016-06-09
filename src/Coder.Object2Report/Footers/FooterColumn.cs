namespace Coder.Object2Report.Footers
{
    public abstract class FooterColumn

    {
        /// <summary>
        ///     Format for output
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract object GetValue();


        /// <summary>
        ///     Calculate of c
        /// </summary>
        /// <param name="value"></param>
        public abstract void Merge(object value);
    }
}