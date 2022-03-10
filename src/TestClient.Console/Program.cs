// See https://aka.ms/new-console-template for more information

using Health;
using Health.Services;
using Health.Services.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostBuilder, services) =>
    {
        if (hostBuilder.HostingEnvironment.IsEnvironment("Local"))
        {
            // Use a stub if you don't have access to the EMPI API yet
            services.AddClientRegistryServiceStub();
        }
        else
        {
            services.AddClientRegistryService(hostBuilder.Configuration.GetSection(ClientRegistrySettings.SectionName));
        }
    })
    .Build();


var service = host.Services.GetRequiredService<IClientRegistry>();
var renderer = new ConsoleRenderer();


Console.WriteLine("Welcome to EMPI Search");

while (true)
{
    // PHN Search
    var gdp = new GetDemographicsParameters();
    while (true)
    {
        Console.Write("Enter PHN >");
        gdp.Phn = Console.ReadLine();
        if (string.IsNullOrEmpty(gdp.Phn))
            break;

        try
        {
            gdp.Validate();
        }
        catch(ArgumentException ex)
        {
            Console.Error.WriteLine(ex.Message); 
            continue;
        }
        break;
    }
    if (!string.IsNullOrEmpty(gdp.Phn))
    {
        Console.WriteLine("Searching...");
        var gdResults = await service.GetDemographicsAsync(gdp);
        Console.WriteLine("");
        renderer.DisplayResults(gdResults);

        Console.WriteLine("\n\n");
        continue;
    }

    // Find Candidates Search
    var fcp = new FindCandidatesParameters();
    Console.Write("Enter Surname >");
    fcp.Surname = Console.ReadLine();
    while (true)
    {
        Console.Write("Enter DOB >");
        var dob = Console.ReadLine();
        if (string.IsNullOrEmpty(dob))
            break;
        if (DateTime.TryParse(dob, out var dateOfBirth))
        {
            fcp.DateOfBirth = dateOfBirth;
            break;
        }
    }
    Console.Write("Enter Given Names >");
    var givenNames = Console.ReadLine();
    if (givenNames != null)
        fcp.Given.AddRange(givenNames.Split(" ", StringSplitOptions.RemoveEmptyEntries));
    Console.Write("Enter Street Address >");
    var addressLine1 = Console.ReadLine();
    if (addressLine1 != null)
        fcp.StreetAddressLine1 = addressLine1;
    Console.Write("Enter Postal Code >");
    fcp.PostalCode = Console.ReadLine();

    Console.WriteLine("Searching...");
    var fcResults = await service.FindCandidatesAsync(fcp);
    Console.WriteLine("");
    renderer.DisplayResults(fcResults);

    Console.WriteLine("\n\n");
}

class ConsoleRenderer
{
    public void DisplayResults(QueryResponse results)
    {
        Console.WriteLine("Result: " + results.Code.Message);

        foreach (var candidate in results.Candidates)
        {
            Console.WriteLine(ForDisplay(candidate));
        }
    }

    string ForDisplay(Candidate c)
    {
        return $"{c.MatchScore,5:0.#} | {c.Phn} | {ForDisplay(c.CardName ?? c.DeclaredName),-30} | {c.DateOfBirth} | {ForDisplay(c.PhysicalAddress ?? c.MailingAddress)}";
    }
    string ForDisplay(PersonName pn)
    {
        if (pn == null) return "";
        if (string.IsNullOrEmpty(pn.FirstPreferredName))
            return $"{pn.Surname.ToUpper()}, {pn.FirstGivenName} {pn.SecondGivenName} {pn.ThirdGivenName}".Trim();
        else
            return $"{pn.Surname.ToUpper()}, {pn.FirstGivenName} \"{pn.FirstPreferredName}\" {pn.SecondGivenName} {pn.ThirdGivenName}".Trim();
    }

    string ForDisplay(Address a)
    {
        if (a == null) return "";
        return $"{string.Join(", ", a.StreetAddressLines)}, {a.City}, {a.StateProvince}, {a.Country}";
    }

}