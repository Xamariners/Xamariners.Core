using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamariners.Core.Model.Attributes
{
    public class ForeignEntityKeyAttribute : Attribute
    {
        private readonly string _name;

        public string Name
        {
            get { return this._name; }
        }

        public ForeignEntityKeyAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("ForeignKeyAttribute should not be null or contain whitespaces");

            this._name = name;
        }
    }
   
}