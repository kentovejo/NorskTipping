using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using NorskTipping;

namespace Presentation.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AddClientCode();
            }
        }

        protected void chkSorted_OnCheckedChanged(object sender, EventArgs e)
        {
            AddClientCode();
        }

        protected void cboLastRounds_OnValueChanged(object sender, EventArgs e)
        {
            AddClientCode();
        }

        private void AddClientCode()
        {
            if (!Page.ClientScript.IsStartupScriptRegistered("LottoResults"))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "LottoResults",
                    new LottoToJson(chkSorted.Checked).Do(Convert.ToInt32(cboLastRounds.SelectedItem.Value)), true);
            }
        }
    }
}