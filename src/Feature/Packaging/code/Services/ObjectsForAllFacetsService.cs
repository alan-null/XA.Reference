using System.Collections.Generic;
using System.Linq;
using XA.Reference.Feature.Packaging.Models;

namespace XA.Reference.Feature.Packaging.Services
{
    public class ObjectsForAllFacetsService
    {
        public IEnumerable<IObject> Run(IEnumerable<IObject> objects, IEnumerable<IFacet> facets, int itemsPerFacet)
        {
            var comparableObjects = objects.Select(o => new ComparableObject(o)).ToList();
            var comparableFacets = facets.Select(f => new ComparableFacet(f,itemsPerFacet)).ToList();
            comparableObjects.ForEach(o => o.RefreshScore(comparableFacets.Select(f => f as IFacet).ToList()));

            var result = new List<IObject>();
            while (comparableFacets.Any() && comparableObjects.Any())
            {
                comparableObjects.Sort((o1, o2) => o1.Score - o2.Score);
                var obj = comparableObjects.Last();
                result.Add(obj.Object);

                comparableFacets.ForEach(f => f.RefreshFacets(obj.Result));
                comparableObjects.Remove(obj);
                comparableObjects.ForEach(o => o.RefreshScore(comparableFacets.Select(facet => facet as IFacet).ToList()));
                comparableObjects.RemoveAll(o => o.Score == 0);
            }

            return result;
        }
    }
}