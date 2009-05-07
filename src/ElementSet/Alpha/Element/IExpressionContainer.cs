using System;

using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element
{
    /// <summary>
    /// Interface to be implemented to allow an object to be used to supply substitution expansions for an expression.
    /// </summary>
    public interface IExpressionContainer
    {
	string GetExpressionSubstitution(string substitutionExpression, string idString, ParserValidationDelegate vd);
    }
}
