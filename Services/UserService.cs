using ApiDevBP.Context;
using ApiDevBP.Contracts;
using ApiDevBP.Controllers;
using ApiDevBP.Entities;
using ApiDevBP.Models;
using ApiDevBP.Response;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ApiDevBP.Services
{
    public class UserService : IUserService
    {
        private readonly ApiDBContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        public UserService(ILogger<UserService> logger, ApiDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<bool>> Delete(int userId)
        {
            var response = new Response<bool>();

            try
            {
                var entity = await _context.Set<UserEntity>()
                        .AsNoTracking()
                        .SingleOrDefaultAsync(x => x.Id.Equals(userId));

                if (entity == null)
                {
                    throw new Exception("No se encontro al usuario en BD");
                }

                _context.Remove(entity);
                _context.SaveChanges();
                response.Data = true;
                response.IsSuccess = true;
                response.Message = "Eliminación Exitosa!!!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar usuario");
                response.Data = false;
                response.Message = "Error al eliminar usuario: " + ex.Message;
            }

            return response;
            
        }

        public async Task<Response<UserModel>> Get(int userId)
        {
            var response = new Response<UserModel>();

            try
            {
                var result = await _context.
                        Set<UserEntity>().AsNoTracking().
                        SingleOrDefaultAsync(x => x.Id.Equals(userId));

                if (result == null)
                {
                    throw new Exception("No se encontro al usuario en BD");
                }
                response.Data = _mapper.Map<UserModel>(result);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuario");
                response.Message = "Error al obtener usuario: " + ex.Message;
            }

            return response;
        }

        public async Task<Response<IEnumerable<UserModel>>> GetAll()
        {
            var response = new Response<IEnumerable<UserModel>>();
            try
            {
                var result = await _context.Set<UserEntity>().AsNoTracking().ToListAsync();
                response.Data = _mapper.Map<IEnumerable<UserModel>>(result);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }
            }
            catch (Exception ex)
            {
                response.Message = "Error al obtener usuarios: " + ex.Message;
            }
            return response;
        }

        public async Task<Response<bool>> Insert(UserModel userModel)
        {
            var response = new Response<bool>();
            try
            {
                var entity = await _context.Set<UserEntity>().AsNoTracking().SingleOrDefaultAsync(x => x.Name.Equals(userModel.Name) && x.Lastname.Equals(userModel.Lastname));
                if (entity != null)
                {
                    throw new Exception("Usuario existente en BD");
                }
                var user = _mapper.Map<UserEntity>(userModel);
                _context.Set<UserEntity>().Add(user);
                _context.SaveChanges();
                response.Data = true;
                response.IsSuccess = true;
                response.Message = "Registro Exitoso!!!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar usuario");
                response.Data = false;
                response.Message = "Error al generar usuario: " + ex.Message;
            }

            return response;
        }

        public async Task<Response<bool>> Update(UserModel userModel, int id)
        {
            var response = new Response<bool>();
            try
            {
                var entity = await _context.Set<UserEntity>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(id));
                if (entity == null)
                {
                    throw new Exception("No se encontro al usuario en BD");
                }
                entity.Name = userModel.Name;
                entity.Lastname = userModel.Lastname;

                _context.Set<UserEntity>().Update(entity);
                _context.SaveChanges();
                response.Data = true;
                response.IsSuccess = true;
                response.Message = "Registro Exitoso!!!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar usuario");
                response.Data = false;
                response.Message = "Error al actualizar usuario: " + ex.Message;
            }

            return response;
        }
    }
}
