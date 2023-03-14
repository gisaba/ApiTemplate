using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


//RejSon
using StackExchange.Redis;
using NReJSON;
using Newtonsoft.Json;
using System.Dynamic;

namespace RockApi.Controllers
{
    public class Address
    {
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
    }
    public class UserInfo
    {
        public string userId { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public int rank { get; set; }
        public Address address { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        UserInfo _userInfo;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

       // private readonly IConnectionMultiplexer _redis;

        public ValuesController(IConnectionMultiplexer redis)
        {
            UserInfo userInfo = new UserInfo()
            {
                userId = "AC-67292",
                fullName = "Bob James",
                email = "bobjames@sample.com",
                rank = 15,
                address = new Address()
                {
                    street = "678 Winona Street",
                    city = "New Medford",
                    state = "Delaware",
                    zip = "12345"
                }
            };

            _userInfo = userInfo;

            //Replace with the IP of your redis server

            //string conn = "127.0.0.1:6379";

            _connectionMultiplexer = redis;

            //    ConnectionMultiplexer.Connect(conn);

           // _redis = redis;
        }

        [HttpGet("redisjsonsample/initial/person")]
        public IActionResult GetPersonData()
        {
            return Ok(_userInfo);
        }

        //********Save UserInfo to Redis**********
        [HttpGet("redisjsonsample/save/profile")] //save initial data
        public async Task<IActionResult> SaveUserProfile()
        {
            IDatabase db = _connectionMultiplexer.GetDatabase();
            string key = "userprofile:" + _userInfo.email.ToLower();
            string json = JsonConvert.SerializeObject(_userInfo);
            //use try/catch to catch error
            OperationResult result = await db.JsonSetAsync(key, json);

            if (result.IsSuccess) { return Ok("SaveUserProfile Succeeded"); }

            return BadRequest("SaveUserProfile  Failed");
        }

        //********Read UserInfo from Redis**********
        [HttpGet("redisjsonsample/get/profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            IDatabase db = _connectionMultiplexer.GetDatabase();
            string key = "userprofile:" + _userInfo.email.ToLower();
            string[] parms = { "." };
            RedisResult result = await db.JsonGetAsync(key, parms);
            if (result.IsNull) { return BadRequest("GetUserProfile Failed"); }
            string profile = (string)result;
            return Ok(profile);
        }

        [HttpGet("redisjsonsample/change/state_plus_fullname")]
        public async Task<IActionResult> ChangeSomeProps()
        {
            IDatabase db = _connectionMultiplexer.GetDatabase();
            string key = "userprofile:" + _userInfo.email.ToLower();

            //change state -- note: full path is needed .address.state 
            //use try/catch to catch error
            OperationResult result = await db.JsonSetAsync(key, JsonConvert.SerializeObject("New York"), ".address.state");
            string resultValue = result.RawResult.ToString();  //- - > OK

            //change fullName
            //use try/catch to catch error
            OperationResult res = await db.JsonSetAsync(key, JsonConvert.SerializeObject("Bob T Jones"), ".fullName");

            string fullNameResult = res.RawResult.ToString(); //- - > OK  

            return Ok(resultValue + "**" + fullNameResult);

        }

        /*
        [HttpGet]
        [Route("/api/redisjsonsample/incr/rank")]
        public async Task<IActionResult> IncrementRank()
        {
            IDatabase db = _connectionMultiplexer.GetDatabase();
            string key = "userprofile:" + _userInfo.email.ToLower();

            //note the value is being incremented by 3 -- hardcoded
            //In real life you'll pass it in as a parameter
            //use try/catch to catch error
            RedisResult res = await db.JsonIncrementNumberAsync(key, ".rank", 3);
            //returns 15 + 3   --> 18  (the first time it's used)
            return Ok((string)res);
        }
        */

        [HttpGet]
        [Route("/api/redisjsonsample/mprops")]
        public async Task<IActionResult> GetMultipleProps()
        {
            IDatabase db = _connectionMultiplexer.GetDatabase();
            string key = "userprofile:" + _userInfo.email.ToLower();
            string[] multipleKeys = { ".fullName", ".address.state", ".address.zip" };
            RedisResult result = await db.JsonGetAsync(key, multipleKeys);

            if (result.IsNull)
            {
                return BadRequest("bad request");
            }
            return Ok((string)result);
            //NOTE: returns {".fullName":"Bob T Jones",".address.state":"New York",".address.zip":"12345"}
        }


        [HttpGet]
        [Route("/api/redisjsonsample/add/addziplusfour")]
        public async Task<IActionResult> AddNewProperty()
        {
            IDatabase db = _connectionMultiplexer.GetDatabase();
            string key = "userprofile:" + _userInfo.email.ToLower();

            string zipPlusFour = "12345-6789";
            string jsonZipPlusFour = JsonConvert.SerializeObject(zipPlusFour);
            OperationResult opResult = await db.JsonSetAsync(key, jsonZipPlusFour, ".address.zipPlusFour");

            if (opResult.IsSuccess)
            {
                string result = opResult.RawResult.ToString();
                return Ok(result); // --> OK
            }
            return BadRequest("Error - cannot create new property");
        }


        [HttpGet]
        [Route("/api/redisjsonsample/add/secondaddress")]
        public async Task<IActionResult> AddJsonObject()
        {
            Address shipTo = new Address()
            {
                street = "4592 Vacation Drive",
                city = "Vacation City",
                state = "New Hampshire",
                zip = "36448"
            };
            IDatabase db = _connectionMultiplexer.GetDatabase();
            string key = "userprofile:" + _userInfo.email.ToLower();

            string jsonshipTo = JsonConvert.SerializeObject(shipTo);
            //Note the new key
            OperationResult opResult = await db.JsonSetAsync(key, jsonshipTo, ".address.shipTo");

            if (opResult.IsSuccess)
            {
                string result = opResult.RawResult.ToString();
                return Ok(result); // --> OK
            }
            return BadRequest("Error - cannot add object");
        }

        [Route("/api/redisjsonsample/strappend/tostreet")]
        public async Task<IActionResult> AppendString()
        {
            IDatabase db = _connectionMultiplexer.GetDatabase();
            string key = "userprofile:" + _userInfo.email.ToLower();
            string jsonStrappend = JsonConvert.SerializeObject(" Boulevard");
            int result = (int)await db.ExecuteAsync("JSON.STRAPPEND", new object[] { key, ".address.shipTo.street", jsonStrappend });
            return Ok(result);
        }
        /****************************************************************************/

        // GET api/values/emp1
        [HttpGet("redis/{key}")] //emp1,emp2,emp3
        public async Task<IActionResult> Foo(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var foo = await db.StringGetAsync(key); 
           
            return Ok(foo.ToString());
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
