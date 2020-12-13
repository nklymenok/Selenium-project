using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milliman.Pixel.Web.Tests.PageObjects
{
    [TestFixture]
    class DDLTestCases
    {
        const string adminSharedRates = "AdminShared";
        const string adminClientRates = "AdminLuxoft";
        const string adminTestRates = "AdminTest";
        const string adminSharedLosses = "LossAdminShared";
        const string adminClientLosses = "LossAdminLuxoft";
        const string adminTestLosses = "LossAdminTest";

        public static IEnumerable<TestCaseData> ImportDatasetsTestData
        {
            get
            {
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "full");
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "only Policies");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "without JSON");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Losses files are empty");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Carriers and Rates files are empty");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "with licensed file");
                //yield return new TestCaseData(Type.DP3_Florida, "full");
                //yield return new TestCaseData(Type.DP3_Florida, "only Policies");
                //yield return new TestCaseData(Type.DP3_Florida, "without JSON");
                //yield return new TestCaseData(Type.DP3_Florida, "Carriers and Rates files are empty");
                //yield return new TestCaseData(Type.Flood_Alabama_FEMA, "full");
                //yield return new TestCaseData(Type.HO6_Florida, "full");
                //yield return new TestCaseData(Type.Flood_Arizona_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_California_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Colorado_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Connecticut_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Alabama_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Arkansas_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Delaware_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Florida, "full");
                //yield return new TestCaseData(Type.Flood_Florida_FEMA, "full");
                //yield return new TestCaseData(Type.Flood_Florida_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Florida_Syndeste, "full");
                //yield return new TestCaseData(Type.Flood_Florida_Wright, "full");
                //yield return new TestCaseData(Type.HO3_South_Carolina, "full");
                //yield return new TestCaseData(Type.Flood_Georgia_FEMA, "full");
                //yield return new TestCaseData(Type.Flood_Georgia_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Idaho_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Illinois_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Indiana_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Iowa_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Kansasa_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Kentucky_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Louisiana, "full");
                //yield return new TestCaseData(Type.Flood_Louisiana_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Maine_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Maryland_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Massachusetts_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Michigan_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Minnesota_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Mississippi_FEMA, "full");
                //yield return new TestCaseData(Type.Flood_Mississippi_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Missouri_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Montana_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Nebraska_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Nevada_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_New_Hampshire_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_New_Jersey, "full");
                //yield return new TestCaseData(Type.Flood_New_Jersey_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_New_Jersey_Wright, "full");
                //yield return new TestCaseData(Type.Flood_New_Jersey_Wright_New, "full");
                //yield return new TestCaseData(Type.Flood_New_Mexico_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_New_York_Mlmn_Std, "full");
            }
        }

        public static IEnumerable<TestCaseData> ImportDatasetsFullCycleTestData
        {
            get
            {
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "full");
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "only Policies");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "without JSON");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Losses files are empty");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Carriers and Rates files are empty");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "with licensed file");
                //yield return new TestCaseData(Type.DP3_Florida, "full");
                //yield return new TestCaseData(Type.DP3_Florida, "only Policies");
                //yield return new TestCaseData(Type.DP3_Florida, "without JSON");
                //yield return new TestCaseData(Type.DP3_Florida, "Carriers and Rates files are empty");
                //yield return new TestCaseData(Type.Flood_Alabama_FEMA, "full");
                //yield return new TestCaseData(Type.HO6_Florida, "full");
                //yield return new TestCaseData(Type.Flood_Arizona_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_California_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Colorado_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Connecticut_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Alabama_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Arkansas_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Delaware_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Florida, "full");
                //yield return new TestCaseData(Type.Flood_Florida_FEMA, "full");
                //yield return new TestCaseData(Type.Flood_Florida_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Florida_Syndeste, "full");
                //yield return new TestCaseData(Type.Flood_Florida_Wright, "full");
                //yield return new TestCaseData(Type.HO3_South_Carolina, "full");
            }
        }

        public static IEnumerable<TestCaseData> AddRatesByAdminTestData
        {
            get
            {
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "full", adminSharedRates, true, false);
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "only Policies", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "without JSON", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Losses files are empty", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Carriers and Rates files are empty", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "with licensed file", adminSharedRates, true, false);
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "full", adminClientRates, false, true);
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "only Policies", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "without JSON", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Losses files are empty", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Carriers and Rates files are empty", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "with licensed file", adminClientRates, false, true);
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "full", adminTestRates, false, false);
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "only Policies", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "without JSON", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Losses files are empty", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Carriers and Rates files are empty", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "with licensed file", adminTestRates, false, false);
                //yield return new TestCaseData(Type.DP3_Florida, "full", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.DP3_Florida, "only Policies", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.DP3_Florida, "without JSON", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.DP3_Florida, "Carriers and Rates files are empty", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.DP3_Florida, "full", adminClientRates, false, true);
                //yield return new TestCaseData(Type.DP3_Florida, "only Policies", adminClientRates, false, true);
                //yield return new TestCaseData(Type.DP3_Florida, "without JSON", adminClientRates, false, true);
                //yield return new TestCaseData(Type.DP3_Florida, "Carriers and Rates files are empty", adminClientRates, false, true);
                //yield return new TestCaseData(Type.DP3_Florida, "full", adminTestRates, false, false);
                //yield return new TestCaseData(Type.DP3_Florida, "only Policies", adminTestRates, false, false);
                //yield return new TestCaseData(Type.DP3_Florida, "without JSON", adminTestRates, false, false);
                //yield return new TestCaseData(Type.DP3_Florida, "Carriers and Rates files are empty", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_Alabama_FEMA, "full", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_Alabama_FEMA, "full", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_Alabama_FEMA, "full", adminTestRates, false, false);
                //yield return new TestCaseData(Type.HO6_Florida, "full", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.HO6_Florida, "full", adminClientRates, false, true);
                //yield return new TestCaseData(Type.HO6_Florida, "full", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_Arizona_Mlmn_Std, "full", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_Arizona_Mlmn_Std, "full", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_Arizona_Mlmn_Std, "full", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_California_Mlmn_Std, "full", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_California_Mlmn_Std, "full", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_California_Mlmn_Std, "full", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_Colorado_Mlmn_Std, "full", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_Colorado_Mlmn_Std, "full", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_Colorado_Mlmn_Std, "full", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_Connecticut_Mlmn_Std, "full", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_Connecticut_Mlmn_Std, "full", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_Connecticut_Mlmn_Std, "full", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_Alabama_Mlmn_Std, "full", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_Alabama_Mlmn_Std, "full", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_Alabama_Mlmn_Std, "full", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_Arkansas_Mlmn_Std, "full", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_Arkansas_Mlmn_Std, "full", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_Arkansas_Mlmn_Std, "full", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_Delaware_Mlmn_Std, "full", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_Delaware_Mlmn_Std, "full", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_Delaware_Mlmn_Std, "full", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_Florida, "full", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_Florida, "full", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_Florida, "full", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_Florida_FEMA, "full", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_Florida_FEMA, "full", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_Florida_FEMA, "full", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_Florida_Mlmn_Std, "full", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_Florida_Mlmn_Std, "full", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_Florida_Mlmn_Std, "full", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_Florida_Syndeste, "full", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_Florida_Syndeste, "full", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_Florida_Syndeste, "full", adminTestRates, false, false);
                //yield return new TestCaseData(Type.Flood_Florida_Wright, "full", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.Flood_Florida_Wright, "full", adminClientRates, false, true);
                //yield return new TestCaseData(Type.Flood_Florida_Wright, "full", adminTestRates, false, false);
                //yield return new TestCaseData(Type.HO3_South_Carolina, "full", adminSharedRates, true, false);
                //yield return new TestCaseData(Type.HO3_South_Carolina, "full", adminClientRates, false, true);
                //yield return new TestCaseData(Type.HO3_South_Carolina, "full", adminTestRates, false, false);
            }
        }

        public static IEnumerable<TestCaseData> AddLossesByAdminTestData
        {
            get
            {
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "full", adminSharedLosses, true, false);
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "only Policies", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "without JSON", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Losses files are empty", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Carriers and Rates files are empty", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "with licensed file", adminSharedLosses, true, false);
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "full", adminClientLosses, false, true);
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "only Policies", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "without JSON", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Losses files are empty", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Carriers and Rates files are empty", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "with licensed file", adminClientLosses, false, true);
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "full", adminTestLosses, false, false);
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "only Policies", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "without JSON", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Losses files are empty", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Carriers and Rates files are empty", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "with licensed file", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.DP3_Florida, "full", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.DP3_Florida, "only Policies", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.DP3_Florida, "without JSON", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.DP3_Florida, "Carriers and Rates files are empty", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.DP3_Florida, "full", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.DP3_Florida, "only Policies", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.DP3_Florida, "without JSON", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.DP3_Florida, "Carriers and Rates files are empty", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.DP3_Florida, "full", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.DP3_Florida, "only Policies", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.DP3_Florida, "without JSON", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.DP3_Florida, "Carriers and Rates files are empty", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_Alabama_FEMA, "full", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_Alabama_FEMA, "full", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_Alabama_FEMA, "full", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.HO6_Florida, "full", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.HO6_Florida, "full", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.HO6_Florida, "full", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_Arizona_Mlmn_Std, "full", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_Arizona_Mlmn_Std, "full", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_Arizona_Mlmn_Std, "full", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_California_Mlmn_Std, "full", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_California_Mlmn_Std, "full", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_California_Mlmn_Std, "full", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_Colorado_Mlmn_Std, "full", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_Colorado_Mlmn_Std, "full", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_Colorado_Mlmn_Std, "full", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_Connecticut_Mlmn_Std, "full", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_Connecticut_Mlmn_Std, "full", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_Connecticut_Mlmn_Std, "full", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_Alabama_Mlmn_Std, "full", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_Alabama_Mlmn_Std, "full", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_Alabama_Mlmn_Std, "full", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_Arkansas_Mlmn_Std, "full", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_Arkansas_Mlmn_Std, "full", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_Arkansas_Mlmn_Std, "full", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_Delaware_Mlmn_Std, "full", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_Delaware_Mlmn_Std, "full", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_Delaware_Mlmn_Std, "full", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_Florida, "full", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_Florida, "full", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_Florida, "full", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_Florida_FEMA, "full", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_Florida_FEMA, "full", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_Florida_FEMA, "full", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_Florida_Mlmn_Std, "full", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_Florida_Mlmn_Std, "full", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_Florida_Mlmn_Std, "full", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_Florida_Syndeste, "full", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_Florida_Syndeste, "full", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_Florida_Syndeste, "full", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.Flood_Florida_Wright, "full", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.Flood_Florida_Wright, "full", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.Flood_Florida_Wright, "full", adminTestLosses, false, false);
                //yield return new TestCaseData(Type.HO3_South_Carolina, "full", adminSharedLosses, true, false);
                //yield return new TestCaseData(Type.HO3_South_Carolina, "full", adminClientLosses, false, true);
                //yield return new TestCaseData(Type.HO3_South_Carolina, "full", adminTestLosses, false, false);

            }
        }

        public static IEnumerable<TestCaseData> DeleteDatasetsTestData
        {
            get
            {
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "only Policies");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "without JSON");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Losses files are empty");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "Carriers and Rates files are empty");
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "with licensed file");
                //yield return new TestCaseData(Type.DP3_Florida, "full");
                //yield return new TestCaseData(Type.DP3_Florida, "without JSON");
                //yield return new TestCaseData(Type.DP3_Florida, "Carriers and Rates files are empty");
            }
        }

        public static IEnumerable<TestCaseData> RemainingDatasetsTestData
        {
            get
            {
                yield return new TestCaseData(Type.Flood_Louisiana_FEMA, "full");
                //yield return new TestCaseData(Type.DP3_Florida, "only Policies");
                //yield return new TestCaseData(Type.HO6_Florida, "full");
                //yield return new TestCaseData(Type.Flood_Arizona_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_California_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Colorado_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Connecticut_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Alabama_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Arkansas_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Delaware_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Florida, "full");
                //yield return new TestCaseData(Type.Flood_Florida_FEMA, "full");
                //yield return new TestCaseData(Type.Flood_Florida_Mlmn_Std, "full");
                //yield return new TestCaseData(Type.Flood_Florida_Syndeste, "full");
                //yield return new TestCaseData(Type.Flood_Florida_Wright, "full");
                //yield return new TestCaseData(Type.HO3_South_Carolina, "full");
            }
        }

        public static IEnumerable<TestCaseData> ImportDatasetsMandatoryTestData
        {
            get
            {
                //yield return new TestCaseData(Type.Flood_Louisiana_FEMA);
                yield return new TestCaseData(Type.DP3_Florida);
                yield return new TestCaseData(Type.Flood_Alabama_FEMA);
                //yield return new TestCaseData(Type.HO6_Florida);
                //yield return new TestCaseData(Type.Flood_Arizona_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_California_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Colorado_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Connecticut_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Alabama_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Arkansas_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Delaware_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Florida);
                //yield return new TestCaseData(Type.Flood_Florida_FEMA);
                //yield return new TestCaseData(Type.Flood_Florida_Mlmn_Std);
                //yield return new TestCaseData(Type.HO3_South_Carolina);
                //yield return new TestCaseData(Type.Flood_Georgia_FEMA);
                //yield return new TestCaseData(Type.Flood_Georgia_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Idaho_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Illinois_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Indiana_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Iowa_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Kansasa_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Kentucky_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Louisiana);
                //yield return new TestCaseData(Type.Flood_Louisiana_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Maine_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Maryland_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Massachusetts_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Michigan_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Minnesota_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Mississippi_FEMA);
                //yield return new TestCaseData(Type.Flood_Mississippi_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Missouri_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Montana_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Nebraska_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Nevada_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_New_Hampshire_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_New_Jersey);
                //yield return new TestCaseData(Type.Flood_New_Jersey_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_New_Jersey_Wright_New);
                //yield return new TestCaseData(Type.Flood_New_Mexico_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_New_York_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Arizona_FEMA);
                //yield return new TestCaseData(Type.Flood_Arkansas_FEMA);
                //yield return new TestCaseData(Type.Flood_California_FEMA);
                //yield return new TestCaseData(Type.Flood_California);
                //yield return new TestCaseData(Type.Flood_Carolina);
                //yield return new TestCaseData(Type.Flood_Colorado_FEMA);
                //yield return new TestCaseData(Type.Flood_Connecticut_FEMA);
                //yield return new TestCaseData(Type.Flood_Delaware_FEMA);
                //yield return new TestCaseData(Type.Flood_District_of_Columbia_FEMA);
                //yield return new TestCaseData(Type.Flood_Idaho_FEMA);
                //yield return new TestCaseData(Type.Flood_Illinois_FEMA);
                //yield return new TestCaseData(Type.Flood_Indiana_FEMA);
                //yield return new TestCaseData(Type.Flood_Iowa_FEMA);
                //yield return new TestCaseData(Type.Flood_Kansas_FEMA);
                //yield return new TestCaseData(Type.Flood_Kentucky_FEMA);
                //yield return new TestCaseData(Type.Flood_Maine_FEMA);
                //yield return new TestCaseData(Type.Flood_Maryland_FEMA);
                //yield return new TestCaseData(Type.Flood_Massachusetts_FEMA);
                //yield return new TestCaseData(Type.Flood_Michigan_FEMA);
                //yield return new TestCaseData(Type.Flood_Minnesota_FEMA);
                //yield return new TestCaseData(Type.Flood_Missouri_FEMA);
                //yield return new TestCaseData(Type.Flood_Montana_FEMA);
                //yield return new TestCaseData(Type.Flood_Nebraska_FEMA);
                //yield return new TestCaseData(Type.Flood_Nevada_FEMA);
                //yield return new TestCaseData(Type.Flood_New_Hampshire_FEMA);
                //yield return new TestCaseData(Type.Flood_New_Jersey_FEMA);
                //yield return new TestCaseData(Type.Flood_New_Mexico_FEMA);
                //yield return new TestCaseData(Type.Flood_New_York_FEMA);
                //yield return new TestCaseData(Type.Flood_New_York);
                //yield return new TestCaseData(Type.Flood_North_Carolina_FEMA);
                //yield return new TestCaseData(Type.Flood_North_Carolina);
                //yield return new TestCaseData(Type.Flood_North_Carolina_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_North_Carolina_NCRB);
                //yield return new TestCaseData(Type.Flood_North_Dakota_FEMA);
                //yield return new TestCaseData(Type.Flood_North_Dakota_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Ohio_FEMA);
                //yield return new TestCaseData(Type.Flood_Ohio_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Oklahoma_FEMA);
                //yield return new TestCaseData(Type.Flood_Oklahoma_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Oregon_FEMA);
                //yield return new TestCaseData(Type.Flood_Oregon_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Pennsylvania_FEMA);
                //yield return new TestCaseData(Type.Flood_Pennsylvania_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Rhode_Island_FEMA);
                //yield return new TestCaseData(Type.Flood_Rhode_Island_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_South_Carolina_FEMA);
                //yield return new TestCaseData(Type.Flood_South_Carolina_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_South_Carolina_Wright);
                //yield return new TestCaseData(Type.Flood_South_Dakota_FEMA);
                //yield return new TestCaseData(Type.Flood_South_Dakota_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_St_Louis);
                //yield return new TestCaseData(Type.Flood_Tennessee_FEMA);
                //yield return new TestCaseData(Type.Flood_Tennessee_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Texas_FEMA);
                //yield return new TestCaseData(Type.Flood_Texas_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Texas);
                //yield return new TestCaseData(Type.Flood_Utah_FEMA);
                //yield return new TestCaseData(Type.Flood_Utah_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Vermont_FEMA);
                //yield return new TestCaseData(Type.Flood_Vermont_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Virginia_FEMA);
                //yield return new TestCaseData(Type.Flood_Virginia_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Washington_FEMA);
                //yield return new TestCaseData(Type.Flood_Washington_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_West_Virginia_FEMA);
                //yield return new TestCaseData(Type.Flood_West_Virginia_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Wisconsin_FEMA);
                //yield return new TestCaseData(Type.Flood_Wisconsin_Mlmn_Std);
                //yield return new TestCaseData(Type.Flood_Wyoming_FEMA);
                //yield return new TestCaseData(Type.Flood_Wyoming_Mlmn_Std);
                //yield return new TestCaseData(Type.HO3_Alabama);
                //yield return new TestCaseData(Type.HO3_Florida);
                //yield return new TestCaseData(Type.HO3_Louisiana);
                //yield return new TestCaseData(Type.HO3_Massachusetts);
                //yield return new TestCaseData(Type.HO3_New_Jersey);
                //yield return new TestCaseData(Type.HO3_Texas);

            }
        }
    }
}
