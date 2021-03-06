﻿using MazeWebServer.Entitys;
using MazeWebServer.Models;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;
using System.Web.Http;

namespace MazeWebServer.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class SolveController : ApiController
    {
        /// <summary>
        /// The sm model
        /// </summary>
        private SolveMazeModel smModel;
        /// <summary>
        /// Initializes a new instance of the <see cref="SolveController"/> class.
        /// </summary>
        public SolveController()
        {
            this.smModel = new SolveMazeModel();
        }
        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Solve/Get")]
        public IHttpActionResult Get(SolveParams value)
        {
            Algorithm algo = Algorithm.BFS;
            if (value.AlgoSelector == 1)
            {
                algo = Algorithm.BFS;
            }
            else
            {
                algo = Algorithm.DFS;
            }
            Solution<MazeLib.Position> sol = this.smModel.Solve(value.Name, algo);
            string directions = MazeAdapter.AdaptSolution.ToDirection(sol);
            JObject jobj = new JObject();
            jobj["Name"] = value.Name;
            jobj["Solution"] = directions.ToString();
            jobj["NodesEvaluated"] = sol.EvaluatedNodes;
            return Ok(jobj);
        }
    }
}
