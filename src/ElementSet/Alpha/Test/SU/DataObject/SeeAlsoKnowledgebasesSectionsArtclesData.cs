using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class SeeAlsoKnowledgebasesSectionsArtclesData : Spring2.Core.DataObject.DataObject
    {

	private IntegerType knowledgebasesSectionsArticlesID = IntegerType.DEFAULT;
	private StringType title = StringType.DEFAULT;
	private IntegerType id = IntegerType.DEFAULT;
	private IntegerType seeAlsoID = IntegerType.DEFAULT;

	public static readonly String KNOWLEDGEBASESSECTIONSARTICLESID = "KnowledgebasesSectionsArticlesID";
	public static readonly String TITLE = "Title";
	public static readonly String ID = "Id";
	public static readonly String SEEALSOID = "SeeAlsoID";

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

	public StringType Title
	{
	    get
	    {
		return this.title;
	    }
	    set
	    {
		this.title = value;
	    }
	}

	public IntegerType Id
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
