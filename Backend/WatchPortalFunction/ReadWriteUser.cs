using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using WatchPortalFunction.Entities;
using WatchPortalFunction.Interfaces;

namespace WatchPortalFunction;

public static class ReadWriteUser
{
    //private static readonly Idb<ClientInfo> db;

    static List<ClientInfo> clientInfo = new List<ClientInfo> //temp data simulating DB
    {
        new ClientInfo("PID1234567890","1993-03-09","9303091324") 
        {Name = "Adam", Surname = "Jensen", 
        Address1 = "Zeleň 43/1",
        Address2 = "Prague - Překážka",
        Email = "Adam.Jensen@dex.cz",
        MobileNumber = "77422914",
        ProfileImage = @"https://rostupload.blob.core.windows.net/images/adam.jpg"}
    };

    internal class DbTools : Idb<ClientInfo> //nested class because I need data from clientInfo
    {
        public ClientInfo GetUser(string pid) //Return our data from "DB"
        {
            return clientInfo.Find(x => x.Pid == pid); 
        }

        public void UpdateUser(ClientInfo item) //get data from api and saving to our "DB"
        {
            var i = clientInfo.FindIndex(x => x.Pid == item.Pid);

            clientInfo[i].Address1 = item.Address1;
            clientInfo[i].Address2 = item.Address2;
            clientInfo[i].Email = item.Email;
            clientInfo[i].Name = item.Name;
            clientInfo[i].Surname = item.Surname;
            clientInfo[i].MobileNumber = item.MobileNumber;
            clientInfo[i].ProfileImage = item.ProfileImage;
            Save();
        }

        public void Save() 
        {
            Console.WriteLine("Imagine that we are saving our db context to DB using entity framework method dbContext.SaveChanges()... "); //imagination
        }

    }

    [FunctionName("ReadUserSettings")] //read user profile by PID number
    public static async Task<IActionResult> Read(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "readuser")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        string pid = req.Query["pid"];

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);
        pid = pid ?? data?.pid;

        string responseMessage = string.IsNullOrEmpty(pid)
            ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response.":"";
        return new OkObjectResult(new DbTools().GetUser(pid));
    }

    [FunctionName("WriteUserSettings")] //write into user profile 
    public static async Task<IActionResult> Write( 
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "writeuser")] HttpRequest req,
    ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var updateUser = new DbTools();
        var content = await new StreamReader(req.Body).ReadToEndAsync();//Most important thing for post method is getting request forom frontend! 
        //var jsonInfo = JsonConvert.DeserializeObject(content);
        ClientInfo jsonInfo = JsonConvert.DeserializeObject<ClientInfo>(content);
        updateUser.UpdateUser(jsonInfo);

        return new OkObjectResult(clientInfo);
    }
}
