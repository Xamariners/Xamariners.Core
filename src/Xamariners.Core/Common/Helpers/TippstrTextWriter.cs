using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Xamariners.Common.Helpers
{
    using Newtonsoft.Json;

    internal class XamarinersTextWriter : JsonTextWriter
    {
        public XamarinersTextWriter(TextWriter textWriter)
            : base(textWriter)
        {
        }

        public override void WriteValue(double value)
        {
            double minValue = 0.01;
            value = Math.Max(value, minValue);
            var sOut =  String.Format("{0:0.00}", value);
            base.WriteValue(sOut);
        }
    }
}
