using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleManagement.Models;
using Dapper;
using System.Net.Http.Headers;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using System.Security.Claims;
using System.Security.Principal;
using System.Dynamic;
using System.Text;
using System.Reflection.Metadata.Ecma335;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
using Newtonsoft.Json;
//using System.IO.Pipelines;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication;
using VehicleManagement.Models.hireModels;
using QRCoder;
using System.Drawing;
using VehicleManagement.Models.Authorisation;

namespace VehicleManagement.Controllers
{
    public class HomeController : Controller
    {
        AuthorisedUsers authUsers = new AuthorisedUsers();
        private readonly ILogger<HomeController> _logger;

        public string username = "";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Load the index page
        public IActionResult Index()
        {
            return View();
        }

        // Load the success page
        public IActionResult Success()
        {
            return View();
        }

        // Load the generate PO Code page
        public IActionResult GeneratePOCode()
        {
            return View();
        }

        // Load the add hire vehicle page
        public IActionResult AddHireVehicle()
        {
            return View();
        }

        // Load the hire form page and await user request type
        public IActionResult LoadHireForm(int id)
        {
            // load pages based on the request type
            string viewToLoad;
            if (id == 1)
            {
                viewToLoad = "~/Views/Home/PartialViews/_AddHireVehicleForPerson.cshtml";
            }
            else
            {
                viewToLoad = "~/Views/Home/PartialViews/_AddHireVehicleForDepot.cshtml";
            }

            return PartialView(viewToLoad);
        }

