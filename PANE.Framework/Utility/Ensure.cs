using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO;
using System.Diagnostics;

namespace PANE.Framework.Utility
{
    public interface IEnsure<T>
    {
        IEnsure<T> IsRequired();
        IEnsure<T> IsRequired(string paramName);
        IEnsure<T> IsRequired(string message, params object[] format);
        IEnsure<T> Has(Func<T, bool> condition, string message);
        IEnsure<T> Has<R>(Func<R, bool> condition, string message);
    }

    public sealed class Ensure : Ensure<object>
    {
        private Ensure(object entity)
            : base(entity)
        {

        }
    }

    public class Ensure<T> : IEnsure<T>
    {
        private T _entity;
        public static bool UseTrace { get; set; }

        public static IEnsure<T> That(T entity)
        {
            return new Ensure<T>(entity);
        }

        protected Ensure(T entity)
        {
            _entity = entity;
        }

        public IEnsure<T> IsRequired()
        {
            return IsRequired(null);
        }
        public IEnsure<T> IsRequired(string paramName)
        {
            return IsRequired("{0} is null. Ensure that {0} has a value.", paramName ?? "Required parameter");
        }

        public IEnsure<T> IsRequired(string message, params object[] format)
        {
            if (_entity == null)
            {
                Fail(string.Format("Required: "+message, format));
            }
            return this;
        }

        public IEnsure<T> Has<R>(Func<R, bool> condition, string message)
        {
            return Has(condition, message);
        }

        public IEnsure<T> Has(Func<T, bool> condition, string message)
        {
            if (!condition.Invoke(_entity))
            {
                Fail(message);
            }
            return this;
        }

        private void Fail(string message)
        {
            try
            {
                throw new DesignByContractException(message);
            }
            catch (Exception ex)
            {
                if (UseTrace)
                { 
                    Trace.TraceError(ex.ToString());
                }
                throw ex;
            }
        }
        
    }

    public class DesignByContractException : ApplicationException
    {
        public DesignByContractException() { }
        public DesignByContractException(string message) : base(message) { }
        public DesignByContractException(string message, Exception inner) : base(message, inner) { }
    }
}
