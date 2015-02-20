using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for Utilities
/// </summary>
public class Utilities
{
	public Utilities()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string GreyBoxFolderURL
    {
        get
        {
            return ConfigurationManager.AppSettings["WebSiteURL"] + "/greybox/";
        }
    }
}