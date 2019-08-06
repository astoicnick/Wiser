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
    public partial class CreateWisdom : System.Web.UI.Page
    {
        private WisdomCreateItem wisdomToCreate = new WisdomCreateItem();
        private WisdomService _wisdomService = new WisdomService("eea3f712-43ad-41a8-9475-de635ee2a1a2");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitWisdom(object sender, EventArgs e)
        {
            wisdomToCreate.AuthorId = int.Parse(Author.SelectedValue);
            wisdomToCreate.Content = Content.Text;
            wisdomToCreate.Source = Source.Text;
            if (_wisdomService.CreateWisdom(wisdomToCreate))
            {
                var wisdomCount = (_wisdomService.GetWisdomList().Count) - 1;
                ContentLabel.Text = _wisdomService.GetWisdomList()[wisdomCount].Content;
            }
        }
    }
}