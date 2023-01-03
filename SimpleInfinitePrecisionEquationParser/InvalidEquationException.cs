using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPEP;


[Serializable]
public class InvalidEquationException : Exception
{
	public InvalidEquationException() { }
	public InvalidEquationException(string message) : base(message) { }
	public InvalidEquationException(string message, Exception inner) : base(message, inner) { }
	protected InvalidEquationException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
