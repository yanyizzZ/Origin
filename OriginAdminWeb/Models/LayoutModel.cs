using System.Collections.Generic;

namespace OriginAdminWeb.Models
{
    public class LayoutModel
    {
        // public List<Permission_Pages> History { get; set; }
        public List<ViewMenu> Menu { get; set; }
        public string Title { get; set; }
    }


    public class LayoutModel<T>
    {
        //public List<Permission_Pages> History { get; set; }
        public List<ViewMenu> Menu { get; set; }
        public string Title { get; set; }
        public T Model { get; set; }
    }
}