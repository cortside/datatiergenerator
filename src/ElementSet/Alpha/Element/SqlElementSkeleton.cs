using System;

namespace Spring2.DataTierGenerator.Element {
    /// <summary>
    /// Summary description for SqlElementSkeleton.
    /// </summary>
    public class SqlElementSkeleton : ElementSkeleton {

	/// <summary>
	/// Name that is safe to use in code (i.e. characters that are allowed as valid name characters in
	/// some databases that are not allowed in class or field names).
	/// </summary>
	public String CodeSafeName {
	    get { return RemoveInvalidCharacters(this.name); }
	}

	/// <summary>
	/// Removes characters that are not valid in code (i.e. spaces and numeric operators)
	/// </summary>
	/// <param name="s"></param>
	/// <returns></returns>
	private String RemoveInvalidCharacters(String s) {
	    return s.Replace(" ", String.Empty).Replace("/", String.Empty).Replace("-", String.Empty);
	}

    }
}
