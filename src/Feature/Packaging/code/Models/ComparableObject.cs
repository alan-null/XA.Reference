using System.Collections.Generic;
using System.Linq;

namespace XA.Reference.Feature.Packaging.Models
{
    public class ComparableObject
    {
        public IObject Object { get; }
        public int Score { get; private set; }
        public Dictionary<string, List<object>> Result { get; private set; }

        public ComparableObject(IObject o)
        {
            Object = o;
        }

        public void RefreshScore(IEnumerable<IFacet> facets)
        {
            Result = new Dictionary<string, List<object>>();
            Score = 0;
            foreach (var f in facets)
            {
                var valuesForFacet = Object.GetValuesForFacet(f);
                var facetValues = f.GetFacetValues();
                var validFacets = valuesForFacet.Where(l => facetValues.Contains(l));
                if (validFacets.Any())
                {
                    Result.Add(f.Key, valuesForFacet);
                    Score += valuesForFacet.Count;
                }
            }
        }
    }
}