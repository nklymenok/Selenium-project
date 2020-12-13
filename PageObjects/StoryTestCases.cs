using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milliman.Pixel.Web.Tests.PageObjects
{
    [TestFixture]
    class StoryTestCases
    {
        //const string adminSharedRates = "AdminShared";
        //const string adminClientRates = "AdminLuxoft";
        //const string adminTestRates = "AdminTest";
        //const string adminSharedLosses = "LossAdminShared";
        //const string adminClientLosses = "LossAdminLuxoft";
        //const string adminTestLosses = "LossAdminTest";

        public static IEnumerable<TestCaseData> CarrierByVariableTestData
        {
            get
            {
                yield return new TestCaseData(Variable.Adj_Ground_Elev, "\r\n18 to 20 feet\r\n$1\r\n$1\r\nOver 40 feet\r\n$1\r\n$1");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "only Policies");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "without JSON");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Losses files are empty");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Carriers and Rates files are empty");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "with licensed file");
                //yield return new TestCaseData(Type.DP3_Florida, "full");
                //yield return new TestCaseData(Type.DP3_Florida, "only Policies");
                //yield return new TestCaseData(Type.DP3_Florida, "without JSON");
                //yield return new TestCaseData(Type.DP3_Florida, "Carriers and Rates files are empty");
                //yield return new TestCaseData(Type.Flood_Alabama_FEMA, "full");
                //yield return new TestCaseData(Type.Flood_Alabama_FEMA, "only Policies");
                //yield return new TestCaseData(Type.Flood_Alabama_FEMA, "without JSON");
                //yield return new TestCaseData(Type.Flood_Alabama_FEMA, "Losses files are empty");
                //yield return new TestCaseData(Type.Flood_Alabama_FEMA, "Carriers and Rates files are empty");
            }
        }
    }
}
