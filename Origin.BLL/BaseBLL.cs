using System;
using Origin.Model;

namespace Origin.BLL
{
    public class BaseBLL : IDisposable
    {
        public OriginEntities db = new OriginEntities();

        public void Dispose()
        {
        }
    }
}
