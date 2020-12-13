using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace Milliman.Pixel.Web.Tests.PageObjects.Pages
{
    class DefaultDatasetsPage: MenuPage
    {
        private IWebDriver driver;

        public DefaultDatasetsPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "accordion")]
        public IWebElement FilterPanelLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='accordion']//span")]
        public IWebElement ExpandFilterLocator { get; set; }

        [FindsBy(How = How.Id, Using = "PanelGeneral")]
        public IWebElement GeneralPanelLocator { get; set; }

        [FindsBy(How = How.Id, Using = "Losses")]
        public IWebElement LossesPanelLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='DataTables_Table_1']/tbody/tr/td[2]")]
        public IList<IWebElement> LossesLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='DataTables_Table_1']//tr/td[1]/input")]
        public IList<IWebElement> LossesCheckboxLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='PanelGeneral']//tr[3]/td[2]")]
        public IWebElement ExpandPrimaryCarrierLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='PanelGeneral']//tr[4]/td[2]")]
        public IWebElement ExpandSecondaryCarrierLocator { get; set; }

        //[FindsBy(How = How.XPath, Using = "//*[@class='ui-multiselect-checkboxes ui-helper-reset']//li")]
        //public IList<IWebElement> PrimaryCarrierLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='ui-multiselect-menu ui-widget ui-widget-content ui-corner-all DFilterSingleselctControl ui-multiselect-single']//li")]
        public IList<IWebElement> PrimaryCarrierLocator { get; set; }
        //*[@class='ui-multiselect-menu ui-widget ui-widget-content ui-corner-all DFilterSingleselctControl ui-multiselect-single']//li

        [FindsBy(How = How.XPath, Using = "(//*[@class='ui-multiselect-menu ui-widget ui-widget-content ui-corner-all DFilterMultiselctControl'])[1]/ul/li")]
        public IList<IWebElement> SecondaryCarrierLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@class='ui-multiselect-all']/*[text()='Check all'])[1]")]
        public IWebElement SecondaryCarrierSelectAllLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='FilterSummaryPlaceHolder']/a")]
        public IWebElement FilterSummaryLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='FilterSummary']/table/tbody/tr[1]/td[2]")]
        public IWebElement DatasetFilterSummaryLocator { get; set; }

        [FindsBy(How = How.Id, Using = "UpdateResults")]
        public IWebElement UpdateResultsButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "ActionsDD")]
        public IWebElement ActionsButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "DashboardSaveAsLink")]
        public IWebElement SaveAsButtonLocator { get; set; }

        [FindsBy(How = How.Name, Using = "ResultName")]
        public IWebElement StoryNameTextBoxLocator { get; set; }

        [FindsBy(How = How.Id, Using = "ResultDescription")]
        public IWebElement StoryDescriptionTextBoxLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='saveAsFolderTree']//*[text()='Pixel']")]
        public IWebElement PixelFolderSaveStoryLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='saveAsFolderTree']//*[text()='Milliman - San Francisco']")]
        public IWebElement SanFranciscoFolderSaveStoryLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='saveAsFolderTree']//*[text()='Luxoft']")]
        public IWebElement LuxoftFolderSaveStoryLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='ui-dialog-buttonset']//*[text()='Save']")]
        public IWebElement SaveDashboardStoryButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "StoryName")]
        public IWebElement StoryNameLocator { get; set; }

        [FindsBy(How = How.Id, Using = "Geography")]
        public IWebElement GeographyTabLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='PanelGeography']//td[1]//tr[1]//button")]
        public IWebElement GeographyTypeButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='522']/option")]
        public IList<IWebElement> GeographyTypeHO3ListLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@name='multiselect_524']")]
        public IList<IWebElement> GeographyTypeDP3ListLocator { get; set; }

        [FindsBy(How = How.Id, Using = "0")]
        public IWebElement PoliciesWithPremiumTabLocator { get; set; }

        [FindsBy(How = How.Id, Using = "1")]
        public IWebElement AveragePremiumTabLocator { get; set; }

        [FindsBy(How = How.Id, Using = "2")]
        public IWebElement WinRateTabLocator { get; set; }

        [FindsBy(How = How.Id, Using = "3")]
        public IWebElement CarrierByVariableTabLocator { get; set; }

        [FindsBy(How = How.Id, Using = "4")]
        public IWebElement PremiumRankTabLocator { get; set; }

        [FindsBy(How = How.Id, Using = "5")]
        public IWebElement DifferenceToMarketTabLocator { get; set; }

        [FindsBy(How = How.Id, Using = "6")]
        public IWebElement SegmentationTabLocator { get; set; }

        [FindsBy(How = How.Id, Using = "7")]
        public IWebElement ByRatingVariableTabLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='StoryAndVisualization']//a/span[1]")]
        public IWebElement StoryAndVisualizationLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu dropdown-menu-right ReportsDDMenu']//*[text()='Policies with a Premium']")]
        public IWebElement PoliciesWithPremiumMinimizedLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu dropdown-menu-right ReportsDDMenu']//*[text()='Average Premium']")]
        public IWebElement AveragePremiumMinimizedLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu dropdown-menu-right ReportsDDMenu']//*[text()='Win Rate']")]
        public IWebElement WinRateMinimizedLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu dropdown-menu-right ReportsDDMenu']//*[text()='Carrier By Variable']")]
        public IWebElement CarrierByVariableMinimizedLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu dropdown-menu-right ReportsDDMenu']//*[text()='Premium Rank']")]
        public IWebElement PremiumRankMinimizedLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu dropdown-menu-right ReportsDDMenu']//*[text()='Difference to Market']")]
        public IWebElement DifferenceToMarketMinimizedLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu dropdown-menu-right ReportsDDMenu']//*[text()='Segmentation']")]
        public IWebElement SegmentationMinimizedLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu dropdown-menu-right ReportsDDMenu']//*[text()='By Rating Variable']")]
        public IWebElement ByRatingVariableMinimizedLocator { get; set; }


        #region Policies with a Premium

        [FindsBy(How = How.XPath, Using = "//*[@id='RiskWithPremiumForm']/label")]
        public IWebElement PoliciesWithPremiumLabelLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@class='highcharts-axis-labels highcharts-xaxis-labels '])[1]")]
        public IWebElement CarrierPwPLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@class='highcharts-series-group'])[1]")]
        public IWebElement PwPChartLocator { get; set; }

        #endregion Policies with a Premium

        #region Average Premium

        [FindsBy(How = How.XPath, Using = "//*[@id='AveragePremiumForm']/label")]
        public IWebElement AveragePremiumLabelLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@class='highcharts-axis-labels highcharts-xaxis-labels '])[2]")]
        public IWebElement CarrierAPLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@class='highcharts-series-group'])[2]")]
        public IWebElement APChartLocator { get; set; }

        #endregion Average Premium

        #region Win Rate

        [FindsBy(How = How.XPath, Using = "//*[@id='WinRateForm']/label")]
        public IWebElement WinRateLabelLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@class='highcharts-axis-labels highcharts-yaxis-labels '])[2]")]
        public IWebElement CarrierWRLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@class='highcharts-series-group'])[2]")]
        public IWebElement WRChartLocator { get; set; }

        #endregion Win Rate

        #region Carrier By Variable

        [FindsBy(How = How.Id, Using = "ByVar1_val-button")]
        public IWebElement ByVariable1ButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='ByVar1_val-menu']//li")]
        public IList<IWebElement> ByVariable1ListLocator { get; set; }

        [FindsBy(How = How.Id, Using = "CarrierByVarPivotUpdateFilters")]
        public IWebElement CBVCalculateButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='CbVTable_wrapper']/div")]
        public IWebElement CBVTableLocator { get; set; }

        #endregion Carrier By Variable

        #region Premium Rank

        [FindsBy(How = How.XPath, Using = "//*[@class='highcharts-title']")]
        public IWebElement PremiumRankLabelLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@class='highcharts-grid highcharts-yaxis-grid '])[2]")]
        public IWebElement PRChartLocator { get; set; }

        #endregion Premium Rank

        #region Difference to Market

        [FindsBy(How = How.Id, Using = "Type_val-button")]
        public IWebElement Diff2MarketTypeLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='Type_val-menu']//*[text()='% Difference']")]
        public IWebElement DifferencePercentTypeLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='Type_val-menu']//*[text()='$ Difference']")]
        public IWebElement DifferenceDollarTypeLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='Type_val-menu']//*[text()='$ Distribution']")]
        public IWebElement DistributionTypeLocator { get; set; }

        [FindsBy(How = How.Id, Using = "Measure_val-button")]
        public IWebElement MeasureButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='Measure_val-menu']//*[text()='Median']")]
        public IWebElement MedianMeasureLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='Measure_val-menu']//*[text()='Average']")]
        public IWebElement AverageMeasureLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='Measure_val-menu']//*[text()='Minimum']")]
        public IWebElement MinimumMeasureLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='Measure_val-menu']//*[text()='25th Percentile']")]
        public IWebElement PersentileMeasureLocator { get; set; }

        [FindsBy(How = How.Id, Using = "LowerBucket")]
        public IWebElement LowerBucketLocator { get; set; }

        [FindsBy(How = How.Id, Using = "UpperBucket")]
        public IWebElement UpperBucketLocator { get; set; }

        [FindsBy(How = How.Id, Using = "NumberOfBuckets")]
        public IWebElement NumberOfBucketsLocator { get; set; }

        [FindsBy(How = How.Id, Using = "PercentDifferenceCalc")]
        public IWebElement Diff2MartetCalculateButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "lowerBucketReset")]
        public IWebElement LowerBucketResetLocator { get; set; }

        [FindsBy(How = How.Id, Using = "upperBucketReset")]
        public IWebElement UpperBucketResetLocator { get; set; }

        [FindsBy(How = How.Id, Using = "NumberOfBuckets")]
        public IWebElement Diff2MarketNumberOfBucketsLocator { get; set; }

        [FindsBy(How = How.Id, Using = "OutsideOfRanges")]
        public IWebElement OutsideOfRangesCheckboxLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@class='highcharts-grid highcharts-yaxis-grid '])[2]")]
        public IWebElement D2MChartLocator { get; set; }

        [FindsBy(How = How.Id, Using = "PercentDifferenceCalc")]
        public IWebElement D2MCalculateButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='highcharts-series highcharts-series-0 highcharts-column-series highcharts-color-0 highcharts-tracker']")]
        public IWebElement D2MChartColumnLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='highcharts-series highcharts-series-1 highcharts-column-series highcharts-color-1 highcharts-tracker']/*[name()='rect']")]
        public IList<IWebElement> D2MChartPrimaryColumnLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='highcharts-series highcharts-series-2 highcharts-column-series highcharts-color-2 highcharts-tracker']/*[name()='rect']")]
        public IList<IWebElement> D2MChartSecondaryColumnLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='highcharts-series highcharts-series-2 highcharts-column-series highcharts-color-2 highcharts-tracker ']/*[name()='rect']")]
        public IList<IWebElement> D2MChartSecondaryColumnLocator2 { get; set; }

        //[FindsBy(How = How.XPath, Using = "//*[@class='highcharts-series highcharts-series-2 highcharts-column-series highcharts-color-2 highcharts-tracker ']")]
        //public IWebElement D2MChartSecondaryColumnLocator1 { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='highcharts-markers highcharts-series-4 highcharts-spline-series highcharts-color-undefined highcharts-tracker']")]
        public IWebElement D2MChartLossRatioLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='highcharts-markers highcharts-series-4 highcharts-spline-series highcharts-color-undefined highcharts-tracker ']")]
        public IWebElement D2MChartLossRatioLocator2 { get; set; }

        [FindsBy(How = How.Id, Using = "DisplayLossRateCheck")]
        public IWebElement DisplayLossRatioCheckboxLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='highcharts-legend-item highcharts-column-series highcharts-color-1 highcharts-series-1']")]
        public IWebElement PrimaryLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='highcharts-legend-item highcharts-column-series highcharts-color-2 highcharts-series-2']")]
        public IWebElement CompetitorLocator { get; set; }

        //*[@id="highcharts-2ls79qo-12"]/svg/g[9]/g/g/g[2]

        //*[@id="highcharts-2ls79qo-3"]/svg/g[9]/g/g/g[2]/text


        #endregion Difference to Market

        #region Segmentation

        [FindsBy(How = How.Id, Using = "UpdateRegression")]
        public IWebElement CalculateButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "faTableOutputAngle")]
        public IWebElement ExpandTableButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "fRegressionPosition")]
        public IWebElement SegmentationTableLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='div-calculating']/div/div[1]")]
        public IWebElement CalculatingSpinnerLocator { get; set; }

        [FindsBy(How = How.Id, Using = "UserDefinedValues")]
        public IWebElement UserDefinedValuesCheckboxLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='CollapseSettingsSecond']//button")]
        public IWebElement SegmentationVariableButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@name='multiselect_ByVariable']")]
        public IList<IWebElement> SegmentationVariableLocator { get; set; }

        [FindsBy(How = How.Id, Using = "ByVariableLevel-button")]
        public IWebElement SegmentationByVariableButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='ui-menu-item']")]
        public IList<IWebElement> SegmentationByVariableLocator { get; set; }

        #endregion Segmentation

        #region By Rating Variable

        [FindsBy(How = How.Id, Using = "ByVar1_val-button")]
        public IWebElement ByVariable1BRVButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='ByVar1_val-menu']/li")]
        public IList<IWebElement> ByVariable1BRVListLocator { get; set; }

        [FindsBy(How = How.Id, Using = "boxPlotBlock")]
        public IWebElement BRVReportResultLocator { get; set; }

        [FindsBy(How = How.Id, Using = "CalculateBoxPlot")]
        public IWebElement BRVCalculateButtonLocator { get; set; }

        //*[@id="CalculateBoxPlot"]

        #endregion By Rating Variable

        #region Methods

        public bool IsLossDisplayed(string lossName)
        {
            bool isDisplayed = false;

            for(int i=0; i<LossesLocator.Count; i++)
            {
                if (LossesLocator[i].Text.Contains(lossName)) isDisplayed = true;
            }
            return isDisplayed;
        }

        public bool IsCarrierDisplayed(string carrierName)
        {
            bool isDisplayed = false;

            for (int i = 0; i < PrimaryCarrierLocator.Count; i++)
            {
                if (PrimaryCarrierLocator[i].Text.Contains(carrierName)) isDisplayed = true;                  
            }
            return isDisplayed;
        }

        public string GetRandomString()
        {
            FilterSummaryLocator.Click();
       
            return DatasetFilterSummaryLocator.WaitForElementPresentAndEnabled(driver).Text.Substring(8, 5);
        }

        public void SelectPrimaryCarrier(string carrierName)
        {
            ExpandPrimaryCarrierLocator.Click();

            for (int i=0; i < PrimaryCarrierLocator.Count; i++)
            {
                if (PrimaryCarrierLocator[i].Text.Contains(carrierName)) PrimaryCarrierLocator[i].Click();
            }
        }

        public void SelectSecondaryCarrier(string carrierName)
        {
            ExpandSecondaryCarrierLocator.Click();

            for (int i = 0; i < SecondaryCarrierLocator.Count; i++)
            {
                if (SecondaryCarrierLocator[i].Text.Contains(carrierName)) SecondaryCarrierLocator[i].Click();
            }
        }

        public void SelectLoss(string lossName)
        {
            for (int i = 0; i < LossesLocator.Count; i++)
            {
                if (LossesLocator[i].Text.Contains(lossName))LossesCheckboxLocator[i].Click();
            }
        }

        public bool IsPoliciesWithPremiumReportExist(string primaryCarrier, string secondaryCarrier)
        {
            bool areAllElementsExist = true;

            PoliciesWithPremiumLabelLocator.WaitForElementPresentAndEnabled(driver);

            if (!PoliciesWithPremiumLabelLocator.Text.Equals("Policies with a Premium")) areAllElementsExist = false;

            if (!CarrierPwPLocator.Text.Equals($"{primaryCarrier}\r\n{secondaryCarrier}")) areAllElementsExist = false;

            if (!PwPChartLocator.Displayed) areAllElementsExist = false;

            return areAllElementsExist;
        }

        public bool IsAveragePremiumReportExist(string primaryCarrier, string secondaryCarrier)
        {
            bool areAllElementsExist = true;

            AveragePremiumLabelLocator.WaitForElementPresentAndEnabled(driver);

            if (!AveragePremiumLabelLocator.Text.Equals("Average Premium")) areAllElementsExist = false;

            if (!CarrierAPLocator.Text.Equals($"{primaryCarrier}\r\n{secondaryCarrier}")) areAllElementsExist = false;

            if (!APChartLocator.Displayed) areAllElementsExist = false;

            return areAllElementsExist;
        }

        

        public void ScrollToUpdateResultsButton()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(UpdateResultsButtonLocator);
            actions.Perform();
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0,400)");
        }

        public void ScrollToCompetitorLabel()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(CompetitorLocator);
            actions.Perform();
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0,100)");
        }

        public void ScrollUp()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(DataMenuLocator);
            actions.Perform();
        }

        public bool IsWinRateReportExist(string primaryCarrier, string secondaryCarrier)
        {
            bool areAllElementsExist = true;

            WinRateLabelLocator.WaitForElementPresentAndEnabled(driver);

            if (!WinRateLabelLocator.Text.Equals("Win Rate")) areAllElementsExist = false;

            if (!CarrierWRLocator.Text.Equals($"{primaryCarrier}\r\n{secondaryCarrier}")) areAllElementsExist = false;

            if (!WRChartLocator.Displayed) areAllElementsExist = false;

            return areAllElementsExist;
        }

        public bool IsTableExist(string variable1, string primaryCarrier, string secondaryCarrier, string tableResult)
        {
            bool isTableExists = false;

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(600));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ByVar1_val-button"))).Click();

            for (int i=0; i < ByVariable1ListLocator.Count; i++)
            {
                if (ByVariable1ListLocator[i].Text.Equals(variable1)) ByVariable1ListLocator[i].Click();
            }
            CBVCalculateButtonLocator.Click();

            Utils.WaitUntilCalculatingDisappears(driver, 3000);

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='CbVTable_wrapper']/div")));

            var m = CBVTableLocator.Text;

            if (CBVTableLocator.Text.Equals($"{variable1} {primaryCarrier} {secondaryCarrier}{tableResult}")) isTableExists = true;

            return isTableExists;
        }

        public bool isVariableExistCBV(string variable1)
        {
            bool isVarExists = false;

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ByVar1_val-button"))).Click();

            for (int i = 0; i < ByVariable1ListLocator.Count; i++)
            {
                if (ByVariable1ListLocator[i].Text.Equals(variable1)) isVarExists = true;
            }
            return isVarExists;
        }

        public bool IsPremiumRankReportExist()
        {
            bool areAllElementsExist = true;

            PremiumRankLabelLocator.WaitForElementPresentAndEnabled(driver);

            if (!PremiumRankLabelLocator.Text.Equals("Primary Carrier Premium Rank (1=lowest)")) areAllElementsExist = false;

            //if (!CarrierAPLocator.Text.Equals($"{primaryCarrier}\r\n{secondaryCarrier}")) areAllElementsExist = false;

            if (!PRChartLocator.Displayed) areAllElementsExist = false;

            return areAllElementsExist;
        }

        //public bool IsDiff2MarketReportExist(string primaryCarrier, string secondaryCarrier)
        //{
        //    bool areAllElementsExist = true;

        //    WinRateLabelLocator.WaitForElementPresentAndEnabled(driver);

        //    if (!WinRateLabelLocator.Text.Equals("Win Rate")) areAllElementsExist = false;

        //    if (!CarrierWRLocator.Text.Equals($"{primaryCarrier}\r\n{secondaryCarrier}")) areAllElementsExist = false;

        //    if (!WRChartLocator.Displayed) areAllElementsExist = false;

        //    return areAllElementsExist;
        //}

        public bool IsSegmentationTableExist()
        {
            bool isTableExist = false;

            CalculateButtonLocator.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//*[@id='div-calculating']/div/div[1]")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("faTableOutputAngle")));
            ExpandTableButtonLocator.Click();

           // var a = SegmentationTableLocator.Text;

            return isTableExist;
        }

        public bool IsSegmentationVariableExist(string variable)
        {
            bool isVariableExist = false;

            for (int i = 0; i<SegmentationVariableLocator.Count; i++)
            {
                if (SegmentationVariableLocator[i].GetAttribute("title") == variable) isVariableExist = true;
            }
            return isVariableExist;
        }

        public bool IsSegmentationByVariableExist(string variable)
        {
            bool isVariableExist = false;

            for (int i = 0; i < SegmentationByVariableLocator.Count; i++)
            {
                if (SegmentationByVariableLocator[i].Text == variable) isVariableExist = true;
            }
            return isVariableExist;
        }

        #endregion Methods

        public bool IsBRVReportResultExist(string variable1, string tableResult)
        {
            bool isTableExists = false;

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ByVar1_val-button"))).Click();

            for (int i = 0; i < ByVariable1BRVListLocator.Count; i++)
            {
                if (ByVariable1BRVListLocator[i].Text.Equals(variable1)) ByVariable1ListLocator[i].Click();
            }
            BRVCalculateButtonLocator.Click();

            Utils.WaitUntilCalculatingDisappears(driver, 120);

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("boxPlotBlock")));

            var c = BRVReportResultLocator.Text;

            if (BRVReportResultLocator.Text.Equals(tableResult)) isTableExists = true;

            return isTableExists;
        }

        public bool isVariableExistBRV(string variable1)
        {
            bool isVarExists = false;

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ByVar1_val-button"))).Click();

            for (int i = 0; i < ByVariable1BRVListLocator.Count; i++)
            {
                if (ByVariable1BRVListLocator[i].Text.Equals(variable1)) isVarExists = true;
            }
            return isVarExists;
        }

    }
}
