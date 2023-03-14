﻿using Microsoft.EntityFrameworkCore;
using BovineBoss_API.Models;
using BovineBoss_API.Services.Contrato;
using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.SqlServer.Server;
using System.Globalization;

namespace BovineBoss_API.Services.Implementacion
{
    public class PersonaService : IAdminService
    {


        private BovineBossContext dbContext;

        public PersonaService(BovineBossContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<AdminDto>> GetListAdmin()
        {
            try
            {
                List<Persona> listaPersona = new List<Persona>();
                listaPersona = dbContext.Personas.Where(p => p.TipoPersona == "A").ToList();
                List<AdminDto> listaAdminDto = listaPersona.Select(a => new AdminDto
                {
                    NombrePersona = a.NombrePersona,
                    ApellidoPersona = a.ApellidoPersona,
                    Cedula = a.Cedula,
                    Salario = a.Salario,
                    FechaContratacion = a.FechaContratacion,
                    Usuario = a.Usuario
                }).ToList();
           
                return listaAdminDto;
            }
            catch (Exception ex)
            {
                throw ex;

            }



        }

        public async Task<AdminDto> GetPersona(int idPersona)
        {
            try
            {
                Persona? persona = new Persona();
                persona = await dbContext.Personas.Where(e => e.IdPersona == idPersona).FirstOrDefaultAsync();
                AdminDto admin = new AdminDto
                {
                    NombrePersona = persona.NombrePersona,
                    Cedula = persona.Cedula,
                    ApellidoPersona = persona.ApellidoPersona,
                    Salario = persona.Salario,
                    FechaContratacion = persona.FechaContratacion,
                    Usuario = persona.Usuario
                };

                return admin;

            }
            catch (Exception ex)
            {
                throw ex;


            }
        }

        public async Task<CreateEmployeeDto> AddAdministrator(CreateEmployeeDto Admin)
        {
            try
            { 
                Persona persona = new Persona()
                {
                    NombrePersona = Admin.NombrePersona,
                    ApellidoPersona = Admin.ApellidoPersona,
                    Cedula = Admin.Cedula,
                    TipoPersona = "A",
                    Salario = Admin.Salario,
                    FechaContratacion = DateTime.ParseExact(DateTime.UtcNow.ToString("MM-dd-yyyy"), "MM-dd-yyyy", CultureInfo.InvariantCulture),
                    Usuario= Admin.Usuario,
                    Contrasenia = BCrypt.Net.BCrypt.HashPassword(Admin.Contrasenia),
                    TelefonoPersona = Admin.TelefonoPersona
                };

                dbContext.Personas.Add(persona);
                await dbContext.SaveChangesAsync();
                return Admin;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

    }

}