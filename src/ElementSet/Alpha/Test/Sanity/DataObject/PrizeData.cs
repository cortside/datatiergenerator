using System;

using Spring2.Core.Types;

namespace Golf.Tournament.DataObject {
    public class PrizeData : Spring2.Core.DataObject.DataObject {

	private StringType description = StringType.DEFAULT;
	private CurrencyType amount = CurrencyType.DEFAULT;

	public static readonly String DESCRIPTION = "Description";
	public static readonly String AMOUNT = "Amount";

	public StringType Description {
	    get { return this.description; }
	    set { this.description = value; }
	}

	public CurrencyType Amount {
	    get { return this.amount; }
	    set { this.amount = value; }
	}
    }
}
