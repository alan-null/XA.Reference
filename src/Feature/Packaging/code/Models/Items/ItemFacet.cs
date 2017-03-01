using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;

namespace XA.Reference.Feature.Packaging.Models.Items
{
    public class ItemFacet : IFacet
    {
        public string Key => _item.DisplayName;
        private readonly Item _item;

        public ItemFacet(Item item)
        {
            _item = item;
        }

        public List<object> GetFacetValues()
        {
            return _item.Children.Select(item => item.ID.ToString()).Cast<object>().ToList();
        }
    }
}