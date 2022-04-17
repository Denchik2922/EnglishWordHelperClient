using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishWordHelperClient.Infrastructure.Helpers
{
    public static class ListHelper
    {
        public static bool AddEntityInList(List<string> list, string data)
        {
            var exsistEntity = list.FirstOrDefault(i => i.ToLower() == data.ToLower());
            if (exsistEntity == null && !String.IsNullOrEmpty(data))
            {
                list.Add(data);
                return true;
            }
            return false;
        }

        public static void RemoveEntityFromList(List<string> list, string data)
        {
            var exsistEntity = list.FirstOrDefault(t => t.ToLower() == data.ToLower());
            if (exsistEntity != null)
            {
                list.RemoveAll(x => x == data);
            }
        }
    }
}
