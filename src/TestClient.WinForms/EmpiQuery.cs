using Health;
using Health.Services;

namespace TestClient.WinForms
{
    public partial class EmpiQuery : Form
    {
        readonly IClientRegistry _service;

        public EmpiQuery(IClientRegistry service)
        {
            InitializeComponent();
            _service = service;
        }

        private async void btnGetDemographics_Click(object sender, EventArgs e)
        {
            try
            {
                string phn = txtPhn.Text;
                // Validation?
                var gdp = new GetDemographicsParameters() { Phn = phn };
                gdp.Validate();

                var response = await _service.GetDemographicsAsync(gdp);

                ReplaceList(response);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnFindCandidates_Click(object sender, EventArgs e)
        {
            try
            {
                var fcp = new FindCandidatesParameters();
                fcp.Surname = txtSurname.Text;
                if (string.IsNullOrEmpty(txtGivenNames.Text))
                    fcp.Given.AddRange(txtGivenNames.Text.Split(" "));
                fcp.DateOfBirth = string.IsNullOrEmpty(txtDateOfBirth.Text) ? null : DateTime.Parse(txtDateOfBirth.Text);
                fcp.StreetAddressLine1 = txtAddressLine1.Text;
                fcp.PostalCode = txtPostalCode.Text;
                fcp.Validate();

                var response = await _service.FindCandidatesAsync(fcp);

                ReplaceList(response);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReplaceList(QueryResponse response)
        {
            IEnumerable<ListViewItem> items = response.Candidates.Select(c => new ListViewItem(new string[] { c.MatchScore.ToString(), c.Phn, ForDisplay(c.CardName ?? c.DeclaredName), c.DateOfBirth.ToString(), ForDisplay(c.PhysicalAddress ?? c.MailingAddress) }));
            lvCandidates.View = View.Details;
            lvCandidates.Items.Clear();
            lvCandidates.Items.AddRange(items.ToArray());
        }

        private string ForDisplay(Address a)
        {
            if (a == null) return "";
            return $"{string.Join(", ", a.StreetAddressLines)}, {a.City}, {a.StateProvince}, {a.Country}";
        }

        string ForDisplay(Candidate c)
        {
            return $"[{c.MatchScore}] {c.Phn}: {ForDisplay(c.DeclaredName)}";
        }

        string ForDisplay(PersonName pn)
        {
            if (pn == null) return "";
            if (string.IsNullOrEmpty(pn.FirstPreferredName))
                return $"{pn.Surname.ToUpper()}, {pn.FirstGivenName} {pn.SecondGivenName} {pn.ThirdGivenName}".Trim();
            else
                return $"{pn.Surname.ToUpper()}, {pn.FirstGivenName} \"{pn.FirstPreferredName}\" {pn.SecondGivenName} {pn.ThirdGivenName}".Trim();
        }

        private void EmpiQuery_SizeChanged(object sender, EventArgs e)
        {
            lvCandidates.Width = this.ClientSize.Width - lvCandidates.Left - 30;
            lvCandidates.Height = this.ClientSize.Height - lvCandidates.Top - 30;
        }
    }
}