        // Add hire vehicle form processor
        [HttpPost]
        public IActionResult AddHireVehicle(AddHireVehicleModel hireVehicle, string user, int requestId)
        {
            // get the current date in SQL Server format
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            // Create a new hire vehicle record in SQL Server

            if (requestId == 1) // 
            {
                var insertSql = @"INSERT INTO loadingApp.dbo.vehiclesHire (oakPONumber,hiredFor,vehicleRegistration,make,model,hireCompany,hireProvider,hiredFrom,reasonForHire,dateAddedToMid,payloadCapacity,vehicleReplacing,isDiscontinued) VALUES
                             ('" + hireVehicle.OakPONumber +
                             "','" + hireVehicle.HiredFor +
                             "','" + hireVehicle.VehicleRegistration +
                             "','" + hireVehicle.Make.Trim() +
                             "','" + hireVehicle.Model.Trim() +
                             "','" + hireVehicle.HireCompany +
                             "','" + hireVehicle.HireProvider +
                             "','" + hireVehicle.HiredFrom.ToString("yyyy-MM-dd HH:mm:ss") +
                             "','" + hireVehicle.ReasonForHire +
                             "','" + hireVehicle.DateAddedToMid.ToString("yyyy-MM-dd HH:mm:ss") +
                             "'," + hireVehicle.PayLoadCapacity +
                             ",'" + hireVehicle.VehicleReplacing +
                             "',0)";

                // check if vehicle registration already exists
                var checkDb = @"SELECT vehicleRegistration FROM loadingApp.dbo.vehiclesHire WHERE vehicleRegistration = '" + hireVehicle.VehicleRegistration + "'";
                // record the insert action to the vehicle history log
                var addHistory = @"INSERT INTO loadingApp.dbo.vehicleHistory (vehicleRegistration, actionReason, actionDate, additionalComments, actionUser) 
                                VALUES ('" + hireVehicle.VehicleRegistration + "',14,'" + currentDate + "','" + "No Comments" + "','" + user + "')";
                var checkResult = SQLDataAccess.LoadData<string>(checkDb);
                // throw response to view 
                if (checkResult.Count > 0)
                {
                    TempData["notice"] = hireVehicle.VehicleRegistration + " already exists";
                }
                else
                {
                    TempData["notice"] = hireVehicle.VehicleRegistration + " saved successfully!";
                    SQLDataAccess.SaveData(insertSql, hireVehicle);
                    SQLDataAccess.SaveData(addHistory, hireVehicle);
                }

                return PartialView("~/Views/Home/PartialViews/_Success.cshtml");
            }
            else
            {

                var insertSql = @"INSERT INTO loadingApp.dbo.vehiclesHire (oakPONumber,hiredFor,vehicleRegistration,make,model,hireCompany,hireProvider,hiredFrom,reasonForHire,dateAddedToMid,payloadCapacity,vehicleReplacing,isDiscontinued) VALUES
                             ('" + hireVehicle.OakPONumber +
                                 "','" + hireVehicle.HiredFor +
                                 "','" + hireVehicle.VehicleRegistration +
                                 "','" + hireVehicle.Make.Trim() +
                                 "','" + hireVehicle.Model.Trim() +
                                 "','" + hireVehicle.HireCompany +
                                 "','" + hireVehicle.HireProvider +
                                 "','" + hireVehicle.HiredFrom.ToString("yyyy-MM-dd HH:mm:ss") +
                                 "','" + hireVehicle.ReasonForHire +
                                 "','" + hireVehicle.DateAddedToMid.ToString("yyyy-MM-dd HH:mm:ss") +
                                 "'," + hireVehicle.PayLoadCapacity +
                                 ",'" + hireVehicle.VehicleReplacing +
                                 "',0)";

            

                var checkDb = @"SELECT vehicleRegistration FROM loadingApp.dbo.vehiclesHire WHERE vehicleRegistration = '" + hireVehicle.VehicleRegistration + "'";
                var addHistory = @"INSERT INTO loadingApp.dbo.vehicleHistory (vehicleRegistration, actionReason, actionDate, additionalComments, actionUser) 
                                VALUES ('" + hireVehicle.VehicleRegistration + "',14,'" + currentDate + "','" + "No Comments" + "','" + user + "')";
                var checkResult = SQLDataAccess.LoadData<string>(checkDb);
                if (checkResult.Count > 0)
                {
                    TempData["notice"] = hireVehicle.VehicleRegistration + " already exists";
                }
                else
                {
                    TempData["notice"] = hireVehicle.VehicleRegistration + " saved successfully!";
                    SQLDataAccess.SaveData(insertSql, hireVehicle);
                    SQLDataAccess.SaveData(addHistory, hireVehicle);
                }

                return PartialView("~/Views/Home/PartialViews/_Success.cshtml");
            }
        }

        // fetch the last row added to the hire vehicles table
        public int GetLastHireId()
        {
            var getId = "SELECT MAX(Id) as lastId FROM loadingApp.dbo.vehiclesHire";
            return SQLDataAccess.LoadData<int>(getId).First();
        }

        // load the privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        // load the error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // this is the home page (replacing index.cshtml)
        public IActionResult Vehicles()
        {
            System.Diagnostics.Debug.WriteLine(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            var logSession = @"INSERT INTO vmsSessionLog SELECT TOP 1 '" + System.Security.Principal.WindowsIdentity.GetCurrent().Name + "', GETDATE()";
            SQLDataAccess.SaveData(logSession,"");
            //System.Diagnostics.Debug.WriteLine(User.IsInRole(""));

            return View();
        }

        // load the add vehicle page
        public IActionResult AddVehicle()
        {
            return View();
        }

        // load the add make/model page
        public IActionResult AddMakeModel()
        {
            return View();
        }

        // Add make/model form processor
        [HttpPost]
        public IActionResult AddMakeModel(AddMakeModelClass addMakeModel)
        {
            var insertSql = @"INSERT INTO loadingApp.dbo.vehicleMake (name,model) VALUES ('" + addMakeModel.Make + "','" + addMakeModel.Model + "')";
            var checkDbQuery = @"SELECT name, model FROM loadingApp.dbo.vehicleMake WHERE name = '" + addMakeModel.Make + "' AND model = '" + addMakeModel.Model + "'";

            // check that the current make and model doesn't already exist
            var returnData = SQLDataAccess.LoadData<AddMakeModelClass>(checkDbQuery);
            if (returnData.Count > 0)
            {
                TempData["error"] = "A record for a " + addMakeModel.Make + " " + addMakeModel.Model + " already exists.";
            }
            else
            {
                TempData["saved"] = "Record added successfully!";
                SQLDataAccess.SaveData(insertSql, addMakeModel);
            }
            return View();
        }

        // load the edit vehicle page
        public IActionResult EditVehicle()
        {
            return View();
        }

        // load the discontinue vehicle page
        public IActionResult DiscontinueVehicle()
        {
            return View();
        }

        // load the edit vehicles page, loading the vehicle details requested by the user
        public IActionResult EditVehicles(int Id)
        {
            var returnVehicles = GetVehiclesForEdit(Id);
            return PartialView("~/Views/Home/PartialViews/_EditVehicles.cshtml", returnVehicles);
        }

        // load the discontinue vehicles page, loading the vehicle details requested by the user
        public IActionResult VehiclesToDiscontinue(int Id)
        {
            var returnVehicles = GetVehicles(Id);
            return PartialView("~/Views/Home/PartialViews/_DiscontinueVehicle.cshtml", returnVehicles);
        }

        // load the upload documents page, passing in the vehicle selected by the user
        public IActionResult UploadDocs(int Id)
        {
            var returnVehicles = GetAllVehicles(Id);
            return PartialView("~/Views/Home/PartialViews/_UploadDocuments.cshtml", returnVehicles);
        }

        // load the PO number page
        public IActionResult LoadPONumber(string Id)
        {
            // fetch the last created PO number
            var sql = @"SELECT MAX(Id) AS Id FROM [loadingApp].[dbo].[hirePONumbers]";
            var poId = SQLDataAccess.LoadData<string>(sql);

            PurchaseOrderNumber purchaseOrderNumber = new PurchaseOrderNumber()
            {
                PONumber = Id + "/" + DateTime.Now.Year.ToString().Substring(2) + "/" + poId.First().ToString()
            };

            // Save the new PO number to the database
            var logPONumber = @"INSERT INTO [loadingApp].[dbo].[hirePONumbers] VALUES ('" + purchaseOrderNumber.PONumber + "')";
            SQLDataAccess.SaveData(logPONumber, purchaseOrderNumber);
            return PartialView("~/Views/Home/PartialViews/_LoadPurchaseOrderNumber.cshtml", purchaseOrderNumber);
        }

        // load the Upload Documents page
        public IActionResult UploadDocuments()
        {
            return View();
        }

        // load all active vehicles to the edit page
        public List<EditVehicle> GetVehiclesForEdit(int Id)
        {
            var dynamicWhere = "";
            if (Id != 0)
            {
                dynamicWhere = " AND Depot = " + Id;
            }
            string sql = @"SELECT [Id]
                              ,[Depot]
							  ,CASE WHEN depot = 1 THEN 'Haydock' WHEN depot = 3 THEN 'Leeds' WHEN depot = 7 THEN 'Trafford' ELSE 'Tyne & Wear' END AS DepotName
                              ,[vanRegistration]
                              ,[make]
                              ,[model]
                              ,[warrantyMileage]
                              ,[startDate]
                              ,[endDate]
                              ,[mileageDate]
                              ,[currentMileage]
                              ,[taxDue]
                              ,[motDue]
                              ,[inWarranty]
                              ,[serviceInterval]
                              ,[driver]
                              ,[livery]
                              ,[tyreSize]
                              ,[camera]
                              ,[masternaught]
                          FROM [loadingApp].[dbo].[vehicles]
                          WHERE isDiscontinued = 0"
                        + dynamicWhere + 
                         " ORDER BY vanRegistration";
            return SQLDataAccess.LoadData<EditVehicle>(sql);
        }

        // load all active hire vehicles to the edit page 
        public IActionResult GetHireVehiclesForEdit()
        {
            string sql = @"SELECT [Id]
                              ,[oakPONumber]
                              ,[hiredFor]
                              ,[vehicleRegistration]
                              ,[make]
                              ,[model]
                              ,[hireCompany]
                              ,[hireProvider]
                              ,[hiredFrom]
                              ,[hiredTo]
                              ,[reasonForHire]
                              ,[dateAddedToMid]
                              ,[dateRemovedFromMid]
                              ,[payloadCapacity]
                              ,[vehicleReplacing]
                          FROM [loadingApp].[dbo].[vehiclesHire]
                          WHERE isDiscontinued = 0";
            var hireVehicles = SQLDataAccess.LoadData<EditHireVehicle>(sql);
            return View(hireVehicles);
        }

        // fetch the specific details of a hire vehicle using the id
        public IActionResult LoadHireVehiclesForEdit(int id)
        {
            string sql = @"SELECT [Id]
                              ,[oakPONumber]
                              ,[hiredFor]
                              ,[vehicleRegistration]
                              ,[make]
                              ,[model]
                              ,[hireCompany]
                              ,[hireProvider]
                              ,[hiredFrom]
                              ,[hiredTo]
                              ,[reasonForHire]
                              ,[dateAddedToMid]
                              ,[dateRemovedFromMid]
                              ,[payloadCapacity]
                              ,[vehicleReplacing]
                          FROM [loadingApp].[dbo].[vehiclesHire]
                          WHERE isDiscontinued = 0
                          AND Id =" + id;
            var hireVehicles = SQLDataAccess.LoadData<EditHireVehicle>(sql).First();
            return View("~/Views/Home/EditIndividualHireVehicle.cshtml", hireVehicles);
        }

        // load the list of hire vehicles for the discontinue list
        public IActionResult DiscontinueHireVehicle()
        {
            string sql = @"SELECT [Id]
                              ,[oakPONumber]
                              ,[hiredFor]
                              ,[vehicleRegistration]
                              ,[make]
                              ,[model]
                              ,[hireCompany]
                              ,[hireProvider]
                              ,[hiredFrom]
                              ,[hiredTo]
                              ,[reasonForHire]
                              ,[dateAddedToMid]
                              ,[dateRemovedFromMid]
                              ,[payloadCapacity]
                              ,[vehicleReplacing]
                          FROM [loadingApp].[dbo].[vehiclesHire]
                          WHERE isDiscontinued = 0";
            var hireVehicles = SQLDataAccess.LoadData<EditHireVehicle>(sql);
            return View(hireVehicles);
        }

        // process the edit hire vehicle form
        [HttpPost]
        public IActionResult EditHireVehicle(EditHireVehicle editHireVehicle, string user)
        {
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            var midOverride = editHireVehicle.DateAddedToMid.ToString();
            var removedMidOverride = editHireVehicle.DateRemovedFromMid.ToString();
            var hiredTo = editHireVehicle.HiredTo.ToString();
            if (midOverride == "01/01/0001 00:00:00")
            {
                midOverride = "";
            }
            else
            {
                midOverride = editHireVehicle.DateAddedToMid.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (removedMidOverride == "01/01/0001 00:00:00")
            {
                removedMidOverride = "";
            }
            else
            {
                removedMidOverride = editHireVehicle.DateRemovedFromMid.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (hiredTo == "01/01/0001 00:00:00")
            {
                hiredTo = "";
            }
            else
            {
                hiredTo = editHireVehicle.HiredTo.ToString("yyyy-MM-dd HH:mm:ss");
            }
            string updateSql = @"UPDATE [loadingApp].[dbo].[vehiclesHire]
                                 SET oakPONumber = '" + editHireVehicle.OakPONumber + "'," +
                                 "hiredFor = '" + editHireVehicle.HiredFor + "'," +
                                 "make = '" + editHireVehicle.Make + "'," +
                                 "model = '" + editHireVehicle.Model + "'," +
                                 "hireCompany = '" + editHireVehicle.HireCompany + "'," +
                                 "hireProvider = '" + editHireVehicle.HireProvider + "'," +
                                 "hiredFrom = '" + editHireVehicle.HiredFrom.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "'," +
                                 "hiredTo = '" + hiredTo + "'," +
                                 "reasonForHire = '" + editHireVehicle.ReasonForHire + "'," +
                                 "dateAddedToMid = '" + midOverride + "'," +
                                 "dateRemovedFromMid = '" + removedMidOverride + "'," +
                                 "payLoadCapacity = " + editHireVehicle.PayloadCapacity + "," +
                                 "vehicleReplacing = '" + editHireVehicle.VehicleReplacing + "' WHERE vehicleRegistration = '" + editHireVehicle.VehicleRegistration + "'";
            var addHistory = @"INSERT INTO loadingApp.dbo.vehicleHistory (vehicleRegistration, actionReason, actionDate, additionalComments, actionUser) 
                                VALUES ('" + editHireVehicle.VehicleRegistration + "',15,'" + currentDate + "','" + "No Comments" + "','" + user + "')";
            SQLDataAccess.SaveData(updateSql, editHireVehicle);
            TempData["saved"] = editHireVehicle.VehicleRegistration + " updated successfully!"; // render validation message to the page
            return RedirectToAction("GetHireVehiclesForEdit");
        }

        // Return a list of active vehicles in the depot selected
        public List<DiscontinueVehicleModel> GetVehicles(int Id)
        {
            string sql = @"SELECT [Id]
                              ,[Depot]
							  ,CASE WHEN depot = 1 THEN 'Haydock' WHEN depot = 3 THEN 'Leeds' WHEN depot = 7 THEN 'Trafford' ELSE 'Tyne & Wear' END AS DepotName
                              ,[vanRegistration]
                              ,[make]
                              ,[model]
                              ,[warrantyMileage]
                              ,[startDate]
                              ,[endDate]
                              ,[mileageDate]
                              ,[currentMileage]
                              ,[taxDue]
                              ,[motDue]
                              ,CASE WHEN [inWarranty] = 1 THEN 'Yes' ELSE 'No' END AS [inWarranty]
                              ,[serviceInterval]
                              ,[driver]
                              ,[livery]
                              ,[tyreSize]
                              ,[camera]
                              ,[masternaught]
                          FROM [loadingApp].[dbo].[vehicles]
                          WHERE Depot = " + Id + " AND isDiscontinued = 0 ORDER BY vanRegistration";
            return SQLDataAccess.LoadData<DiscontinueVehicleModel>(sql);
        }

        // Reutrn a list of all vehicles in the depot selected
        public List<DiscontinueVehicleModel> GetAllVehicles(int Id)
        {
            string sql = @"SELECT [Id]
                              ,[Depot]
							  ,CASE WHEN depot = 1 THEN 'Haydock' WHEN depot = 3 THEN 'Leeds' WHEN depot = 7 THEN 'Trafford' ELSE 'Tyne & Wear' END AS DepotName
                              ,[vanRegistration]
                              ,[make]
                              ,[model]
                              ,[warrantyMileage]
                              ,[startDate]
                              ,[endDate]
                              ,[mileageDate]
                              ,[currentMileage]
                              ,[taxDue]
                              ,[motDue]
                              ,CASE WHEN [inWarranty] = 1 THEN 'Yes' ELSE 'No' END AS [inWarranty]
                              ,[serviceInterval]
                              ,[driver]
                              ,[livery]
                              ,[tyreSize]
                              ,[camera]
                              ,[masternaught]
                          FROM [loadingApp].[dbo].[vehicles]
                          WHERE Depot = " + Id + " ORDER BY vanRegistration";
            return SQLDataAccess.LoadData<DiscontinueVehicleModel>(sql);
        }

        // load the data for the selected vehicle
        public IActionResult LoadVehicleForEdit(int Id)
        {
            string sql = @"SELECT [Id]
                              ,[Depot]
                              ,[vanRegistration]
                              ,[make]
                              ,[model]
                              ,[warrantyMileage]
                              ,[startDate]
                              ,[endDate]
                              ,[mileageDate]
                              ,[currentMileage]
                              ,[taxDue]
                              ,[motDue]
                              ,[inWarranty]
                              ,[serviceInterval]
                              ,[driver]
                              ,[livery]
                              ,[tyreSize]
                              ,[camera]
                              ,[masternaught]
                              ,[payloadCapacity]
                          FROM [loadingApp].[dbo].[vehicles]
                          WHERE Id = " + Id +
                          " AND isDiscontinued = 0";
            var getVehicle = SQLDataAccess.LoadData<EditVehicle>(sql).First();
            return View("~/Views/Home/EditIndividualVehicle.cshtml", getVehicle);
        }

        // Show a list of any inactive vehicle that has not been sold
        public IActionResult ReinstateVehicle()
        {
            string sql = @"SELECT
                                depot,
	                            vanRegistration,
	                            make,
	                            model,
	                            currentMileage,
	                            warrantyMileage,
	                            startDate,
	                            endDate,
	                            mileageDate as mileageLastChecked,
	                            [lastAction],
	                            [lastActionDate]
                            FROM loadingApp.dbo.vehicles
                            LEFT JOIN (
	                            SELECT
		                            vehicleHistory.vehicleRegistration,
		                            vehicleActions.Reason AS [lastAction],
		                            vehicleHistory.actionDate AS [lastActionDate]
	                            FROM vehicleHistory
	                            INNER JOIN vehicleActions ON vehicleActions.Id = vehicleHistory.actionReason
	                            INNER JOIN (SELECT MAX(Id) as link, vehicleRegistration FROM vehicleHistory GROUP BY vehicleRegistration) linker ON linker.link = vehicleHistory.Id
                            ) lastActions ON lastActions.vehicleRegistration = vehicles.vanRegistration
                            WHERE isDiscontinued = 1
                            AND lastAction <> 'SOLD'";
            var getVehicle = SQLDataAccess.LoadData<ReinstateVehicleModel>(sql);
            return View(getVehicle);
        }

        // Update the database with updated vehicle information
        [HttpPost]
        public IActionResult EditIndividualVehicle(EditVehicle vehicle, string user)
        {
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string sql = @"UPDATE loadingApp.dbo.vehicles
                           SET Depot = " + vehicle.Depot +
                           ", make = '" + vehicle.Make +
                           "', model = '" + vehicle.Model +
                           "', warrantyMileage = " + vehicle.WarrantyMileage +
                           ", startDate = '" + vehicle.StartDate.ToString("yyyy-MM-dd") +
                           "', endDate = '" + vehicle.EndDate.ToString("yyyy-MM-dd") +
                           "', mileageDate = '" + vehicle.MileageDate.ToString("yyyy-MM-dd") +
                           "', currentMileage = " + vehicle.CurrentMileage +
                           ", taxDue = '" + vehicle.TaxDue.ToString("yyyy-MM-dd") +
                           "', motDue = '" + vehicle.MotDue.ToString("yyyy-MM-dd") +
                           "', inWarranty = '" + vehicle.InWarranty +
                           "', serviceInterval = " + vehicle.ServiceInterval +
                           ", driver = '" + vehicle.Driver +
                           "', livery = '" + vehicle.Livery +
                           "', tyreSize = '" + vehicle.TyreSize +
                           "', camera = '" + vehicle.Camera +
                           "', masternaught = '" + vehicle.Masternaught +
                           "', payloadCapacity = " + vehicle.PayLoadCapacity +
                           " WHERE vanRegistration = '" + vehicle.VanRegistration + "'";

            // Add history log
            var addHistory = @"INSERT INTO loadingApp.dbo.vehicleHistory (vehicleRegistration, actionReason, actionDate, additionalComments, actionUser) 
                                VALUES ('" + vehicle.VanRegistration + "',15,'" + currentDate + "','" + "No Comments" + "','" + user + "')";
            SQLDataAccess.SaveData(sql, vehicle);
            SQLDataAccess.SaveData(addHistory, vehicle);
            TempData["notice"] = vehicle.VanRegistration + " updated successfully!";
            return RedirectToAction("Success");
        }

        // Process the new vehicle form
        [HttpPost]
        public IActionResult AddVehicle(AddVehicleModel vehicle, string user)
        {
            var sD = vehicle.StartDate.ToString("yyyy-MM-dd");
            var eD = vehicle.EndDate.ToString("yyyy-MM-dd");
            var mD = vehicle.MileageDate.ToString("yyyy-MM-dd");
            var taxD = vehicle.TaxDue.ToString("yyyy-MM-dd");
            var motD = vehicle.MotDue.ToString("yyyy-MM-dd");
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd");


            string sql = @"INSERT INTO [loadingApp].[dbo].[vehicles] ([Depot]
                                          ,[vanRegistration]
                                          ,[make]
                                          ,[model]
                                          ,[warrantyMileage]
                                          ,[startDate]
                                          ,[endDate]
                                          ,[mileageDate]
                                          ,[currentMileage]
                                          ,[taxDue]
                                          ,[motDue]
                                          ,[inWarranty]
                                          ,[serviceInterval]
                                          ,[driver]
                                          ,[livery]
                                          ,[tyreSize]
                                          ,[camera]
                                          ,[masternaught]
                                          ,[isDiscontinued]
                                          ,[payloadCapacity])
                          VALUES ('" + vehicle.Depot + "','" + vehicle.VehicleRegistration + "','" + vehicle.Make.Trim() + "','" + vehicle.Model.Trim() + "'," + vehicle.WarrantyMileage + ",'" +
                                       sD
                                       + "','" + eD
                                       + "','" + mD
                                       + "'," + vehicle.CurrentMileage + ",'" +
                                       taxD
                                       + "','" + motD
                                       + "'," + vehicle.InWarranty + "," + vehicle.ServiceInterval + ",'" +
                                       vehicle.Driver + "','" + vehicle.Livery + "','" + vehicle.TyreSize + "'," + vehicle.Camera + ",'" + vehicle.Masternaught + "',0," + vehicle.PayLoadCapacity + ")";

            // Check the vehicle doesn't exist
            var checkDb = @"SELECT vanRegistration vanRegistration FROM loadingApp.dbo.vehicles WHERE vanRegistration = '" + vehicle.VehicleRegistration + "'";
            // Add history log
            var addHistory = @"INSERT INTO loadingApp.dbo.vehicleHistory (vehicleRegistration, actionReason, actionDate, additionalComments, actionUser) 
                                VALUES ('" + vehicle.VehicleRegistration + "',14,'" + currentDate + "','" + "No Comments" + "','" + user + "')";
            var checkResult = SQLDataAccess.LoadData<string>(checkDb);
            if (checkResult.Count > 0)
            {
                TempData["notice"] = vehicle.VehicleRegistration + " already exists";
            }
            else
            {
                TempData["notice"] = vehicle.VehicleRegistration + " saved successfully!";
                SQLDataAccess.SaveData(sql, vehicle);
                SQLDataAccess.SaveData(addHistory, vehicle);
            }

            return PartialView("~/Views/Home/PartialViews/_Success.cshtml");
        }

        // fetch the number of records with the given vehicle registration
        [HttpPost]
        public string CheckDatabase(string data)
        {
            string sql = @"SELECT SUM(1) AS records FROM [loadingApp].[dbo].[vehicles] WHERE vanRegistration = '" + data + "'";
            var checkData = SQLDataAccess.LoadData<List<string>>(sql);
            if (checkData.Count > 0)
            {
                return "This registration already exists";
            }
            else
            {
                return "Ok";
            }
        }

        // load a list of vehicles with approaching tax due dates
        public IActionResult UpcomingTax()
        {
            string sql = @"SELECT
	                        depot,
	                        CASE WHEN depot = 1 THEN 'Haydock'
		                         WHEN depot = 3 THEN 'Leeds'
		                         WHEN depot = 7 THEN 'Trafford'
		                         WHEN depot = 9 THEN 'Tyne & Wear'
	                        END AS depotName,
	                        vanRegistration,
	                        make,
	                        model,
	                        taxDue AS Due,
	                        CASE WHEN DATEDIFF(D,GETDATE(),taxDue) IN (5,6) THEN 'Less than 1 week'
	                        WHEN DATEDIFF(D,GETDATE(),taxDue) IN (1,2,3,4) THEN 'Due in ' + CAST(DATEDIFF(D,GETDATE(),taxDue) AS VARCHAR) + ' days'
	                        WHEN DATEDIFF(D,GETDATE(),taxDue) = 7 THEN 'Less than 1 week'
	                        WHEN DATEDIFF(WK,GETDATE(),taxDue) = 2 THEN 'Due in 2 weeks'
	                        WHEN DATEDIFF(Wk,GETDATE(),taxDue) = 3 THEN 'Due in 3 weeks'
	                        WHEN DATEDIFF(WK,GETDATE(),taxDue) = 4 THEN 'Due next month'
	                        WHEN DATEDIFF(WK,GETDATE(),taxDue) IN (5,6,7,8) THEN 'Due in about 2 months'
	                        WHEN DATEDIFF(WK,GETDATE(),taxDue) IN (9,10,11,12,13,14) THEN 'Due in about 3 months'
	                        ELSE 'Overdue by ' + CAST(DATEDIFF(D,GETDATE(),taxDue)*-1 AS VARCHAR) + ' days'
	                        END AS dueText
                        FROM vehicles
                        WHERE 
	                        DATEDIFF(WK,GETDATE(),taxDue) <= 4
                            AND isDiscontinued = 0
                        ORDER BY depot, taxDue";
            var upcomingServices = SQLDataAccess.LoadData<UpcomingEventsModel>(sql);
            return View(upcomingServices);
        }

        // load a list of vehicles with approaching MOT due dates
        public IActionResult UpcomingMOT()
        {
            string sql = @"SELECT
	                        depot,
	                        CASE WHEN depot = 1 THEN 'Haydock'
		                         WHEN depot = 3 THEN 'Leeds'
		                         WHEN depot = 7 THEN 'Trafford'
		                         WHEN depot = 9 THEN 'Tyne & Wear'
	                        END AS depotName,
	                        vanRegistration,
	                        make,
	                        model,
	                        motDue AS Due,
	                        CASE WHEN DATEDIFF(D,GETDATE(),motDue) IN (5,6) THEN 'Less than 1 week'
	                        WHEN DATEDIFF(D,GETDATE(),motDue) IN (1,2,3,4) THEN 'Due in ' + CAST(DATEDIFF(D,GETDATE(),motDue) AS VARCHAR) + ' days'
	                        WHEN DATEDIFF(D,GETDATE(),motDue) IN (7,9,10,11,12) THEN 'Less than 1 week'
	                        WHEN DATEDIFF(WK,GETDATE(),motDue) = 2 THEN 'Due in 2 weeks'
	                        WHEN DATEDIFF(Wk,GETDATE(),motDue) = 3 THEN 'Due in 3 weeks'
	                        WHEN DATEDIFF(WK,GETDATE(),motDue) = 4 THEN 'Due next month'
	                        WHEN DATEDIFF(WK,GETDATE(),motDue) IN (5,6,7,8) THEN 'Due in about 2 months'
	                        WHEN DATEDIFF(WK,GETDATE(),motDue) IN (9,10,11,12,13,14) THEN 'Due in about 3 months'
	                        ELSE 'Overdue by ' + CAST(DATEDIFF(D,GETDATE(),motDue)*-1 AS VARCHAR) + ' days'
	                        END AS dueText
                        FROM vehicles
                        WHERE 
	                        DATEDIFF(WK,GETDATE(),motDue) <= 4
                            AND isDiscontinued = 0
                        ORDER BY depot, motDue";
            var upcomingServices = SQLDataAccess.LoadData<UpcomingEventsModel>(sql);
            return View(upcomingServices);
        }

        // load of a list of vehicles where the miles until next service is approaching
        public IActionResult UpcomingServices()
        {
            string sql = @"SELECT
	                        depot,
	                        CASE WHEN depot = 1 THEN 'Haydock'
		                         WHEN depot = 3 THEN 'Leeds'
		                         WHEN depot = 7 THEN 'Trafford'
		                         WHEN depot = 9 THEN 'Tyne & Wear'
	                        END AS depotName,
	                        vanRegistration,
	                        make,
	                        model,
	                        CAST(milesToService AS VARCHAR) AS Due,
	                        CASE WHEN milesToService <= 0 THEN 'Service overdue by ' + CAST(milesToService * -1 AS VARCHAR) + ' miles'
							ELSE 'Service due in ' + CAST(milesToService AS VARCHAR) + ' miles'
							END AS dueText
                        FROM vehicles
                        WHERE 
	                        milesToService <= 1000
                            AND isDiscontinued = 0
                        ORDER BY depot, milesToService";
            var upcomingServices = SQLDataAccess.LoadData<UpcomingEventsModel>(sql);
            return View(upcomingServices);
        }

        // load a list of vehicles where the warranty is nearly expired
        public IActionResult UpcomingWarranty()
        {
            string sql = @"SELECT
	                        depot,
	                        CASE WHEN depot = 1 THEN 'Haydock'
		                         WHEN depot = 3 THEN 'Leeds'
		                         WHEN depot = 7 THEN 'Trafford'
		                         WHEN depot = 9 THEN 'Tyne & Wear'
	                        END AS depotName,
	                        vanRegistration,
							currentMileage,
							endDate,
	                        make,
	                        model,
                            CASE WHEN inWarranty = 1 THEN 'In warranty' ELSE 'Out of warranty' END AS Discontinued
                        FROM vehicles
                        WHERE (inWarranty = 0 
						OR (make = 'man' AND DATEDIFF(WK,GETDATE(),endDate) <=12)
						OR (make = 'renault' AND currentMileage BETWEEN 140000 AND 149999))
                        AND isDiscontinued = 0
                        ORDER BY depot, endDate";
            var upcomingServices = SQLDataAccess.LoadData<UpcomingWarrantyModel>(sql);
            return View(upcomingServices);
        }

        // create a set of dashboard metrics and return to page as numerical values
        public IActionResult Dashboard()
        {
            string sql = @"SELECT
	                            SUM(CASE WHEN DATEDIFF(WK,GETDATE(),taxDue) <= 4 AND isDiscontinued = 0 THEN 1 ELSE 0 END) AS TaxDue
	                            ,SUM(CASE WHEN DATEDIFF(WK,GETDATE(),motDue) <= 4 AND isDiscontinued = 0 THEN 1 ELSE 0 END) AS MOTDue
	                            ,SUM(CASE WHEN DATEDIFF(M,GETDATE(),motDue) < 0 AND isDiscontinued = 0 THEN 1 ELSE 0 END) AS MOTOverdue
	                            ,SUM(CASE WHEN DATEDIFF(M,GETDATE(),taxDue) < 0 AND isDiscontinued = 0 THEN 1 ELSE 0 END) AS TaxOverdue
								,SUM(CASE WHEN make = 'Renault' AND (currentMileage >= 140000 OR ((DATEDIFF(WK, GETDATE(), endDate) <=12) AND make = 'MAN')) AND isDiscontinued = 0 AND inWarranty = 1 THEN 1 ELSE 0 END) AS OutOfWarranty
	                            ,SUM(CASE WHEN milesToService <= 1000 AND milesToService > 0 AND isDiscontinued = 0 THEN 1 ELSE 0 END) AS ServicesDue
                                ,SUM(CASE WHEN milesToService <= 0 AND isDiscontinued = 0 THEN 1 ELSE 0 END) AS ServicesOverdue
								,SUM(CASE WHEN isDiscontinued = 0 THEN 1 ELSE 0 END) AS ActiveVehicles
								,SUM(CASE WHEN isDiscontinued = 1 THEN 1 ELSE 0 END) AS InactiveVehicles
								,MAX(soldThisMonth) AS soldThisMonth
								,MAX(sornedThisMonth) AS sornedThisMonth
								,MAX(writeOffThisMonth) AS writeOffThisMonth
								,MAX(CASE WHEN isDiscontinued = 1 THEN outForRepair ELSE 0 END) AS outForRepair
								,MAX(totalOnHire) as totalOnHire

								,MAX(inspections.[Haydock Checks]) as haydockChecks
								,MAX(inspections.[Leeds Checks]) as leedsChecks
								,MAX(inspections.[Traff Checks]) as traffordChecks
								,MAX(inspections.[Tyne Checks]) as tyneChecks
								,MAX(inspections.[Haydock Not Checked]) as haydockNotChecked
								,MAX(inspections.[Leeds Not Checked]) as leedsNotChecked
								,MAX(inspections.[Traff Not Checked]) as traffordNotChecked
								,MAX(inspections.[Tyne Not Checked]) as tyneNotChecked
								,MAX(inspections.[Haydock Checks] + inspections.[Leeds Checks] + inspections.[Traff Checks] + inspections.[Tyne Checks]) as allChecks
								,MAX(inspections.[Haydock Not Checked] + inspections.[Leeds Not Checked] + inspections.[Traff Not Checked] + inspections.[Tyne Not Checked]) as allNoneChecks
								,MAX(inspections.[Haydock Checks] + inspections.[Leeds Checks] + inspections.[Traff Checks] + inspections.[Tyne Checks]) / SUM(CASE WHEN isDiscontinued = 0 THEN 1.0 ELSE 0.0 END) as percentChecked
								,MAX(inspections.[Haydock Not Checked] + inspections.[Leeds Not Checked] + inspections.[Traff Not Checked] + inspections.[Tyne Not Checked]) / SUM(CASE WHEN isDiscontinued = 0 THEN 1.0 ELSE 0.0 END) as percentNotChecked
                           FROM loadingApp.dbo.vehicles
						   LEFT JOIN (
							SELECT
								1 as Link
								,SUM(CASE WHEN Reason = 'Sold' THEN 1 ELSE 0 END) AS soldThisMonth
								,SUM(CASE WHEN Reason = 'SORN' THEN 1 ELSE 0 END) AS sornedThisMonth
								,SUM(CASE WHEN Reason = 'Written Off' THEN 1 ELSE 0 END) AS writeOffThisMonth
							FROM loadingApp.dbo.vehicleHistory
							LEFT JOIN loadingApp.dbo.vehicleActions ON vehicleActions.Id = vehicleHistory.actionReason
							WHERE MONTH(actionDate) = MONTH(GETDATE())
							AND YEAR(actionDate) = YEAR(GETDATE())
						   ) eventsMTD ON eventsMTD.Link = 1
						   LEFT JOIN (
							SELECT
								1 AS Link
								,SUM(CASE WHEN Reason IN ('Mechanical Repair', 'Accident Repair') THEN 1 ELSE 0 END) AS outForRepair
							FROM loadingApp.dbo.vehicleHistory
							LEFT JOIN loadingApp.dbo.vehicleActions ON vehicleActions.Id = vehicleHistory.actionReason
							INNER JOIN (SELECT MAX(Id) as link, vehicleRegistration FROM loadingApp.dbo.vehicleHistory GROUP BY vehicleRegistration) lastAct ON lastAct.vehicleRegistration = vehicleHistory.vehicleRegistration
                            ) repairs ON repairs.Link = 1
							LEFT JOIN (
								SELECT
									1 as Link,
									SUM(CASE WHEN isDiscontinued = 0 then 1 ELSE 0 END) as totalOnHire
								FROM loadingApp.dbo.vehiclesHire
							) hires ON hires.Link = 1
							LEFT JOIN (
								SELECT
									1 AS Link,
									SUM(CASE WHEN depot = 1 AND inspection.vehicleRegistration IS NOT NULL THEN 1 ELSE 0 END) AS [Haydock Checks],
									SUM(CASE WHEN depot = 3 AND inspection.vehicleRegistration IS NOT NULL THEN 1 ELSE 0 END) AS [Leeds Checks],
									SUM(CASE WHEN depot = 7 AND inspection.vehicleRegistration IS NOT NULL THEN 1 ELSE 0 END) AS [Traff Checks],
									SUM(CASE WHEN depot = 9 AND inspection.vehicleRegistration IS NOT NULL THEN 1 ELSE 0 END) AS [Tyne Checks],

									SUM(CASE WHEN depot = 1 AND inspection.vehicleRegistration IS NULL THEN 1 ELSE 0 END) AS [Haydock Not Checked],
									SUM(CASE WHEN depot = 3 AND inspection.vehicleRegistration IS NULL THEN 1 ELSE 0 END) AS [Leeds Not Checked],
									SUM(CASE WHEN depot = 7 AND inspection.vehicleRegistration IS NULL THEN 1 ELSE 0 END) AS [Traff Not Checked],
									SUM(CASE WHEN depot = 9 AND inspection.vehicleRegistration IS NULL THEN 1 ELSE 0 END) AS [Tyne Not Checked]
								FROM vehicles
								LEFT JOIN (
									SELECT DISTINCT
										vehicleRegistration,
										driverFullname
									FROM vanInspection
									WHERE CONVERT(DATE,checkDate) = CONVERT(DATE,GETDATE())
								) inspection ON inspection.vehicleRegistration = vehicles.vanRegistration
								WHERE isDiscontinued = 0
							) inspections ON inspections.Link = 1";
            var metricData = SQLDataAccess.LoadData<VehicleMaintenanceMeticsModel>(sql);
            return View(metricData);
        }

        // count the number of reported defects per vehicle
        public IActionResult VehicleDefectsDetail(int depot)
        {
            string sql = @"SELECT
	                            vehicleList.depot,
	                            vanRegistration,
	                            usualDriver,
	                            driverFullname,
	                            SUM(
		                            CASE WHEN commentsLightsIndicators <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsReflectorsMarkers <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsMirrors <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsOilCoolantLevel <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsAdBlueLevel <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsTyres <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsWheels <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsBodyPanels <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsHorn <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsFuelOilLeaks <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsSpeedometer <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsExhaustAndSmoke <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsBattery <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsSeatBelts <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsDoorsCondition <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsWipersAndWashers <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsInstrumentPanel <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsWindscreenCondition <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsFireExtinguisher <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsDashcam <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsWheelChangingKit <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsSpareWheel <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsWarningTriangle <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsFirstAidKit <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsDrivingLicense <> '' THEN 1 ELSE 0 END +
		                            CASE WHEN commentsAlcoholOrDrugs <> '' THEN 1 ELSE 0 END + 
		                            CASE WHEN ([nsfDepth] <= 2.0 OR [osfDepth] <= 2.0 OR [nsrDepth] <= 2.0 OR [osrDepth] <= 2.0 OR [spareDepth] <= 2.0) THEN 1 ELSE 0 END
                            ) AS totalDefects
                            FROM (
								                            SELECT
									                            CASE WHEN vehicles.depot = 1 THEN 'Haydock'
		                                                         WHEN vehicles.depot = 3 THEN 'Leeds'
		                                                         WHEN vehicles.depot = 7 THEN 'Trafford'
		                                                         WHEN vehicles.depot = 9 THEN 'Tyne & Wear'
	                                                            END AS depot,
									                            vehicles.depot AS depotNum,
									                            vehicles.vanRegistration,
									                            vehicles.driver AS usualDriver,
									                            isDiscontinued
								                            FROM vehicles
								                            UNION ALL
								                            SELECT
									                            hiredFor AS depot,
									                            CASE WHEN vehiclesHire.hiredFor = 'Haydock' THEN 1
		                                                         WHEN vehiclesHire.hiredFor = 'Leeds' THEN 3
		                                                         WHEN vehiclesHire.hiredFor = 'Trafford' THEN 7
		                                                         WHEN vehiclesHire.hiredFor = 'Tyne' THEN 9
	                                                            END AS depotNum,
									                            vehicleRegistration,
									                            '' AS usualDriver,
									                            isDiscontinued
								                            FROM vehiclesHire
                            ) vehicleList
                            LEFT JOIN (
	                            SELECT DISTINCT
		                            *
	                            FROM vanInspection
	                            WHERE CONVERT(DATE,checkDate) = CONVERT(DATE,GETDATE())
                            ) inspection ON inspection.vehicleRegistration = vehicleList.vanRegistration
                            WHERE isDiscontinued = 0
                            AND inspection.vehicleRegistration IS NOT NULL
                            AND (
	                            commentsLightsIndicators <> '' OR
	                            commentsReflectorsMarkers <> '' OR
	                            commentsMirrors <> '' OR
	                            commentsOilCoolantLevel <> '' OR
	                            commentsAdBlueLevel <> '' OR
	                            commentsTyres <> '' OR
	                            commentsWheels <> '' OR
	                            commentsBodyPanels <> '' OR
	                            commentsHorn <> '' OR
	                            commentsFuelOilLeaks <> '' OR
	                            commentsSpeedometer <> '' OR
	                            commentsExhaustAndSmoke <> '' OR
	                            commentsBattery <> '' OR
	                            commentsSeatBelts <> '' OR
	                            commentsDoorsCondition <> '' OR
	                            commentsWipersAndWashers <> '' OR
	                            commentsInstrumentPanel <> '' OR
	                            commentsWindscreenCondition <> '' OR
	                            commentsFireExtinguisher <> '' OR
	                            commentsDashcam <> '' OR
	                            commentsWheelChangingKit <> '' OR
	                            commentsSpareWheel <> '' OR
	                            commentsWarningTriangle <> '' OR
	                            commentsFirstAidKit <> '' OR
	                            commentsDrivingLicense <> '' OR
	                            commentsAlcoholOrDrugs <> '' OR
	                            [nsfDepth] <= 2.0 OR
	                            [osfDepth] <= 2.0 OR
	                            [nsrDepth] <= 2.0 OR
	                            [osrDepth] <= 2.0 OR
	                            [spareDepth] <= 2.0
                            )
                            AND vehicleList.depotNum = " + depot + @"
                            GROUP BY vehicleList.depot,
	                            vehicleList.vanRegistration,
	                            vehicleList.usualDriver,
	                            driverFullname
                                ";
            var inspectionDetail = SQLDataAccess.LoadData<VehicleDefectsDetailModel>(sql);
            return View(inspectionDetail);
        }

        // return the actual defects reported
        public IActionResult IndividualVehicleDefects(string reg)
        {
            string sql = @"SELECT
                                    checkDate,
                                    vehicles.vanRegistration,
									'' AS usualDriver,
									driverFullname AS currentDriver,
									commentsLightsIndicators,
									commentsReflectorsMarkers,
									commentsMirrors,
									commentsOilCoolantLevel,
									commentsAdBlueLevel,
									commentsTyres,
									commentsWheels,
									commentsBodyPanels,
									commentsHorn,
									commentsFuelOilLeaks,
									commentsSpeedometer,
									commentsExhaustAndSmoke,
									commentsBattery,
									commentsSeatBelts,
									commentsDoorsCondition,
									commentsWipersAndWashers,
									commentsInstrumentPanel,
									commentsWindscreenCondition,
									commentsFireExtinguisher,
									commentsDashcam,
									commentsWheelChangingKit,
									commentsSpareWheel,
									commentsWarningTriangle,
									commentsFirstAidKit,
									commentsDrivingLicense,
									commentsAlcoholOrDrugs,
									[nsfDepth],
									[osfDepth],
									[nsrDepth],
									[osrDepth],
									[spareDepth]
								FROM (

								 SELECT
									CASE WHEN vehicles.depot = 1 THEN 'Haydock'
		                                WHEN vehicles.depot = 3 THEN 'Leeds'
		                                WHEN vehicles.depot = 7 THEN 'Trafford'
		                                WHEN vehicles.depot = 9 THEN 'Tyne & Wear'
	                                END AS depot,
									vehicles.depot AS depotNum,
									vehicles.vanRegistration,
									vehicles.driver AS usualDriver,
									isDiscontinued
								FROM vehicles
								UNION ALL
								SELECT
									hiredFor AS depot,
									CASE WHEN vehiclesHire.hiredFor = 'Haydock' THEN 1
		                                WHEN vehiclesHire.hiredFor = 'Leeds' THEN 3
		                                WHEN vehiclesHire.hiredFor = 'Trafford' THEN 7
		                                WHEN vehiclesHire.hiredFor = 'Tyne' THEN 9
	                                END AS depotNum,
									vehicleRegistration,
									'' AS usualDriver,
									isDiscontinued
								FROM vehiclesHire
								) vehicles
								LEFT JOIN (
									SELECT DISTINCT
										*
									FROM vanInspection
									WHERE CONVERT(DATE,checkDate) = CONVERT(DATE,GETDATE())
								) inspection ON inspection.vehicleRegistration = vehicles.vanRegistration
								WHERE inspection.vehicleRegistration IS NOT NULL
								AND (
									commentsLightsIndicators <> '' OR
									commentsReflectorsMarkers <> '' OR
									commentsMirrors <> '' OR
									commentsOilCoolantLevel <> '' OR
									commentsAdBlueLevel <> '' OR
									commentsTyres <> '' OR
									commentsWheels <> '' OR
									commentsBodyPanels <> '' OR
									commentsHorn <> '' OR
									commentsFuelOilLeaks <> '' OR
									commentsSpeedometer <> '' OR
									commentsExhaustAndSmoke <> '' OR
									commentsBattery <> '' OR
									commentsSeatBelts <> '' OR
									commentsDoorsCondition <> '' OR
									commentsWipersAndWashers <> '' OR
									commentsInstrumentPanel <> '' OR
									commentsWindscreenCondition <> '' OR
									commentsFireExtinguisher <> '' OR
									commentsDashcam <> '' OR
									commentsWheelChangingKit <> '' OR
									commentsSpareWheel <> '' OR
									commentsWarningTriangle <> '' OR
									commentsFirstAidKit <> '' OR
									commentsDrivingLicense <> '' OR
									commentsAlcoholOrDrugs <> '' OR
									[nsfDepth] <= 2.0 OR
									[osfDepth] <= 2.0 OR
									[nsrDepth] <= 2.0 OR
									[osrDepth] <= 2.0 OR
									[spareDepth] <= 2.0
								)
                               
                                AND vehicles.vanRegistration= '" + reg + "'";
            //AND (CASE WHEN vehicles.depot = 1 THEN 'Haydock'
		          //               WHEN vehicles.depot = 3 THEN 'Leeds'
		          //               WHEN vehicles.depot = 7 THEN 'Trafford'
		          //               WHEN vehicles.depot = 9 THEN 'Tyne & Wear'
	           //                     END) 
            var individualInspection = SQLDataAccess.LoadData<VehicleDefectsDetailModel>(sql).First();
            return View(individualInspection);
        }

        // load the detail of each vehicle inspection
        public IActionResult IndividualVehicleInspections(string reg, int checkId)
        {
            string sql = @"SELECT
	                            vanRegistration,
	                            usualDriver,
	                            driverFullname AS currentDriver,
                                checkDate,
	                            commentsLightsIndicators,
	                            commentsReflectorsMarkers,
	                            commentsMirrors,
	                            commentsOilCoolantLevel,
	                            commentsAdBlueLevel,
	                            commentsTyres,
	                            commentsWheels,
	                            commentsBodyPanels,
	                            commentsHorn,
	                            commentsFuelOilLeaks,
	                            commentsSpeedometer,
	                            commentsExhaustAndSmoke,
	                            commentsBattery,
	                            commentsSeatBelts,
	                            commentsDoorsCondition,
	                            commentsWipersAndWashers,
	                            commentsInstrumentPanel,
	                            commentsWindscreenCondition,
	                            commentsFireExtinguisher,
	                            commentsDashcam,
	                            commentsWheelChangingKit,
	                            commentsSpareWheel,
	                            commentsWarningTriangle,
	                            commentsFirstAidKit,
	                            commentsDrivingLicense,
	                            commentsAlcoholOrDrugs,
	                            [nsfDepth],
	                            [osfDepth],
	                            [nsrDepth],
	                            [osrDepth],
	                            [spareDepth]
                            FROM (
	                            SELECT
		                            vehicles.vanRegistration,
		                            vehicles.driver AS usualDriver
	                            FROM vehicles
	                            UNION ALL
	                            SELECT
		                            vehiclesHire.vehicleRegistration,
		                            '' AS usualDriver
	                            FROM vehiclesHire
                            ) vehiclesList
                            LEFT JOIN (
	                            SELECT DISTINCT
		                            *
	                            FROM vanInspection
	                            WHERE Id = " + checkId + @"
                            ) inspection ON inspection.vehicleRegistration = vehiclesList.vanRegistration
                            WHERE inspection.vehicleRegistration IS NOT NULL
                            AND (
	                            commentsLightsIndicators <> '' OR
	                            commentsReflectorsMarkers <> '' OR
	                            commentsMirrors <> '' OR
	                            commentsOilCoolantLevel <> '' OR
	                            commentsAdBlueLevel <> '' OR
	                            commentsTyres <> '' OR
	                            commentsWheels <> '' OR
	                            commentsBodyPanels <> '' OR
	                            commentsHorn <> '' OR
	                            commentsFuelOilLeaks <> '' OR
	                            commentsSpeedometer <> '' OR
	                            commentsExhaustAndSmoke <> '' OR
	                            commentsBattery <> '' OR
	                            commentsSeatBelts <> '' OR
	                            commentsDoorsCondition <> '' OR
	                            commentsWipersAndWashers <> '' OR
	                            commentsInstrumentPanel <> '' OR
	                            commentsWindscreenCondition <> '' OR
	                            commentsFireExtinguisher <> '' OR
	                            commentsDashcam <> '' OR
	                            commentsWheelChangingKit <> '' OR
	                            commentsSpareWheel <> '' OR
	                            commentsWarningTriangle <> '' OR
	                            commentsFirstAidKit <> '' OR
	                            commentsDrivingLicense <> '' OR
	                            commentsAlcoholOrDrugs <> '' OR
	                            [nsfDepth] <= 2.0 OR
	                            [osfDepth] <= 2.0 OR
	                            [nsrDepth] <= 2.0 OR
	                            [osrDepth] <= 2.0 OR
	                            [spareDepth] <= 2.0
                            )
                               
                            AND vehiclesList.vanRegistration= '" + reg + "'";
            //AND (CASE WHEN vehicles.depot = 1 THEN 'Haydock'
            //               WHEN vehicles.depot = 3 THEN 'Leeds'
            //               WHEN vehicles.depot = 7 THEN 'Trafford'
            //               WHEN vehicles.depot = 9 THEN 'Tyne & Wear'
            //                     END) 
            var individualInspection = SQLDataAccess.LoadData<VehicleDefectsDetailModel>(sql).First();
            return View("/Views/Home/IndividualVehicleDefects.cshtml", individualInspection);
        }

        // load any vehicles that have not been checker per depot
        public IActionResult VehiclesNotChecked(int depot)
        {
            string sql = @"SELECT
	                            depot,
	                            vanRegistration,
	                            CONVERT(DATE,lastChecked) AS lastChecked
                            FROM (

	                            SELECT
		                            vehicles.depot,
		                            vehicles.vanRegistration,
		                            vehicles.driver AS usualDriver,
		                            isDiscontinued
	                            FROM vehicles
	                            UNION ALL
	                            SELECT
		                            CASE WHEN vehiclesHire.hiredFor = 'Haydock' THEN 1
		                                WHEN vehiclesHire.hiredFor = 'Leeds' THEN 3
		                                WHEN vehiclesHire.hiredFor = 'Trafford' THEN 7
		                                WHEN vehiclesHire.hiredFor = 'Tyne' THEN 9
	                                END AS depotNum,
		                            vehiclesHire.vehicleRegistration,
		                            '' as usualDriver,
		                            isDiscontinued
	                            FROM vehiclesHire
                            ) vehiclesList

	                            LEFT JOIN (
		                            SELECT DISTINCT
			                            vehicleRegistration
		                            FROM vanInspection
		                            WHERE CONVERT(DATE,checkDate) = CONVERT(DATE,GETDATE())
	                            ) inspection ON inspection.vehicleRegistration = vehiclesList.vanRegistration
	                            LEFT JOIN (
		                            SELECT
			                            vehicleRegistration,
			                            MAX(checkDate) as lastChecked
		                            FROM vanInspection
		                            GROUP BY vehicleRegistration
	                            ) lastInspection ON lastInspection.vehicleRegistration = vehiclesList.vanRegistration
	                            WHERE isDiscontinued = 0
	                            AND inspection.vehicleRegistration IS NULL
	                            AND depot = " + depot;

            var notChecked = SQLDataAccess.LoadData<VehicleMissingCheckModel>(sql);
            return View(notChecked);
        }

        // fetch the list of makes
        public static List<VehicleMakesModel> GetMakes()
        {
            string sqlquery = @"SELECT DISTINCT 1 as Id, name AS Text FROM loadingApp.dbo.vehicleMake";
            return SQLDataAccess.LoadData<VehicleMakesModel>(sqlquery);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // call the list of vehicle makes and create an array for the select2 dropdown list
        public ActionResult GetVehicleMakes(string q)
        {
            var makesFetched = GetMakes();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                makesFetched = makesFetched.Where(x => x.Text.ToLower().StartsWith(q.ToLower())).ToList();
            }
            return Json(new { items = makesFetched });
        }

        // fetch the list of models
        public static List<VehicleMakesModel> GetModels()
        {
            string sqlquery = @"SELECT DISTINCT 1 as Id, model AS Text FROM loadingApp.dbo.vehicleMake";
            return SQLDataAccess.LoadData<VehicleMakesModel>(sqlquery);
        }

        // fetch the list of vehicle registration numbers
        public static List<VehicleRegistrationListModel> GetRegistrations(int depot)
        {
            string sql = @"SELECT 1 as Id, vanRegistration as Text FROM loadingApp.dbo.vehicles WHERE depot = " + depot + " AND isDiscontinued = 0";
            return SQLDataAccess.LoadData<VehicleRegistrationListModel>(sql);
        }

        // fecth the list of discontinue reasons
        public static List<ReasonListModel> GetDiscontinueReason()
        {
            string sql = @"SELECT Id, reason AS Text FROM loadingApp.dbo.vehicleActions WHERE statusCode = 2";
            return SQLDataAccess.LoadData<ReasonListModel>(sql);
        }

        // fecth the list of update reasons
        public static List<ReasonListModel> GetUpdateReason()
        {
            string sql = @"SELECT Id, reason AS Text FROM loadingApp.dbo.vehicleActions WHERE statusCode = 3";
            return SQLDataAccess.LoadData<ReasonListModel>(sql);
        }

        // fecth the list of reinstate reasons
        public static List<ReasonListModel> GetReinstateReason()
        {
            string sql = @"SELECT Id, reason AS Text FROM loadingApp.dbo.vehicleActions WHERE statusCode = 1";
            return SQLDataAccess.LoadData<ReasonListModel>(sql);
        }

        // fecth the list of cost centre codes
        public static List<CostCentreModel> GetCostCentreCode()
        {
            string sql = @"SELECT costCentre AS Text, code AS Id FROM loadingApp.dbo.hireCostCentre";
            return SQLDataAccess.LoadData<CostCentreModel>(sql);
        }

        // fecth the list of both fleet and hire vehicle registrations
        public static List<VehicleRegistrationListModel> GetAllRegistrations()
        {
            string sql = @"SELECT 1 as Id, vanRegistration as Text FROM loadingApp.dbo.vehicles UNION ALL SELECT 1 as Id, vehicleRegistration as Text FROM loadingApp.dbo.vehiclesHire";
            return SQLDataAccess.LoadData<VehicleRegistrationListModel>(sql);
        }

        // call the list of cost centres and create an array for the select2 dropdown list
        public ActionResult GetCostCentreCodes(string q)
        {
            var codesFetched = GetCostCentreCode();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                codesFetched = codesFetched.Where(x => x.Text.ToLower().StartsWith(q.ToLower())).ToList();
            }
            return Json(new { items = codesFetched });
        }

        // call the list of vehicle models and create an array for the select2 dropdown list
        public ActionResult GetVehicleModels(string q)
        {
            var makesFetched = GetModels();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                makesFetched = makesFetched.Where(x => x.Text.ToLower().StartsWith(q.ToLower())).ToList();
            }
            return Json(new { items = makesFetched });
        }

        // call the list of both fleet and rental vehicle registrations and create an array for the select2 dropdown list
        public ActionResult GetAllVehicleRegistrations(string q)
        {
            var regsFetched = GetAllRegistrations();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                regsFetched = regsFetched.Where(x => x.Text.ToLower().StartsWith(q.ToLower())).ToList();
            }
            return Json(new { items = regsFetched });
        }

        // call the list of fleet vehicles and create an array for the select2 dropdown list
        public ActionResult GetVehicleRegistrations(string q, int depot)
        {
            var regsFetched = GetRegistrations(depot);
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                regsFetched = regsFetched.Where(x => x.Text.ToLower().StartsWith(q.ToLower())).ToList();
            }
            return Json(new { items = regsFetched });
        }

        // call the list of discontinue reasons and create an array for the select2 dropdown list
        public ActionResult GetDiscontinueReasons(string q)
        {
            var reasonsFetched = GetDiscontinueReason();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                reasonsFetched = reasonsFetched.Where(x => x.Text.ToLower().StartsWith(q.ToLower())).ToList();
            }
            return Json(new { items = reasonsFetched });
        }

