using System.Collections.Generic;
using System.Linq;

namespace XA.Reference.Feature.Packaging.Models
{
    public class ComparableFacet : IFacet
    {
        public string Key => _facet.Key;
        private readonly IFacet _facet;
        public Dictionary<object, int> RequiredUsagesForFacets { get; set; }
        private List<object> FacetValues { get; }

        public ComparableFacet(IFacet f, int amount)
        {
            _facet = f;
            FacetValues = f.GetFacetValues();
            RequiredUsagesForFacets = new Dictionary<object, int>();
            FacetValues.ForEach(o => RequiredUsagesForFacets.Add(o, amount));
        }

        public List<object> GetFacetValues()
        {
            return FacetValues;
        }

        public void RefreshFacets(Dictionary<string, List<object>> result)
        {
            FacetValues.RemoveAll(s =>
            {
                var key = _facet.Key;
                if (!result.ContainsKey(key))
                {
                    return false;
                }

                result[key].Where(o => RequiredUsagesForFacets.ContainsKey(o)).ToList().ForEach(o => RequiredUsagesForFacets[o]--);
                return result[key].Contains(s) && RequiredUsagesForFacets[s] <= 0;
            });
        }
    }
}