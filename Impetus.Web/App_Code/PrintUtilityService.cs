using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EvoPaj.Base;

/// <summary>
/// Summary description for PrintUtilityService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class PrintUtilityService : System.Web.Services.WebService
{

    public PrintUtilityService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public LicenseData GenerateLicenseFromToken(string token,string licenseType,string duration)
    {
        //use the token to generate the license
        LicenseData result = new LicenseData();
        string license = Guid.NewGuid().ToString();
        result.LicenseField = license;
        result.token = token;
        return result;
    }
    [WebMethod]
    public LicenseData GenerateRightCodeFromErrorCode(string errorCode)
    {
        //use the token to generate the license
        LicenseData result = new LicenseData();
        string rightcode = Guid.NewGuid().ToString();
        string rightpassword = string.Format("{0}{1}", "1c3", "xbd7");
        result.rightCode = rightcode;
        result.AdminPassword = rightpassword;
        result.errorCode = errorCode;
        return result;
    }
    [WebMethod]
    public LicenseData GenerateRightServiceCode(string serviceCode)
    {
        //use the token to generate the license
        LicenseData result = new LicenseData();
        string firstservicecode = Guid.NewGuid().ToString();
        string secondservicecode = Guid.NewGuid().ToString();
        result.wrongserviceCode = serviceCode;
        result.rightserviceCode = string.Concat(firstservicecode, secondservicecode);
        return result;
    }

}