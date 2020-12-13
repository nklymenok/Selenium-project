using Milliman.Pixel.Web.Tests.PageObjects;
using Milliman.Pixel.Web.Tests.PageObjects.Pages;
using Milliman.Pixel.Web.Tests.PageObjects.Pages.DDL;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using Extras = SeleniumExtras.WaitHelpers;

namespace Milliman.Pixel.Web.Tests.TestCases
{
    [TestFixture]
    class TerritorySetsTests
    {
        IWebDriver driver;
        LoginPage loginPage;

        WebDriverWait wait;
        WebDriverWait waitQuick;

        static string user = "03_testuser@test.com";

        static string randomString = Utils.RandomString(5);

        const string adminSharedRates = "AdminShared";
        const string adminClientRates = "AdminLuxoft";
        const string adminTestRates = "AdminTest";
        const string adminSharedLosses = "LossAdminShared";
        const string adminClientLosses = "LossAdminLuxoft";
        const string adminTestLosses = "LossAdminTest";

        static string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString()).ToString()).ToString();

        const string CypressFireTerritories = "\r\n1\r\n$2,012\r\n$1,522\r\n10\r\n$2,430\r\n$2,065\r\n11\r\n$1,197\r\n$1,391\r\n12\r\n$9,428\r\n$8,546\r\n2\r\n$2,972\r\n$2,388\r\n3\r\n$2,178\r\n$2,040\r\n4\r\n$2,171\r\n$1,774\r\n5\r\n$3,631\r\n$4,036\r\n6\r\n$3,107\r\n$2,811\r\n7\r\n$2,435\r\n$2,116\r\n8\r\n$6,112\r\n$5,614\r\n9\r\n$6,608\r\n$6,893";
        const string PixelRegion = "\r\nCentral East\r\n$3,611\r\n$2,943\r\nCentral West\r\n$3,065\r\n$2,702\r\nGulf\r\n$1,660\r\n$1,794\r\nInland 1\r\n$2,412\r\n$1,937\r\nInland 2\r\n$1,957\r\n$1,930\r\nNorth East\r\n$2,065\r\n$1,708\r\nSouth East\r\n$7,663\r\n$7,066\r\nSouth West\r\n$3,417\r\n$3,286\r\nWest-Panhandle\r\n$3,765\r\n$2,673";
        const string County = "\r\nAlachua\r\n$1,439\r\n$1,619\r\nBaker\r\n$1,587\r\n$2,176\r\nBay\r\n$3,331\r\n$2,269\r\nBradford\r\n$1,552\r\n$2,138\r\nBrevard\r\n$3,792\r\n$2,771\r\nBroward\r\n$6,791\r\n$6,332\r\nCalhoun\r\n$1,943\r\n$2,680\r\nCharlotte\r\n$2,793\r\n$2,851\r\nCitrus\r\n$1,703\r\n$1,853\r\nClay\r\n$1,504\r\n$1,557\r\nCollier\r\n$5,715\r\n$5,437\r\nColumbia\r\n$1,581\r\n$2,132\r\nDeSoto\r\n$1,878\r\n$2,101\r\nDixie\r\n$1,567\r\n$1,866\r\nDuval\r\n$1,840\r\n$1,632\r\nEscambia\r\n$3,622\r\n$2,370\r\nFlagler\r\n$1,772\r\n$1,393\r\nFranklin\r\n$3,899\r\n$3,236\r\nGadsden\r\n$1,621\r\n$2,164\r\nGilchrist\r\n$1,746\r\n$1,920\r\nGlades\r\n$2,287\r\n$2,316\r\nGulf\r\n$3,326\r\n$2,900\r\nHamilton\r\n$1,570\r\n$2,190\r\nHardee\r\n$2,040\r\n$2,238\r\nHendry\r\n$2,466\r\n$2,452\r\nHernando\r\n$1,964\r\n$1,975\r\nHighlands\r\n$1,813\r\n$1,843\r\nHillsborough\r\n$2,652\r\n$2,669\r\nHolmes\r\n$1,875\r\n$2,663\r\nIndian River\r\n$5,101\r\n$4,405\r\nJackson\r\n$1,556\r\n$2,132\r\nJefferson\r\n$1,866\r\n$1,883\r\nLafayette\r\n$2,554\r\n$3,282\r\nLake\r\n$1,360\r\n$1,401\r\nLee\r\n$3,067\r\n$3,117\r\nLeon\r\n$1,704\r\n$1,554\r\nLevy\r\n$1,708\r\n$1,838\r\nLiberty\r\n$2,016\r\n$2,984\r\nMadison\r\n$1,701\r\n$2,676\r\nManatee\r\n$2,896\r\n$2,564\r\nMarion\r\n$1,107\r\n$1,394\r\nMartin\r\n$6,883\r\n$6,020\r\nMiami-Dade\r\n$9,103\r\n$8,473\r\nMonroe\r\n$5,149\r\n$3,899\r\nNassau\r\n$2,694\r\n$2,048\r\nOkaloosa\r\n$4,089\r\n$2,938\r\nOkeechobee\r\n$2,138\r\n$2,274\r\nOrange\r\n$2,524\r\n$2,339\r\nOsceola\r\n$2,746\r\n$1,975\r\nPalm Beach\r\n$7,126\r\n$6,455\r\nPasco\r\n$2,191\r\n$1,887\r\nPinellas\r\n$4,774\r\n$3,631\r\nPolk\r\n$2,364\r\n$1,892\r\nPutnam\r\n$1,510\r\n$2,100\r\nSaint Johns\r\n$2,687\r\n$1,991\r\nSaint Lucie\r\n$3,675\r\n$2,826\r\nSanta Rosa\r\n$3,453\r\n$2,541\r\nSarasota\r\n$3,076\r\n$2,816\r\nSeminole\r\n$2,916\r\n$2,447\r\nSumter\r\n$1,160\r\n$1,287\r\nSuwannee\r\n$1,437\r\n$1,965\r\nTaylor\r\n$1,763\r\n$1,890\r\nUnion\r\n$2,599\r\n$3,326\r\nVolusia\r\n$2,107\r\n$1,982\r\nWakulla\r\n$1,520\r\n$1,663\r\nWalton\r\n$5,169\r\n$4,118\r\nWashington\r\n$2,019\r\n$2,192";
        const string CitizensTerritory = "\r\nAlachua (192)\r\n$1,439\r\n$1,619\r\nBaker (292)\r\n$1,587\r\n$2,176\r\nBay - Coastal (601)\r\n$4,847\r\n$3,074\r\nBay - Remainder (721)\r\n$2,514\r\n$1,835\r\nBradford (392)\r\n$1,552\r\n$2,138\r\nBrevard - Coastal (57)\r\n$7,322\r\n$4,596\r\nBrevard - Remainder (64)\r\n$2,918\r\n$2,319\r\nBroward - Coastal (361)\r\n$14,878\r\n$10,865\r\nBroward - Hlwd & Ft Laud (35)\r\n$7,809\r\n$6,602\r\nBroward - Remainder (37)\r\n$6,536\r\n$6,243\r\nCalhoun (193)\r\n$1,943\r\n$2,680\r\nCharlotte - Coastal (581)\r\n$3,416\r\n$2,997\r\nCharlotte - Remainder (711)\r\n$2,537\r\n$2,790\r\nCitrus - Coastal (591)\r\n$2,284\r\n$2,763\r\nCitrus - Remainder (731)\r\n$1,661\r\n$1,787\r\nClay (492)\r\n$1,504\r\n$1,557\r\nCollier - Coastal (541)\r\n$9,703\r\n$8,948\r\nCollier - Remainder (551)\r\n$4,658\r\n$4,507\r\nColumbia (293)\r\n$1,581\r\n$2,132\r\nDade - Coastal (31)\r\n$31,358\r\n$22,868\r\nDade - Hialeah (33)\r\n$6,121\r\n$5,869\r\nDade - Miami (32)\r\n$9,988\r\n$8,330\r\nDade - Miami Beach (30)\r\n$37,213\r\n$29,960\r\nDade - Remainder (34)\r\n$8,787\r\n$8,349\r\nDesoto (712)\r\n$1,878\r\n$2,101\r\nDixie - Coastal (592)\r\n$1,723\r\n$2,175\r\nDixie - Remainder (732)\r\n$1,397\r\n$1,532\r\nDuval - Coastal (41)\r\n$3,055\r\n$2,291\r\nDuval - Jacksonville (39)\r\n$1,752\r\n$1,584\r\nDuval - Remainder (40)\r\n$1,217\r\n$1,458\r\nEscambia - Coastal (602)\r\n$7,383\r\n$5,540\r\nEscambia - Remainder (43)\r\n$3,541\r\n$2,301\r\nFlagler - Coastal (531)\r\n$2,831\r\n$2,344\r\nFlagler - Remainder (701)\r\n$1,649\r\n$1,283\r\nFranklin (603)\r\n$3,899\r\n$3,236\r\nGadsden (393)\r\n$1,621\r\n$2,164\r\nGilchrist (923)\r\n$1,746\r\n$1,920\r\nGlades (552)\r\n$2,287\r\n$2,316\r\nGulf - Coastal (604)\r\n$3,800\r\n$3,066\r\nGulf - Remainder (722)\r\n$1,556\r\n$2,278\r\nHamilton (493)\r\n$1,570\r\n$2,190\r\nHardee (713)\r\n$2,040\r\n$2,238\r\nHendry (553)\r\n$2,466\r\n$2,452\r\nHernando - Coastal (159)\r\n$2,775\r\n$3,352\r\nHernando - Remainder (733)\r\n$1,888\r\n$1,846\r\nHighlands (714)\r\n$1,813\r\n$1,843\r\nHillsborough - Excl Tampa (80)\r\n$2,445\r\n$2,488\r\nHillsborough -Tampa (47)\r\n$3,304\r\n$3,238\r\nHolmes (593)\r\n$1,875\r\n$2,663\r\nIndian River - Coastal (181)\r\n$19,241\r\n$16,873\r\nIndian River - Remainder (561)\r\n$3,705\r\n$3,175\r\nJackson (693)\r\n$1,556\r\n$2,132\r\nJefferson - Remainder (793)\r\n$1,866\r\n$1,883\r\nLafayette (893)\r\n$2,554\r\n$3,282\r\nLake (692)\r\n$1,360\r\n$1,401\r\nLee - Coastal (542)\r\n$8,906\r\n$8,057\r\nLee - Remainder (554)\r\n$2,788\r\n$2,881\r\nLeon (993)\r\n$1,704\r\n$1,554\r\nLevy - Coastal (594)\r\n$2,204\r\n$2,448\r\nLevy - Remainder (734)\r\n$1,456\r\n$1,528\r\nLiberty (931)\r\n$2,016\r\n$2,984\r\nMadison (932)\r\n$1,701\r\n$2,676\r\nManatee - Coastal (582)\r\n$6,778\r\n$5,939\r\nManatee - Remainder (735)\r\n$2,670\r\n$2,368\r\nMarion (792)\r\n$1,107\r\n$1,394\r\nMartin - Coastal (182)\r\n$21,652\r\n$18,433\r\nMartin - Remainder (10)\r\n$6,540\r\n$5,731\r\nMonroe - Key West (7)\r\n$4,795\r\n$4,296\r\nMonroe - Remainder (5)\r\n$5,228\r\n$3,811\r\nNassau - Coastal (532)\r\n$4,003\r\n$2,902\r\nNassau - Remainder (892)\r\n$1,946\r\n$1,559\r\nOkaloossa - Coastal (606)\r\n$8,003\r\n$5,597\r\nOkaloossa - Remainder (723)\r\n$3,566\r\n$2,582\r\nOkeechobee (555)\r\n$2,138\r\n$2,274\r\nOrange - Orlando (49)\r\n$2,303\r\n$2,300\r\nOrange - Remainder (90)\r\n$2,563\r\n$2,345\r\nOsceola (511)\r\n$2,746\r\n$1,975\r\nPalm Beach - Coastal (362)\r\n$22,538\r\n$16,188\r\nPalm Beach - Remainder (38)\r\n$6,814\r\n$6,258\r\nPasco - Coastal (595)\r\n$3,166\r\n$2,796\r\nPasco - Remainder (736)\r\n$2,101\r\n$1,803\r\nPinellas - Coastal (42)\r\n$9,318\r\n$7,116\r\nPinellas - Remainder (81)\r\n$4,504\r\n$3,423\r\nPinellas - St Petersburg (46)\r\n$4,645\r\n$3,538\r\nPolk (50)\r\n$2,364\r\n$1,891\r\nPutnam (992)\r\n$1,510\r\n$2,100\r\nSanta Rosa - Coastal (607)\r\n$5,506\r\n$3,867\r\nSanta Rosa - Remainder (724)\r\n$3,426\r\n$2,524\r\nSarasota - Coastal (583)\r\n$3,972\r\n$3,731\r\nSarasota - Remainder (715)\r\n$2,554\r\n$2,283\r\nSeminole (512)\r\n$2,916\r\n$2,447\r\nSt Johns - Coastal (533)\r\n$4,816\r\n$3,770\r\nSt Johns - Remainder (702)\r\n$1,982\r\n$1,402\r\nSt. Lucie - Coastal (183)\r\n$6,654\r\n$4,748\r\nSt. Lucie - Remainder (562)\r\n$3,607\r\n$2,782\r\nSumter (921)\r\n$1,160\r\n$1,287\r\nSuwannee (933)\r\n$1,437\r\n$1,965\r\nTaylor - Coastal (596)\r\n$2,084\r\n$2,124\r\nTaylor - Remainder (737)\r\n$1,545\r\n$1,732\r\nUnion (922)\r\n$2,599\r\n$3,326\r\nVolusia - Coastal (62)\r\n$4,965\r\n$3,115\r\nVolusia - Remainder (63)\r\n$1,803\r\n$1,861\r\nWakulla - Coastal (608)\r\n$2,208\r\n$3,040\r\nWakulla - Remainder (725)\r\n$1,419\r\n$1,460\r\nWalton - Coastal (609)\r\n$6,855\r\n$5,354\r\nWalton - Remainder (726)\r\n$2,184\r\n$1,932\r\nWashington (934)\r\n$2,019\r\n$2,192";
        const string CypressLiabilityTerritories = "\r\n1\r\n$7,707\r\n$7,122\r\n2\r\n$2,824\r\n$2,477";
        const string CypressOtherTerritories = "\r\n1\r\n$3,871\r\n$3,502\r\n2\r\n$3,056\r\n$2,388";
        const string CypressSinkholeTerritories = "\r\n10\r\n$6,540\r\n$5,731\r\n159\r\n$2,775\r\n$3,352\r\n181\r\n$19,247\r\n$16,865\r\n182\r\n$21,652\r\n$18,433\r\n183\r\n$6,654\r\n$4,748\r\n192\r\n$1,439\r\n$1,619\r\n193\r\n$1,943\r\n$2,680\r\n292\r\n$1,587\r\n$2,176\r\n293\r\n$1,581\r\n$2,132\r\n30\r\n$37,213\r\n$29,960\r\n31\r\n$31,332\r\n$22,870\r\n32\r\n$9,988\r\n$8,330\r\n33\r\n$6,127\r\n$5,859\r\n34\r\n$8,788\r\n$8,351\r\n35\r\n$7,580\r\n$6,342\r\n361\r\n$14,878\r\n$10,865\r\n362\r\n$21,799\r\n$15,682\r\n37\r\n$6,531\r\n$6,287\r\n38\r\n$6,806\r\n$6,253\r\n39\r\n$1,752\r\n$1,584\r\n392\r\n$1,552\r\n$2,138\r\n393\r\n$1,621\r\n$2,164\r\n40\r\n$1,217\r\n$1,458\r\n41\r\n$3,055\r\n$2,291\r\n42\r\n$9,573\r\n$7,318\r\n43\r\n$3,541\r\n$2,301\r\n46\r\n$4,564\r\n$3,491\r\n47\r\n$3,250\r\n$3,210\r\n49\r\n$2,286\r\n$2,287\r\n492\r\n$1,504\r\n$1,557\r\n493\r\n$1,570\r\n$2,190\r\n5\r\n$5,246\r\n$3,804\r\n50\r\n$2,364\r\n$1,891\r\n511\r\n$2,746\r\n$1,975\r\n512\r\n$2,916\r\n$2,447\r\n531\r\n$2,795\r\n$2,378\r\n532\r\n$4,003\r\n$2,902\r\n533\r\n$4,810\r\n$3,757\r\n541\r\n$9,703\r\n$8,948\r\n542\r\n$8,950\r\n$8,144\r\n551\r\n$4,658\r\n$4,507\r\n552\r\n$2,287\r\n$2,316\r\n553\r\n$2,466\r\n$2,452\r\n554\r\n$2,796\r\n$2,886\r\n555\r\n$2,138\r\n$2,274\r\n561\r\n$3,686\r\n$3,159\r\n562\r\n$3,607\r\n$2,782\r\n57\r\n$7,322\r\n$4,596\r\n581\r\n$3,414\r\n$2,996\r\n582\r\n$7,709\r\n$7,103\r\n583\r\n$3,944\r\n$3,722\r\n591\r\n$2,284\r\n$2,763\r\n592\r\n$1,723\r\n$2,175\r\n593\r\n$1,875\r\n$2,663\r\n594\r\n$2,204\r\n$2,448\r\n595\r\n$3,166\r\n$2,796\r\n596\r\n$2,084\r\n$2,124\r\n601\r\n$4,845\r\n$3,074\r\n602\r\n$7,383\r\n$5,540\r\n603\r\n$3,899\r\n$3,236\r\n604\r\n$3,800\r\n$3,066\r\n606\r\n$8,003\r\n$5,597\r\n607\r\n$5,506\r\n$3,867\r\n608\r\n$2,208\r\n$3,040\r\n609\r\n$6,855\r\n$5,354\r\n62\r\n$4,993\r\n$3,127\r\n63\r\n$1,792\r\n$1,857\r\n64\r\n$2,918\r\n$2,319\r\n692\r\n$1,360\r\n$1,401\r\n693\r\n$1,556\r\n$2,132\r\n7\r\n$4,788\r\n$4,256\r\n701\r\n$1,675\r\n$1,300\r\n702\r\n$1,997\r\n$1,417\r\n711\r\n$2,540\r\n$2,791\r\n712\r\n$1,878\r\n$2,101\r\n713\r\n$2,040\r\n$2,238\r\n714\r\n$1,813\r\n$1,843\r\n715\r\n$2,577\r\n$2,295\r\n721\r\n$2,514\r\n$1,835\r\n722\r\n$1,556\r\n$2,278\r\n723\r\n$3,566\r\n$2,582\r\n724\r\n$3,426\r\n$2,524\r\n725\r\n$1,419\r\n$1,460\r\n726\r\n$2,184\r\n$1,932\r\n731\r\n$1,661\r\n$1,787\r\n732\r\n$1,397\r\n$1,532\r\n733\r\n$1,888\r\n$1,846\r\n734\r\n$1,456\r\n$1,528\r\n735\r\n$2,701\r\n$2,381\r\n736\r\n$2,101\r\n$1,803\r\n737\r\n$1,545\r\n$1,732\r\n792\r\n$1,107\r\n$1,394\r\n793\r\n$1,866\r\n$1,883\r\n80\r\n$2,448\r\n$2,484\r\n81\r\n$4,510\r\n$3,420\r\n892\r\n$1,946\r\n$1,559\r\n893\r\n$2,554\r\n$3,282\r\n90\r\n$2,568\r\n$2,348\r\n921\r\n$1,160\r\n$1,287\r\n922\r\n$2,599\r\n$3,326\r\n923\r\n$1,746\r\n$1,920\r\n931\r\n$2,016\r\n$2,984\r\n932\r\n$1,701\r\n$2,676\r\n933\r\n$1,437\r\n$1,965\r\n934\r\n$2,019\r\n$2,192\r\n992\r\n$1,510\r\n$2,100\r\n993\r\n$1,704\r\n$1,554";
        const string CypressTerritories = "\r\nAlachua\r\n$1,439\r\n$1,619\r\nBaker\r\n$1,587\r\n$2,176\r\nBay - Coastal\r\n$4,845\r\n$3,074\r\nBay - Remainder\r\n$2,514\r\n$1,835\r\nBradford\r\n$1,552\r\n$2,138\r\nBrevard - Coastal\r\n$6,904\r\n$4,468\r\nBrevard - Remainder\r\n$2,891\r\n$2,279\r\nBroward - Coastal\r\n$13,070\r\n$9,379\r\nBroward - Hlwd & Ft Laud\r\n$6,431\r\n$5,678\r\nBroward - Remainder\r\n$6,399\r\n$6,237\r\nCalhoun\r\n$1,943\r\n$2,680\r\nCharlotte - Coastal\r\n$3,415\r\n$2,998\r\nCharlotte - Remainder\r\n$2,541\r\n$2,791\r\nCitrus - Coastal\r\n$2,284\r\n$2,763\r\nCitrus - Remainder\r\n$1,661\r\n$1,787\r\nClay\r\n$1,504\r\n$1,557\r\nCollier - Coastal\r\n$9,703\r\n$8,948\r\nCollier - Remainder\r\n$4,658\r\n$4,507\r\nColumbia\r\n$1,581\r\n$2,132\r\nDade - Coastal\r\n$13,265\r\n$10,577\r\nDade - Hialeah\r\n$6,130\r\n$5,857\r\nDade - Miami\r\n$8,166\r\n$7,243\r\nDade - Miami Beach (Coastal)\r\n$37,213\r\n$29,960\r\nDade - Remainder\r\n$8,069\r\n$8,034\r\nDe Soto\r\n$1,878\r\n$2,101\r\nDixie - Coastal\r\n$1,723\r\n$2,175\r\nDixie - Remainder\r\n$1,397\r\n$1,532\r\nDuval - Coastal\r\n$3,055\r\n$2,291\r\nDuval - Jacksonville\r\n$1,752\r\n$1,584\r\nDuval - Remainder\r\n$1,217\r\n$1,458\r\nEscambia - Coastal\r\n$7,383\r\n$5,540\r\nEscambia - Remainder\r\n$3,541\r\n$2,301\r\nFlagler - Coastal\r\n$2,318\r\n$1,785\r\nFlagler - Remainder\r\n$1,470\r\n$1,176\r\nFranklin\r\n$3,899\r\n$3,236\r\nGadsden\r\n$1,621\r\n$2,164\r\nGilchrist\r\n$1,746\r\n$1,920\r\nGlades\r\n$2,287\r\n$2,316\r\nGulf - Coastal\r\n$3,800\r\n$3,066\r\nGulf - Remainder\r\n$1,556\r\n$2,278\r\nHamilton\r\n$1,570\r\n$2,190\r\nHardee\r\n$2,040\r\n$2,238\r\nHendry\r\n$2,466\r\n$2,452\r\nHernando - Coastal\r\n$2,008\r\n$1,932\r\nHernando - Remainder\r\n$1,697\r\n$2,233\r\nHighlands\r\n$1,813\r\n$1,843\r\nHillsborough - Excl Tampa\r\n$2,449\r\n$2,485\r\nHillsborough - Tampa\r\n$3,245\r\n$3,207\r\nHolmes\r\n$1,875\r\n$2,663\r\nIndian River - Coastal\r\n$13,067\r\n$11,249\r\nIndian River - Remainder\r\n$3,552\r\n$3,075\r\nJackson\r\n$1,556\r\n$2,132\r\nJefferson - Remainder\r\n$1,866\r\n$1,883\r\nLafayette\r\n$2,554\r\n$3,282\r\nLake\r\n$1,360\r\n$1,401\r\nLee - Coastal\r\n$3,676\r\n$3,554\r\nLee - Remainder\r\n$2,387\r\n$2,630\r\nLeon\r\n$1,704\r\n$1,554\r\nLevy - Coastal\r\n$2,204\r\n$2,448\r\nLevy - Remainder\r\n$1,456\r\n$1,528\r\nLiberty\r\n$2,016\r\n$2,984\r\nMadison\r\n$1,701\r\n$2,676\r\nManatee - Coastal\r\n$4,180\r\n$3,516\r\nManatee - Remainder\r\n$2,375\r\n$2,179\r\nMarion\r\n$1,107\r\n$1,394\r\nMartin - Coastal\r\n$8,555\r\n$6,998\r\nMartin - Remainder\r\n$5,830\r\n$5,403\r\nMonroe - Key West (Coastal)\r\n$4,788\r\n$4,256\r\nMonroe - Remainder (Coastal)\r\n$5,246\r\n$3,804\r\nNassau - Coastal\r\n$4,003\r\n$2,902\r\nNassau - Remainder\r\n$1,946\r\n$1,559\r\nOkaloosa - Coastal\r\n$5,019\r\n$3,288\r\nOkaloosa - Remainder\r\n$1,779\r\n$2,068\r\nOkeechobee\r\n$2,138\r\n$2,274\r\nOrange - Orlando\r\n$2,285\r\n$2,284\r\nOrange - Remainder\r\n$2,569\r\n$2,349\r\nOsceola\r\n$2,746\r\n$1,975\r\nPalm Beach - Coastal\r\n$15,290\r\n$11,764\r\nPalm Beach - Remainder\r\n$6,753\r\n$6,212\r\nPasco - Coastal\r\n$2,415\r\n$1,957\r\nPasco - Remainder\r\n$1,843\r\n$1,778\r\nPinellas - Coastal\r\n$9,573\r\n$7,318\r\nPinellas - Remainder\r\n$4,513\r\n$3,423\r\nPinellas - St. Petersburg\r\n$4,556\r\n$3,485\r\nPolk\r\n$2,364\r\n$1,892\r\nPutnam\r\n$1,510\r\n$2,100\r\nSaint Johns - Coastal\r\n$3,266\r\n$2,612\r\nSaint Johns - Remainder\r\n$2,168\r\n$1,435\r\nSaint Lucie - Coastal\r\n$5,116\r\n$3,378\r\nSaint Lucie - Remainder\r\n$3,482\r\n$2,752\r\nSanta Rosa - Coastal\r\n$5,506\r\n$3,867\r\nSanta Rosa - Remainder\r\n$3,426\r\n$2,524\r\nSarasota - Coastal\r\n$4,556\r\n$4,196\r\nSarasota - Remainder\r\n$2,636\r\n$2,406\r\nSeminole\r\n$2,916\r\n$2,447\r\nSumter\r\n$1,160\r\n$1,287\r\nSuwannee\r\n$1,437\r\n$1,965\r\nTaylor - Coastal\r\n$2,084\r\n$2,124\r\nTaylor - Remainder\r\n$1,545\r\n$1,732\r\nUnion\r\n$2,599\r\n$3,326\r\nVolusia - Coastal\r\n$3,884\r\n$2,721\r\nVolusia - Remainder\r\n$1,761\r\n$1,838\r\nWakulla - Coastal\r\n$2,552\r\n$2,692\r\nWakulla - Remainder\r\n$1,303\r\n$1,447\r\nWalton - Coastal\r\n$6,855\r\n$5,354\r\nWalton - Remainder\r\n$2,184\r\n$1,932\r\nWashington\r\n$2,019\r\n$2,192";
        const string CypressTheftTerritories = "\r\n1\r\n$5,019\r\n$3,700\r\n2\r\n$3,065\r\n$2,497\r\n3\r\n$3,111\r\n$2,743\r\n4\r\n$2,610\r\n$2,288\r\n5\r\n$5,282\r\n$5,178\r\n6\r\n$8,883\r\n$7,879";
        const string CypressWaterTerritories = "\r\n1\r\n$2,585\r\n$2,278\r\n2\r\n$2,962\r\n$2,350\r\n3\r\n$3,841\r\n$3,487\r\n4\r\n$6,793\r\n$5,984\r\n5\r\n$7,439\r\n$7,126\r\n6\r\n$9,444\r\n$7,504\r\n7\r\n$9,701\r\n$8,365";
        const string CypressWaterTerritoriesV2 = "\r\n1\r\n$2,820\r\n$2,350\r\n10\r\n$2,746\r\n$1,975\r\n11\r\n$2,524\r\n$2,339\r\n12\r\n$2,023\r\n$1,686\r\n13\r\n$3,067\r\n$3,117\r\n14\r\n$5,699\r\n$4,983\r\n2\r\n$2,642\r\n$2,218\r\n3\r\n$2,054\r\n$1,763\r\n4\r\n$2,784\r\n$2,696\r\n5\r\n$7,058\r\n$6,396\r\n6\r\n$9,103\r\n$8,473\r\n7\r\n$6,791\r\n$6,332\r\n8\r\n$5,627\r\n$5,199\r\n9\r\n$3,675\r\n$2,826";
        const string CypressWindTerritories = "\r\n10\r\n$7,200\r\n$5,147\r\n100\r\n$1,755\r\n$1,674\r\n101\r\n$1,590\r\n$1,551\r\n102\r\n$1,646\r\n$1,659\r\n103\r\n$1,588\r\n$1,564\r\n104\r\n$1,506\r\n$1,583\r\n105\r\n$1,505\r\n$1,521\r\n106\r\n$1,569\r\n$1,620\r\n107\r\n$1,850\r\n$1,951\r\n108\r\n$1,754\r\n$1,957\r\n109\r\n$1,572\r\n$2,053\r\n11\r\n$5,974\r\n$4,489\r\n110\r\n$1,486\r\n$2,008\r\n111\r\n$1,473\r\n$2,131\r\n112\r\n$1,475\r\n$1,821\r\n113\r\n$1,463\r\n$1,905\r\n114\r\n$1,737\r\n$2,105\r\n12\r\n$6,237\r\n$4,739\r\n13\r\n$7,398\r\n$5,609\r\n14\r\n$39,984\r\n$33,963\r\n15\r\n$37,442\r\n$29,818\r\n16\r\n$29,709\r\n$22,734\r\n17\r\n$12,954\r\n$9,104\r\n18\r\n$13,234\r\n$9,804\r\n19\r\n$9,840\r\n$7,373\r\n2\r\n$3,955\r\n$3,815\r\n20\r\n$12,016\r\n$9,364\r\n21\r\n$12,013\r\n$9,533\r\n22\r\n$10,644\r\n$8,605\r\n23\r\n$9,862\r\n$7,870\r\n24\r\n$10,727\r\n$8,594\r\n25\r\n$9,773\r\n$7,547\r\n26\r\n$10,350\r\n$8,105\r\n27\r\n$10,855\r\n$8,729\r\n28\r\n$9,291\r\n$7,087\r\n29\r\n$8,694\r\n$6,449\r\n3\r\n$5,552\r\n$4,421\r\n30\r\n$7,922\r\n$5,954\r\n31\r\n$7,825\r\n$6,083\r\n32\r\n$6,599\r\n$4,809\r\n33\r\n$7,625\r\n$5,942\r\n34\r\n$7,744\r\n$6,108\r\n35\r\n$7,891\r\n$5,999\r\n36\r\n$7,693\r\n$5,811\r\n37\r\n$7,981\r\n$6,232\r\n38\r\n$7,507\r\n$5,724\r\n39\r\n$7,093\r\n$5,633\r\n4\r\n$5,243\r\n$4,267\r\n40\r\n$6,998\r\n$5,717\r\n41\r\n$6,867\r\n$5,645\r\n42\r\n$6,399\r\n$5,257\r\n43\r\n$6,115\r\n$5,050\r\n44\r\n$6,522\r\n$5,447\r\n45\r\n$6,514\r\n$5,446\r\n46\r\n$5,852\r\n$4,928\r\n47\r\n$5,537\r\n$4,831\r\n48\r\n$6,500\r\n$6,135\r\n49\r\n$6,191\r\n$5,966\r\n5\r\n$6,038\r\n$5,233\r\n50\r\n$5,702\r\n$5,415\r\n51\r\n$5,657\r\n$5,463\r\n52\r\n$6,008\r\n$5,733\r\n53\r\n$6,295\r\n$6,182\r\n54\r\n$5,594\r\n$5,555\r\n55\r\n$4,731\r\n$4,457\r\n56\r\n$4,038\r\n$3,277\r\n57\r\n$3,513\r\n$2,765\r\n58\r\n$3,741\r\n$2,852\r\n59\r\n$3,633\r\n$2,915\r\n6\r\n$3,741\r\n$2,909\r\n60\r\n$3,439\r\n$2,771\r\n61\r\n$3,436\r\n$2,953\r\n62\r\n$3,052\r\n$2,645\r\n63\r\n$2,979\r\n$2,642\r\n64\r\n$2,909\r\n$2,715\r\n65\r\n$2,968\r\n$2,756\r\n66\r\n$2,876\r\n$2,608\r\n67\r\n$2,723\r\n$2,529\r\n68\r\n$2,561\r\n$2,418\r\n69\r\n$2,512\r\n$2,394\r\n7\r\n$5,175\r\n$3,540\r\n70\r\n$2,503\r\n$2,436\r\n71\r\n$2,673\r\n$2,610\r\n72\r\n$2,490\r\n$2,476\r\n73\r\n$2,369\r\n$2,237\r\n74\r\n$2,518\r\n$2,426\r\n75\r\n$2,365\r\n$2,267\r\n76\r\n$2,209\r\n$2,101\r\n77\r\n$2,157\r\n$1,982\r\n78\r\n$2,150\r\n$1,945\r\n79\r\n$2,396\r\n$2,236\r\n8\r\n$8,474\r\n$6,149\r\n80\r\n$2,232\r\n$2,268\r\n81\r\n$2,044\r\n$2,130\r\n82\r\n$2,212\r\n$2,019\r\n83\r\n$2,020\r\n$1,811\r\n84\r\n$2,074\r\n$1,832\r\n85\r\n$2,211\r\n$1,959\r\n86\r\n$2,326\r\n$1,981\r\n87\r\n$2,428\r\n$2,115\r\n88\r\n$2,328\r\n$2,159\r\n89\r\n$2,306\r\n$2,313\r\n9\r\n$7,370\r\n$5,296\r\n90\r\n$1,804\r\n$1,846\r\n91\r\n$1,689\r\n$1,756\r\n92\r\n$1,864\r\n$1,882\r\n93\r\n$1,441\r\n$1,508\r\n94\r\n$1,483\r\n$1,758\r\n95\r\n$1,424\r\n$1,655\r\n96\r\n$1,510\r\n$1,629\r\n97\r\n$1,608\r\n$1,519\r\n98\r\n$1,787\r\n$1,612\r\n99\r\n$1,783\r\n$1,662";
        const string HeritageRegions = "\r\nE Panhandle\r\n$1,663\r\n$1,800\r\nInland\r\n$2,211\r\n$1,975\r\nNorth East\r\n$2,010\r\n$1,774\r\nSE Peninsula\r\n$4,058\r\n$3,240\r\nSouthern Peninsula\r\n$7,663\r\n$7,066\r\nSW Peninsula\r\n$3,397\r\n$3,271\r\nTampa Bay Area\r\n$3,158\r\n$2,760\r\nW Panhandle\r\n$3,637\r\n$2,642";
        const string HeritageTerritories = "\r\n100F10\r\n$3,236\r\n$4,818\r\n100F11\r\n$5,647\r\n$5,948\r\n120F05\r\n$4,015\r\n$2,261\r\n120F06\r\n$3,644\r\n$2,109\r\n120F07\r\n$4,801\r\n$2,809\r\n120F08\r\n$4,627\r\n$2,910\r\n120F09\r\n$5,240\r\n$2,669\r\n120F10\r\n$5,519\r\n$3,074\r\n121F03\r\n$2,429\r\n$2,346\r\n121F07\r\n$4,133\r\n$2,304\r\n121F10\r\n$5,072\r\n$2,884\r\n121F12\r\n$8,015\r\n$5,714\r\n122F05\r\n$4,047\r\n$2,930\r\n122F06\r\n$4,076\r\n$2,588\r\n122F08\r\n$5,196\r\n$3,332\r\n122F09\r\n$5,059\r\n$2,821\r\n122F10\r\n$8,032\r\n$5,604\r\n123F01\r\n$2,298\r\n$2,277\r\n123F03\r\n$2,756\r\n$1,779\r\n123F06\r\n$6,377\r\n$5,025\r\n123F10\r\n$7,689\r\n$5,761\r\n124F03\r\n$2,805\r\n$1,899\r\n124F04\r\n$2,602\r\n$1,865\r\n124F05\r\n$3,364\r\n$2,035\r\n124F06\r\n$5,121\r\n$3,371\r\n124F09\r\n$4,963\r\n$3,048\r\n125F03\r\n$3,332\r\n$2,679\r\n125F05\r\n$5,147\r\n$3,603\r\n125F06\r\n$3,672\r\n$3,009\r\n126F01\r\n$1,916\r\n$1,939\r\n127F01\r\n$2,194\r\n$2,029\r\n127F02\r\n$2,325\r\n$1,951\r\n127F04\r\n$3,077\r\n$2,356\r\n127F05\r\n$3,612\r\n$2,589\r\n128F03\r\n$2,387\r\n$2,375\r\n128F04\r\n$2,151\r\n$1,787\r\n128F05\r\n$2,165\r\n$1,941\r\n128F06\r\n$3,316\r\n$3,655\r\n129F06\r\n$2,544\r\n$1,826\r\n129F07\r\n$2,659\r\n$1,944\r\n129F08\r\n$3,275\r\n$2,572\r\n129F09\r\n$2,777\r\n$2,410\r\n130F07\r\n$2,957\r\n$2,290\r\n130F08\r\n$2,588\r\n$2,132\r\n130F09\r\n$2,670\r\n$2,529\r\n130F10\r\n$4,636\r\n$3,409\r\n130F11\r\n$4,251\r\n$3,121\r\n130F14\r\n$6,193\r\n$4,835\r\n130F15\r\n$8,953\r\n$8,191\r\n131F06\r\n$2,552\r\n$2,309\r\n131F07\r\n$2,966\r\n$2,617\r\n131F08\r\n$3,600\r\n$2,408\r\n131F09\r\n$3,073\r\n$2,941\r\n131F10\r\n$3,320\r\n$3,517\r\n131F11\r\n$3,855\r\n$3,114\r\n131F12\r\n$5,057\r\n$4,570\r\n131F14\r\n$8,171\r\n$8,105\r\n132F07\r\n$2,407\r\n$2,691\r\n132F08\r\n$2,793\r\n$2,871\r\n132F10\r\n$3,036\r\n$2,526\r\n132F13\r\n$9,533\r\n$8,162\r\n133F08\r\n$2,840\r\n$2,951\r\n133F09\r\n$2,929\r\n$2,854\r\n133F10\r\n$4,218\r\n$3,921\r\n133F12\r\n$9,609\r\n$7,872\r\n133F13\r\n$4,839\r\n$4,557\r\n133F14\r\n$5,213\r\n$4,739\r\n133F15\r\n$7,200\r\n$6,324\r\n133F16\r\n$12,124\r\n$10,611\r\n134F10\r\n$4,713\r\n$4,129\r\n134F11\r\n$6,632\r\n$6,256\r\n134F14\r\n$12,025\r\n$10,473\r\n134F15\r\n$9,948\r\n$8,917\r\n134F16\r\n$17,604\r\n$19,076\r\n134F17\r\n$11,266\r\n$8,520\r\n141F10\r\n$2,529\r\n$2,242\r\n142F08\r\n$3,067\r\n$3,177\r\n142F09\r\n$3,204\r\n$3,106\r\n142F10\r\n$3,474\r\n$3,204\r\n143F08\r\n$4,418\r\n$2,362\r\n143F10\r\n$4,323\r\n$2,916\r\n143F11\r\n$6,566\r\n$3,332\r\n143F12\r\n$7,786\r\n$4,226\r\n143F13\r\n$10,177\r\n$5,440\r\n143F14\r\n$8,656\r\n$5,006\r\n143F16\r\n$9,249\r\n$4,804\r\n144F06\r\n$2,470\r\n$2,161\r\n144F07\r\n$3,055\r\n$2,744\r\n145F05\r\n$3,048\r\n$1,838\r\n145F07\r\n$5,032\r\n$3,045\r\n145F09\r\n$5,433\r\n$3,230\r\n146F01\r\n$1,158\r\n$1,079\r\n146F02\r\n$1,308\r\n$1,105\r\n146F03\r\n$2,779\r\n$2,262\r\n146F05\r\n$3,752\r\n$2,429\r\n146F06\r\n$2,951\r\n$2,177\r\n147F01\r\n$1,552\r\n$1,217\r\n147F02\r\n$3,611\r\n$2,549\r\n150F07\r\n$2,384\r\n$2,301\r\n150F08\r\n$2,194\r\n$2,388\r\n180F13\r\n$5,418\r\n$4,549\r\n180F14\r\n$5,296\r\n$5,926\r\n180F15\r\n$9,900\r\n$7,947\r\n180F18\r\n$6,106\r\n$4,387\r\n180F19\r\n$10,953\r\n$9,352\r\n181F10\r\n$3,283\r\n$2,554\r\n181F11\r\n$3,652\r\n$3,219\r\n181F12\r\n$3,089\r\n$3,275\r\n181F13\r\n$4,897\r\n$3,187\r\n181F14\r\n$4,851\r\n$3,709\r\n181F16\r\n$3,983\r\n$3,460\r\n181F18\r\n$7,608\r\n$5,772\r\n182F11\r\n$3,787\r\n$2,917\r\n182F12\r\n$3,816\r\n$3,048\r\n182F13\r\n$3,696\r\n$3,106\r\n182F14\r\n$4,090\r\n$3,925\r\n182F17\r\n$19,247\r\n$16,865\r\n310F13\r\n$9,357\r\n$8,576\r\n310F14\r\n$8,959\r\n$8,210\r\n310F15\r\n$6,535\r\n$5,572\r\n310F16\r\n$8,866\r\n$7,653\r\n310F17\r\n$12,651\r\n$10,923\r\n310F18\r\n$13,485\r\n$11,902\r\n310F19\r\n$8,310\r\n$7,152\r\n310F20\r\n$18,143\r\n$15,178\r\n310F21\r\n$15,701\r\n$11,939\r\n310F22\r\n$16,541\r\n$12,492\r\n310F23\r\n$29,471\r\n$21,871\r\n310F24\r\n$44,921\r\n$37,079\r\n310F25\r\n$30,217\r\n$21,525\r\n320F12\r\n$6,994\r\n$7,376\r\n320F13\r\n$6,709\r\n$7,324\r\n320F14\r\n$6,868\r\n$7,981\r\n320F15\r\n$10,014\r\n$11,748\r\n340F11\r\n$9,148\r\n$8,109\r\n340F12\r\n$6,897\r\n$6,950\r\n340F13\r\n$5,204\r\n$3,970\r\n340F14\r\n$7,231\r\n$6,755\r\n350F11\r\n$5,964\r\n$6,034\r\n350F12\r\n$6,490\r\n$6,580\r\n350F13\r\n$4,971\r\n$5,216\r\n360F13\r\n$6,262\r\n$5,638\r\n360F14\r\n$5,595\r\n$5,219\r\n360F17\r\n$7,764\r\n$5,828\r\n360F18\r\n$7,117\r\n$5,227\r\n360F19\r\n$10,180\r\n$7,651\r\n360F20\r\n$8,075\r\n$5,858\r\n360F21\r\n$14,406\r\n$10,081\r\n360F23\r\n$11,911\r\n$7,972\r\n361F13\r\n$7,051\r\n$5,545\r\n361F15\r\n$6,605\r\n$5,419\r\n361F16\r\n$8,114\r\n$6,783\r\n361F17\r\n$5,476\r\n$4,486\r\n361F18\r\n$8,139\r\n$6,650\r\n361F19\r\n$16,925\r\n$11,696\r\n361F20\r\n$6,626\r\n$5,796\r\n361F21\r\n$10,721\r\n$8,213\r\n361F22\r\n$10,809\r\n$8,505\r\n370F11\r\n$6,654\r\n$6,746\r\n370F12\r\n$6,379\r\n$6,306\r\n370F13\r\n$7,294\r\n$7,217\r\n380F11\r\n$5,891\r\n$5,639\r\n380F12\r\n$6,583\r\n$6,715\r\n380F13\r\n$6,891\r\n$5,671\r\n381F09\r\n$6,570\r\n$8,068\r\n381F10\r\n$6,636\r\n$6,998\r\n381F11\r\n$4,903\r\n$5,378\r\n390F01\r\n$1,873\r\n$1,629\r\n390F02\r\n$1,901\r\n$1,550\r\n391F01\r\n$1,419\r\n$1,469\r\n410F03\r\n$3,034\r\n$2,275\r\n420F12\r\n$5,833\r\n$3,570\r\n420F13\r\n$6,988\r\n$4,850\r\n420F14\r\n$8,063\r\n$5,730\r\n420F15\r\n$10,335\r\n$7,827\r\n430F01\r\n$1,637\r\n$2,339\r\n430F02\r\n$2,201\r\n$2,288\r\n430F03\r\n$2,058\r\n$2,052\r\n430F04\r\n$2,099\r\n$1,923\r\n440F06\r\n$2,361\r\n$2,509\r\n440F07\r\n$1,947\r\n$1,708\r\n441F05\r\n$2,115\r\n$2,340\r\n441F06\r\n$2,219\r\n$2,539\r\n442F02\r\n$1,538\r\n$1,887\r\n442F03\r\n$1,703\r\n$1,857\r\n442F04\r\n$1,991\r\n$1,859\r\n442F05\r\n$2,041\r\n$1,915\r\n450F01\r\n$2,366\r\n$1,497\r\n450F02\r\n$1,847\r\n$1,568\r\n451F02\r\n$2,114\r\n$2,342\r\n451F03\r\n$2,276\r\n$2,338\r\n452F01\r\n$1,745\r\n$2,334\r\n452F02\r\n$1,779\r\n$2,058\r\n453F02\r\n$1,392\r\n$1,849\r\n454F01\r\n$1,619\r\n$1,613\r\n454F02\r\n$2,280\r\n$1,801\r\n454F03\r\n$1,977\r\n$1,666\r\n455F01\r\n$1,556\r\n$2,278\r\n456F01\r\n$1,416\r\n$1,728\r\n457F01\r\n$1,444\r\n$1,857\r\n457F02\r\n$1,460\r\n$1,835\r\n458F02\r\n$1,358\r\n$1,438\r\n458F03\r\n$1,654\r\n$1,879\r\n458F04\r\n$1,679\r\n$1,827\r\n459F04\r\n$1,996\r\n$1,965\r\n459F05\r\n$1,819\r\n$1,726\r\n459F06\r\n$1,555\r\n$1,409\r\n460F06\r\n$2,233\r\n$2,190\r\n460F07\r\n$2,016\r\n$2,321\r\n461F06\r\n$1,351\r\n$1,347\r\n461F07\r\n$2,867\r\n$3,105\r\n462F06\r\n$2,319\r\n$2,509\r\n462F07\r\n$2,620\r\n$2,971\r\n463F07\r\n$2,745\r\n$3,361\r\n463F08\r\n$2,174\r\n$2,425\r\n464F08\r\n$3,322\r\n$3,661\r\n464F12\r\n$3,320\r\n$3,538\r\n465F05\r\n$1,995\r\n$2,253\r\n465F06\r\n$1,878\r\n$2,101\r\n466F05\r\n$1,732\r\n$1,815\r\n466F06\r\n$1,890\r\n$1,863\r\n466F07\r\n$2,180\r\n$2,288\r\n470F04\r\n$2,803\r\n$3,365\r\n470F05\r\n$2,673\r\n$2,926\r\n470F06\r\n$1,914\r\n$2,283\r\n470F07\r\n$3,275\r\n$2,985\r\n470F08\r\n$2,563\r\n$2,445\r\n471F04\r\n$2,854\r\n$2,634\r\n471F05\r\n$3,004\r\n$2,756\r\n471F06\r\n$1,951\r\n$1,804\r\n472F07\r\n$2,193\r\n$1,699\r\n472F08\r\n$2,756\r\n$1,934\r\n472F09\r\n$6,424\r\n$5,351\r\n472F10\r\n$4,287\r\n$3,253\r\n472F11\r\n$3,839\r\n$2,404\r\n473F04\r\n$2,295\r\n$2,856\r\n473F05\r\n$1,958\r\n$2,443\r\n480F05\r\n$5,762\r\n$4,870\r\n480F06\r\n$3,666\r\n$3,274\r\n480F07\r\n$3,568\r\n$3,070\r\n480F08\r\n$4,512\r\n$3,436\r\n480F09\r\n$4,337\r\n$3,396\r\n480F10\r\n$4,724\r\n$3,491\r\n480F11\r\n$6,335\r\n$3,942\r\n481F05\r\n$4,867\r\n$4,543\r\n481F06\r\n$3,900\r\n$3,507\r\n481F08\r\n$2,746\r\n$2,824\r\n490F04\r\n$2,515\r\n$2,686\r\n500F04\r\n$2,176\r\n$1,531\r\n500F05\r\n$2,388\r\n$1,954\r\n500F06\r\n$2,276\r\n$2,160\r\n50F19\r\n$4,298\r\n$3,127\r\n50F21\r\n$5,905\r\n$4,327\r\n50F23\r\n$4,729\r\n$3,352\r\n50F24\r\n$5,161\r\n$3,549\r\n510F04\r\n$2,956\r\n$2,132\r\n510F05\r\n$2,727\r\n$1,947\r\n510F06\r\n$3,709\r\n$2,905\r\n511F03\r\n$3,027\r\n$2,540\r\n511F04\r\n$2,684\r\n$2,317\r\n520F03\r\n$2,386\r\n$2,031\r\n520F04\r\n$2,612\r\n$2,318\r\n520F05\r\n$2,179\r\n$2,029\r\n521F02\r\n$1,206\r\n$1,554\r\n521F03\r\n$1,238\r\n$1,341\r\n521F04\r\n$1,273\r\n$1,402\r\n521F05\r\n$1,422\r\n$1,356\r\n522F01\r\n$1,053\r\n$1,550\r\n522F02\r\n$1,108\r\n$1,458\r\n522F03\r\n$1,091\r\n$1,325\r\n523F01\r\n$1,492\r\n$1,610\r\n523F02\r\n$1,650\r\n$2,209\r\n524F01\r\n$1,447\r\n$1,657\r\n525F01\r\n$1,638\r\n$2,191\r\n526F01\r\n$1,634\r\n$2,220\r\n527F01\r\n$2,021\r\n$2,398\r\n527F02\r\n$2,075\r\n$2,228\r\n527F06\r\n$1,782\r\n$1,893\r\n528F01\r\n$1,704\r\n$1,554\r\n529F01\r\n$1,754\r\n$1,743\r\n70F19\r\n$4,867\r\n$4,240";
        const string MonarchRegions = "\r\nCoastal West Coast\r\n$3,881\r\n$3,167\r\nJacksonville\r\n$1,840\r\n$1,728\r\nOrlando\r\n$2,385\r\n$2,178\r\nPanhandle\r\n$3,114\r\n$2,401\r\nSoutheast Florida\r\n$7,663\r\n$7,066\r\nSouthwest Florida\r\n$3,627\r\n$3,602\r\nTampa/St. Pete\r\n$2,303\r\n$2,176\r\nTreasure Coast\r\n$3,824\r\n$3,007";
        const string MonarchTerritories = "\r\n100\r\n$5,647\r\n$5,948\r\n120\r\n$5,142\r\n$2,740\r\n121\r\n$4,919\r\n$2,910\r\n122\r\n$6,354\r\n$4,093\r\n123\r\n$6,754\r\n$5,237\r\n124\r\n$4,723\r\n$3,016\r\n125\r\n$3,868\r\n$3,050\r\n126\r\n$1,916\r\n$1,939\r\n127\r\n$2,699\r\n$2,179\r\n128\r\n$2,283\r\n$2,049\r\n129\r\n$2,744\r\n$2,072\r\n130\r\n$8,881\r\n$8,103\r\n131\r\n$3,925\r\n$3,699\r\n132\r\n$3,864\r\n$3,359\r\n133\r\n$8,900\r\n$7,645\r\n134\r\n$12,115\r\n$10,564\r\n141\r\n$2,529\r\n$2,242\r\n142\r\n$3,329\r\n$3,161\r\n143\r\n$8,108\r\n$4,637\r\n144\r\n$2,881\r\n$2,570\r\n145\r\n$5,169\r\n$3,097\r\n146\r\n$3,849\r\n$3,020\r\n147\r\n$3,611\r\n$2,549\r\n150\r\n$2,279\r\n$2,348\r\n180\r\n$7,484\r\n$6,210\r\n181\r\n$3,702\r\n$2,832\r\n182\r\n$5,685\r\n$4,815\r\n310\r\n$11,051\r\n$9,488\r\n320\r\n$6,801\r\n$7,444\r\n340\r\n$7,111\r\n$6,899\r\n350\r\n$6,002\r\n$6,118\r\n360\r\n$7,734\r\n$6,172\r\n361\r\n$8,869\r\n$6,957\r\n370\r\n$6,774\r\n$6,736\r\n380\r\n$6,285\r\n$6,106\r\n381\r\n$5,863\r\n$6,424\r\n390\r\n$1,874\r\n$1,626\r\n391\r\n$1,419\r\n$1,469\r\n410\r\n$3,034\r\n$2,275\r\n420\r\n$6,967\r\n$4,708\r\n430\r\n$2,090\r\n$1,985\r\n440\r\n$2,054\r\n$1,914\r\n441\r\n$2,155\r\n$2,417\r\n442\r\n$2,025\r\n$1,898\r\n450\r\n$2,348\r\n$1,499\r\n451\r\n$2,190\r\n$2,291\r\n452\r\n$1,779\r\n$2,058\r\n453\r\n$1,392\r\n$1,849\r\n454\r\n$2,025\r\n$1,688\r\n455\r\n$1,556\r\n$2,278\r\n456\r\n$1,416\r\n$1,728\r\n457\r\n$1,456\r\n$1,840\r\n458\r\n$1,693\r\n$1,808\r\n459\r\n$1,840\r\n$1,757\r\n460\r\n$2,187\r\n$2,218\r\n461\r\n$1,773\r\n$1,835\r\n462\r\n$2,517\r\n$2,814\r\n463\r\n$2,285\r\n$2,607\r\n464\r\n$3,322\r\n$3,661\r\n465\r\n$1,923\r\n$2,160\r\n466\r\n$1,813\r\n$1,843\r\n470\r\n$2,959\r\n$2,614\r\n471\r\n$2,563\r\n$2,358\r\n472\r\n$5,478\r\n$4,373\r\n473\r\n$2,115\r\n$2,636\r\n480\r\n$4,719\r\n$3,494\r\n481\r\n$3,590\r\n$3,400\r\n490\r\n$2,515\r\n$2,686\r\n50\r\n$5,237\r\n$3,793\r\n500\r\n$2,354\r\n$1,887\r\n510\r\n$2,778\r\n$1,989\r\n511\r\n$2,951\r\n$2,491\r\n520\r\n$2,506\r\n$2,222\r\n521\r\n$1,293\r\n$1,366\r\n522\r\n$1,099\r\n$1,411\r\n523\r\n$1,498\r\n$1,634\r\n524\r\n$1,447\r\n$1,657\r\n525\r\n$1,638\r\n$2,191\r\n526\r\n$1,634\r\n$2,220\r\n527\r\n$2,025\r\n$2,376\r\n528\r\n$1,704\r\n$1,554\r\n529\r\n$1,754\r\n$1,743\r\n601\r\n$3,236\r\n$4,818\r\n612\r\n$4,520\r\n$2,576\r\n613\r\n$3,635\r\n$2,154\r\n622\r\n$2,429\r\n$2,346\r\n631\r\n$4,278\r\n$2,649\r\n642\r\n$3,767\r\n$2,685\r\n643\r\n$2,298\r\n$2,277\r\n651\r\n$2,700\r\n$1,882\r\n663\r\n$2,387\r\n$2,375\r\n671\r\n$4,114\r\n$3,116\r\n672\r\n$2,793\r\n$2,299\r\n682\r\n$3,588\r\n$2,687\r\n683\r\n$2,636\r\n$2,503\r\n691\r\n$2,494\r\n$2,536\r\n692\r\n$2,612\r\n$2,938\r\n70\r\n$4,867\r\n$4,240\r\n703\r\n$3,593\r\n$3,478\r\n704\r\n$2,784\r\n$2,842\r\n705\r\n$2,022\r\n$1,936\r\n712\r\n$4,523\r\n$4,073\r\n713\r\n$6,383\r\n$5,908\r\n722\r\n$3,911\r\n$2,341\r\n731\r\n$1,280\r\n$1,064\r\n732\r\n$1,925\r\n$1,486\r\n733\r\n$1,263\r\n$1,161\r\n741\r\n$1,552\r\n$1,217\r\n762\r\n$1,637\r\n$2,339\r\n772\r\n$2,331\r\n$2,202\r\n773\r\n$1,885\r\n$1,842\r\n774\r\n$1,508\r\n$1,773\r\n781\r\n$2,346\r\n$3,082\r\n791\r\n$1,745\r\n$2,334\r\n801\r\n$1,619\r\n$1,613\r\n811\r\n$1,746\r\n$2,020\r\n812\r\n$1,544\r\n$1,733\r\n821\r\n$2,507\r\n$2,802\r\n822\r\n$2,039\r\n$2,592\r\n832\r\n$2,539\r\n$1,844\r\n847\r\n$3,482\r\n$3,163";
        const string PMAFLMarketingZone = "\r\n1\r\n$3,154\r\n$2,408\r\n2\r\n$1,837\r\n$1,734\r\n3\r\n$2,864\r\n$2,458\r\n4\r\n$5,835\r\n$5,396";
        const string PreferredMGATerritories =  "\r\n1\r\n$2,831\r\n$2,295\r\n2\r\n$2,507\r\n$2,236\r\n3\r\n$2,691\r\n$2,273\r\n4\r\n$5,835\r\n$5,396";

        const string PixelRegionBRV = "Central East\r\nCentral West\r\nGulf\r\nInland 1\r\nInland 2\r\nNorth East\r\nSouth East\r\nSouth West\r\nWest-Panhandle\r\n-50 %\r\n-25 %\r\n0 %\r\n25 %\r\n50 %\r\n75 %\r\n100 %\r\n12…\r\nPolicies\r\n0\r\n25k\r\n50k\r\n75k\r\n100k";
        const string CypressFireTerritoriesBRV = "1\r\n10\r\n11\r\n12\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n-50 %\r\n-25 %\r\n0 %\r\n25 %\r\n50 %\r\n75 %\r\n100 %\r\n12…\r\nPolicies\r\n0\r\n25k\r\n50k\r\n75k\r\n100k";
        const string CountyBRV = "Alachua\r\nBaker\r\nBay\r\nBradford\r\nBrevard\r\nBroward\r\nCalhoun\r\nCharlotte\r\nCitrus\r\nClay\r\nCollier\r\nColumbia\r\nDeSoto\r\nDixie\r\nDuval\r\nEscambia\r\nFlagler\r\nFranklin\r\nGadsden\r\nGilchrist\r\nGlades\r\nGulf\r\nHamilton\r\nHardee\r\nHendry\r\nHernando\r\nHighlands\r\nHillsborough\r\nHolmes\r\nIndian River\r\nJackson\r\nJefferson\r\nLafayette\r\nLake\r\nLee\r\nLeon\r\nLevy\r\nLiberty\r\nMadison\r\nManatee\r\nMarion\r\nMartin\r\nMiami-Dade\r\nMonroe\r\nNassau\r\nOkaloosa\r\nOkeechobee\r\nOrange\r\nOsceola\r\nPalm Beach\r\nPasco\r\nPinellas\r\nPolk\r\nPutnam\r\nSaint Johns\r\nSaint Lucie\r\nSanta Rosa\r\nSarasota\r\nSeminole\r\nSumter\r\nSuwannee\r\nTaylor\r\nUnion\r\nVolusia\r\nWakulla\r\nWalton\r\nWashington\r\n-50 %\r\n0 %\r\n50 %\r\n100 %\r\n150 %\r\n-100 %\r\n20…\r\nPolicies\r\n0\r\n10k\r\n20k\r\n30k";
        const string CitizensTerritoriesBRV = "Alachua (192)\r\nBaker (292)\r\nBay - Coastal (601)\r\nBay - Remainder (721)\r\nBradford (392)\r\nBrevard - Coastal (57)\r\nBrevard - Remainder (64)\r\nBroward - Coastal (361)\r\nBroward - Hlwd & Ft Laud(35)\r\nBroward - Remainder (37)\r\nCalhoun (193)\r\nCharlotte - Coastal (581)\r\nCharlotte - Remainder(711)\r\nCitrus - Coastal (591)\r\nCitrus - Remainder (731)\r\nClay (492)\r\nCollier - Coastal (541)\r\nCollier - Remainder (551)\r\nColumbia (293)\r\nDade - Coastal (31)\r\nDade - Hialeah (33)\r\nDade - Miami (32)\r\nDade - Miami Beach (30)\r\nDade - Remainder (34)\r\nDesoto (712)\r\nDixie - Coastal (592)\r\nDixie - Remainder (732)\r\nDuval - Coastal (41)\r\nDuval - Jacksonville (39)\r\nDuval - Remainder (40)\r\nEscambia - Coastal (602)\r\nEscambia - Remainder (43)\r\nFlagler - Coastal (531)\r\nFlagler - Remainder (701)\r\nFranklin (603)\r\nGadsden (393)\r\nGilchrist (923)\r\nGlades (552)\r\nGulf - Coastal (604)\r\nGulf - Remainder (722)\r\nHamilton (493)\r\nHardee (713)\r\nHendry (553)\r\nHernando - Coastal (159)\r\nHernando - Remainder(733)\r\nHighlands (714)\r\nHillsborough - Excl Tampa(80)\r\nHillsborough -Tampa (47)\r\nHolmes (593)\r\nIndian River - Coastal (181)\r\nIndian River - Remainder(561)\r\nJackson (693)\r\nJefferson - Remainder(793)\r\nLafayette (893)\r\nLake (692)\r\nLee - Coastal (542)\r\nLee - Remainder (554)\r\nLeon (993)\r\nLevy - Coastal (594)\r\nLevy - Remainder (734)\r\nLiberty (931)\r\nMadison (932)\r\nManatee - Coastal (582)\r\nManatee - Remainder (735)\r\nMarion (792)\r\nMartin - Coastal (182)\r\nMartin - Remainder (10)\r\nMonroe - Key West (7)\r\nMonroe - Remainder (5)\r\nNassau - Coastal (532)\r\nNassau - Remainder (892)\r\nOkaloossa - Coastal (606)\r\nOkaloossa - Remainder(723)\r\nOkeechobee (555)\r\nOrange - Orlando (49)\r\nOrange - Remainder (90)\r\nOsceola (511)\r\nPalm Beach - Coastal (362)\r\nPalm Beach - Remainder(38)\r\nPasco - Coastal (595)\r\nPasco - Remainder (736)\r\nPinellas - Coastal (42)\r\nPinellas - Remainder (81)\r\nPinellas - St Petersburg (46)\r\nPolk (50)\r\nPutnam (992)\r\nSanta Rosa - Coastal (607)\r\nSanta Rosa - Remainder(724)\r\nSarasota - Coastal (583)\r\nSarasota - Remainder (715)\r\nSeminole (512)\r\nSt Johns - Coastal (533)\r\nSt Johns - Remainder (702)\r\nSt. Lucie - Coastal (183)\r\nSt. Lucie - Remainder (562)\r\nSumter (921)\r\nSuwannee (933)\r\nTaylor - Coastal (596)\r\nTaylor - Remainder (737)\r\nUnion (922)\r\nVolusia - Coastal (62)\r\nVolusia - Remainder (63)\r\nWakulla - Coastal (608)\r\nWakulla - Remainder (725)\r\nWalton - Coastal (609)\r\nWalton - Remainder (726)\r\nWashington (934)\r\n-50 %\r\n0 %\r\n50 %\r\n100 %\r\n150 %\r\n-100 %\r\n20…\r\nPolicies\r\n0\r\n5k\r\n10k\r\n15k\r\n20k\r\n25k\r\n30k";
        const string CypressLiabilityTerritoriesBRV = "1\r\n2\r\n-40 %\r\n-20 %\r\n0 %\r\n20 %\r\n40 %\r\n60 %\r\n80 %\r\n10…\r\nPolicies\r\n0\r\n100k\r\n200k\r\n300k\r\n400k";
        const string CypressOtherTerritoriesBRV = "1\r\n2\r\n-50 %\r\n-25 %\r\n0 %\r\n25 %\r\n50 %\r\n75 %\r\n100 %\r\n12…\r\nPolicies\r\n0\r\n100k\r\n200k\r\n300k\r\n400k";
        const string CypressSinkholeTerritoriesBRV = "10\r\n159\r\n181\r\n182\r\n183\r\n192\r\n193\r\n292\r\n293\r\n30\r\n31\r\n32\r\n33\r\n34\r\n35\r\n361\r\n362\r\n37\r\n38\r\n39\r\n392\r\n393\r\n40\r\n41\r\n42\r\n43\r\n46\r\n47\r\n49\r\n492\r\n493\r\n5\r\n50\r\n511\r\n512\r\n531\r\n532\r\n533\r\n541\r\n542\r\n551\r\n552\r\n553\r\n554\r\n555\r\n561\r\n562\r\n57\r\n581\r\n582\r\n583\r\n591\r\n592\r\n593\r\n594\r\n595\r\n596\r\n601\r\n602\r\n603\r\n604\r\n606\r\n607\r\n608\r\n609\r\n62\r\n63\r\n64\r\n692\r\n693\r\n7\r\n701\r\n702\r\n711\r\n712\r\n713\r\n714\r\n715\r\n721\r\n722\r\n723\r\n724\r\n725\r\n726\r\n731\r\n732\r\n733\r\n734\r\n735\r\n736\r\n737\r\n792\r\n793\r\n80\r\n81\r\n892\r\n893\r\n90\r\n921\r\n922\r\n923\r\n931\r\n932\r\n933\r\n934\r\n992\r\n993\r\n-75 %\r\n-50 %\r\n-25 %\r\n0 %\r\n25 %\r\n50 %\r\n75 %\r\n100 %\r\n125 %\r\n150 %\r\n1…\r\nPolicies\r\n0\r\n5k\r\n10k\r\n15k\r\n20k\r\n25k\r\n30k";
        const string CypressTerritoriesBRV = "Alachua\r\nBaker\r\nBay - Coastal\r\nBay - Remainder\r\nBradford\r\nBrevard - Coastal\r\nBrevard - Remainder\r\nBroward - Coastal\r\nBroward - Hlwd & Ft Laud\r\nBroward - Remainder\r\nCalhoun\r\nCharlotte - Coastal\r\nCharlotte - Remainder\r\nCitrus - Coastal\r\nCitrus - Remainder\r\nClay\r\nCollier - Coastal\r\nCollier - Remainder\r\nColumbia\r\nDade - Coastal\r\nDade - Hialeah\r\nDade - Miami\r\nDade - Miami Beach(Coastal)\r\nDade - Remainder\r\nDe Soto\r\nDixie - Coastal\r\nDixie - Remainder\r\nDuval - Coastal\r\nDuval - Jacksonville\r\nDuval - Remainder\r\nEscambia - Coastal\r\nEscambia - Remainder\r\nFlagler - Coastal\r\nFlagler - Remainder\r\nFranklin\r\nGadsden\r\nGilchrist\r\nGlades\r\nGulf - Coastal\r\nGulf - Remainder\r\nHamilton\r\nHardee\r\nHendry\r\nHernando - Coastal\r\nHernando - Remainder\r\nHighlands\r\nHillsborough - Excl Tampa\r\nHillsborough - Tampa\r\nHolmes\r\nIndian River - Coastal\r\nIndian River - Remainder\r\nJackson\r\nJefferson - Remainder\r\nLafayette\r\nLake\r\nLee - Coastal\r\nLee - Remainder\r\nLeon\r\nLevy - Coastal\r\nLevy - Remainder\r\nLiberty\r\nMadison\r\nManatee - Coastal\r\nManatee - Remainder\r\nMarion\r\nMartin - Coastal\r\nMartin - Remainder\r\nMonroe - Key West(Coastal)\r\nMonroe - Remainder(Coastal)\r\nNassau - Coastal\r\nNassau - Remainder\r\nOkaloosa - Coastal\r\nOkaloosa - Remainder\r\nOkeechobee\r\nOrange - Orlando\r\nOrange - Remainder\r\nOsceola\r\nPalm Beach - Coastal\r\nPalm Beach - Remainder\r\nPasco - Coastal\r\nPasco - Remainder\r\nPinellas - Coastal\r\nPinellas - Remainder\r\nPinellas - St. Petersburg\r\nPolk\r\nPutnam\r\nSaint Johns - Coastal\r\nSaint Johns - Remainder\r\nSaint Lucie - Coastal\r\nSaint Lucie - Remainder\r\nSanta Rosa - Coastal\r\nSanta Rosa - Remainder\r\nSarasota - Coastal\r\nSarasota - Remainder\r\nSeminole\r\nSumter\r\nSuwannee\r\nTaylor - Coastal\r\nTaylor - Remainder\r\nUnion\r\nVolusia - Coastal\r\nVolusia - Remainder\r\nWakulla - Coastal\r\nWakulla - Remainder\r\nWalton - Coastal\r\nWalton - Remainder\r\nWashington\r\n-50 %\r\n0 %\r\n50 %\r\n100 %\r\n150 %\r\n-100 %\r\n20…\r\nPolicies\r\n0\r\n5k\r\n10k\r\n15k\r\n20k\r\n25k";
        const string CypressTheftTerritoriesBRV = "1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n-50 %\r\n-25 %\r\n0 %\r\n25 %\r\n50 %\r\n75 %\r\n100 %\r\n12…\r\nPolicies\r\n0\r\n50k\r\n100k\r\n150k\r\n200k\r\n250k";
        const string CypressWaterTerritoriesBRV = "1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n-50 %\r\n-25 %\r\n0 %\r\n25 %\r\n50 %\r\n75 %\r\n100 %\r\n12…\r\nPolicies\r\n0\r\n50k\r\n100k\r\n150k\r\n200k\r\n250k";
        const string CypressWaterTerritoriesV2BRV = "1\r\n10\r\n11\r\n12\r\n13\r\n14\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n-50 %\r\n-25 %\r\n0 %\r\n25 %\r\n50 %\r\n75 %\r\n100 %\r\n12…\r\nPolicies\r\n0\r\n25k\r\n50k\r\n75k\r\n100k";
        const string CypressWindTerritoriesBRV = "10\r\n100\r\n101\r\n102\r\n103\r\n104\r\n105\r\n106\r\n107\r\n108\r\n109\r\n11\r\n110\r\n111\r\n112\r\n113\r\n114\r\n12\r\n13\r\n14\r\n15\r\n16\r\n17\r\n18\r\n19\r\n2\r\n20\r\n21\r\n22\r\n23\r\n24\r\n25\r\n26\r\n27\r\n28\r\n29\r\n3\r\n30\r\n31\r\n32\r\n33\r\n34\r\n35\r\n36\r\n37\r\n38\r\n39\r\n4\r\n40\r\n41\r\n42\r\n43\r\n44\r\n45\r\n46\r\n47\r\n48\r\n49\r\n5\r\n50\r\n51\r\n52\r\n53\r\n54\r\n55\r\n56\r\n57\r\n58\r\n59\r\n6\r\n60\r\n61\r\n62\r\n63\r\n64\r\n65\r\n66\r\n67\r\n68\r\n69\r\n7\r\n70\r\n71\r\n72\r\n73\r\n74\r\n75\r\n76\r\n77\r\n78\r\n79\r\n8\r\n80\r\n81\r\n82\r\n83\r\n84\r\n85\r\n86\r\n87\r\n88\r\n89\r\n9\r\n90\r\n91\r\n92\r\n93\r\n94\r\n95\r\n96\r\n97\r\n98\r\n99\r\n-75 %\r\n-50 %\r\n-25 %\r\n0 %\r\n25 %\r\n50 %\r\n75 %\r\n100 %\r\n125 %\r\n150 %\r\n1…\r\nPolicies\r\n0\r\n5k\r\n10k\r\n15k\r\n20k";
        const string MonarchRegionsBRV = "Coastal West Coast\r\nJacksonville\r\nOrlando\r\nPanhandle\r\nSoutheast Florida\r\nSouthwest Florida\r\nTampa/St. Pete\r\nTreasure Coast\r\n-50 %\r\n-25 %\r\n0 %\r\n25 %\r\n50 %\r\n75 %\r\n100 %\r\n12…\r\nPolicies\r\n0\r\n25k\r\n50k\r\n75k\r\n100k";
        const string HeritageRegionsBRV = "E Panhandle\r\nInland\r\nNorth East\r\nSE Peninsula\r\nSouthern Peninsula\r\nSW Peninsula\r\nTampa Bay Area\r\nW Panhandle\r\n-50 %\r\n-25 %\r\n0 %\r\n25 %\r\n50 %\r\n75 %\r\n100 %\r\n1…\r\nPolicies\r\n0\r\n25k\r\n50k\r\n75k\r\n100k";
        const string HeritageTerritoriesBRV = "100F10\r\n100F11\r\n120F05\r\n120F06\r\n120F07\r\n120F08\r\n120F09\r\n120F10\r\n121F03\r\n121F07\r\n121F10\r\n121F12\r\n122F05\r\n122F06\r\n122F08\r\n122F09\r\n122F10\r\n123F01\r\n123F03\r\n123F06\r\n123F10\r\n124F03\r\n124F04\r\n124F05\r\n124F06\r\n124F09\r\n125F03\r\n125F05\r\n125F06\r\n126F01\r\n127F01\r\n127F02\r\n127F04\r\n127F05\r\n128F03\r\n128F04\r\n128F05\r\n128F06\r\n129F06\r\n129F07\r\n129F08\r\n129F09\r\n130F07\r\n130F08\r\n130F09\r\n130F10\r\n130F11\r\n130F14\r\n130F15\r\n131F06\r\n131F07\r\n131F08\r\n131F09\r\n131F10\r\n131F11\r\n131F12\r\n131F14\r\n132F07\r\n132F08\r\n132F10\r\n132F13\r\n133F08\r\n133F09\r\n133F10\r\n133F12\r\n133F13\r\n133F14\r\n133F15\r\n133F16\r\n134F10\r\n134F11\r\n134F14\r\n134F15\r\n134F16\r\n134F17\r\n141F10\r\n142F08\r\n142F09\r\n142F10\r\n143F08\r\n143F10\r\n143F11\r\n143F12\r\n143F13\r\n143F14\r\n143F16\r\n144F06\r\n144F07\r\n145F05\r\n145F07\r\n145F09\r\n146F01\r\n146F02\r\n146F03\r\n146F05\r\n146F06\r\n147F01\r\n147F02\r\n150F07\r\n150F08\r\n180F13\r\n180F14\r\n180F15\r\n180F18\r\n180F19\r\n181F10\r\n181F11\r\n181F12\r\n181F13\r\n181F14\r\n181F16\r\n181F18\r\n182F11\r\n182F12\r\n182F13\r\n182F14\r\n182F17\r\n310F13\r\n310F14\r\n310F15\r\n310F16\r\n310F17\r\n310F18\r\n310F19\r\n310F20\r\n310F21\r\n310F22\r\n310F23\r\n310F24\r\n310F25\r\n320F12\r\n320F13\r\n320F14\r\n320F15\r\n340F11\r\n340F12\r\n340F13\r\n340F14\r\n350F11\r\n350F12\r\n350F13\r\n360F13\r\n360F14\r\n360F17\r\n360F18\r\n360F19\r\n360F20\r\n360F21\r\n360F23\r\n361F13\r\n361F15\r\n361F16\r\n361F17\r\n361F18\r\n361F19\r\n361F20\r\n361F21\r\n361F22\r\n370F11\r\n370F12\r\n370F13\r\n380F11\r\n380F12\r\n380F13\r\n381F09\r\n381F10\r\n381F11\r\n390F01\r\n390F02\r\n391F01\r\n410F03\r\n420F12\r\n420F13\r\n420F14\r\n420F15\r\n430F01\r\n430F02\r\n430F03\r\n430F04\r\n440F06\r\n440F07\r\n441F05\r\n441F06\r\n442F02\r\n442F03\r\n442F04\r\n442F05\r\n450F01\r\n450F02\r\n451F02\r\n451F03\r\n452F01\r\n452F02\r\n453F02\r\n454F01\r\n454F02\r\n454F03\r\n455F01\r\n456F01\r\n457F01\r\n457F02\r\n458F02\r\n458F03\r\n458F04\r\n459F04\r\n459F05\r\n459F06\r\n460F06\r\n460F07\r\n461F06\r\n461F07\r\n462F06\r\n462F07\r\n463F07\r\n463F08\r\n464F08\r\n464F12\r\n465F05\r\n465F06\r\n466F05\r\n466F06\r\n466F07\r\n470F04\r\n470F05\r\n470F06\r\n470F07\r\n470F08\r\n471F04\r\n471F05\r\n471F06\r\n472F07\r\n472F08\r\n472F09\r\n472F10\r\n472F11\r\n473F04\r\n473F05\r\n480F05\r\n480F06\r\n480F07\r\n480F08\r\n480F09\r\n480F10\r\n480F11\r\n481F05\r\n481F06\r\n481F08\r\n490F04\r\n500F04\r\n500F05\r\n500F06\r\n50F19\r\n50F21\r\n50F23\r\n50F24\r\n510F04\r\n510F05\r\n510F06\r\n511F03\r\n511F04\r\n520F03\r\n520F04\r\n520F05\r\n521F02\r\n521F03\r\n521F04\r\n521F05\r\n522F01\r\n522F02\r\n522F03\r\n523F01\r\n523F02\r\n524F01\r\n525F01\r\n526F01\r\n527F01\r\n527F02\r\n527F06\r\n528F01\r\n529F01\r\n70F19\r\n-100 %\r\n-50 %\r\n0 %\r\n50 %\r\n100 %\r\n150 %\r\n200 %\r\n25…\r\nPolicies\r\n0\r\n5k\r\n10k\r\n15k";
        const string MonarchTerritoriesBRV = "100\r\n120\r\n121\r\n122\r\n123\r\n124\r\n125\r\n126\r\n127\r\n128\r\n129\r\n130\r\n131\r\n132\r\n133\r\n134\r\n141\r\n142\r\n143\r\n144\r\n145\r\n146\r\n147\r\n150\r\n180\r\n181\r\n182\r\n310\r\n320\r\n340\r\n350\r\n360\r\n361\r\n370\r\n380\r\n381\r\n390\r\n391\r\n410\r\n420\r\n430\r\n440\r\n441\r\n442\r\n450\r\n451\r\n452\r\n453\r\n454\r\n455\r\n456\r\n457\r\n458\r\n459\r\n460\r\n461\r\n462\r\n463\r\n464\r\n465\r\n466\r\n470\r\n471\r\n472\r\n473\r\n480\r\n481\r\n490\r\n50\r\n500\r\n510\r\n511\r\n520\r\n521\r\n522\r\n523\r\n524\r\n525\r\n526\r\n527\r\n528\r\n529\r\n601\r\n612\r\n613\r\n622\r\n631\r\n642\r\n643\r\n651\r\n663\r\n671\r\n672\r\n682\r\n683\r\n691\r\n692\r\n70\r\n703\r\n704\r\n705\r\n712\r\n713\r\n722\r\n731\r\n732\r\n733\r\n741\r\n762\r\n772\r\n773\r\n774\r\n781\r\n791\r\n801\r\n811\r\n812\r\n821\r\n822\r\n832\r\n847\r\n-100 %\r\n-50 %\r\n0 %\r\n50 %\r\n100 %\r\n150 %\r\n200 %\r\nPolicies\r\n0\r\n5k\r\n10k\r\n15k\r\n20k";
        const string PMAFLMarketingZoneBRV = "1\r\n2\r\n3\r\n4\r\n-50 %\r\n-25 %\r\n0 %\r\n25 %\r\n50 %\r\n75 %\r\n100 %\r\n12…\r\nPolicies\r\n0\r\n50k\r\n100k\r\n150k\r\n200k";
        const string PreferredMGATerritoriesBRV = "1\r\n2\r\n3\r\n4\r\n-50 %\r\n-25 %\r\n0 %\r\n25 %\r\n50 %\r\n75 %\r\n100 %\r\n12…\r\nPolicies\r\n0\r\n50k\r\n100k\r\n150k";


        const string PixelRegionDP3CBV = "\r\nCentral West\r\n$972\r\n$770\r\nInland 1\r\n$2,226\r\n$1,583\r\nInland 2\r\n$746\r\n$551\r\nSouth West\r\n$980\r\n$752";
        const string CountyDP3CBV = "\r\nCitrus\r\n$1,233\r\n$676\r\nGadsden\r\n$700\r\n$484\r\nHernando\r\n$891\r\n$952\r\nHillsborough\r\n$793\r\n$683\r\nLee\r\n$980\r\n$752\r\nOkeechobee\r\n$2,226\r\n$1,583\r\nSumter\r\n$791\r\n$617";
        const string CitizensTerritoryDP3CBV = "\r\nCitrus - Coastal (591)\r\n$1,233\r\n$676\r\nGadsden (393)\r\n$700\r\n$484\r\nHernando - Remainder (733)\r\n$891\r\n$952\r\nHillsborough - Excl Tampa (80)\r\n$793\r\n$683\r\nLee - Remainder (554)\r\n$980\r\n$752\r\nOkeechobee (555)\r\n$2,226\r\n$1,583\r\nSumter (921)\r\n$791\r\n$617";

        const string PixelRegionDP3BRV = "Central West\r\nInland 1\r\nInland 2\r\nSouth West\r\n0 %\r\n20 %\r\n40 %\r\n60 %\r\n80 %\r\n-20 %\r\n100…\r\nPolicies\r\n0\r\n1\r\n2\r\n3\r\n4";
        const string CountyDP3BRV = "Citrus\r\nGadsden\r\nHernando\r\nHillsborough\r\nLee\r\nOkeechobee\r\nSumter\r\n0 %\r\n20 %\r\n40 %\r\n60 %\r\n80 %\r\n-20 %\r\n10…\r\nPolicies\r\n0\r\n0.5\r\n1\r\n1.5\r\n2\r\n2.5";
        const string CitizensTerritoryDP3BRV = "Citrus - Coastal (591)\r\nGadsden (393)\r\nHernando - Remainder(733)\r\nHillsborough - Excl Tampa(80)\r\nLee - Remainder (554)\r\nOkeechobee (555)\r\nSumter (921)\r\n0 %\r\n20 %\r\n40 %\r\n60 %\r\n80 %\r\n-20 %\r\n10…\r\nPolicies\r\n0\r\n0.5\r\n1\r\n1.5\r\n2\r\n2.5";

        [SetUp]
        public void InitializeAdmin()
        {
            driver = new ChromeDriver();
            driver.Url = "https://pixel.com";
           

            driver.Manage().Window.Maximize();
            loginPage = new LoginPage(driver);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10000));
            waitQuick = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Order(1)]
        [Test]
        [CustomRetry]
        public void CheckGeographyTypeTest()
        {
            loginPage.LoginToApplication();

            SaveTheStory("HO3 Florida Market Basket 2020-01-01", "American Integrity (cur)", "Anchor (cur)");       

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            defaultDatasetPage.ExpandFilterLocator.WaitForElementPresentAndEnabled(driver, 100).Click();

            defaultDatasetPage.GeneralPanelLocator.WaitForElementPresentAndEnabled(driver);

            defaultDatasetPage.GeographyTabLocator.Click();

            defaultDatasetPage.GeographyTypeButtonLocator.Click();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(19, defaultDatasetPage.GeographyTypeHO3ListLocator.Count);
                Assert.AreEqual("Pixel Regions", defaultDatasetPage.GeographyTypeHO3ListLocator[1].GetAttribute("text"));
                Assert.AreEqual("Counties", defaultDatasetPage.GeographyTypeHO3ListLocator[2].GetAttribute("text"));
                Assert.AreEqual("Citizens Territories", defaultDatasetPage.GeographyTypeHO3ListLocator[3].GetAttribute("text"));
                Assert.AreEqual("Cypress Territories", defaultDatasetPage.GeographyTypeHO3ListLocator[4].GetAttribute("text"));
                Assert.AreEqual("Cypress Fire Territories", defaultDatasetPage.GeographyTypeHO3ListLocator[5].GetAttribute("text"));
                Assert.AreEqual("Cypress Liability Territories", defaultDatasetPage.GeographyTypeHO3ListLocator[6].GetAttribute("text"));
                Assert.AreEqual("Cypress Other Territories", defaultDatasetPage.GeographyTypeHO3ListLocator[7].GetAttribute("text"));
                Assert.AreEqual("Cypress Sinkhole Territories", defaultDatasetPage.GeographyTypeHO3ListLocator[8].GetAttribute("text"));
                Assert.AreEqual("Cypress Theft Territories", defaultDatasetPage.GeographyTypeHO3ListLocator[9].GetAttribute("text"));
                Assert.AreEqual("Cypress Water Territories", defaultDatasetPage.GeographyTypeHO3ListLocator[10].GetAttribute("text"));
                Assert.AreEqual("Heritage Regions", defaultDatasetPage.GeographyTypeHO3ListLocator[11].GetAttribute("text"));
                Assert.AreEqual("Heritage Territories", defaultDatasetPage.GeographyTypeHO3ListLocator[12].GetAttribute("text"));
                Assert.AreEqual("Monarch Regions", defaultDatasetPage.GeographyTypeHO3ListLocator[13].GetAttribute("text"));
                Assert.AreEqual("Monarch Territories", defaultDatasetPage.GeographyTypeHO3ListLocator[14].GetAttribute("text"));
                Assert.AreEqual("Preferred MGA Territories", defaultDatasetPage.GeographyTypeHO3ListLocator[15].GetAttribute("text"));
                Assert.AreEqual("Cypress Wind Territories", defaultDatasetPage.GeographyTypeHO3ListLocator[16].GetAttribute("text"));
                Assert.AreEqual("Cypress Water Territories V2", defaultDatasetPage.GeographyTypeHO3ListLocator[17].GetAttribute("text"));
                Assert.AreEqual("PMA FL Marketing Zone", defaultDatasetPage.GeographyTypeHO3ListLocator[18].GetAttribute("text"));
            });
        }

        [Order(2)]
        [Test]
        [CustomRetry]
        public void CheckCBVReportTerrirorySetsTest()
        {
            loginPage.LoginToApplication();

            OpenStory("HO3 Florida Market Basket 2020-01-01");

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.CarrierByVariableTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.CarrierByVariableMinimizedLocator.Click();
            }
            else defaultDatasetPage.CarrierByVariableTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            Utils.Scroll(new DashboardPage(driver).LogoLocator, driver);

            Assert.Multiple(() =>
            {
                Assert.True(defaultDatasetPage.IsTableExist(Variables.CypressFireTerritories, "American Integrity", "Anchor", CypressFireTerritories), $"Incorrect table result for Cypress Fire Territories (Location-based). Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.PixelRegion, "American Integrity", "Anchor", PixelRegion), $"Incorrect table result for Pixel Region. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.County, "American Integrity", "Anchor", County), $"Incorrect table result for County. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.CitizensTerritories, "American Integrity", "Anchor", CitizensTerritory), $"Incorrect table result for Citizens Territory. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.CypressLiabilityTerritories, "American Integrity", "Anchor", CypressLiabilityTerritories), $"Incorrect table result for Cypress Liability Territories (Location-based). Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.CypressOtherTerritories, "American Integrity", "Anchor", CypressOtherTerritories), $"Incorrect table result for Cypress Other Territories (Location-based). Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.CypressSinkholeTerritories, "American Integrity", "Anchor", CypressSinkholeTerritories), $"Incorrect table result for Cypress Sinkhole Territories (Location-based). Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.CypressTerritories, "American Integrity", "Anchor", CypressTerritories), $"Incorrect table result for Cypress Territories (Location-based). Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.CypressTheftTerritories, "American Integrity", "Anchor", CypressTheftTerritories), $"Incorrect table result for Cypress Theft Territories (Location-based)y. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.CypressWaterTerritories, "American Integrity", "Anchor", CypressWaterTerritories), $"Incorrect table result for Cypress Water Territories (Location-based). Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.CypressWaterTerritoriesV2, "American Integrity", "Anchor", CypressWaterTerritoriesV2), $"Incorrect table result for Cypress Water Territories V2 (Location-based). Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.CypressWindTerritories, "American Integrity", "Anchor", CypressWindTerritories), $"Incorrect table result for Cypress Wind Territories (Location-based). Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.MonarchRegions, "American Integrity", "Anchor", MonarchRegions), $"Incorrect table result for Monarch Regions (Location-based). Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.HeritageRegions, "American Integrity", "Anchor", HeritageRegions), $"Incorrect table result for Heritage Regions (Location-based)	American . Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.HeritageTerritories, "American Integrity", "Anchor", HeritageTerritories), $"Incorrect table result for Heritage Territories (Location-based). Actual: {defaultDatasetPage.CBVTableLocator.Text}");             
                Assert.True(defaultDatasetPage.IsTableExist(Variables.MonarchTerritories, "American Integrity", "Anchor", MonarchTerritories), $"Incorrect table result for Monarch Territories (Location-based). Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.PMAFLMarketingZone, "American Integrity", "Anchor", PMAFLMarketingZone), $"Incorrect table result for PMA FL Marketing Zone (Location-based). Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.PreferredMGATerritories, "American Integrity", "Anchor", PreferredMGATerritories), $"Incorrect table result for Preferred MGA Territories (Location-based). Actual: {defaultDatasetPage.CBVTableLocator.Text}");
            });
        }

        [Order(3)]
        [Test]
        [CustomRetry]
        public void CheckSegmentationReportTerrirorySetsTest()
        {
            loginPage.LoginToApplication();

            OpenStory("HO3 Florida Market Basket 2020-01-01");

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.SegmentationTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.SegmentationMinimizedLocator.Click();
            }
            else defaultDatasetPage.SegmentationTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            defaultDatasetPage.UserDefinedValuesCheckboxLocator.Click();

            Assert.Multiple(() =>
            {
                defaultDatasetPage.SegmentationVariableButtonLocator.WaitForElementPresentAndEnabled(driver).Click();

                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressFireTerritories), $"{Variables.CypressFireTerritories} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.PixelRegion), $"{Variables.PixelRegion} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.County), $"{Variables.County} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.CitizensTerritories), $"{Variables.CitizensTerritories} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressLiabilityTerritories), $"{Variables.CypressLiabilityTerritories} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressOtherTerritories), $"{Variables.CypressOtherTerritories} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressSinkholeTerritories), $"{Variables.CypressSinkholeTerritories} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressTerritories), $"{Variables.CypressTerritories} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressTheftTerritories), $"{Variables.CypressTheftTerritories} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressWaterTerritories), $"{Variables.CypressWaterTerritories} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressWaterTerritoriesV2), $"{Variables.CypressWaterTerritoriesV2} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressWindTerritories), $"{Variables.CypressWindTerritories} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.MonarchRegions), $"{Variables.MonarchRegions} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.MonarchTerritories), $"{Variables.MonarchTerritories} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.HeritageRegions), $"{Variables.HeritageRegions} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.HeritageTerritories), $"{Variables.HeritageTerritories} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.PMAFLMarketingZone), $"{Variables.PMAFLMarketingZone} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.PreferredMGATerritories), $"{Variables.PreferredMGATerritories} doesn`t exist in Segmentation Variable(s) list");
                defaultDatasetPage.SegmentationVariableButtonLocator.Click();

                defaultDatasetPage.SegmentationByVariableButtonLocator.WaitForElementPresentAndEnabled(driver).Click();

                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressFireTerritories), $"{Variables.CypressFireTerritories} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.PixelRegion), $"{Variables.PixelRegion} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.County), $"{Variables.County} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.CitizensTerritories), $"{Variables.CitizensTerritories} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressLiabilityTerritories), $"{Variables.CypressLiabilityTerritories} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressOtherTerritories), $"{Variables.CypressOtherTerritories} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressSinkholeTerritories), $"{Variables.CypressSinkholeTerritories} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressTerritories), $"{Variables.CypressTerritories} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressTheftTerritories), $"{Variables.CypressTheftTerritories} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressWaterTerritories), $"{Variables.CypressWaterTerritories} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressWaterTerritoriesV2), $"{Variables.CypressWaterTerritoriesV2} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressWindTerritories), $"{Variables.CypressWindTerritories} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.MonarchRegions), $"{Variables.MonarchRegions} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.MonarchTerritories), $"{Variables.MonarchTerritories} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.HeritageRegions), $"{Variables.HeritageRegions} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.HeritageTerritories), $"{Variables.HeritageTerritories} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.PMAFLMarketingZone), $"{Variables.PMAFLMarketingZone} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.PreferredMGATerritories), $"{Variables.PreferredMGATerritories} doesn`t exist in By Variable list");
            });
        }

        [Order(4)]
        [Test]
        [CustomRetry]
        public void CheckBRVReportTerrirorySetsTest()
        {
            loginPage.LoginToApplication();

            driver.Manage().Window.Size = new System.Drawing.Size(1024, 1024);

            OpenStory("HO3 Florida Market Basket 2020-01-01");

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.ByRatingVariableTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.ByRatingVariableMinimizedLocator.Click();
            }
            else defaultDatasetPage.ByRatingVariableTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver, 200);

            Utils.Scroll(new DashboardPage(driver).LogoLocator, driver);

            Assert.Multiple(() =>
            {
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.PixelRegion, PixelRegionBRV), $"Incorrect table result for {Variables.PixelRegion}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}, Expected: {PixelRegionBRV}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.CypressFireTerritories, CypressFireTerritoriesBRV), $"Incorrect table result for {Variables.CypressFireTerritories}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.County, CountyBRV), $"Incorrect table result for {Variables.County}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.CitizensTerritories, CitizensTerritoriesBRV), $"Incorrect table result for {Variables.CitizensTerritories}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.CypressLiabilityTerritories, CypressLiabilityTerritoriesBRV), $"Incorrect table result for {Variables.CypressLiabilityTerritories}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.CypressOtherTerritories, CypressOtherTerritoriesBRV), $"Incorrect table result for {Variables.CypressOtherTerritories}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.CypressSinkholeTerritories, CypressSinkholeTerritoriesBRV), $"Incorrect table result for {Variables.CypressSinkholeTerritories}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.CypressTerritories, CypressTerritoriesBRV), $"Incorrect table result for {Variables.CypressTerritories}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.CypressTheftTerritories, CypressTheftTerritoriesBRV), $"Incorrect table result for {Variables.CypressTheftTerritories}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.CypressWaterTerritories, CypressWaterTerritoriesBRV), $"Incorrect table result for {Variables.CypressWaterTerritories}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.CypressWaterTerritoriesV2, CypressWaterTerritoriesV2BRV), $"Incorrect table result for {Variables.CypressWaterTerritoriesV2}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.CypressWindTerritories, CypressWindTerritoriesBRV), $"Incorrect table result for {Variables.CypressWindTerritories}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.MonarchRegions, MonarchRegionsBRV), $"Incorrect table result for {Variables.MonarchRegions}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.HeritageRegions, HeritageRegionsBRV), $"Incorrect table result for {Variables.HeritageRegions}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.HeritageTerritories, HeritageTerritoriesBRV), $"Incorrect table result for {Variables.HeritageTerritories}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.MonarchTerritories, MonarchTerritoriesBRV), $"Incorrect table result for {Variables.MonarchTerritories}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.PMAFLMarketingZone, PMAFLMarketingZoneBRV), $"Incorrect table result for {Variables.PMAFLMarketingZone}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.PreferredMGATerritories, PreferredMGATerritoriesBRV), $"Incorrect table result for {Variables.PreferredMGATerritories}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
            });
        }

        [Order(5)]
        [Test]
        [CustomRetry]
        public void UserCheckGeographyTypeTest()
        {
            loginPage.LoginToApplication(user, "NBV87^yu");

            OpenStory("HO3 Florida Market Basket 2020-01-01");

            var defaultDatasetPage = new DefaultDatasetsPage(driver);


            defaultDatasetPage.ExpandFilterLocator.WaitForElementPresentAndEnabled(driver, 100).Click();

            defaultDatasetPage.GeneralPanelLocator.WaitForElementPresentAndEnabled(driver);

            defaultDatasetPage.GeographyTabLocator.Click();

            defaultDatasetPage.GeographyTypeButtonLocator.Click();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(4, defaultDatasetPage.GeographyTypeHO3ListLocator.Count);
                Assert.AreEqual("Pixel Regions", defaultDatasetPage.GeographyTypeHO3ListLocator[1].GetAttribute("text"));
                Assert.AreEqual("Counties", defaultDatasetPage.GeographyTypeHO3ListLocator[2].GetAttribute("text"));
                Assert.AreEqual("Citizens Territories", defaultDatasetPage.GeographyTypeHO3ListLocator[3].GetAttribute("text"));
            });
        }

        [Order(6)]
        [Test]
        [CustomRetry]
        public void UserCheckCBVReportTerrirorySetsTest()
        {
            loginPage.LoginToApplication(user, "NBV87^yu");

            OpenStory("HO3 Florida Market Basket 2020-01-01");

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.CarrierByVariableTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.CarrierByVariableMinimizedLocator.Click();
            }
            else defaultDatasetPage.CarrierByVariableTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            Utils.Scroll(new DashboardPage(driver).LogoLocator, driver);

            Assert.Multiple(() =>
            {               
                Assert.True(defaultDatasetPage.IsTableExist(Variables.PixelRegion, "American Integrity", "Anchor", PixelRegion), $"Incorrect table result for Pixel Region. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.County, "American Integrity", "Anchor", County), $"Incorrect table result for County. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.CitizensTerritories, "American Integrity", "Anchor", CitizensTerritory), $"Incorrect table result for Citizens Territory. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.CypressFireTerritories), $"Variable {Variables.CypressFireTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.CypressLiabilityTerritories), $"Variable {Variables.CypressLiabilityTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.CypressOtherTerritories), $"Variable {Variables.CypressOtherTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.CypressSinkholeTerritories), $"Variable {Variables.CypressSinkholeTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.CypressTerritories), $"Variable {Variables.CypressTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.CypressTheftTerritories), $"Variable {Variables.CypressTheftTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.CypressWaterTerritories), $"Variable {Variables.CypressWaterTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.CypressWaterTerritoriesV2), $"Variable {Variables.CypressWaterTerritoriesV2} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.CypressWindTerritories), $"Variable {Variables.CypressWindTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.MonarchRegions), $"Variable {Variables.MonarchRegions} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.HeritageRegions), $"Variable {Variables.HeritageRegions} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.HeritageTerritories), $"Variable {Variables.HeritageTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.MonarchTerritories), $"Variable {Variables.MonarchTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.PMAFLMarketingZone), $"Variable {Variables.PMAFLMarketingZone} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.PreferredMGATerritories), $"Variable {Variables.PreferredMGATerritories} exists");
            });
        }

        [Order(7)]
        [Test]
        [CustomRetry]
        public void UserCheckSegmentationReportTerrirorySetsTest()
        {
            loginPage.LoginToApplication(user, "NBV87^yu");

            OpenStory("HO3 Florida Market Basket 2020-01-01");

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.SegmentationTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.SegmentationMinimizedLocator.Click();
            }
            else defaultDatasetPage.SegmentationTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            defaultDatasetPage.UserDefinedValuesCheckboxLocator.Click();

            Assert.Multiple(() =>
            {
                defaultDatasetPage.SegmentationVariableButtonLocator.WaitForElementPresentAndEnabled(driver).Click();

                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.PixelRegion), $"{Variables.PixelRegion} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.County), $"{Variables.County} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.CitizensTerritories), $"{Variables.CitizensTerritories} doesn`t exist in Segmentation Variable(s) list");

                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressFireTerritories), $"{Variables.CypressFireTerritories} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressLiabilityTerritories), $"{Variables.CypressLiabilityTerritories} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressOtherTerritories), $"{Variables.CypressOtherTerritories} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressSinkholeTerritories), $"{Variables.CypressSinkholeTerritories} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressTerritories), $"{Variables.CypressTerritories} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressTheftTerritories), $"{Variables.CypressTheftTerritories} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressWaterTerritories), $"{Variables.CypressWaterTerritories} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressWaterTerritoriesV2), $"{Variables.CypressWaterTerritoriesV2} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressWindTerritories), $"{Variables.CypressWindTerritories} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.MonarchRegions), $"{Variables.MonarchRegions} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.MonarchTerritories), $"{Variables.MonarchTerritories} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.HeritageRegions), $"{Variables.HeritageRegions} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.HeritageTerritories), $"{Variables.HeritageTerritories} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.PMAFLMarketingZone), $"{Variables.PMAFLMarketingZone} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.PreferredMGATerritories), $"{Variables.PreferredMGATerritories} exists in Segmentation Variable(s) list");

                defaultDatasetPage.SegmentationVariableButtonLocator.Click();

                defaultDatasetPage.SegmentationByVariableButtonLocator.WaitForElementPresentAndEnabled(driver).Click();

                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.PixelRegion), $"{Variables.PixelRegion} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.County), $"{Variables.County} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.CitizensTerritories), $"{Variables.CitizensTerritories} doesn`t exist in By Variable list");

                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressFireTerritories), $"{Variables.CypressFireTerritories} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressLiabilityTerritories), $"{Variables.CypressLiabilityTerritories} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressOtherTerritories), $"{Variables.CypressOtherTerritories} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressSinkholeTerritories), $"{Variables.CypressSinkholeTerritories} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressTerritories), $"{Variables.CypressTerritories} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressTheftTerritories), $"{Variables.CypressTheftTerritories} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressWaterTerritories), $"{Variables.CypressWaterTerritories} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressWaterTerritoriesV2), $"{Variables.CypressWaterTerritoriesV2} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressWindTerritories), $"{Variables.CypressWindTerritories} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.MonarchRegions), $"{Variables.MonarchRegions} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.MonarchTerritories), $"{Variables.MonarchTerritories} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.HeritageRegions), $"{Variables.HeritageRegions} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.HeritageTerritories), $"{Variables.HeritageTerritories} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.PMAFLMarketingZone), $"{Variables.PMAFLMarketingZone} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.PreferredMGATerritories), $"{Variables.PreferredMGATerritories} exists in By Variable list");
            });
        }

        [Order(8)]
        [Test]
        [CustomRetry]
        public void UserCheckBRVReportTerrirorySetsTest()
        {
            loginPage.LoginToApplication(user, "NBV87^yu");

            driver.Manage().Window.Size = new System.Drawing.Size(1024, 1024);

            OpenStory("HO3 Florida Market Basket 2020-01-01");

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.ByRatingVariableTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.ByRatingVariableMinimizedLocator.Click();
            }
            else defaultDatasetPage.ByRatingVariableTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            Utils.Scroll(new DashboardPage(driver).LogoLocator, driver);

            Assert.Multiple(() =>
            {
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.PixelRegion, PixelRegionBRV), $"Incorrect table result for {Variables.PixelRegion}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}, Expected: {PixelRegionBRV}");  
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.County, CountyBRV), $"Incorrect table result for {Variables.County}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.CitizensTerritories, CitizensTerritoriesBRV), $"Incorrect table result for {Variables.CitizensTerritories}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");

                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.CypressFireTerritories), $"Variable {Variables.CypressFireTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.CypressLiabilityTerritories), $"Variable {Variables.CypressLiabilityTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.CypressOtherTerritories), $"Variable {Variables.CypressOtherTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.CypressSinkholeTerritories), $"Variable {Variables.CypressSinkholeTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.CypressTerritories), $"Variable {Variables.CypressTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.CypressTheftTerritories), $"Variable {Variables.CypressTheftTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.CypressWaterTerritories), $"Variable {Variables.CypressWaterTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.CypressWaterTerritoriesV2), $"Variable {Variables.CypressWaterTerritoriesV2} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.CypressWindTerritories), $"Variable {Variables.CypressWindTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.MonarchRegions), $"Variable {Variables.MonarchRegions} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.HeritageRegions), $"Variable {Variables.HeritageRegions} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.HeritageTerritories), $"Variable {Variables.HeritageTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.MonarchTerritories), $"Variable {Variables.MonarchTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.PMAFLMarketingZone), $"Variable {Variables.PMAFLMarketingZone} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.PreferredMGATerritories), $"Variable {Variables.PreferredMGATerritories} exists");
            });
        }

        [Order(9)]
        [CustomRetry]
        [TestCase("DP3 Florida full")]
        public void UploadDatasetByUserTest(string dataset)
        {
            loginPage.LoginToApplication(user, "NBV87^yu");

            ImportDataset(dataset, PathToZipUser(dataset));

            var dataProcessingStatus = new DataProcessingStatusPage(driver);

            Utils.WaitUntilLoadingDisappears(driver);

            dataProcessingStatus.CheckRefreshRateToMinimum();

            //Assert.True(driver.Url.Equals("https://qa.millimanpixel.com/Integration/DataProcessingStatus"),
            //    "Data processing Page is not opened");

            Assert.Multiple(() =>
            {
                Assert.AreEqual(user, dataProcessingStatus.ClientUser[0].Text, "Incorrect User in User column");
                Assert.AreEqual("Add", dataProcessingStatus.UserRequestType[0].Text, "Incorrect Request type");
                Assert.AreEqual("Dataset", dataProcessingStatus.UserDataType[0].Text, "Incorrect Data type");
            });

            wait.Until(Extras.ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[5]"), "In queue"));

            wait.Until(Extras.ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[5]"), "In progress"));

            if (!dataProcessingStatus.UserStatus[0].Text.Equals("Completed successfully") | !dataProcessingStatus.UserStatus[0].Text.Equals("Failed"))
            {
                wait.Until(Extras.ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[5]"), "In queue"));

                wait.Until(Extras.ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[5]"), "In progress"));
            }

            Utils.WaitBeforeAssert(driver);

            Assert.AreEqual("Completed successfully", dataProcessingStatus.UserStatus[0].Text, $"Status is not Completed successfully for dataset {dataset}");
        }

        [Order(10)]
        [CustomRetry]
        [TestCase("DP3 Florida full")]
        public void UserCheckGeographyTypeImportedDatasetTest(string dataset)
        {
            loginPage.LoginToApplication(user, "NBV87^yu");

            SaveTheStory(DatasetUserDescription(dataset), "Cypress", "Heritage", false);

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            defaultDatasetPage.ExpandFilterLocator.WaitForElementPresentAndEnabled(driver, 100).Click();

            defaultDatasetPage.GeneralPanelLocator.WaitForElementPresentAndEnabled(driver);

            defaultDatasetPage.GeographyTabLocator.Click();

            defaultDatasetPage.GeographyTypeButtonLocator.ClickEx(driver);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(4, defaultDatasetPage.GeographyTypeDP3ListLocator.Count);
                Assert.AreEqual("Pixel Regions", defaultDatasetPage.GeographyTypeDP3ListLocator[1].GetAttribute("title"));
                Assert.AreEqual("Counties", defaultDatasetPage.GeographyTypeDP3ListLocator[2].GetAttribute("title"));
                Assert.AreEqual("Citizens Territories", defaultDatasetPage.GeographyTypeDP3ListLocator[3].GetAttribute("title"));
            });
        }

        [Order(11)]
        [TestCase("DP3 Florida full")]
        [CustomRetry]
        public void UserCheckCBVReportTerrirorySetsImportedDatasetTest(string dataset)
        {
            loginPage.LoginToApplication(user, "NBV87^yu");

            OpenStory(DatasetUserDescription(dataset));

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.CarrierByVariableTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.CarrierByVariableMinimizedLocator.Click();
            }
            else defaultDatasetPage.CarrierByVariableTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            Utils.Scroll(new DashboardPage(driver).LogoLocator, driver);

            Assert.Multiple(() =>
            {
                Assert.True(defaultDatasetPage.IsTableExist(Variables.PixelRegion, "Cypress", "Heritage", PixelRegionDP3CBV), $"Incorrect table result for Pixel Region. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.County, "Cypress", "Heritage", CountyDP3CBV), $"Incorrect table result for County. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist(Variables.CitizensTerritories, "Cypress", "Heritage", CitizensTerritoryDP3CBV), $"Incorrect table result for Citizens Territory. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.CypressTerritories), $"Variable {Variables.CypressTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.MonarchRegions), $"Variable {Variables.MonarchRegions} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.HeritageRegions), $"Variable {Variables.HeritageRegions} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.HeritageTerritories), $"Variable {Variables.HeritageTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.MonarchTerritories), $"Variable {Variables.MonarchTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.PMAFLMarketingZone), $"Variable {Variables.PMAFLMarketingZone} exists");
                Assert.True(!defaultDatasetPage.isVariableExistCBV(Variables.PreferredMGATerritories), $"Variable {Variables.PreferredMGATerritories} exists");
            });
        }

        [Order(12)]
        [TestCase("DP3 Florida full")]
        [CustomRetry]
        public void UserCheckSegmentationReportImportedDatasetTest(string dataset)
        {
            loginPage.LoginToApplication(user, "NBV87^yu");

            OpenStory(DatasetUserDescription(dataset));

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.SegmentationTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.SegmentationMinimizedLocator.Click();
            }
            else defaultDatasetPage.SegmentationTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            defaultDatasetPage.UserDefinedValuesCheckboxLocator.Click();

            Assert.Multiple(() =>
            {
                defaultDatasetPage.SegmentationVariableButtonLocator.WaitForElementPresentAndEnabled(driver).Click();

                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.PixelRegion), $"{Variables.PixelRegion} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.County), $"{Variables.County} doesn`t exist in Segmentation Variable(s) list");
                Assert.True(defaultDatasetPage.IsSegmentationVariableExist(Variables.CitizensTerritories), $"{Variables.CitizensTerritories} doesn`t exist in Segmentation Variable(s) list");

                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.CypressTerritories), $"{Variables.CypressTerritories} exists in Segmentation Variable(s) list");         
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.MonarchRegions), $"{Variables.MonarchRegions} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.MonarchTerritories), $"{Variables.MonarchTerritories} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.HeritageRegions), $"{Variables.HeritageRegions} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.HeritageTerritories), $"{Variables.HeritageTerritories} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.PMAFLMarketingZone), $"{Variables.PMAFLMarketingZone} exists in Segmentation Variable(s) list");
                Assert.True(!defaultDatasetPage.IsSegmentationVariableExist(Variables.PreferredMGATerritories), $"{Variables.PreferredMGATerritories} exists in Segmentation Variable(s) list");

                defaultDatasetPage.SegmentationVariableButtonLocator.Click();

                defaultDatasetPage.SegmentationByVariableButtonLocator.WaitForElementPresentAndEnabled(driver).Click();

                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.PixelRegion), $"{Variables.PixelRegion} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.County), $"{Variables.County} doesn`t exist in By Variable list");
                Assert.True(defaultDatasetPage.IsSegmentationByVariableExist(Variables.CitizensTerritories), $"{Variables.CitizensTerritories} doesn`t exist in By Variable list");
    
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.CypressTerritories), $"{Variables.CypressTerritories} exists in By Variable list");             
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.MonarchRegions), $"{Variables.MonarchRegions} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.MonarchTerritories), $"{Variables.MonarchTerritories} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.HeritageRegions), $"{Variables.HeritageRegions} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.HeritageTerritories), $"{Variables.HeritageTerritories} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.PMAFLMarketingZone), $"{Variables.PMAFLMarketingZone} exists in By Variable list");
                Assert.True(!defaultDatasetPage.IsSegmentationByVariableExist(Variables.PreferredMGATerritories), $"{Variables.PreferredMGATerritories} exists in By Variable list");
            });
        }

        [Order(13)]
        [TestCase("DP3 Florida full")]
        [CustomRetry]
        public void UserCheckBRVReportImportedDatasetTest(string dataset)
        {
            loginPage.LoginToApplication(user, "NBV87^yu");

            driver.Manage().Window.Size = new System.Drawing.Size(1024, 1024);

            OpenStory(DatasetUserDescription(dataset));

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.ByRatingVariableTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.ByRatingVariableMinimizedLocator.Click();
            }
            else defaultDatasetPage.ByRatingVariableTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            Utils.Scroll(new DashboardPage(driver).LogoLocator, driver);

            Assert.Multiple(() =>
            {
            
                Assert.True(defaultDatasetPage.IsBRVReportResultExist(Variables.CitizensTerritories, CitizensTerritoryDP3BRV), $"Incorrect table result for {Variables.CitizensTerritories}. Actual: {defaultDatasetPage.BRVReportResultLocator.Text}");

                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.CypressTerritories), $"Variable {Variables.CypressTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.MonarchRegions), $"Variable {Variables.MonarchRegions} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.HeritageRegions), $"Variable {Variables.HeritageRegions} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.HeritageTerritories), $"Variable {Variables.HeritageTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.MonarchTerritories), $"Variable {Variables.MonarchTerritories} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.PMAFLMarketingZone), $"Variable {Variables.PMAFLMarketingZone} exists");
                Assert.True(!defaultDatasetPage.isVariableExistBRV(Variables.PreferredMGATerritories), $"Variable {Variables.PreferredMGATerritories} exists");
            });
        }




        public void ImportDataset(string datasetDescription, string pathToZip)
        {
            var menu = new MenuPage(driver);

            menu.DataMenuLocator.ClickEx(driver);

            menu.DataserImportMenuLocator.Click();

            var datasetImportPage = new DatasetImportPage(driver);

            UploadDataset(pathToZip, driver);

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);

            datasetImportPage.DatasetDescriptiontextboxLocator.SendKeys($"{DatasetUserDescription(datasetDescription)}");

            Assert.True(datasetImportPage.UploadedFileTextLocator.Text.Contains($"{datasetDescription}"),
                "dataset is not selected");

            Utils.Scroll(datasetImportPage.FooterLocator, driver);

            datasetImportPage.StartButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);
        }

        public string PathToZipUser(string dataset)
        {
            return Path.Combine(path, "DDLdatasets", "ReportTesting", dataset);
        }

        public string DatasetUserDescription(string dataset)
        {
            return $"TerrSetTesting{randomString} {dataset}";
        }

        public void UploadDataset(string path, IWebDriver driver)
        {
            By uploadButton = By.Id("txtUploadFileDataset");
            driver.FindElement(uploadButton).SendKeys($"{path}.zip");
        }

        public void OpenStory(string dataset)
        {
            var dashboardPage = new DashboardPage(driver);

            dashboardPage.FilterDataset(dataset);

            Assert.True(dashboardPage.Description.Count > 0, $"Dataset {dataset} is not present");

            dashboardPage.OpenDataset(dataset, $"TerritorySets {randomString}");
        }

        public void SaveTheStory(string dataset, string primaryCarrier, string secondaryCarrier, bool isAdmin = true)
        {
            var dashboardPage = new DashboardPage(driver);

            var treeView = new UserStoryListTreeViewPage(driver);

            treeView.openTreeView();

            treeView.Folder[1].ClickEx(driver);

            Utils.WaitUntilLoadingPlaceholderDisappears(driver, secondtToWait: 20);

            treeView.FilterDataset(dataset);

            Assert.True(treeView.Description.Count > 0, $"Dataset {dataset} is not present");

            treeView.OpenDataset(dataset, "Default");

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            //Assert.True(driver.Url.Contains("https://qa.millimanpixel.com/Dashboard/StoryPage/"),
            //   "default page is not opened");

            defaultDatasetPage.ExpandFilterLocator.WaitForElementPresentAndEnabled(driver, 100).Click();

            defaultDatasetPage.GeneralPanelLocator.WaitForElementPresentAndEnabled(driver);

            Assert.True(defaultDatasetPage.GeneralPanelLocator.Displayed, "filter is not opened");

            defaultDatasetPage.SelectPrimaryCarrier(primaryCarrier);

            defaultDatasetPage.SelectSecondaryCarrier(secondaryCarrier);

            defaultDatasetPage.ScrollToUpdateResultsButton();

            defaultDatasetPage.UpdateResultsButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver, 100);

            defaultDatasetPage.ActionsButtonLocator.Click();

            defaultDatasetPage.SaveAsButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            new Actions(driver).SendKeys($"TerritorySets {randomString}").Perform();

            defaultDatasetPage.StoryDescriptionTextBoxLocator.WaitForElementPresentAndEnabled(driver).SendKeys($"TerritorySets {randomString}");

            if (isAdmin) defaultDatasetPage.SanFranciscoFolderSaveStoryLocator.Click();

            defaultDatasetPage.LuxoftFolderSaveStoryLocator.Click();
            
            defaultDatasetPage.SaveDashboardStoryButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver, 100);
        }

        [TearDown]
        public void EndTest()
        {
            driver.Quit();
        }

    }
}
