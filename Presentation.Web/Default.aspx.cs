using System;
using System.Collections.Generic;
using System.Web.Hosting;
using NorskTipping;
using Security.Identity;

namespace Presentation.Web
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly Games _game = new Games();
        private readonly string _savePath = HostingEnvironment.MapPath(@"/App_Data/");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                AddClientCode();
            chkSorted.Enabled = SystemAccess.IsAuthenticated;
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
            FileRepository.FetchResultsToDisk(_savePath, (BasicGame)_game.GameTypes[cboGame.SelectedIndex]);
            SendToClient();
        }

        private void SendToClient()
        {
            if (!Page.ClientScript.IsStartupScriptRegistered("LottoResults"))
            {
                var sorted = SystemAccess.IsAuthenticated && chkSorted.Checked;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "LottoResults",
                _game.Do(cboGame.SelectedIndex, _savePath, (int) cboLastRounds.SelectedItem.Value, sorted,
                    (string) cboFilterRounds.SelectedItem.Value), true);
            }
        }

        protected void btnUpdateEstimate_Click(object sender, EventArgs e)
        {
            FileRepository.FetchResultsToDisk(_savePath, (BasicGame)_game.GameTypes[cboGame.SelectedIndex]);
            var balls = new List<int>();
            var bg = (BasicGame) _game.GameTypes[cboGame.SelectedIndex];
            if(!string.IsNullOrEmpty(txtBall1.Text))
                balls.Add(Convert.ToInt32(txtBall1.Text));
            if (!string.IsNullOrEmpty(txtBall2.Text))
                balls.Add(Convert.ToInt32(txtBall2.Text));
            if (!string.IsNullOrEmpty(txtBall3.Text))
                balls.Add(Convert.ToInt32(txtBall3.Text));
            if (!string.IsNullOrEmpty(txtBall4.Text))
                balls.Add(Convert.ToInt32(txtBall4.Text));
            if (!string.IsNullOrEmpty(txtBall5.Text))
                balls.Add(Convert.ToInt32(txtBall5.Text));
            if (!string.IsNullOrEmpty(txtBall6.Text))
                balls.Add(Convert.ToInt32(txtBall6.Text));
            if (!string.IsNullOrEmpty(txtBall7.Text))
                balls.Add(Convert.ToInt32(txtBall7.Text));
            bg.NextRoundEstimate.UnsortedMainTable = balls;
            SendToClient();
        }
    }
}