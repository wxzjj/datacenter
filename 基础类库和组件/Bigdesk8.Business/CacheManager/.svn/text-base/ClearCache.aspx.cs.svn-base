using System;
using Bigdesk8.Web;

namespace Bigdesk8.Business.CacheManager
{
    public partial class ClearCache : System.Web.UI.Page
    {
        private readonly ICacheStrategy cache = new DefaultCacheStrategy();

        protected void lb_clearallcache_Click(object sender, EventArgs e)
        {
            this.cache.Clear();
            this.WindowAlert("操作成功！");
        }
    }
}
