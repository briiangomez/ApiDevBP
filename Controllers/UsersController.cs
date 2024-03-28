using ApiDevBP.Contracts;
using ApiDevBP.Entities;
using ApiDevBP.Models;
using ApiDevBP.Services;
using Microsoft.AspNetCore.Mvc;
using SQLite;
using System.Reflection;

namespace ApiDevBP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Registra un nuevo usuario en la BD
        /// </summary>
        /// <param name="user">Datos del usuario a insertar en la BD</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveUser(UserModel user)
        {
            if (user == null)
                return BadRequest();
            var response = await _userService.Insert(user);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Actualiza el registro del usuario en la BD
        /// </summary>
        /// <param name="userId">Id del Usuario a Modificar</param>
        /// <param name="user">Datos del usuario actualizados</param>
        /// <returns></returns>
        [HttpPut("UpdateUser/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId,[FromBody] UserModel user)
        {
            if (user == null)
                return BadRequest();
            var response = await _userService.Update(user, userId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);

        }

        /// <summary>
        /// Obtiene todos los usuarios de la BD
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response =  await _userService.GetAll();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }


        /// <summary>
        /// Elimina un usuario de la BD
        /// </summary>
        /// <param name="userId">Id del Usuario a Eliminar</param>
        /// <returns></returns>
        [HttpDelete("Delete/{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            var response = await _userService.Delete(userId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Obtiene un usuario de la base de Datos
        /// </summary>
        /// <param name="userId">Id del usuario a obtener la informacion</param>
        /// <returns></returns>
        [HttpGet("Get/{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var response = await _userService.Get(userId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

    }
}
