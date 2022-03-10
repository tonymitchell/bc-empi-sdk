using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Health;
using Health.Services;

namespace TestClient.Web.Pages
{
    public class QueryModel
    {
        [BindProperty]
        public string? Phn { get; set; }
        [BindProperty]
        public string? Surname { get; set; }
        [BindProperty]
        public string? GivenNames{ get; set; }
        [BindProperty]
        public DateTime? Dob { get; set; }

        [BindProperty]
        public string? Address { get; set; }
        [BindProperty]
        public string? PostalCode { get; set; }
    }

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        IClientRegistry _service;

        public IndexModel(ILogger<IndexModel> logger, IClientRegistry service)
        {
            _logger = logger;
            this._service = service;
        }

        public void OnGet()
        {
        }

        public QueryResponseCode? SearchResultCode { get; private set; }
        public List<Candidate>? SearchResults { get; private set; }

        public async Task<IActionResult> OnPostAsync(QueryModel query)
        {
            if (!ModelState.IsValid)
                return Page();

            // Make query
            QueryResponse response;
            if (query.Phn != null)
            {
                response = await _service.GetDemographicsAsync(new GetDemographicsParameters() { Phn = query.Phn});
            }
            else
            {
                var fcp = new FindCandidatesParameters();
                fcp.Surname = query.Surname;
                if (!string.IsNullOrEmpty(query.GivenNames))
                    fcp.Given.AddRange(query.GivenNames.Split(" "));
                fcp.DateOfBirth = query.Dob;
                fcp.StreetAddressLine1 = query.Address;
                fcp.PostalCode = query.PostalCode;

                response = await _service.FindCandidatesAsync(fcp);
            }

            SearchResultCode = response.Code;
            SearchResults = response.Candidates.ToList();
            
            return Page();
        }

        public static string ForDisplay(PersonName pn)
        {
            if (pn == null) return "";
            if (string.IsNullOrEmpty(pn.FirstPreferredName))
                return $"{pn.Surname.ToUpper()}, {pn.FirstGivenName} {pn.SecondGivenName} {pn.ThirdGivenName}".Trim();
            else
                return $"{pn.Surname.ToUpper()}, {pn.FirstGivenName} \"{pn.FirstPreferredName}\" {pn.SecondGivenName} {pn.ThirdGivenName}".Trim();
        }

        public static string ForDisplay(Address a)
        {
            if (a == null) return "";
            return $"{string.Join(", ", a.StreetAddressLines)}, {a.City}, {a.StateProvince}, {a.Country}";
        }

        public static string ForDisplay(IEnumerable<Identifier> sourceIds)
        {
            if (sourceIds == null) return "";

            List<string> formattedIds = new List<string>();
            foreach (Identifier id in sourceIds)
            {
                if (id.IsActive)
                    formattedIds.Add($"{id.AssigningAuthority}:{id.Value}");
                else
                    formattedIds.Add($"{id.AssigningAuthority}:{id.Value}(INACTIVE)");
            }

            return string.Join("; ", formattedIds);
        }

    }
}