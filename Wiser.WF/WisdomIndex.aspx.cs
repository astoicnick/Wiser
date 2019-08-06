using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wiser.Models.Wisdom;
using Wiser.Services;

namespace Wiser.WF
{
    public partial class WisdomIndex : Page
    {
        public List<WisdomScrollItem> _wisdom = new List<WisdomScrollItem>();
        private WisdomService _wisdomService = new WisdomService();
        protected void Page_Load(object sender, EventArgs e)
        {
            _wisdom = _wisdomService.GetWisdomList();
        }
        protected void Create_Wisdom(object sender, EventArgs e)
        {
            Server.Transfer("CreateWisdom.aspx");
        }
    }
}