using System;
using System.Web.Hosting;
using NorskTipping;

namespace Presentation.Web
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly Games _game = new Games();
        private readonly string _savePath = HostingEnvironment.MapPath(@"/App_Data/");
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

        protected void cboFilterRounds_OnValueChanged(object sender, EventArgs e)
        {
            AddClientCode();
        }

        protected void cboGame_OnValueChanged(object sender, EventArgs e)
        {
            AddClientCode();
        }

        private void AddClientCode()
        {
            ResultsRepository.FetchResultsToDisk(_savePath, (ToJsonBase)_game.GameTypes[cboGame.SelectedIndex]);
            if (!Page.ClientScript.IsStartupScriptRegistered("LottoResults"))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "LottoResults",
                    _game.Do(cboGame.SelectedIndex, _savePath, (int)cboLastRounds.SelectedItem.Value, chkSorted.Checked, (string)cboFilterRounds.SelectedItem.Value), true);
            }
        }
    }
}