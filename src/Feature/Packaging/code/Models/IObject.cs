using System.Collections.Generic;

namespace XA.Reference.Feature.Packaging.Models
{
    public interface IObject
    {
        List<object> GetValuesForFacet(IFacet facet);
    }
}