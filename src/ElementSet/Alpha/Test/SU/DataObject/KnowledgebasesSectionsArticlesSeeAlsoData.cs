using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class KnowledgebasesSectionsArticlesSeeAlsoData : Spring2.Core.DataObject.DataObject
    {

	private IdType id = IdType.DEFAULT;
	private IntegerType knowledgebasesSectionsArticlesID = IntegerType.DEFAULT;
	private IntegerType seeAlsoID = IntegerType.DEFAULT;

	public static readonly String ID = "Id";
	public static readonly String KNOWLEDGEBASESSECTIONSARTICLESID = "KnowledgebasesSectionsArticlesID";
	public static readonly String SEEALSOID = "SeeAlsoID";

	public IdType Id
	{
	    get
	    {
		return this.id;
	    }
	    set
	    {
		this.id = value;
	    }
	}

	public IntegerType KnowledgebasesSectionsArticlesID
	{
	    get
	    {
		return this.knowledgebasesSectionsArticlesID;
	    }
	    set
	    {
		this.knowledgebasesSectionsArticlesID = value;
	    }
	}

	public IntegerType SeeAlsoID
	{
	    get
	    {
		return this.seeAlsoID;
	    }
	    set
	    {
		this.seeAlsoID = value;
	    }
	}
    }
}
