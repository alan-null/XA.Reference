using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;
using XA.Reference.Feature.Packaging.Models;
using XA.Reference.Feature.Packaging.Models.Items;

namespace XA.Reference.Feature.Packaging.Services
{
    public class ItemsForAllFacetsService
    {
        public List<Item> GetItems(List<Item> items, List<Item> facetRoots, int itemsPerFacet)
        {
            List<IObject> objects = new List<IObject>(items.Select(item => new ItemObject(item)));
            List<IFacet> facets = new List<IFacet>(facetRoots.Select(item => new ItemFacet(item)));
            var list = new ObjectsForAllFacetsService().Run(objects, facets, itemsPerFacet);
            return list.Select(o => (ItemObject)o).Select(itemObjects => itemObjects.Item).ToList();
        }
    }
}