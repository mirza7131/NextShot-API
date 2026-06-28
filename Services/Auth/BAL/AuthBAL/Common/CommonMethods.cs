using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.MenuDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBAL.Common
{
    public static class CommonMethods
    {
        public static IList<ViewModulesDto> BuildTree(this IEnumerable<ViewModulesDto> source)
        {
            //var groups = source.Where(x => x.IsActive == true).GroupBy(i => i.ParentId);
            var groups = source.GroupBy(i => i.ParentId);
            var roots = groups.FirstOrDefault(g => g.Key == null).ToList();

            if (roots.Count > 0)
            {
                var dict = groups.Where(g => g.Key != null).ToDictionary(g => g.Key, g => g.ToList());
                for (int i = 0; i < roots.Count; i++)
                    AddChildren(roots[i], dict);
            }

            return roots;
        }

        private static void AddChildren(ViewModulesDto node, IDictionary<Guid?, List<ViewModulesDto>> source)
        {
            if (source.ContainsKey(node.MenuId))
            {
                node.children = source[node.MenuId];
                for (int i = 0; i < node.children.Count; i++)
                    AddChildren(node.children[i], source);
            }
            else
            {
                node.children = new List<ViewModulesDto>();
            }
        }

    }
}
