using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Infrastructure
{
    public class SearchResult<T>
    {
        public SearchResult(IEnumerable<T> ByDialogName, IEnumerable<T> ByMessage)
        {
            this.ByDialogName = ByDialogName;
            this.ByMessage = ByMessage;

        }
        public IEnumerable<T> ByDialogName { get; internal set; }
        public IEnumerable<T> ByMessage { get; internal set; }
    }
}
