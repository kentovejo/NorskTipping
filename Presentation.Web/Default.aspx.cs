using System;
using System.Web.Hosting;
using NorskTipping;

namespace Presentation.Web
{
    public partial class Default : System.Web.UI.Page
    {
        public string SavePath = HostingEnvironment.MapPath(@"/App_Data/");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ResultsRepository.FetchResultsToDisk(SavePath);
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

        protected void cboFilterRounds_OnValueChanged(object sender, EventArgs e)
        {
            AddClientCode();
        }

        private void AddClientCode()
        {
            if (!Page.ClientScript.IsStartupScriptRegistered("LottoResults"))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "LottoResults",
                    new LottoToJson().Do(SavePath, (int)cboLastRounds.SelectedItem.Value, chkSorted.Checked, (string)cboFilterRounds.SelectedItem.Value), true);
            }
        }
    }
}