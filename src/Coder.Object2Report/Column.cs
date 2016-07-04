﻿using System;
using System.Linq.Expressions;
using Coder.Object2Report.Footers;

namespace Coder.Object2Report
{
    public class Column<T, TResult>
        : IColumn<T>,
            IColumnSetting<TResult>
    {
        private Expression<Func<T, TResult>> _expression;

        public Column(string title, Expression<Func<T, TResult>> itemExpression)
        {
            if (title == null)
                throw new ArgumentNullException(nameof(title));
            if (title == null)
                throw new ArgumentNullException(nameof(title));
            Title = title;
            _expression = itemExpression;
            Func = itemExpression.Compile();
        }

        public Column(string title, Func<T, TResult> func)
        {
            if (title == null)
                throw new ArgumentNullException(nameof(title));
            if (title == null)
                throw new ArgumentNullException(nameof(title));
            Title = title;
            Func = func;
        }

        public Func<T, TResult> Func { get; set; }

        public string Title { get; }
        public int Index { get; internal set; }
        public string Format { get; set; }

        public void Write(T t, Action<ReportCell, object, string> action, ReportCell cell)
        {
            var value = Func(t);
            action(cell, value, Format);
            this.Footer?.Calculate(value);
        }

        public void WriteFooter(Action<ReportCell, object, string> action, ReportCell cell)
        {
            this.Footer?.Write(action, cell);
        }
        public FooterCell<TResult> Footer { get; set; }


    }
}