        // call the list of update reasons and create an array for the select2 dropdown list
        public ActionResult GetUpdateReasons(string q)
        {
            var reasonsFetched = GetUpdateReason();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                reasonsFetched = reasonsFetched.Where(x => x.Text.ToLower().StartsWith(q.ToLower())).ToList();
            }
            return Json(new { items = reasonsFetched });
        }

        // call the list of reinstate reasons and create an array for the select2 dropdown list
        public ActionResult GetReinstateReasons(string q)
        {
            var reasonsFetched = GetReinstateReason();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                reasonsFetched = reasonsFetched.Where(x => x.Text.ToLower().StartsWith(q.ToLower())).ToList();
            }
            return Json(new { items = reasonsFetched });
        }

        // return an object for the vehicle registration to be passed to the modal pop up view for updating tax records
        public IActionResult UpdateVehicleTax(string data)
        {
            VehicleRegistrationModel vehicleRegistration = new VehicleRegistrationModel()
            {
                VehicleReg = data
            };
            return PartialView("~/Views/Home/PartialViews/_UpdateVehicleTax.cshtml", vehicleRegistration);
        }

        // return an object for the vehicle registration to be passed to the modal pop up view for updating MOT records
        public IActionResult UpdateVehicleMOT(string data)
        {
            VehicleRegistrationModel vehicleRegistration = new VehicleRegistrationModel
            {
                VehicleReg = data
            };
            return PartialView("~/Views/Home/PartialViews/_UpdateVehicleMOT.cshtml", vehicleRegistration);
        }

        // return an object for the vehicle registration to be passed to the modal pop up view for updating service records
        public IActionResult UpdateVehicleService(string data)
        {
            VehicleRegistrationModel vehicleRegistration = new VehicleRegistrationModel()
            {
                VehicleReg = data
            };
            return PartialView("~/Views/Home/PartialViews/_UpdateVehicleService.cshtml", vehicleRegistration);
        }

        // return an object for the vehicle registration to be passed to the modal pop up view for discontinuing vehicles
        public IActionResult LoadDiscontinueVehicleModal(string data)
        {
            VehicleRegistrationModel vehicleRegistration = new VehicleRegistrationModel()
            {
                VehicleReg = data
            };
            return PartialView("~/Views/Home/PartialViews/_DiscontinueVehicleConfirmation.cshtml", vehicleRegistration);
        }

        // return an object for the vehicle registration to be passed to the modal pop up view for reinstating vehicles
        public IActionResult UpdateReinstateVehicle(string data)
        {
            VehicleRegistrationModel vehicleRegistration = new VehicleRegistrationModel
            {
                VehicleReg = data
            };
            return PartialView("~/Views/Home/PartialViews/_UpdateReinstateVehicle.cshtml", vehicleRegistration);
        }

        // return an object for the vehicle registration to be passed to the modal pop up view for updating warranty records
        public IActionResult UpdateVehicleWarranty(string data)
        {
            VehicleRegistrationModel vehicleRegistration = new VehicleRegistrationModel
            {
                VehicleReg = data
            };
            return PartialView("~/Views/Home/PartialViews/_UpdateVehicleWarranty.cshtml", vehicleRegistration);
        }

        // return an object for the vehicle registration to be passed to the modal pop up view for uploading documents
        public IActionResult UpdateDocuments(string data)
        {
            VehicleRegistrationModel vehicleRegistration = new VehicleRegistrationModel
            {
                VehicleReg = data
            };
            return PartialView("~/Views/Home/PartialViews/_UploadDocumentsConfirmation.cshtml", vehicleRegistration);
        }

        // return an object for the vehicle registration to be passed to the modal pop up view for off hiring vehicles
        public IActionResult LoadDiscontinueHireVehicleModal(string data)
        {
            VehicleRegistrationModel vehicleRegistration = new VehicleRegistrationModel
            {
                VehicleReg = data
            };
            return PartialView("~/Views/Home/PartialViews/_DiscontinueHireVehicle.cshtml", vehicleRegistration);
        }

        // process the discontinue fleet vehicle form
        [HttpPost]
        public void SaveDiscontinue(string data)
        {

            //var user = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var logDate = DateTime.Now.ToString("yyyy-MM-dd");
            DiscontinueObjectModel discontinue = Newtonsoft.Json.JsonConvert.DeserializeObject<DiscontinueObjectModel>(data);
            var insertSql = @"INSERT INTO loadingApp.dbo.vehicleHistory ([vehicleRegistration],[actionReason],[actionDate],[additionalComments],[actionUser])
            VALUES ('" + discontinue.Reg + "'," + discontinue.Reason + ",'" + logDate + "','" + discontinue.Comments + "','" + discontinue.User + "')";

            var updateVehiclesList = @"UPDATE loadingApp.dbo.vehicles SET isDiscontinued = 1 WHERE vanRegistration = '" + discontinue.Reg + "'";

            SQLDataAccess.SaveData(insertSql, discontinue);
            SQLDataAccess.SaveData(updateVehiclesList, discontinue);
        }

        // process the discontinue hire vehicle form
        [HttpPost]
        public void SaveHireDiscontinue(string data)
        {

            //var user = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var logDate = DateTime.Now.ToString("yyyy-MM-dd");
            DiscontinueObjectModel discontinue = Newtonsoft.Json.JsonConvert.DeserializeObject<DiscontinueObjectModel>(data);
            var insertSql = @"INSERT INTO loadingApp.dbo.vehicleHistory ([vehicleRegistration],[actionReason],[actionDate],[additionalComments],[actionUser])
            VALUES ('" + discontinue.Reg + "'," + discontinue.Reason + ",'" + logDate + "','" + discontinue.Comments + "','" + discontinue.User + "')";

            var updateVehiclesList = @"UPDATE loadingApp.dbo.vehiclesHire SET isDiscontinued = 1 WHERE vehicleRegistration = '" + discontinue.Reg + "'";

            SQLDataAccess.SaveData(insertSql, discontinue);
            SQLDataAccess.SaveData(updateVehiclesList, discontinue);
        }

        // process the reinstate vehicle form
        [HttpPost]
        public void SaveReinstate(string data)
        {

            var logDate = DateTime.Now.ToString("yyyy-MM-dd");
            DiscontinueObjectModel reinstate = Newtonsoft.Json.JsonConvert.DeserializeObject<DiscontinueObjectModel>(data);
            var insertSql = @"INSERT INTO loadingApp.dbo.vehicleHistory ([vehicleRegistration],[actionReason],[actionDate],[additionalComments], [actionUser])
            VALUES ('" + reinstate.Reg + "'," + reinstate.Reason + ",'" + logDate + "','" + reinstate.Comments + "','" + reinstate.User + "')";

            var updateVehiclesList = @"UPDATE loadingApp.dbo.vehicles SET isDiscontinued = 0 WHERE vanRegistration = '" + reinstate.Reg + "'";

            SQLDataAccess.SaveData(insertSql, reinstate);
            SQLDataAccess.SaveData(updateVehiclesList, reinstate);
        }

        //private readonly IHostEnvironment host;
        //public HomeController(IHostEnvironment environment)
        //{
        //    host = environment;
        //}

        // process the update MOT form
        [HttpPost]
        public IActionResult UploadMOTUpdate(IFormFile update, string reasonCode, string vanReg, string userName, DateTime motMonths)
        {

            if (update != null)
            {
                if (update.Length > 0)
                {
                    var fileName = Path.GetFileName(update.FileName);
                    var uniqueFilename = "MOT_" + vanReg + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
                    var fileExtension = Path.GetExtension(fileName);
                    var newFileName = String.Concat(uniqueFilename, fileExtension);
                    var rootFilePath = "\\\\oakfs\\OpenShare\\opsPrivateScans\\Mel Cooper\\VanServiceandMOTHistory\\MotDocs\\" + vanReg + "\\";
                    var filePath = "\\\\oakfs\\OpenShare\\opsPrivateScans\\Mel Cooper\\VanServiceandMOTHistory\\MotDocs\\" + vanReg + "\\" + $@"{newFileName}";

                    bool exists = Directory.Exists(rootFilePath);
                    if (!exists)
                    {
                        Directory.CreateDirectory(rootFilePath);
                    }

                    // save uploaded file to the file system (shared drive)
                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        update.CopyTo(fs);
                        fs.Flush();
                        fs.Close();
                    }
                    VehicleHistoryModel historyModel = new VehicleHistoryModel();
                    var actionDate = DateTime.Now.ToString("yyyy-MM-dd");
                    var vehicleHistorySQL = @"INSERT INTO loadingApp.dbo.vehicleHistory (vehicleRegistration, actionReason, actionDate, additionalComments, filePath, actionUser) 
                                     VALUES ('" + vanReg + "','" + reasonCode + "','" + actionDate + "','" + "No Comments" + "','" + filePath + "','" + userName + "')";
                    // Todo - need another check here to prevent the due date from being incremented if the user uploads several documents in error...
                    // or change the upload action to allow mutliple files
                    var updateMOTSQL = @"UPDATE loadingApp.dbo.vehicles SET motDue = '" + motMonths.ToString("yyyy-MM-dd") + "' WHERE vanRegistration = '" + vanReg + "'";
                    SQLDataAccess.SaveData(vehicleHistorySQL, historyModel);
                    SQLDataAccess.SaveData(updateMOTSQL, vanReg);
                }

            }
            return RedirectToAction("UpcomingMOT");
        }

        // process the tax update form
        [HttpPost]
        public IActionResult UploadTAXUpdate(string reasonCode, string vanReg, DateTime taxMonths, string userName)
        {
            VehicleHistoryModel historyModel = new VehicleHistoryModel();
            var actionDate = DateTime.Now.ToString("yyyy-MM-dd");
            var vehicleHistorySQL = @"INSERT INTO loadingApp.dbo.vehicleHistory (vehicleRegistration, actionReason, actionDate, additionalComments, actionUser) 
                                VALUES ('" + vanReg + "','" + reasonCode + "','" + actionDate + "','" + "No Comments" + "','" + userName + "')";
            // Todo - need another check here to prevent the due date from being incremented if the user uploads several documents in error...
            // or change the upload action to allow mutliple files
            var updateMOTSQL = @"UPDATE loadingApp.dbo.vehicles SET taxDue = '" + taxMonths.ToString("yyyy-MM-dd") + "' WHERE vanRegistration = '" + vanReg + "'";
            SQLDataAccess.SaveData(updateMOTSQL, historyModel);
            SQLDataAccess.SaveData(vehicleHistorySQL, vanReg);

            return RedirectToAction("UpcomingTAX");
        }

        [HttpPost]
        public IActionResult UploadServiceUpdate(IFormFile update, string reasonCode, string vanReg, string userName, int serviceMiles)
        {

            if (update != null)
            {
                if (update.Length > 0)
                {
                    var fileName = Path.GetFileName(update.FileName);
                    var uniqueFilename = "Service_" + vanReg + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
                    var fileExtension = Path.GetExtension(fileName);
                    var newFileName = String.Concat(uniqueFilename, fileExtension);
                    var rootFilePath = "\\\\oakfs\\OpenShare\\opsPrivateScans\\Mel Cooper\\VanServiceandMOTHistory\\ServiceDocs\\" + vanReg + "\\";
                    var filePath = "\\\\oakfs\\OpenShare\\opsPrivateScans\\Mel Cooper\\VanServiceandMOTHistory\\ServiceDocs\\" + vanReg + "\\" + $@"{newFileName}";


                    bool exists = Directory.Exists(rootFilePath);
                    if (!exists)
                    {
                        Directory.CreateDirectory(rootFilePath);
                    }

                    // save uploaded file to the file system (shared drive)
                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        update.CopyTo(fs);
                        fs.Flush();
                        fs.Close();
                    }
                    VehicleHistoryModel historyModel = new VehicleHistoryModel();
                    var actionDate = DateTime.Now.ToString("yyyy-MM-dd");
                    serviceMiles += 15000;
                    var vehicleHistorySQL = @"INSERT INTO loadingApp.dbo.vehicleHistory (vehicleRegistration, actionReason, actionDate, additionalComments, filePath, actionUser) 
                                     VALUES ('" + vanReg + "','" + reasonCode + "','" + actionDate + "','" + "No Comments" + "','" + filePath + "','" + userName + "')";
                    // Todo - need another check here to prevent the due date from being incremented if the user uploads several documents in error...
                    // or change the upload action to allow mutliple files
                    var updateServiceSQL = @"UPDATE loadingApp.dbo.vehicles SET serviceInterval = " + serviceMiles + " WHERE vanRegistration = '" + vanReg + "'";
                    System.Diagnostics.Debug.WriteLine(updateServiceSQL);
                    SQLDataAccess.SaveData(updateServiceSQL, historyModel);
                    SQLDataAccess.SaveData(vehicleHistorySQL, vanReg);
                }
            }
            return RedirectToAction("UpcomingServices");
        }

        // process the upload documents form
        [HttpPost]
        public IActionResult UploadDocuments(IFormFile update, string reasonCode, string vanReg, string userName, string uploadComments)
        {

            if (update != null)
            {
                if (update.Length > 0)
                {
                    var fileName = Path.GetFileName(update.FileName);
                    var uniqueFilename = "Misc_" + vanReg + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
                    var fileExtension = Path.GetExtension(fileName);
                    var newFileName = String.Concat(uniqueFilename, fileExtension);
                    var rootFilePath = "\\\\oakfs\\OpenShare\\opsPrivateScans\\Mel Cooper\\VanServiceandMOTHistory\\MISC\\" + vanReg + "\\";
                    var filePath = "\\\\oakfs\\OpenShare\\opsPrivateScans\\Mel Cooper\\VanServiceandMOTHistory\\MISC\\" + vanReg + "\\" + $@"{newFileName}";


                    bool exists = Directory.Exists(rootFilePath);
                    if (!exists)
                    {
                        Directory.CreateDirectory(rootFilePath);
                    }

                    // save file to file system (shared drive)
                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        update.CopyTo(fs);
                        fs.Flush();
                        fs.Close();
                    }
                    VehicleHistoryModel historyModel = new VehicleHistoryModel();
                    var actionDate = DateTime.Now.ToString("yyyy-MM-dd");
                    var vehicleHistorySQL = @"INSERT INTO loadingApp.dbo.vehicleHistory (vehicleRegistration, actionReason, actionDate, additionalComments, filePath, actionUser) 
                                     VALUES ('" + vanReg + "','" + reasonCode + "','" + actionDate + "','" + uploadComments + "','" + filePath + "','" + userName + "')";
                    // Todo - need another check here to prevent the due date from being incremented if the user uploads several documents in error...
                    // or change the upload action to allow mutliple files
                    SQLDataAccess.SaveData(vehicleHistorySQL, vanReg);
                }
            }
            return RedirectToAction("UploadDocuments");
        }

        // process the update warranty form
        [HttpPost]
        public IActionResult UploadWarrantyUpdate(string vanReg)
        {
            var updateWarrantySql = @"UPDATE loadingApp.dbo.vehicles SET inWarranty = 0 WHERE vanRegistration = '" + vanReg + "'";
            SQLDataAccess.SaveData(updateWarrantySql, vanReg);
            return RedirectToAction("UpcomingWarranty");
        }

        // convert byte stream to png image file
        public static byte[] BitmapToBytes(Bitmap img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        // convert vehicle registration to a QR code image
        public IActionResult GenerateQRCode(string vehicleRegistration)
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(vehicleRegistration, QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new QRCode(qRCodeData);
            Bitmap qrcCodeImage = qRCode.GetGraphic(20);
            QRCodeModel qR = new QRCodeModel
            {
                QR = BitmapToBytes(qrcCodeImage),
                VehicleRegistration = vehicleRegistration
            };
            return View(qR);
        }

        // fetch all vehicles
        public IActionResult AllVehicles()
        {
            string sql = @"SELECT [Id]
                              ,[Depot]
							  ,CASE WHEN depot = 1 THEN 'Haydock' WHEN depot = 3 THEN 'Leeds' WHEN depot = 7 THEN 'Trafford' ELSE 'Tyne & Wear' END AS DepotName
                              ,[vanRegistration]
                              ,[make]
                              ,[model]
                              ,[warrantyMileage]
                              ,[startDate]
                              ,[endDate]
                              ,[mileageDate]
                              ,[currentMileage]
                              ,[taxDue]
                              ,[motDue]
                              ,CASE WHEN [inWarranty] = 1 THEN 'Yes' ELSE 'No' END AS [inWarranty]
                              ,[serviceInterval]
                              ,[driver]
                              ,[livery]
                              ,[tyreSize]
                              ,[camera]
                              ,[masternaught]
                          FROM [loadingApp].[dbo].[vehicles]
                          WHERE isDiscontinued = 0

						  UNION ALL

                    SELECT [Id]
                              ,0
							  ,hiredFor
                              ,vehicleRegistration
                              ,[make]
                              ,[model]
                              ,0
                              ,hiredFrom
                              ,hiredTo
                              ,''
                              ,0
                              ,''
                              ,''
                              ,'N/A'
                              ,''
                              ,'Hire'
                              ,'N/A'
                              ,''
                              ,0
                              ,''
                          FROM [loadingApp].[dbo].[vehiclesHire]
                          WHERE isDiscontinued = 0
                          ORDER BY 2, 4";
            var allVans = SQLDataAccess.LoadData<DiscontinueVehicleModel>(sql);
            return View(allVans);
        }

        // fetch the list of vehicles for vehicle history
        public IActionResult VehicleActionHistory()
        {
            string sql = @"SELECT 
							  CASE WHEN depot = 1 THEN 'Haydock' WHEN depot = 3 THEN 'Leeds' WHEN depot = 7 THEN 'Trafford' ELSE 'Tyne & Wear' END AS DepotName
                              ,[vanRegistration]
                              ,[make]
                              ,[model]
							  ,'On Fleet' as FleetOrRent
							  ,CASE WHEN isDiscontinued = 1 THEN 'Discontinued' ELSE 'Active' END AS VehicleStatus
                          FROM [loadingApp].[dbo].[vehicles]

                          UNION ALL

                          SELECT 
							  hiredFor
                              ,vehicleRegistration
                              ,[make]
                              ,[model]
							  ,'Rental'
							  ,CASE WHEN isDiscontinued = 1 THEN 'Discontinued' ELSE 'Active' END AS VehicleStatus
                          FROM [loadingApp].[dbo].[vehiclesHire]
                          ORDER BY 1, 2";
            var allVehicles = SQLDataAccess.LoadData<VehicleActionHistoryModel>(sql);
            return View(allVehicles);
        }

        // show the full history detail of any given vehicle
        public IActionResult VehicleHistoryDetail(string vehicleRegistration)
        {
            string sql = @"SELECT
	                            vehicleRegistration,
	                            depot AS Depot,
	                            Reason,
	                            actionDate,
	                            additionalComments,
	                            actionUser,
	                            fleetOrRent
                            FROM loadingApp.dbo.vehicleHistory
                            INNER JOIN loadingApp.dbo.vehicleActions ON vehicleActions.Id = vehicleHistory.actionReason
                            INNER JOIN (
	                            SELECT
		                            vanRegistration,
		                            CASE WHEN depot = 1 THEN 'Haydock' WHEN depot = 3 THEN 'Leeds' WHEN depot = 7 THEN 'Trafford' WHEN depot = 9 THEN 'Tyne & Wear' END AS depot,
		                            'On Fleet' as fleetOrRent
	                            FROM loadingApp.dbo.vehicles
	                            UNION ALL
	                            SELECT
		                            vehicleRegistration,
		                            hiredFor,
		                            'Rent'
	                            FROM loadingApp.dbo.vehiclesHire
                            ) vehicles ON vehicles.vanRegistration = vehicleHistory.vehicleRegistration
                            WHERE vehicleRegistration = '" + vehicleRegistration + "'";
            var vehicleHistory = SQLDataAccess.LoadData<VehicleHistoryDetailModel>(sql);
            return View(vehicleHistory);
        }

        // fetch each inspection carried out in the last 7 days
        public IActionResult InspectionHistory()
        {
            string sql = @"SELECT 
             [Id]
			,totalDefects
            ,[depot]
            ,[vehicleRegistration]
            ,vehicleMakes.make as [vehicleMake]
            ,[mileage]
            ,[driverFullname]
            ,[lightsIndicators]
            ,[reflectorMarkers]
            ,[mirrors]
            ,[oilCoolantLevel]
            ,[adBlueLevel]
            ,[tyres]
            ,[wheels]
            ,[bodyPanels]
            ,[horns]
            ,[fuelOilLeaks]
            ,[speedometer]
            ,[exhaustAndSmoke]
            ,[battery]
            ,[seatBelts]
            ,[doorCondition]
            ,[instrumentPanel]
            ,[windscreenCondition]
            ,[wipersAndWashers]
            ,[fireExtinguisher]
            ,[dashCam]
            ,[wheelChangingKit]
            ,[spareWheel]
            ,[warningTriangle]
            ,[firstAidKit]
            ,[drivingLicense]
            ,[alcoholOrDrugs]
            ,[contactDetails]
            ,[commentsLightsIndicators]
            ,[commentsReflectorsMarkers]
            ,[commentsMirrors]
            ,[commentsOilCoolantLevel]
            ,[commentsAdBlueLevel]
            ,[commentsTyres]
            ,[commentsWheels]
            ,[commentsBodyPanels]
            ,[commentsHorn]
            ,[commentsFuelOilLeaks]
            ,[commentsSpeedometer]
            ,[commentsExhaustAndSmoke]
            ,[commentsBattery]
            ,[commentsSeatBelts]
            ,[commentsDoorsCondition]
            ,[commentsWipersAndWashers]
            ,[commentsInstrumentPanel]
            ,[commentsWindscreenCondition]
            ,[commentsFireExtinguisher]
            ,[commentsDashcam]
            ,[commentsWheelChangingKit]
            ,[commentsSpareWheel]
            ,[commentsWarningTriangle]
            ,[commentsFirstAidKit]
            ,[commentsDrivingLicense]
            ,[commentsAlcoholOrDrugs]
            ,[checkDate]
            ,[Signature]
            FROM [loadingApp].[dbo].[vanInspection]
			LEFT JOIN (
				SELECT
					vanRegistration,
					make
				FROM loadingApp.dbo.vehicles
				UNION ALL
				SELECT
					vehicleRegistration,
					make
				FROM loadingApp.dbo.vehiclesHire
			) vehicleMakes ON vehicleMakes.vanRegistration = vanInspection.vehicleRegistration
			LEFT JOIN (
				SELECT
					Id as countId,
				SUM(CASE WHEN commentsLightsIndicators <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsReflectorsMarkers <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsMirrors <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsOilCoolantLevel <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsAdBlueLevel <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsTyres <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsWheels <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsBodyPanels <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsHorn <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsFuelOilLeaks <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsSpeedometer <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsExhaustAndSmoke <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsBattery <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsSeatBelts <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsDoorsCondition <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsWipersAndWashers <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsInstrumentPanel <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsWindscreenCondition <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsFireExtinguisher <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsDashcam <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsWheelChangingKit <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsSpareWheel <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsWarningTriangle <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsFirstAidKit <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsDrivingLicense <> '' THEN 1 ELSE 0 END +
					CASE WHEN commentsAlcoholOrDrugs <> '' THEN 1 ELSE 0 END + 
					CASE WHEN ([nsfDepth] <= 2.0 OR [osfDepth] <= 2.0 OR [nsrDepth] <= 2.0 OR [osrDepth] <= 2.0 OR [spareDepth] <= 2.0) THEN 1 ELSE 0 END
				) AS totalDefects
				FROM loadingApp.dbo.vanInspection
				GROUP BY Id
			) inspectionTotals ON inspectionTotals.countId = vanInspection.Id

            WHERE CONVERT(DATE, checkDate) >= DATEADD(DD,-7,CONVERT(DATE,GETDATE()))";

            //string sql = @"SELECT 
            // [Id]
            //,[depot]
            //,[vehicleRegistration]
            //,[vehicleMake]
            //,[mileage]
            //,[driverFullname]
            //,[checkDate]
            //FROM [loadingApp].[dbo].[vanInspection]";

            var inspections = SQLDataAccess.LoadData<VehicleInspectionDetailsModel>(sql);

            return View(inspections);
        }

        // deprecated code
        public IActionResult VehicleInspectionDetail(int InspectionItem)
        {
            string sql = @"SELECT 
              [Id]
            ,[depot]
            ,[vehicleRegistration]
            ,[vehicleMake]
            ,[mileage]
            ,[driverFullname]
            ,[lightsIndicators]
            ,[reflectorMarkers]
            ,[mirrors]
            ,[oilCoolantLevel]
            ,[adBlueLevel]
            ,[tyres]
            ,[wheels]
            ,[bodyPanels]
            ,[horns]
            ,[fuelOilLeaks]
            ,[speedometer]
            ,[exhaustAndSmoke]
            ,[battery]
            ,[seatBelts]
            ,[doorCondition]
            ,[instrumentPanel]
            ,[windscreenCondition]
            ,[wipersAndWashers]
            ,[fireExtinguisher]
            ,[dashCam]
            ,[wheelChangingKit]
            ,[spareWheel]
            ,[warningTriangle]
            ,[firstAidKit]
            ,[drivingLicense]
            ,[alcoholOrDrugs]
            ,[contactDetails]
            ,[commentsLightsIndicators]
            ,[commentsReflectorsMarkers]
            ,[commentsMirrors]
            ,[commentsOilCoolantLevel]
            ,[commentsAdBlueLevel]
            ,[commentsTyres]
            ,[commentsWheels]
            ,[commentsBodyPanels]
            ,[commentsHorn]
            ,[commentsFuelOilLeaks]
            ,[commentsSpeedometer]
            ,[commentsExhaustAndSmoke]
            ,[commentsBattery]
            ,[commentsSeatBelts]
            ,[commentsDoorsCondition]
            ,[commentsWipersAndWashers]
            ,[commentsInstrumentPanel]
            ,[commentsWindscreenCondition]
            ,[commentsFireExtinguisher]
            ,[commentsDashcam]
            ,[commentsWheelChangingKit]
            ,[commentsSpareWheel]
            ,[commentsWarningTriangle]
            ,[commentsFirstAidKit]
            ,[commentsDrivingLicense]
            ,[commentsAlcoholOrDrugs]
            ,[checkDate]
            ,[Signature]
            FROM [loadingApp].[dbo].[vanInspection]
             where Id = '"+ InspectionItem + "'";

            var inspections = SQLDataAccess.LoadData<VehicleInspectionDetailsModel>(sql);
            var inspection = inspections.First();

            return View(inspection);
        }

        // fetch the latest mileage report from the inspections table by vehicle registration
        public IActionResult MileageReport()
        {
            var mileageReportSQL = @"SELECT
                                        UPPER(vanInspection.vehicleRegistration) AS vehicleRegistration,
	                                    mileage,
	                                    checkDate,
	                                    vanInspection.depot
                                    FROM loadingApp.dbo.vanInspection
                                    INNER JOIN(SELECT vehicleRegistration, MAX(Id) AS Id FROM loadingApp.dbo.vanInspection GROUP BY vehicleRegistration) L ON L.Id = vanInspection.Id
                                    INNER JOIN loadingApp.dbo.vehicles ON vehicles.vanRegistration = vanInspection.vehicleRegistration
									WHERE vanInspection.vehicleRegistration NOT IN (
										SELECT
											vehicleRegistration
										FROM loadingApp.dbo.vehicleHistory
										WHERE actionReason = 6
									)
                                    ORDER BY 4,1";
            var mileageReport = SQLDataAccess.LoadData<MileageReportModel>(mileageReportSQL);
            return View(mileageReport);
        }

    }

    }
//where checkDate >= DATEADD(WW,-1, GETDATE())