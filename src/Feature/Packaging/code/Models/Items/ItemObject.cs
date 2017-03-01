using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;

namespace XA.Reference.Feature.Packaging.Models.Items
{
    public class ItemObject : IObject
    {
        public readonly Item Item;

        public ItemObject(Item item)
        {
            Item = item;
        }

        public List<object> GetValuesForFacet(IFacet facet)
        {
            return Item.Fields[facet.Key].Value.Split('|').Cast<object>().ToList();
        }
    }
}