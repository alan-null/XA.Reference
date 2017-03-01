using System.Collections.Generic;

namespace XA.Reference.Feature.Packaging.Models
{
    public interface IFacet
    {
        string Key { get; }
        List<object> GetFacetValues();
    }
}